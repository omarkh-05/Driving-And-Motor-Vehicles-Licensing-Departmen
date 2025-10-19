namespace DLVD.UserControlsUtil
{
    partial class ctrlDrivingLicenseWithFilterInfo
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.drivingLicenseInfo1 = new DLVD.UserControlsUtil.DrivingLicenseInfo();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.btnView = new System.Windows.Forms.Button();
            this.txtLicenseID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // drivingLicenseInfo1
            // 
            this.drivingLicenseInfo1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.drivingLicenseInfo1.Location = new System.Drawing.Point(0, 92);
            this.drivingLicenseInfo1.Name = "drivingLicenseInfo1";
            this.drivingLicenseInfo1.Size = new System.Drawing.Size(904, 372);
            this.drivingLicenseInfo1.TabIndex = 0;
            // 
            // gbFilter
            // 
            this.gbFilter.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gbFilter.Controls.Add(this.btnView);
            this.gbFilter.Controls.Add(this.txtLicenseID);
            this.gbFilter.Controls.Add(this.label1);
            this.gbFilter.Location = new System.Drawing.Point(0, 18);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(887, 68);
            this.gbFilter.TabIndex = 39;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "Filter";
            // 
            // btnView
            // 
            this.btnView.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnView.Image = global::DLVD.Properties.Resources.License_View_32;
            this.btnView.Location = new System.Drawing.Point(403, 21);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(51, 41);
            this.btnView.TabIndex = 9;
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // txtLicenseID
            // 
            this.txtLicenseID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtLicenseID.Location = new System.Drawing.Point(114, 30);
            this.txtLicenseID.Multiline = true;
            this.txtLicenseID.Name = "txtLicenseID";
            this.txtLicenseID.Size = new System.Drawing.Size(283, 24);
            this.txtLicenseID.TabIndex = 7;
            this.txtLicenseID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLicenseID_KeyPress);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "License ID:";
            // 
            // ctrlDrivingLicenseWithFilterInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbFilter);
            this.Controls.Add(this.drivingLicenseInfo1);
            this.Name = "ctrlDrivingLicenseWithFilterInfo";
            this.Size = new System.Drawing.Size(892, 501);
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DrivingLicenseInfo drivingLicenseInfo1;
        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.TextBox txtLicenseID;
        private System.Windows.Forms.Label label1;
    }
}
