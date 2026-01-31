using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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
        private string currentFolder = string.Empty;
        private CancellationTokenSource cts;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbobx_codec.SelectedIndex = 1;
            cmbobx_encoding_speed.SelectedIndex = 0;

            grpbx_rename_files.Visible = false;

            lbl_quality_value.Text = trkbr_Quality.Value.ToString();
        }


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
                else
                {
                    //We have an image so suggest a CRF value and file size
                    SuggestCRF();
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


        private void UpdateProgress(int progress, ProgressBar myProgressbar, RichTextBox myRichTextBox)
        {
            if (myProgressbar.InvokeRequired)
            {
                myProgressbar.Invoke(new Action(() => UpdateProgress(progress, myProgressbar, myRichTextBox)));
                return;
            }

            myProgressbar.Value = Math.Max(0, Math.Min(100, progress));

            Invoke((MethodInvoker)delegate
           {
               myRichTextBox.AppendText($"Progress: {progress}%\r\n");
               myRichTextBox.ScrollToCaret();
           });
        }

        private void btn_ChooseFolder_Click(object sender, EventArgs e)
        {
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
            //string outputPath = @"F:\snapshots\ffmpeg_timelapse.mp4";
            int fps = (int)numUpDn_Fps.Value;
            string Codec = cmbobx_codec.Text;
            string EncodingSpeed = cmbobx_encoding_speed.Text;
            int Quality = trkbr_Quality.Value;
            bool Downscale = chkbx_downscale.Checked;

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
                    progressBar, Codec, EncodingSpeed, Quality, Downscale));
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
            Reset();
        }

        private void Reset()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
               {
                   lst_Images.Items.Clear();
                   lbl_Folder.Text = "";
                   lbl_Status.Text = "";
                   lbl_movie_time.Text = "";
                   picbx_Preview.Image = null;
                   trkbr_Quality.Value = 0;
                   lbl_quality_value.Text = "0";
                   progressBar.Value = 0;
                   rchtxbx_output.Clear();
                   MsgBox.Show("Operation aborted", "Aborted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               });
            }
            else
            {
                lst_Images.Items.Clear();
                lbl_Folder.Text = "";
                lbl_Status.Text = "";
                lbl_movie_time.Text = "";
                picbx_Preview.Image = null;
                trkbr_Quality.Value = 24;
                lbl_quality_value.Text = "24";
                progressBar.Value = 0;
                rchtxbx_output.Clear();
            }
        }

        private void btn_rename_files_Click(object sender, EventArgs e)
        {
            if (!RenameFactory(lbl_Folder.Text, txtWildcard.Text, txtNewNameFormat.Text, true))
                MsgBox.Show("Unable to rename files", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void RunFFmpeg(string[] images, int myFPS, string ffmpegPath, string outputPath,
            CancellationToken token, RichTextBox myRichTextBox, ProgressBar myProgressBar, string myCodec,
            string myEncodingSpeed, int myQuality, bool myDownscaleTo1080p)
        {
            double totalSeconds = images.Length / (double)myFPS;
            TimeSpan totalDuration = TimeSpan.FromSeconds(totalSeconds);


            // Create txt file list of files to add to movie
            // string listFile = Path.Combine(Path.GetTempPath(), "ffmpeg_list.txt");

            string listFile = Path.Combine(Application.StartupPath, "ffmpeg_list.txt");

            using (var sw = new StreamWriter(listFile))
            {
                sw.WriteLine("ffconcat version 1.0");

                double frameDuration = 1.0 / myFPS;

                for (int i = 0; i < images.Length; i++)
                {
                    string img = images[i].Replace("\\", "/");
                    sw.WriteLine($"file '{img}'");
                    sw.WriteLine($"duration {frameDuration}");
                }

                // FFmpeg requires repeating the last frame
                string last = images[images.Length - 1].Replace("\\", "/");
                sw.WriteLine($"file '{last}'");
                sw.WriteLine($"duration {frameDuration}");
            }


            // Optional downscale filter
            string scaleFilter = myDownscaleTo1080p ? "-vf scale=1920:1080" : "";

            // FFmpeg command
            string args =
                $"-y -f concat -safe 0 -i \"{listFile}\" " +
                $"{scaleFilter} -r {myFPS} " +
                $"-c:v {myCodec} -preset {myEncodingSpeed} -crf {myQuality} -pix_fmt yuv420p " +
                $"\"{outputPath}\"";

            var process = new Process
            {
                StartInfo =
                {
                    FileName = ffmpegPath,
                    Arguments = args,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();

            // Read stderr asynchronously for progress
            Task stderrTask = Task.Run(() =>
            {
                string line;
                while ((line = process.StandardError.ReadLine()) != null)
                {
                    if (token.IsCancellationRequested)
                        return;

                    // Log FFmpeg output
                    //myRichTextBox.Invoke(new Action(() =>
                    //{
                    //    myRichTextBox.AppendText(line + "\r\n");
                    //    myRichTextBox.ScrollToCaret();
                    //}));

                    if (line.Contains("time="))
                    {
                        string timeString = ExtractTime(line);

                        if (TimeSpan.TryParse(timeString, out TimeSpan current))
                        {
                            int progress = (int)(current.TotalSeconds / totalDuration.TotalSeconds * 100);
                            UpdateProgress(progress, myProgressBar, myRichTextBox);
                        }
                    }
                }
            });

            // Wait for FFmpeg to finish
            process.WaitForExit();
            stderrTask.Wait();

            if (process.ExitCode == 0)
            {
                Invoke((MethodInvoker)delegate
                {
                    progressBar.Value = 100;
                    rchtxbx_output.AppendText($"Progress: 100%\r\n");
                    rchtxbx_output.ScrollToCaret();
                    MsgBox.Show($"Video created successfully:\n{outputPath}",
                        "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                });
            }
            else
            {
                Invoke((MethodInvoker)delegate
                {
                    progressBar.Value = 0;
                    MsgBox.Show("FFmpeg failed. Check log for details.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                });
            }
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

        private void trkbr_Quality_Scroll(object sender, EventArgs e)
        {
            lbl_quality_value.Text = trkbr_Quality.Value.ToString();
        }

        private void chkbx_rename_files_CheckedChanged(object sender, EventArgs e)
        {
            grpbx_rename_files.Visible = chkbx_rename_files.Checked ? true : false;
        }


        public void SuggestCRF()
        {
            //Suggest best quality setting. User can change if they want
            var images = lst_Images.Items.Cast<string>().ToArray();
            var bitmap = new Bitmap(images[0]);

            int crfValue = 0;
            int width = bitmap.Width;
            int height = bitmap.Height;
            string codec = cmbobx_codec.Text;
            bool downscale = chkbx_downscale.Checked;


            // If downscaling to 1080p, we can push CRF slightly higher
            bool is4K = width >= 3000;
            bool is1080p = width >= 1800 && width < 3000;

            if (codec.Contains("265"))
            {
                crfValue = 24;                          // fallback
                if (is4K && !downscale) crfValue = 20;  // 4K HEVC
                if (is4K && downscale) crfValue = 22;   // 4K → 1080p
                if (is1080p) crfValue = 22;             // native 1080p

            }
            else if (codec.Contains("264"))
            {
                crfValue = 22;
                if (is4K && !downscale) crfValue = 18;
                if (is4K && downscale) crfValue = 20;
                if (is1080p) crfValue = 20;

            }

            trkbr_Quality.Value = crfValue;
            lbl_quality_value.Text = crfValue.ToString();

            GC.Collect();
        }
        
       
        private void cmbobx_codec_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst_Images.Items.Count != 0)
            {
                SuggestCRF();
            }
        }

        private void chkbx_downscale_CheckedChanged(object sender, EventArgs e)
        {
            if (lst_Images.Items.Count != 0)
            {
                SuggestCRF();
            }
        }
    }
}
