using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accord.Video.FFMPEG;

namespace myTimelapseMovieMaker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            //Text = "Timelapse Movie Maker";
            //Width = 1100;
            //Height = 650;
            //StartPosition = FormStartPosition.CenterScreen;

           // InitializeControls();
            InitializeComponent();
        }
        // UI controls
       // private Button btnChooseFolder;
        //private Button btn_Start;
        //private Button btn_Abort;
        //private Button btn_MoveUp;
        //private Button btn_MoveDown;
        //private Button btn_Remove;
        //private Label lbl_Folder;
        //private Label lbl_Fps;
        //private NumericUpDown numUpDn_Fps;
       // private ListBox lst_Images;
       // private PictureBox picbx_Preview;
       // private ProgressBar progressBar;
       // private Label lbl_Status;
       // private SaveFileDialog saveFileDialog;

        // State
        private string currentFolder = string.Empty;
        private CancellationTokenSource cts;
        private bool isRendering = false;

        
        //private void InitializeControls()
        //{
            //// Folder label
            //lbl_Folder = new Label
            //{
            //    Text = "Folder: (none selected)",
            //    AutoSize = true,
            //    Location = new Point(10, 10)
            //};

            //// Choose folder button
            //btnChooseFolder = new Button
            //{
            //    Text = "Choose Folder...",
            //    Location = new Point(10, 35),
            //    Width = 130
            //};
            //btnChooseFolder.Click += BtnChooseFolder_Click;

            // FPS label
            //lbl_Fps = new Label
            //{
            //    Text = "Frame rate (fps):",
            //    AutoSize = true,
            //    Location = new Point(160, 40)
            //};

            // FPS numeric
            //numUpDn_Fps = new NumericUpDown
            //{
            //    Location = new Point(270, 35),
            //    Minimum = 1,
            //    Maximum = 120,
            //    Value = 25,
            //    Width = 60
            //};

            // Start button
            //btn_Start = new Button
            //{
            //    Text = "Start",
            //    Location = new Point(350, 35),
            //    Width = 80
            //};
            //btn_Start.Click += BtnStart_Click;

            // Abort button
            //btn_Abort = new Button
            //{
            //    Text = "Abort",
            //    Location = new Point(440, 35),
            //    Width = 80,
            //    Enabled = false
            //};
            //btn_Abort.Click += BtnAbort_Click;

            // List of images
            //lst_Images = new ListBox
            //{
            //    Location = new Point(10, 80),
            //    Width = 500,
            //    Height = 400
            //};
            //lst_Images.SelectedIndexChanged += LstImages_SelectedIndexChanged;

            // Move up button
            //btn_MoveUp = new Button
            //{
            //    Text = "Move Up",
            //    Location = new Point(520, 80),
            //    Width = 100
            //};
            //btn_MoveUp.Click += btn_MoveUp_Click;

            // Move down button
            //btn_MoveDown = new Button
            //{
            //    Text = "Move Down",
            //    Location = new Point(520, 115),
            //    Width = 100
            //};
            //btn_MoveDown.Click += btn_MoveDown_Click;

            // Remove button
            //btn_Remove = new Button
            //{
            //    Text = "Remove",
            //    Location = new Point(520, 150),
            //    Width = 100
            //};
            //btn_Remove.Click += btn_Remove_Click;

            // Picture preview
            //picbx_Preview = new PictureBox
            //{
            //    Location = new Point(640, 80),
            //    Width = 420,
            //    Height = 400,
            //    BorderStyle = BorderStyle.FixedSingle,
            //    SizeMode = PictureBoxSizeMode.Zoom
            //};

            // Progress bar
            //progressBar = new ProgressBar
            //{
            //    Location = new Point(10, 500),
            //    Width = 1050,
            //    Height = 25,
            //    Minimum = 0,
            //    Maximum = 100
            //};

            // Status label
            //lbl_Status = new Label
            //{
            //    Text = "Status: Idle",
            //    AutoSize = true,
            //    Location = new Point(10, 535)
            //};

            // Save file dialog
            //saveFileDialog = new SaveFileDialog
            //{
            //    Filter = "MP4 Video|*.mp4",
            //    Title = "Save Timelapse Video"
            //};

            //Controls.Add(lbl_Folder);
            //Controls.Add(btnChooseFolder);
            //Controls.Add(lbl_Fps);
            //Controls.Add(numUpDn_Fps);
            //Controls.Add(btn_Start);
            //Controls.Add(btn_Abort);
            //Controls.Add(lst_Images);
            //Controls.Add(btn_MoveUp);
            //Controls.Add(btn_MoveDown);
            //Controls.Add(btn_Remove);
            //Controls.Add(picbx_Preview);
           // Controls.Add(progressBar);
           // Controls.Add(lbl_Status);
       // }

        //private void BtnChooseFolder_Click(object sender, EventArgs e)
        //{
           
        //}

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

        //private void LstImages_SelectedIndexChanged(object sender, EventArgs e)
        //{
           
        //}

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
                    lbl_Folder.Text = "Folder: " + currentFolder;
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
    }

}
