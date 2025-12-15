namespace ShelterManagementSystem.Forms
{
    partial class UserProfileForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.dtGrid = new System.Windows.Forms.DataGridView();
            this.pnlPass = new System.Windows.Forms.Panel();
            this.lblOld = new System.Windows.Forms.Label();
            this.txtOld = new System.Windows.Forms.TextBox();
            this.lblNew = new System.Windows.Forms.Label();
            this.txtNew = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrid)).BeginInit();
            this.pnlPass.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(200, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "My Adoption History";
            // 
            // dtGrid
            // 
            this.dtGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGrid.Location = new System.Drawing.Point(20, 60);
            this.dtGrid.Name = "dtGrid";
            this.dtGrid.Size = new System.Drawing.Size(840, 300);
            this.dtGrid.TabIndex = 1;
            // 
            // pnlPass
            // 
            this.pnlPass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPass.Controls.Add(this.btnSave);
            this.pnlPass.Controls.Add(this.txtNew);
            this.pnlPass.Controls.Add(this.lblNew);
            this.pnlPass.Controls.Add(this.txtOld);
            this.pnlPass.Controls.Add(this.lblOld);
            this.pnlPass.Location = new System.Drawing.Point(20, 380);
            this.pnlPass.Name = "pnlPass";
            this.pnlPass.Size = new System.Drawing.Size(840, 200);
            this.pnlPass.TabIndex = 2;
            // 
            // lblOld
            // 
            this.lblOld.AutoSize = true;
            this.lblOld.Location = new System.Drawing.Point(20, 30);
            this.lblOld.Name = "lblOld";
            this.lblOld.Size = new System.Drawing.Size(75, 13);
            this.lblOld.TabIndex = 0;
            this.lblOld.Text = "Old Password:";
            // 
            // txtOld
            // 
            this.txtOld.Location = new System.Drawing.Point(100, 30);
            this.txtOld.Name = "txtOld";
            this.txtOld.Size = new System.Drawing.Size(200, 20);
            this.txtOld.TabIndex = 1;
            this.txtOld.UseSystemPasswordChar = true;
            // 
            // lblNew
            // 
            this.lblNew.AutoSize = true;
            this.lblNew.Location = new System.Drawing.Point(20, 70);
            this.lblNew.Name = "lblNew";
            this.lblNew.Size = new System.Drawing.Size(81, 13);
            this.lblNew.TabIndex = 2;
            this.lblNew.Text = "New Password:";
            // 
            // txtNew
            // 
            this.txtNew.Location = new System.Drawing.Point(100, 70);
            this.txtNew.Name = "txtNew";
            this.txtNew.Size = new System.Drawing.Size(200, 20);
            this.txtNew.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(100, 110);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(200, 30);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Change Password";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // UserProfileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 600);
            this.Controls.Add(this.pnlPass);
            this.Controls.Add(this.dtGrid);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UserProfileForm";
            this.Text = "UserProfileForm";
            ((System.ComponentModel.ISupportInitialize)(this.dtGrid)).EndInit();
            this.pnlPass.ResumeLayout(false);
            this.pnlPass.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dtGrid;
        private System.Windows.Forms.Panel pnlPass;
        private System.Windows.Forms.Label lblOld;
        private System.Windows.Forms.TextBox txtOld;
        private System.Windows.Forms.Label lblNew;
        private System.Windows.Forms.TextBox txtNew;
        private System.Windows.Forms.Button btnSave;
    }
}
