using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CenteredMessagebox;


namespace myTimelapseMovieMaker
{
    public partial class Form1
    {
        //Here we will first backup the files then rename them
        private bool RenameFactory(string myFolderPath, string myWildCard, string MyNamePrefix, bool myFlag)
        {
            bool result = false;

            // Validate inputs
            if (string.IsNullOrWhiteSpace(myFolderPath) || !Directory.Exists(myFolderPath))
            {
                if (myFlag) MsgBox.Show("Please select a valid folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return result;
            }

            if (string.IsNullOrWhiteSpace(myWildCard))
            {
                if (myFlag) MsgBox.Show("Please specify a myWildCard pattern (e.g., *.txt).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return result;
            }

            if (string.IsNullOrWhiteSpace(MyNamePrefix))
            {
                if (myFlag)
                {
                    DialogResult Question =
                        MsgBox.Show("Are you sure you do not want to prefix the file number? e.g File_{0}.",
                            "Are you Sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (Question == DialogResult.No)
                    {
                        return result;
                    }
                }
            }

            try
            {
                //var files = Directory.GetFiles(myFolderPath, myWildCard);
                lbl_renamed_file_path.Text = myFolderPath + "\\renamed";
                
                string[] myImagesArray = lst_Images.Items.Cast<string>().ToArray();

                //get the start value of our counter, we always add a counter to file name to make it unique
                int counter = int.Parse(txtbx_rename_counter.Text);

                foreach (var file in myImagesArray)
                {
                    string extension = Path.GetExtension(file);
                    string newFileName = MyNamePrefix + counter + extension;
                    string newFilePath = Path.Combine(lbl_renamed_file_path.Text, newFileName);
                   
                    //create the new folder if it does not exist
                    Directory.CreateDirectory(lbl_renamed_file_path.Text);

                    //// Ensure no overwriting of existing files
                    //if (File.Exists(newFilePath))
                    //{
                    //    rchtxtbx_renamed_file_name.AppendText(newFilePath + "Already exists - Skipping\r");
                    //    rchtxtbx_renamed_file_name.ScrollToCaret();

                    //    if (myFlag) MsgBox.Show($"File {newFileName} already exists. Skipping.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    continue;
                    //}
                    //else
                    //{
                    //    rchtxtbx_renamed_file_name.AppendText(newFilePath + "\r");
                    //    rchtxtbx_renamed_file_name.ScrollToCaret();
                    //}

                    //File.Move(file, newFilePath);
                    File.Copy(file, newFilePath);
                    counter++;
                }
                LoadImagesFromFolder(lbl_renamed_file_path.Text);
                result = true;
                if (myFlag) MsgBox.Show("Files renamed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                result = false;
                if (myFlag) MsgBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }
    }
}