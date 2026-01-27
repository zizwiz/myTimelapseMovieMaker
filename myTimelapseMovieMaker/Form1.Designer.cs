
namespace myTimelapseMovieMaker
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_ChooseFolder = new System.Windows.Forms.Button();
            this.lbl_Folder = new System.Windows.Forms.Label();
            this.lbl_Fps = new System.Windows.Forms.Label();
            this.numUpDn_Fps = new System.Windows.Forms.NumericUpDown();
            this.btn_Start = new System.Windows.Forms.Button();
            this.btn_Abort = new System.Windows.Forms.Button();
            this.lst_Images = new System.Windows.Forms.ListBox();
            this.btn_MoveUp = new System.Windows.Forms.Button();
            this.btn_MoveDown = new System.Windows.Forms.Button();
            this.btn_Remove = new System.Windows.Forms.Button();
            this.picbx_Preview = new System.Windows.Forms.PictureBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lbl_Status = new System.Windows.Forms.Label();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_rename_files = new System.Windows.Forms.Button();
            this.txtbx_rename_counter = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWildcard = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNewNameFormat = new System.Windows.Forms.TextBox();
            this.lbl_renamed_file_path = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.rchtxbx_output = new System.Windows.Forms.RichTextBox();
            this.lbl_movie_time = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDn_Fps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbx_Preview)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_ChooseFolder
            // 
            this.btn_ChooseFolder.Location = new System.Drawing.Point(12, 12);
            this.btn_ChooseFolder.Name = "btn_ChooseFolder";
            this.btn_ChooseFolder.Size = new System.Drawing.Size(146, 44);
            this.btn_ChooseFolder.TabIndex = 0;
            this.btn_ChooseFolder.Text = "Choose Folder...";
            this.btn_ChooseFolder.UseVisualStyleBackColor = true;
            this.btn_ChooseFolder.Click += new System.EventHandler(this.btn_ChooseFolder_Click);
            // 
            // lbl_Folder
            // 
            this.lbl_Folder.AutoSize = true;
            this.lbl_Folder.Location = new System.Drawing.Point(164, 24);
            this.lbl_Folder.Name = "lbl_Folder";
            this.lbl_Folder.Size = new System.Drawing.Size(172, 20);
            this.lbl_Folder.TabIndex = 1;
            this.lbl_Folder.Text = "Folder: (none selected)";
            // 
            // lbl_Fps
            // 
            this.lbl_Fps.AutoSize = true;
            this.lbl_Fps.Location = new System.Drawing.Point(371, 24);
            this.lbl_Fps.Name = "lbl_Fps";
            this.lbl_Fps.Size = new System.Drawing.Size(127, 20);
            this.lbl_Fps.TabIndex = 2;
            this.lbl_Fps.Text = "Frame rate (fps):";
            // 
            // numUpDn_Fps
            // 
            this.numUpDn_Fps.Location = new System.Drawing.Point(504, 22);
            this.numUpDn_Fps.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numUpDn_Fps.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDn_Fps.Name = "numUpDn_Fps";
            this.numUpDn_Fps.Size = new System.Drawing.Size(120, 26);
            this.numUpDn_Fps.TabIndex = 3;
            this.numUpDn_Fps.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(679, 18);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(113, 32);
            this.btn_Start.TabIndex = 4;
            this.btn_Start.Text = "Start";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // btn_Abort
            // 
            this.btn_Abort.Location = new System.Drawing.Point(798, 18);
            this.btn_Abort.Name = "btn_Abort";
            this.btn_Abort.Size = new System.Drawing.Size(113, 32);
            this.btn_Abort.TabIndex = 5;
            this.btn_Abort.Text = "Abort";
            this.btn_Abort.UseVisualStyleBackColor = true;
            this.btn_Abort.Click += new System.EventHandler(this.btn_Abort_Click);
            // 
            // lst_Images
            // 
            this.lst_Images.FormattingEnabled = true;
            this.lst_Images.ItemHeight = 20;
            this.lst_Images.Location = new System.Drawing.Point(49, 76);
            this.lst_Images.Name = "lst_Images";
            this.lst_Images.Size = new System.Drawing.Size(365, 344);
            this.lst_Images.TabIndex = 6;
            this.lst_Images.SelectedIndexChanged += new System.EventHandler(this.lst_Images_SelectedIndexChanged);
            // 
            // btn_MoveUp
            // 
            this.btn_MoveUp.Location = new System.Drawing.Point(456, 84);
            this.btn_MoveUp.Name = "btn_MoveUp";
            this.btn_MoveUp.Size = new System.Drawing.Size(114, 30);
            this.btn_MoveUp.TabIndex = 7;
            this.btn_MoveUp.Text = "Move Up";
            this.btn_MoveUp.UseVisualStyleBackColor = true;
            this.btn_MoveUp.Click += new System.EventHandler(this.btn_MoveUp_Click);
            // 
            // btn_MoveDown
            // 
            this.btn_MoveDown.Location = new System.Drawing.Point(456, 120);
            this.btn_MoveDown.Name = "btn_MoveDown";
            this.btn_MoveDown.Size = new System.Drawing.Size(114, 30);
            this.btn_MoveDown.TabIndex = 8;
            this.btn_MoveDown.Text = "Move Down";
            this.btn_MoveDown.UseVisualStyleBackColor = true;
            this.btn_MoveDown.Click += new System.EventHandler(this.btn_MoveDown_Click);
            // 
            // btn_Remove
            // 
            this.btn_Remove.Location = new System.Drawing.Point(456, 156);
            this.btn_Remove.Name = "btn_Remove";
            this.btn_Remove.Size = new System.Drawing.Size(114, 30);
            this.btn_Remove.TabIndex = 9;
            this.btn_Remove.Text = "Remove";
            this.btn_Remove.UseVisualStyleBackColor = true;
            this.btn_Remove.Click += new System.EventHandler(this.btn_Remove_Click);
            // 
            // picbx_Preview
            // 
            this.picbx_Preview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picbx_Preview.Location = new System.Drawing.Point(630, 87);
            this.picbx_Preview.Name = "picbx_Preview";
            this.picbx_Preview.Size = new System.Drawing.Size(435, 251);
            this.picbx_Preview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picbx_Preview.TabIndex = 10;
            this.picbx_Preview.TabStop = false;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(26, 532);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1050, 25);
            this.progressBar.TabIndex = 11;
            // 
            // lbl_Status
            // 
            this.lbl_Status.AutoSize = true;
            this.lbl_Status.Location = new System.Drawing.Point(12, 569);
            this.lbl_Status.Name = "lbl_Status";
            this.lbl_Status.Size = new System.Drawing.Size(56, 20);
            this.lbl_Status.TabIndex = 12;
            this.lbl_Status.Text = "Status";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "MP4 Video|*.mp4";
            this.saveFileDialog.Title = "Save Timelapse Video";
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(938, 571);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(126, 49);
            this.btn_close.TabIndex = 13;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(917, 18);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(113, 32);
            this.btn_reset.TabIndex = 14;
            this.btn_reset.Text = "Reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_rename_files
            // 
            this.btn_rename_files.Location = new System.Drawing.Point(44, 450);
            this.btn_rename_files.Name = "btn_rename_files";
            this.btn_rename_files.Size = new System.Drawing.Size(114, 30);
            this.btn_rename_files.TabIndex = 15;
            this.btn_rename_files.Text = "Rename files";
            this.btn_rename_files.UseVisualStyleBackColor = true;
            this.btn_rename_files.Click += new System.EventHandler(this.btn_rename_files_Click);
            // 
            // txtbx_rename_counter
            // 
            this.txtbx_rename_counter.Location = new System.Drawing.Point(342, 452);
            this.txtbx_rename_counter.Name = "txtbx_rename_counter";
            this.txtbx_rename_counter.Size = new System.Drawing.Size(86, 26);
            this.txtbx_rename_counter.TabIndex = 16;
            this.txtbx_rename_counter.Text = "1";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(182, 455);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(154, 20);
            this.label12.TabIndex = 17;
            this.label12.Text = "Counter Start Value:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(434, 455);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 20);
            this.label3.TabIndex = 18;
            this.label3.Text = "File Type to Rename:";
            // 
            // txtWildcard
            // 
            this.txtWildcard.Location = new System.Drawing.Point(599, 452);
            this.txtWildcard.Name = "txtWildcard";
            this.txtWildcard.Size = new System.Drawing.Size(76, 26);
            this.txtWildcard.TabIndex = 19;
            this.txtWildcard.Text = "*.jpg";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(681, 455);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 20);
            this.label4.TabIndex = 20;
            this.label4.Text = "New Name Format:";
            // 
            // txtNewNameFormat
            // 
            this.txtNewNameFormat.Location = new System.Drawing.Point(832, 452);
            this.txtNewNameFormat.Name = "txtNewNameFormat";
            this.txtNewNameFormat.Size = new System.Drawing.Size(132, 26);
            this.txtNewNameFormat.TabIndex = 21;
            // 
            // lbl_renamed_file_path
            // 
            this.lbl_renamed_file_path.AutoSize = true;
            this.lbl_renamed_file_path.Location = new System.Drawing.Point(337, 491);
            this.lbl_renamed_file_path.Name = "lbl_renamed_file_path";
            this.lbl_renamed_file_path.Size = new System.Drawing.Size(29, 20);
            this.lbl_renamed_file_path.TabIndex = 22;
            this.lbl_renamed_file_path.Text = ".....";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(182, 491);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(149, 20);
            this.label6.TabIndex = 23;
            this.label6.Text = "Renamed File Path:";
            // 
            // rchtxbx_output
            // 
            this.rchtxbx_output.Location = new System.Drawing.Point(257, 566);
            this.rchtxbx_output.Name = "rchtxbx_output";
            this.rchtxbx_output.Size = new System.Drawing.Size(642, 98);
            this.rchtxbx_output.TabIndex = 24;
            this.rchtxbx_output.Text = "";
            // 
            // lbl_movie_time
            // 
            this.lbl_movie_time.AutoSize = true;
            this.lbl_movie_time.Location = new System.Drawing.Point(12, 631);
            this.lbl_movie_time.Name = "lbl_movie_time";
            this.lbl_movie_time.Size = new System.Drawing.Size(84, 20);
            this.lbl_movie_time.TabIndex = 25;
            this.lbl_movie_time.Text = "Movie time";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1099, 676);
            this.Controls.Add(this.lbl_movie_time);
            this.Controls.Add(this.rchtxbx_output);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lbl_renamed_file_path);
            this.Controls.Add(this.txtNewNameFormat);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtWildcard);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtbx_rename_counter);
            this.Controls.Add(this.btn_rename_files);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.lbl_Status);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.picbx_Preview);
            this.Controls.Add(this.btn_Remove);
            this.Controls.Add(this.btn_MoveDown);
            this.Controls.Add(this.btn_MoveUp);
            this.Controls.Add(this.lst_Images);
            this.Controls.Add(this.btn_Abort);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.numUpDn_Fps);
            this.Controls.Add(this.lbl_Fps);
            this.Controls.Add(this.lbl_Folder);
            this.Controls.Add(this.btn_ChooseFolder);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Timelapse Movie Maker";
            ((System.ComponentModel.ISupportInitialize)(this.numUpDn_Fps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbx_Preview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_ChooseFolder;
        private System.Windows.Forms.Label lbl_Folder;
        private System.Windows.Forms.Label lbl_Fps;
        private System.Windows.Forms.NumericUpDown numUpDn_Fps;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.Button btn_Abort;
        private System.Windows.Forms.ListBox lst_Images;
        private System.Windows.Forms.Button btn_MoveUp;
        private System.Windows.Forms.Button btn_MoveDown;
        private System.Windows.Forms.Button btn_Remove;
        private System.Windows.Forms.PictureBox picbx_Preview;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lbl_Status;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Button btn_rename_files;
        private System.Windows.Forms.TextBox txtbx_rename_counter;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtWildcard;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNewNameFormat;
        private System.Windows.Forms.Label lbl_renamed_file_path;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox rchtxbx_output;
        private System.Windows.Forms.Label lbl_movie_time;
    }
}

