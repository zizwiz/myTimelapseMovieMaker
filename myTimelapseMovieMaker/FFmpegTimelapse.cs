using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CenteredMessagebox;


namespace myTimelapseMovieMaker
{
    public class FFmpegTimelapse
    {
        public static void CreateTimelapse(
            string ffmpegPath,
            string myOutputVideo,
            string[] imagePaths,
            int myFrameRate,
            RichTextBox myRichTextBox,
            ListBox myListBox)
        {
            string[] myImagesArray = myListBox.Items.Cast<string>().ToArray();


            // FFmpeg command
            //string ffmpegPath = "ffmpeg"; // Ensure FFmpeg is in your PATH or provide the full path
            string arguments = "-y -f image2pipe -r " + myFrameRate +
                               " -i pipe:0 -c:v libx265 -preset slow -crf -1 -pix_fmt yuv420p " + myOutputVideo;

            /*
             * FFMPEG options
             *
             * usage = ffmpeg  [Global options]  [[input1_options] -i infile]  [[output1_options] outfile]
             *
             * -y = overwrite output files
             * -f fmt = force format - for mp4 extension ffmpeg auto selects h264 encoder and yuv420p pixel format
             * -framerate = set video framerate can also use -r
             * -i = infile
             * -c codec = codec name
             * -preset = set encoding speed preset [slow, medium, fast]
             * -crf = select quality for constant quality mode [range 18 - 28 lower = better]
             * -pix_fmt = pixel format to use
             *
             *  -safe = concatenation will not fail with segments of different format
             *  -r = set video frame rate
             *
             *  yuv420p = 12 bits per pixel (for YouTube)
             *  image2pipe = put the still image into a pipe
             *  pipe0 = use the data from the first pipe
             *  libx265 or libx264 = video codec to use
             */








            //// 2. Build FFmpeg command
            //string args =
            //    $"-y -f concat -safe 0 -i \"{listFile}\" " +
            //    $"-r {fps} " +
            //    "-c:v libx264 -preset slow -crf 18 -pix_fmt yuv420p " +
            //    $"\"{outputPath}\"";

            //// 3. Run FFmpeg
            //var process = new Process();
            //process.StartInfo.FileName = ffmpegPath;
            //process.StartInfo.Arguments = args;
            //process.StartInfo.CreateNoWindow = true;
            //process.StartInfo.UseShellExecute = false;
            //process.StartInfo.RedirectStandardError = true;

            try
            {
                var process = new Process
                {
                    StartInfo =
                    {
                        FileName = ffmpegPath,
                        Arguments = arguments,
                        RedirectStandardInput = true, // Redirect standard input
                        RedirectStandardOutput = true, // Redirect standard output
                        RedirectStandardError = true, // Redirect standard error
                        UseShellExecute = false, // Required for redirection
                        CreateNoWindow = true // Optional: Run without creating a window
                    }
                };





                //using (var process = new Process())
                //{
                //    process.StartInfo.FileName = ffmpegPath;
                //    process.StartInfo.Arguments = arguments;
                //    process.StartInfo.UseShellExecute = false;
                //    process.StartInfo.RedirectStandardInput = true;
                //    process.StartInfo.RedirectStandardOutput = true;
                //    process.StartInfo.RedirectStandardError = true;
                //    process.StartInfo.CreateNoWindow = true;

                process.Start();

                Stream ffmpegInput = process.StandardInput.BaseStream;

                // Write images to FFmpeg's standard input
                // using (var ffmpegInput = process.StandardInput.BaseStream)
                using (ffmpegInput)
                {
                    foreach (string imageFile in myImagesArray)
                    {
                        using (var bitmap = new Bitmap(imageFile))
                        {
                            bitmap.Save(ffmpegInput, ImageFormat.Jpeg);
                            myRichTextBox.AppendText("Adding:" + imageFile + "\r");
                            myRichTextBox.ScrollToCaret();
                        }
                    }
                }

                //Close the process
                process.Close();

                // Wait for FFmpeg to finish
                if (!process.HasExited) process.WaitForExit();

               

                    if (process.ExitCode == 0)
                    {
                        MsgBox.Show($"Video created successfully: {myOutputVideo}", "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    else
                    {
                        string error = process.StandardError.ReadToEnd();
                        MsgBox.Show($"FFmpeg error: {error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                












                //// 1. Build temporary file list for FFmpeg
                //// string listFile = Path.Combine(Path.GetTempPath(), "ffmpeg_images.txt");

                //string listFile = "ffmpeg_images.txt";


                //var sb = new StringBuilder();
                //foreach (var img in imagePaths)
                //{
                //    sb.AppendLine($"file '{img.Replace("\\", "/")}'");
                //}
                //File.WriteAllText(listFile, sb.ToString());

                //// 2. Build FFmpeg command
                //string args =
                //    $"-y -f concat -safe 0 -i \"{listFile}\" " +
                //    $"-r {fps} " +
                //    "-c:v libx264 -preset slow -crf 18 -pix_fmt yuv420p " +
                //    $"\"{outputPath}\"";

                //// 3. Run FFmpeg
                //var process = new Process();
                //process.StartInfo.FileName = ffmpegPath;
                //process.StartInfo.Arguments = args;
                //process.StartInfo.CreateNoWindow = true;
                //process.StartInfo.UseShellExecute = false;
                //process.StartInfo.RedirectStandardError = true;

                //process.Start();

                //// Optional: read progress output
                //string line;
                //while ((line = process.StandardError.ReadLine()) != null)
                //{
                //    // MsgBox.Show(line, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //    myRichTextBox.AppendText(line + "\r");
                //    myRichTextBox.ScrollToCaret();
                //}

                //process.WaitForExit();
            }
            catch
            {

            }
        }
    }
}
