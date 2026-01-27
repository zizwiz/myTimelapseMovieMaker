using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        //private Process ffmpegProcess;

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


        private void UpdateProgress(int value, string statusText)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int, string>(UpdateProgress), value, statusText);
                return;
            }

            progressBar.Value = Math.Min(100, Math.Max(0, value));
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
            if (cts != null)
            {
                cts.Cancel();
            }

            lbl_Status.Text = "Aborting...";
            btn_Abort.Enabled = false;
        }

        private async void btn_Start_Click(object sender, EventArgs e)
        {
            var images = lst_Images.Items.Cast<string>().ToArray();
            string ffmpegPath = Path.Combine(Application.StartupPath, "ffmpeg", "ffmpeg.exe");
            string outputPath = @"F:\snapshots\ffmpeg_timelapse.mp4";
            int fps = (int)numUpDn_Fps.Value;
            string Codec = cmbobx_codec.Text;
            string EncodingSpeed = cmbobx_encoding_speed.Text;
            int Quality = trkbr_Quality.Value;

            btn_Start.Enabled = false;
            btn_Abort.Enabled = true;
            btn_ChooseFolder.Enabled = false;
            btn_MoveUp.Enabled = false;
            btn_MoveDown.Enabled = false;
            btn_Remove.Enabled = false;
            numUpDn_Fps.Enabled = false;
            btn_reset.Enabled = false;
            progressBar.Value = 0;
            lbl_Status.Text = "Starting...";

            cts = new CancellationTokenSource();

            try
            {
                TimeSpan totalDuration = TimeSpan.FromSeconds(images.Length / (double)fps);

                lbl_movie_time.Text = "Movie duration = " + totalDuration.Hours + "h " + totalDuration.Minutes + "m " +
                                      totalDuration.Seconds + "s";

                await Task.Run(() => RunFFmpeg(images, fps, ffmpegPath, outputPath, cts.Token, rchtxbx_output, 
                    progressBar, Codec, EncodingSpeed, Quality));
                lbl_Status.Text = "Completed";
            }
            catch (OperationCanceledException)
            {
                lbl_Status.Text = "Aborted";
            }
            catch (Exception ex)
            {
                lbl_Status.Text = "Error";
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btn_Start.Enabled = true;
                btn_Abort.Enabled = false;
                btn_ChooseFolder.Enabled = true;
                btn_MoveUp.Enabled = true;
                btn_MoveDown.Enabled = true;
                btn_Remove.Enabled = true;
                numUpDn_Fps.Enabled = true;
                btn_reset.Enabled = true;
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

        private void RunFFmpeg(
            string[] images,
            int myFPS,
            string ffmpegPath,
            string outputPath,
            CancellationToken token,
            RichTextBox myRichTextBox,
            ProgressBar myProgressBar, 
            string myCodec,
            string myEncodingSpeed,
            int myQuality
        )
        {
            token.ThrowIfCancellationRequested();

            
            int fileCount = images.Length + 1;
            int counter = 1;

            myProgressBar.Invoke(new Action(() =>
            {
                myProgressBar.Value = 0;
                myProgressBar.Maximum = fileCount;
            }));

           
            string args = "-y -f image2pipe -r " + myFPS +
                          " -i pipe:0 -c:v " + myCodec + " -preset " + myEncodingSpeed + " -crf " + myQuality +" -pix_fmt yuv420p " + outputPath;

            var process = new Process
            {
                StartInfo =
                {
                    FileName = ffmpegPath,
                    Arguments = args,
                    RedirectStandardInput = true,  // Redirect standard input
                    RedirectStandardOutput = true, // Redirect standard output
                    RedirectStandardError = true,  // Redirect standard error
                    UseShellExecute = false,       // Required for redirection
                    CreateNoWindow = true          // Optional: Run without creating a window
                }
            };

            process.Start();

            Stream ffmpegInput = process.StandardInput.BaseStream;

            // Write images to FFmpeg's standard input
            // using (var ffmpegInput = process.StandardInput.BaseStream)

            using (ffmpegInput)
            {
                foreach (string imageFile in images)
                {
                    while (!token.IsCancellationRequested) // do unless aborted
                    {
                        counter++;

                        using (var bitmap = new Bitmap(imageFile))
                        {
                            bitmap.Save(ffmpegInput, ImageFormat.Jpeg);
                        }

                        //invoke to prevent cross threading
                        myRichTextBox.Invoke(new Action(() =>
                        {
                            myRichTextBox.AppendText("Adding:" + imageFile + "\r");
                            myRichTextBox.ScrollToCaret();
                        }));

                        myProgressBar.Invoke(new Action(() => { myProgressBar.Value = counter; }));
                    }
                }
            }

            //Close the process
            process.Close();

          //  token.ThrowIfCancellationRequested();
        }

        private string ExtractTime(string line)
        {
            int idx = line.IndexOf("time=");
            if (idx < 0) return "00:00:00";

            string part = line.Substring(idx + 5);
            int space = part.IndexOf(' ');
            if (space > 0)
                part = part.Substring(0, space);

            return part.Trim();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbobx_codec.SelectedIndex = 1;
            cmbobx_encoding_speed.SelectedIndex = 2;
        }
    }

}
