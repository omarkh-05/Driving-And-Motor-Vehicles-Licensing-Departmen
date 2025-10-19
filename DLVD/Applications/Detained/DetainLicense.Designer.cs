namespace DLVD.Applications.Detained
{
    partial class DetainLicense
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
            this.components = new System.ComponentModel.Container();
            this.llShowLicenseInfo = new System.Windows.Forms.LinkLabel();
            this.llShowLicenseHistory = new System.Windows.Forms.LinkLabel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtFineFees = new System.Windows.Forms.TextBox();
            this.lblCreatedBy = new System.Windows.Forms.Label();
            this.pictureBox16 = new System.Windows.Forms.PictureBox();
            this.pictureBox18 = new System.Windows.Forms.PictureBox();
            this.lblLicenseID = new System.Windows.Forms.Label();
            this.lblDetainDate = new System.Windows.Forms.Label();
            this.lblDetainID = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.pictureBox24 = new System.Windows.Forms.PictureBox();
            this.pictureBox26 = new System.Windows.Forms.PictureBox();
            this.pictureBox27 = new System.Windows.Forms.PictureBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.ctrlDrivingLicenseWithFilterInfo1 = new DLVD.UserControlsUtil.ctrlDrivingLicenseWithFilterInfo();
            this.btnDetain = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox26)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox27)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // llShowLicenseInfo
            // 
            this.llShowLicenseInfo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.llShowLicenseInfo.AutoSize = true;
            this.llShowLicenseInfo.Location = new System.Drawing.Point(276, 743);
            this.llShowLicenseInfo.Name = "llShowLicenseInfo";
            this.llShowLicenseInfo.Size = new System.Drawing.Size(144, 16);
            this.llShowLicenseInfo.TabIndex = 53;
            this.llShowLicenseInfo.TabStop = true;
            this.llShowLicenseInfo.Text = "Show Now License Info";
            this.llShowLicenseInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // llShowLicenseHistory
            // 
            this.llShowLicenseHistory.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.llShowLicenseHistory.AutoSize = true;
            this.llShowLicenseHistory.Location = new System.Drawing.Point(107, 743);
            this.llShowLicenseHistory.Name = "llShowLicenseHistory";
            this.llShowLicenseHistory.Size = new System.Drawing.Size(135, 16);
            this.llShowLicenseHistory.TabIndex = 52;
            this.llShowLicenseHistory.TabStop = true;
            this.llShowLicenseHistory.Text = "Show License History";
            this.llShowLicenseHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox3.Controls.Add(this.txtFineFees);
            this.groupBox3.Controls.Add(this.lblCreatedBy);
            this.groupBox3.Controls.Add(this.pictureBox16);
            this.groupBox3.Controls.Add(this.pictureBox18);
            this.groupBox3.Controls.Add(this.lblLicenseID);
            this.groupBox3.Controls.Add(this.lblDetainDate);
            this.groupBox3.Controls.Add(this.lblDetainID);
            this.groupBox3.Controls.Add(this.label31);
            this.groupBox3.Controls.Add(this.label33);
            this.groupBox3.Controls.Add(this.pictureBox24);
            this.groupBox3.Controls.Add(this.pictureBox26);
            this.groupBox3.Controls.Add(this.pictureBox27);
            this.groupBox3.Controls.Add(this.label38);
            this.groupBox3.Controls.Add(this.label40);
            this.groupBox3.Controls.Add(this.label41);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(104, 560);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(887, 163);
            this.groupBox3.TabIndex = 49;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Detain Info";
            // 
            // txtFineFees
            // 
            this.txtFineFees.Location = new System.Drawing.Point(229, 108);
            this.txtFineFees.Name = "txtFineFees";
            this.txtFineFees.Size = new System.Drawing.Size(74, 22);
            this.txtFineFees.TabIndex = 63;
            this.txtFineFees.Validating += new System.ComponentModel.CancelEventHandler(this.txtFineFees_Validating);
            // 
            // lblCreatedBy
            // 
            this.lblCreatedBy.AutoSize = true;
            this.lblCreatedBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreatedBy.Location = new System.Drawing.Point(595, 73);
            this.lblCreatedBy.Name = "lblCreatedBy";
            this.lblCreatedBy.Size = new System.Drawing.Size(28, 16);
            this.lblCreatedBy.TabIndex = 62;
            this.lblCreatedBy.Text = "???";
            // 
            // pictureBox16
            // 
            this.pictureBox16.Image = global::DLVD.Properties.Resources.User_32__2;
            this.pictureBox16.Location = new System.Drawing.Point(546, 61);
            this.pictureBox16.Name = "pictureBox16";
            this.pictureBox16.Size = new System.Drawing.Size(31, 29);
            this.pictureBox16.TabIndex = 58;
            this.pictureBox16.TabStop = false;
            // 
            // pictureBox18
            // 
            this.pictureBox18.Image = global::DLVD.Properties.Resources.Number_32;
            this.pictureBox18.Location = new System.Drawing.Point(546, 21);
            this.pictureBox18.Name = "pictureBox18";
            this.pictureBox18.Size = new System.Drawing.Size(31, 29);
            this.pictureBox18.TabIndex = 56;
            this.pictureBox18.TabStop = false;
            // 
            // lblLicenseID
            // 
            this.lblLicenseID.AutoSize = true;
            this.lblLicenseID.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLicenseID.Location = new System.Drawing.Point(595, 30);
            this.lblLicenseID.Name = "lblLicenseID";
            this.lblLicenseID.Size = new System.Drawing.Size(28, 16);
            this.lblLicenseID.TabIndex = 55;
            this.lblLicenseID.Text = "???";
            // 
            // lblDetainDate
            // 
            this.lblDetainDate.AutoSize = true;
            this.lblDetainDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetainDate.Location = new System.Drawing.Point(226, 72);
            this.lblDetainDate.Name = "lblDetainDate";
            this.lblDetainDate.Size = new System.Drawing.Size(28, 16);
            this.lblDetainDate.TabIndex = 48;
            this.lblDetainDate.Text = "???";
            // 
            // lblDetainID
            // 
            this.lblDetainID.AutoSize = true;
            this.lblDetainID.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetainID.Location = new System.Drawing.Point(226, 30);
            this.lblDetainID.Name = "lblDetainID";
            this.lblDetainID.Size = new System.Drawing.Size(28, 16);
            this.lblDetainID.TabIndex = 47;
            this.lblDetainID.Text = "???";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(425, 70);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(115, 20);
            this.label31.TabIndex = 43;
            this.label31.Text = "Created By: ";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(428, 26);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(112, 20);
            this.label33.TabIndex = 41;
            this.label33.Text = "LIcense ID: ";
            // 
            // pictureBox24
            // 
            this.pictureBox24.Image = global::DLVD.Properties.Resources.money_32;
            this.pictureBox24.Location = new System.Drawing.Point(175, 101);
            this.pictureBox24.Name = "pictureBox24";
            this.pictureBox24.Size = new System.Drawing.Size(31, 29);
            this.pictureBox24.TabIndex = 36;
            this.pictureBox24.TabStop = false;
            // 
            // pictureBox26
            // 
            this.pictureBox26.Image = global::DLVD.Properties.Resources.Number_32;
            this.pictureBox26.Location = new System.Drawing.Point(175, 21);
            this.pictureBox26.Name = "pictureBox26";
            this.pictureBox26.Size = new System.Drawing.Size(31, 29);
            this.pictureBox26.TabIndex = 34;
            this.pictureBox26.TabStop = false;
            // 
            // pictureBox27
            // 
            this.pictureBox27.Image = global::DLVD.Properties.Resources.Calendar_32;
            this.pictureBox27.Location = new System.Drawing.Point(175, 60);
            this.pictureBox27.Name = "pictureBox27";
            this.pictureBox27.Size = new System.Drawing.Size(31, 29);
            this.pictureBox27.TabIndex = 33;
            this.pictureBox27.TabStop = false;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(29, 108);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(112, 20);
            this.label38.TabIndex = 3;
            this.label38.Text = "Detain Fine:";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(28, 68);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(122, 20);
            this.label40.TabIndex = 1;
            this.label40.Text = "Detain Date: ";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(49, 30);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(101, 20);
            this.label41.TabIndex = 0;
            this.label41.Text = "Detain ID: ";
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(422, 20);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(217, 32);
            this.label15.TabIndex = 48;
            this.label15.Text = "Detain License";
            // 
            // ctrlDrivingLicenseWithFilterInfo1
            // 
            this.ctrlDrivingLicenseWithFilterInfo1.FilterEnabled = true;
            this.ctrlDrivingLicenseWithFilterInfo1.Location = new System.Drawing.Point(104, 77);
            this.ctrlDrivingLicenseWithFilterInfo1.Name = "ctrlDrivingLicenseWithFilterInfo1";
            this.ctrlDrivingLicenseWithFilterInfo1.Size = new System.Drawing.Size(892, 477);
            this.ctrlDrivingLicenseWithFilterInfo1.TabIndex = 54;
            this.ctrlDrivingLicenseWithFilterInfo1.OnLicenseSelected += new System.Action<int>(this.ctrlDrivingLicenseWithFilterInfo1_OnLicenseSelected);
            // 
            // btnDetain
            // 
            this.btnDetain.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDetain.BackColor = System.Drawing.Color.White;
            this.btnDetain.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDetain.Image = global::DLVD.Properties.Resources.Detain_32;
            this.btnDetain.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDetain.Location = new System.Drawing.Point(871, 729);
            this.btnDetain.Name = "btnDetain";
            this.btnDetain.Size = new System.Drawing.Size(120, 43);
            this.btnDetain.TabIndex = 51;
            this.btnDetain.Text = "       Detain";
            this.btnDetain.UseVisualStyleBackColor = false;
            this.btnDetain.Click += new System.EventHandler(this.btnDetain_Click);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Image = global::DLVD.Properties.Resources.Close_32;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(760, 729);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(105, 43);
            this.button2.TabIndex = 50;
            this.button2.Text = "     close";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // DetainLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1090, 809);
            this.Controls.Add(this.ctrlDrivingLicenseWithFilterInfo1);
            this.Controls.Add(this.llShowLicenseInfo);
            this.Controls.Add(this.llShowLicenseHistory);
            this.Controls.Add(this.btnDetain);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label15);
            this.Name = "DetainLicense";
            this.Text = "DetainLicense";
            this.Load += new System.EventHandler(this.DetainLicense_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox26)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox27)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel llShowLicenseInfo;
        private System.Windows.Forms.LinkLabel llShowLicenseHistory;
        private System.Windows.Forms.Button btnDetain;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblCreatedBy;
        private System.Windows.Forms.PictureBox pictureBox16;
        private System.Windows.Forms.PictureBox pictureBox18;
        private System.Windows.Forms.Label lblLicenseID;
        private System.Windows.Forms.Label lblDetainDate;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.PictureBox pictureBox24;
        private System.Windows.Forms.PictureBox pictureBox26;
        private System.Windows.Forms.PictureBox pictureBox27;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtFineFees;
        private UserControlsUtil.ctrlDrivingLicenseWithFilterInfo ctrlDrivingLicenseWithFilterInfo1;
        private System.Windows.Forms.Label lblDetainID;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}