
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
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
            this.lbl_codec = new System.Windows.Forms.Label();
            this.cmbobx_codec = new System.Windows.Forms.ComboBox();
            this.cmbobx_encoding_speed = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.trkbr_Quality = new System.Windows.Forms.TrackBar();
            this.chkbx_downscale = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl_quality_value = new System.Windows.Forms.Label();
            this.grpbx_rename_files = new System.Windows.Forms.GroupBox();
            this.chkbx_rename_files = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDn_Fps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbx_Preview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbr_Quality)).BeginInit();
            this.grpbx_rename_files.SuspendLayout();
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
            14,
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
            this.btn_rename_files.Location = new System.Drawing.Point(21, 37);
            this.btn_rename_files.Name = "btn_rename_files";
            this.btn_rename_files.Size = new System.Drawing.Size(114, 30);
            this.btn_rename_files.TabIndex = 15;
            this.btn_rename_files.Text = "Rename";
            this.btn_rename_files.UseVisualStyleBackColor = true;
            this.btn_rename_files.Click += new System.EventHandler(this.btn_rename_files_Click);
            // 
            // txtbx_rename_counter
            // 
            this.txtbx_rename_counter.Location = new System.Drawing.Point(301, 8);
            this.txtbx_rename_counter.Name = "txtbx_rename_counter";
            this.txtbx_rename_counter.Size = new System.Drawing.Size(86, 26);
            this.txtbx_rename_counter.TabIndex = 16;
            this.txtbx_rename_counter.Text = "1";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(141, 11);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(154, 20);
            this.label12.TabIndex = 17;
            this.label12.Text = "Counter Start Value:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(393, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 20);
            this.label3.TabIndex = 18;
            this.label3.Text = "File Type to Rename:";
            // 
            // txtWildcard
            // 
            this.txtWildcard.Location = new System.Drawing.Point(558, 8);
            this.txtWildcard.Name = "txtWildcard";
            this.txtWildcard.Size = new System.Drawing.Size(76, 26);
            this.txtWildcard.TabIndex = 19;
            this.txtWildcard.Text = "*.jpg";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(640, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 20);
            this.label4.TabIndex = 20;
            this.label4.Text = "New Name Format:";
            // 
            // txtNewNameFormat
            // 
            this.txtNewNameFormat.Location = new System.Drawing.Point(791, 8);
            this.txtNewNameFormat.Name = "txtNewNameFormat";
            this.txtNewNameFormat.Size = new System.Drawing.Size(132, 26);
            this.txtNewNameFormat.TabIndex = 21;
            // 
            // lbl_renamed_file_path
            // 
            this.lbl_renamed_file_path.AutoSize = true;
            this.lbl_renamed_file_path.Location = new System.Drawing.Point(296, 47);
            this.lbl_renamed_file_path.Name = "lbl_renamed_file_path";
            this.lbl_renamed_file_path.Size = new System.Drawing.Size(29, 20);
            this.lbl_renamed_file_path.TabIndex = 22;
            this.lbl_renamed_file_path.Text = ".....";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(141, 47);
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
            // lbl_codec
            // 
            this.lbl_codec.AutoSize = true;
            this.lbl_codec.Location = new System.Drawing.Point(429, 355);
            this.lbl_codec.Name = "lbl_codec";
            this.lbl_codec.Size = new System.Drawing.Size(55, 20);
            this.lbl_codec.TabIndex = 26;
            this.lbl_codec.Text = "Codec";
            // 
            // cmbobx_codec
            // 
            this.cmbobx_codec.FormattingEnabled = true;
            this.cmbobx_codec.Items.AddRange(new object[] {
            "libx264",
            "libx265"});
            this.cmbobx_codec.Location = new System.Drawing.Point(490, 352);
            this.cmbobx_codec.Name = "cmbobx_codec";
            this.cmbobx_codec.Size = new System.Drawing.Size(121, 28);
            this.cmbobx_codec.TabIndex = 27;
            this.cmbobx_codec.SelectedIndexChanged += new System.EventHandler(this.cmbobx_codec_SelectedIndexChanged);
            // 
            // cmbobx_encoding_speed
            // 
            this.cmbobx_encoding_speed.FormattingEnabled = true;
            this.cmbobx_encoding_speed.Items.AddRange(new object[] {
            "slow",
            "medium",
            "fast"});
            this.cmbobx_encoding_speed.Location = new System.Drawing.Point(747, 352);
            this.cmbobx_encoding_speed.Name = "cmbobx_encoding_speed";
            this.cmbobx_encoding_speed.Size = new System.Drawing.Size(121, 28);
            this.cmbobx_encoding_speed.TabIndex = 29;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(617, 355);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 20);
            this.label1.TabIndex = 28;
            this.label1.Text = "Encoding speed";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(429, 400);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 20);
            this.label2.TabIndex = 30;
            this.label2.Text = "Quality";
            // 
            // trkbr_Quality
            // 
            this.trkbr_Quality.Location = new System.Drawing.Point(492, 386);
            this.trkbr_Quality.Maximum = 51;
            this.trkbr_Quality.Name = "trkbr_Quality";
            this.trkbr_Quality.Size = new System.Drawing.Size(538, 69);
            this.trkbr_Quality.TabIndex = 31;
            this.trkbr_Quality.Value = 18;
            this.trkbr_Quality.Scroll += new System.EventHandler(this.trkbr_Quality_Scroll);
            // 
            // chkbx_downscale
            // 
            this.chkbx_downscale.AutoSize = true;
            this.chkbx_downscale.Checked = true;
            this.chkbx_downscale.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbx_downscale.Location = new System.Drawing.Point(884, 356);
            this.chkbx_downscale.Name = "chkbx_downscale";
            this.chkbx_downscale.Size = new System.Drawing.Size(180, 24);
            this.chkbx_downscale.TabIndex = 33;
            this.chkbx_downscale.Text = "Downscale to 1080p";
            this.chkbx_downscale.UseVisualStyleBackColor = true;
            this.chkbx_downscale.CheckedChanged += new System.EventHandler(this.chkbx_downscale_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(488, 425);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 20);
            this.label5.TabIndex = 34;
            this.label5.Text = "Best";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(988, 425);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 20);
            this.label7.TabIndex = 35;
            this.label7.Text = "Worst";
            // 
            // lbl_quality_value
            // 
            this.lbl_quality_value.AutoSize = true;
            this.lbl_quality_value.Location = new System.Drawing.Point(714, 425);
            this.lbl_quality_value.Name = "lbl_quality_value";
            this.lbl_quality_value.Size = new System.Drawing.Size(29, 20);
            this.lbl_quality_value.TabIndex = 36;
            this.lbl_quality_value.Text = ".....";
            // 
            // grpbx_rename_files
            // 
            this.grpbx_rename_files.Controls.Add(this.label6);
            this.grpbx_rename_files.Controls.Add(this.lbl_renamed_file_path);
            this.grpbx_rename_files.Controls.Add(this.txtNewNameFormat);
            this.grpbx_rename_files.Controls.Add(this.label4);
            this.grpbx_rename_files.Controls.Add(this.txtWildcard);
            this.grpbx_rename_files.Controls.Add(this.label3);
            this.grpbx_rename_files.Controls.Add(this.label12);
            this.grpbx_rename_files.Controls.Add(this.txtbx_rename_counter);
            this.grpbx_rename_files.Controls.Add(this.btn_rename_files);
            this.grpbx_rename_files.Location = new System.Drawing.Point(16, 444);
            this.grpbx_rename_files.Name = "grpbx_rename_files";
            this.grpbx_rename_files.Size = new System.Drawing.Size(931, 75);
            this.grpbx_rename_files.TabIndex = 37;
            this.grpbx_rename_files.TabStop = false;
            this.grpbx_rename_files.Text = "Rename Files";
            // 
            // chkbx_rename_files
            // 
            this.chkbx_rename_files.AutoSize = true;
            this.chkbx_rename_files.Location = new System.Drawing.Point(954, 481);
            this.chkbx_rename_files.Name = "chkbx_rename_files";
            this.chkbx_rename_files.Size = new System.Drawing.Size(133, 24);
            this.chkbx_rename_files.TabIndex = 38;
            this.chkbx_rename_files.Text = "Rename Files";
            this.chkbx_rename_files.UseVisualStyleBackColor = true;
            this.chkbx_rename_files.CheckedChanged += new System.EventHandler(this.chkbx_rename_files_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1099, 676);
            this.Controls.Add(this.chkbx_rename_files);
            this.Controls.Add(this.grpbx_rename_files);
            this.Controls.Add(this.lbl_quality_value);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chkbx_downscale);
            this.Controls.Add(this.trkbr_Quality);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbobx_encoding_speed);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbobx_codec);
            this.Controls.Add(this.lbl_codec);
            this.Controls.Add(this.lbl_movie_time);
            this.Controls.Add(this.rchtxbx_output);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Timelapse Movie Maker";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numUpDn_Fps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbx_Preview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbr_Quality)).EndInit();
            this.grpbx_rename_files.ResumeLayout(false);
            this.grpbx_rename_files.PerformLayout();
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
        private System.Windows.Forms.Label lbl_codec;
        private System.Windows.Forms.ComboBox cmbobx_codec;
        private System.Windows.Forms.ComboBox cmbobx_encoding_speed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trkbr_Quality;
        private System.Windows.Forms.CheckBox chkbx_downscale;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbl_quality_value;
        private System.Windows.Forms.GroupBox grpbx_rename_files;
        private System.Windows.Forms.CheckBox chkbx_rename_files;
    }
}

