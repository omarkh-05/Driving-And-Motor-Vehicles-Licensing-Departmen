namespace DLVD.Applications
{
    partial class IssueDrivingLicense
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
            this.btnSave = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            this.applicationInfo1 = new DLVD.UserControlsUtil.ApplicationInfo();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSave.BackColor = System.Drawing.Color.White;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::DLVD.Properties.Resources.Save_32;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(465, 574);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(105, 43);
            this.btnSave.TabIndex = 116;
            this.btnSave.Text = "      Issue";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Image = global::DLVD.Properties.Resources.Close_32;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(465, 639);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(105, 43);
            this.button2.TabIndex = 115;
            this.button2.Text = "     close";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtNotes
            // 
            this.txtNotes.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtNotes.Location = new System.Drawing.Point(251, 448);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(527, 120);
            this.txtNotes.TabIndex = 114;
            this.txtNotes.WordWrap = false;
            // 
            // pictureBox8
            // 
            this.pictureBox8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox8.Image = global::DLVD.Properties.Resources.Notes_32;
            this.pictureBox8.Location = new System.Drawing.Point(214, 448);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(31, 29);
            this.pictureBox8.TabIndex = 113;
            this.pictureBox8.TabStop = false;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(150, 446);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 22);
            this.label11.TabIndex = 112;
            this.label11.Text = "Note: ";
            // 
            // applicationInfo1
            // 
            this.applicationInfo1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.applicationInfo1.Location = new System.Drawing.Point(150, 85);
            this.applicationInfo1.Name = "applicationInfo1";
            this.applicationInfo1.Size = new System.Drawing.Size(700, 336);
            this.applicationInfo1.TabIndex = 0;
            // 
            // IssueDrivingLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 744);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.pictureBox8);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.applicationInfo1);
            this.Name = "IssueDrivingLicense";
            this.Text = "IssueDrivingLicense";
            this.Load += new System.EventHandler(this.IssueDrivingLicense_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControlsUtil.ApplicationInfo applicationInfo1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.Label label11;
    }
}