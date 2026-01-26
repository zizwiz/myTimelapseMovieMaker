using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accord.Video.FFMPEG;
using CenteredMessagebox;

namespace myTimelapseMovieMaker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
           InitializeComponent();
        }
       
        // State
        private string currentFolder = string.Empty;
        private CancellationTokenSource cts;
        private bool isRendering = false;

        private void LoadImagesFromFolder(string folder)
        {
            lst_Images.Items.Clear();
            picbx_Preview.Image = null;

            if (string.IsNullOrWhiteSpace(folder) || !Directory.Exists(folder))
            {
                MessageBox.Show("Folder does not exist.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Get JPG files
                var files = Directory.GetFiles(folder, "*.jpg", SearchOption.TopDirectoryOnly);

                // Sort by numeric suffix x in DDMMYYYY_HHMMSS_x.jpg
                // If parsing fails, fall back to normal alphabetical order.
                var sorted = files
                    .Select(path => new
                    {
                        Path = path,
                        Name = Path.GetFileName(path),
                        Index = ParseNumericSuffix(Path.GetFileNameWithoutExtension(path))
                    })
                    .OrderBy(x => x.Index)
                    .ThenBy(x => x.Name)
                    .ToList();

                foreach (var f in sorted)
                {
                    lst_Images.Items.Add(f.Path);
                }

                if (lst_Images.Items.Count == 0)
                {
                    MessageBox.Show("No .jpg files found in the selected folder.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading images: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int ParseNumericSuffix(string fileNameWithoutExtension)
        {
            // Expected pattern: DDMMYYYY_HHMMSS_x
            // We try to get the part after the last underscore and parse as int.
            try
            {
                var parts = fileNameWithoutExtension.Split('_');
                if (parts.Length == 0)
                    return int.MaxValue;

                var last = parts[parts.Length - 1];
                if (int.TryParse(last, out int value))
                    return value;

                return int.MaxValue;
            }
            catch
            {
                return int.MaxValue;
            }
        }

        private void btn_MoveUp_Click(object sender, EventArgs e)
        {
            MoveSelectedItem(-1);
        }

        private void btn_MoveDown_Click(object sender, EventArgs e)
        {
            MoveSelectedItem(1);
        }

        private void MoveSelectedItem(int direction)
        {
            if (lst_Images.SelectedItem == null)
                return;

            int index = lst_Images.SelectedIndex;
            int newIndex = index + direction;

            if (newIndex < 0 || newIndex >= lst_Images.Items.Count)
                return;

            var item = lst_Images.Items[index];
            lst_Images.Items.RemoveAt(index);
            lst_Images.Items.Insert(newIndex, item);
            lst_Images.SelectedIndex = newIndex;
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (lst_Images.SelectedItem == null)
                return;

            int index = lst_Images.SelectedIndex;
            lst_Images.Items.RemoveAt(index);

            if (lst_Images.Items.Count > 0)
            {
                lst_Images.SelectedIndex = Math.Min(index, lst_Images.Items.Count - 1);
            }
            else
            {
                picbx_Preview.Image = null;
            }
        }

       private void RenderVideo(List<string> imagePaths, string outputPath, int fps, CancellationToken token)
        {
            if (imagePaths == null || imagePaths.Count == 0)
                throw new InvalidOperationException("No images to render.");

            // Determine video size from first image
            int width, height;
            using (var firstImg = Image.FromFile(imagePaths[0]))
            {
                width = firstImg.Width;
                height = firstImg.Height;
            }

            // Create writer
            using (var writer = new VideoFileWriter())
            {
                // Choose a codec and bitrate.
                // MPEG4 is widely supported; bitrate controls size/quality.
                // You can tweak bitrate for smaller files.
                int bitrate = 4000000; // ~4 Mbps, adjust as needed

                

                writer.Open(
                    outputPath,
                    width,
                    height,
                    fps,
                    VideoCodec.MPEG4,
                    bitrate);

                if (!writer.IsOpen)
                    throw new Exception("Failed to open video writer.");

                int total = imagePaths.Count;
                for (int i = 0; i < total; i++)
                {
                    token.ThrowIfCancellationRequested();

                    string path = imagePaths[i];
                    if (!File.Exists(path))
                        continue; // Skip missing files

                    try
                    {
                        using (var img = (Bitmap)Image.FromFile(path))
                        {
                            // If image size differs, resize to match video size
                            Bitmap frame;
                            if (img.Width != width || img.Height != height)
                            {
                                frame = new Bitmap(width, height);
                                using (var g = Graphics.FromImage(frame))
                                {
                                    g.DrawImage(img, 0, 0, width, height);
                                }
                            }
                            else
                            {
                                frame = new Bitmap(img);
                            }

                            writer.WriteVideoFrame(frame);
                            frame.Dispose();
                        }
                    }
                    catch
                    {
                        // Skip problematic image but continue
                    }

                    // Update progress on UI thread
                    int progress = (int)((i + 1) * 100.0 / total);
                    UpdateProgress(progress, $"Status: Rendering... ({i + 1}/{total})");
                }

                writer.Close();
            }
        }

        private void UpdateProgress(int value, string statusText)
        {
            if (InvokeRequired)
            {
                try
                {
                    Invoke(new Action<int, string>(UpdateProgress), value, statusText);
                }
                catch
                {
                    // Form might be closing; ignore
                }
                return;
            }

            if (value < progressBar.Minimum) value = progressBar.Minimum;
            if (value > progressBar.Maximum) value = progressBar.Maximum;

            progressBar.Value = value;
            lbl_Status.Text = statusText;
        }

        private void btn_ChooseFolder_Click(object sender, EventArgs e)
        {
            if (isRendering)
            {
                MessageBox.Show("Cannot change folder while rendering.", "Busy",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Select folder containing timelapse images";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    currentFolder = fbd.SelectedPath;
                    lbl_Folder.Text = currentFolder;
                    LoadImagesFromFolder(currentFolder);
                }
            }
        }

        private void btn_Abort_Click(object sender, EventArgs e)
        {
            if (!isRendering || cts == null)
                return;

            // Signal cancellation
            cts.Cancel();
            lbl_Status.Text = "Status: Aborting...";
            btn_Abort.Enabled = false;
        }

        private async void btn_Start_Click(object sender, EventArgs e)
        {
            if (isRendering)
            {
                MessageBox.Show("Rendering is already in progress.", "Busy",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (lst_Images.Items.Count == 0)
            {
                MessageBox.Show("No images to process.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (numUpDn_Fps.Value <= 0)
            {
                MessageBox.Show("Frame rate must be greater than zero.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Ask where to save the video
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            string outputPath = saveFileDialog.FileName;
            if (string.IsNullOrWhiteSpace(outputPath))
                return;

            // Confirm overwrite
            if (File.Exists(outputPath))
            {
                var result = MessageBox.Show("File already exists. Overwrite?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result != DialogResult.Yes)
                    return;
            }

            // Prepare for rendering
            isRendering = true;
            btn_Start.Enabled = false;
            btn_Abort.Enabled = true;
            btn_ChooseFolder.Enabled = false;
            btn_MoveUp.Enabled = false;
            btn_MoveDown.Enabled = false;
            btn_Remove.Enabled = false;
            numUpDn_Fps.Enabled = false;
            progressBar.Value = 0;
            lbl_Status.Text = "Status: Preparing...";

            cts = new CancellationTokenSource();
            var token = cts.Token;

            // Copy list of images to avoid UI thread issues
            var imagePaths = lst_Images.Items.Cast<string>().ToList();
            int fps = (int)numUpDn_Fps.Value;




            try
            {
                await Task.Run(() => RenderVideo(imagePaths, outputPath, fps, token), token);

                if (!token.IsCancellationRequested)
                {
                    lbl_Status.Text = "Status: Completed successfully.";
                    MessageBox.Show("Video created successfully.", "Done",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    lbl_Status.Text = "Status: Aborted.";
                    if (File.Exists(outputPath))
                    {
                        // Optionally delete incomplete file
                        try { File.Delete(outputPath); } catch { /* ignore */ }
                    }
                }
            }
           
            
            
            catch (OperationCanceledException)
            {
                lbl_Status.Text = "Status: Aborted.";
                if (File.Exists(outputPath))
                {
                    try { File.Delete(outputPath); } catch { /* ignore */ }
                }
            }
            catch (Exception ex)
            {
                lbl_Status.Text = "Status: Error.";
                MessageBox.Show("Error while creating video: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (File.Exists(outputPath))
                {
                    try { File.Delete(outputPath); } catch { /* ignore */ }
                }
            }
            finally
            {
                isRendering = false;
                btn_Start.Enabled = true;
                btn_Abort.Enabled = false;
                btn_ChooseFolder.Enabled = true;
                btn_MoveUp.Enabled = true;
                btn_MoveDown.Enabled = true;
                btn_Remove.Enabled = true;
                numUpDn_Fps.Enabled = true;
                cts.Dispose();
                cts = null;
            }
        }

        private void lst_Images_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Show thumbnail of selected image
            if (lst_Images.SelectedItem == null)
            {
                picbx_Preview.Image = null;
                return;
            }

            var path = lst_Images.SelectedItem.ToString();
            if (!File.Exists(path))
            {
                picbx_Preview.Image = null;
                return;
            }

            try
            {
                // Dispose previous image to avoid locking files
                if (picbx_Preview.Image != null)
                {
                    var old = picbx_Preview.Image;
                    picbx_Preview.Image = null;
                    old.Dispose();
                }

                // Load a copy of the image to avoid file locks
                using (var temp = Image.FromFile(path))
                {
                    picbx_Preview.Image = new Bitmap(temp);
                }
            }
            catch
            {
                // If preview fails, just clear it
                picbx_Preview.Image = null;
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            lst_Images.Items.Clear();
            lbl_Folder.Text = "";
            lbl_Status.Text = "";
            picbx_Preview.Image = null;
        }

        private void btn_rename_files_Click(object sender, EventArgs e)
        {
            if (!RenameFactory(lbl_Folder.Text, txtWildcard.Text, txtNewNameFormat.Text, true))
                MsgBox.Show("Unable to rename files", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }

}
