namespace DLVD.Users
{
    partial class UserDetails
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
            this.label1 = new System.Windows.Forms.Label();
            this.userInfo1 = new DLVD.UserControlsUtil.UserInfo();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(416, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(273, 38);
            this.label1.TabIndex = 1;
            this.label1.Text = "User Information";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // userInfo1
            // 
            this.userInfo1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.userInfo1.Location = new System.Drawing.Point(76, 116);
            this.userInfo1.Name = "userInfo1";
            this.userInfo1.Size = new System.Drawing.Size(957, 467);
            this.userInfo1.TabIndex = 0;
            // 
            // UserDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 631);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.userInfo1);
            this.Name = "UserDetails";
            this.Text = "UserDetails";
            this.Load += new System.EventHandler(this.UserDetails_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControlsUtil.UserInfo userInfo1;
        private System.Windows.Forms.Label label1;
    }
}