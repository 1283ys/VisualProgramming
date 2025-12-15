namespace ShelterManagementSystem.Forms
{
    partial class AnimalForm
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
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblSpecies = new System.Windows.Forms.Label();
            this.txtSpecies = new System.Windows.Forms.TextBox();
            this.lblGender = new System.Windows.Forms.Label();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.lblHealth = new System.Windows.Forms.Label();
            this.cmbHealth = new System.Windows.Forms.ComboBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtAdmission = new System.Windows.Forms.DateTimePicker();
            this.lblCity = new System.Windows.Forms.Label();
            this.cmbCity = new System.Windows.Forms.ComboBox();
            this.lblEnc = new System.Windows.Forms.Label();
            this.cmbEnclosure = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdopt = new System.Windows.Forms.Button();
            this.grid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(20, 20);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(70, 20);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(110, 20);
            this.txtName.TabIndex = 1;
            // 
            // lblSpecies
            // 
            this.lblSpecies.AutoSize = true;
            this.lblSpecies.Location = new System.Drawing.Point(190, 20);
            this.lblSpecies.Name = "lblSpecies";
            this.lblSpecies.Size = new System.Drawing.Size(48, 13);
            this.lblSpecies.TabIndex = 2;
            this.lblSpecies.Text = "Species:";
            // 
            // txtSpecies
            // 
            this.txtSpecies.Location = new System.Drawing.Point(250, 20);
            this.txtSpecies.Name = "txtSpecies";
            this.txtSpecies.Size = new System.Drawing.Size(110, 20);
            this.txtSpecies.TabIndex = 3;
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.Location = new System.Drawing.Point(370, 20);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(45, 13);
            this.lblGender.TabIndex = 4;
            this.lblGender.Text = "Gender:";
            // 
            // cmbGender
            // 
            this.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGender.FormattingEnabled = true;
            this.cmbGender.Items.AddRange(new object[] {
            "Male",
            "Female",
            "Unknown"});
            this.cmbGender.Location = new System.Drawing.Point(430, 20);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(80, 21);
            this.cmbGender.TabIndex = 5;
            // 
            // lblHealth
            // 
            this.lblHealth.AutoSize = true;
            this.lblHealth.Location = new System.Drawing.Point(530, 20);
            this.lblHealth.Name = "lblHealth";
            this.lblHealth.Size = new System.Drawing.Size(41, 13);
            this.lblHealth.TabIndex = 6;
            this.lblHealth.Text = "Health:";
            // 
            // cmbHealth
            // 
            this.cmbHealth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHealth.FormattingEnabled = true;
            this.cmbHealth.Items.AddRange(new object[] {
            "Healthy",
            "Sick",
            "Treatment",
            "Recovering"});
            this.cmbHealth.Location = new System.Drawing.Point(590, 20);
            this.cmbHealth.Name = "cmbHealth";
            this.cmbHealth.Size = new System.Drawing.Size(100, 21);
            this.cmbHealth.TabIndex = 7;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(20, 55);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(33, 13);
            this.lblDate.TabIndex = 8;
            this.lblDate.Text = "Date:";
            // 
            // dtAdmission
            // 
            this.dtAdmission.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtAdmission.Location = new System.Drawing.Point(70, 55);
            this.dtAdmission.Name = "dtAdmission";
            this.dtAdmission.Size = new System.Drawing.Size(110, 20);
            this.dtAdmission.TabIndex = 9;
            // 
            // lblCity
            // 
            this.lblCity.AutoSize = true;
            this.lblCity.Location = new System.Drawing.Point(190, 55);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(27, 13);
            this.lblCity.TabIndex = 10;
            this.lblCity.Text = "City:";
            // 
            // cmbCity
            // 
            this.cmbCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCity.FormattingEnabled = true;
            this.cmbCity.Location = new System.Drawing.Point(250, 55);
            this.cmbCity.Name = "cmbCity";
            this.cmbCity.Size = new System.Drawing.Size(110, 21);
            this.cmbCity.TabIndex = 11;
            // 
            // lblEnc
            // 
            this.lblEnc.AutoSize = true;
            this.lblEnc.Location = new System.Drawing.Point(370, 55);
            this.lblEnc.Name = "lblEnc";
            this.lblEnc.Size = new System.Drawing.Size(57, 13);
            this.lblEnc.TabIndex = 12;
            this.lblEnc.Text = "Enclosure:";
            // 
            // cmbEnclosure
            // 
            this.cmbEnclosure.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEnclosure.FormattingEnabled = true;
            this.cmbEnclosure.Location = new System.Drawing.Point(440, 55);
            this.cmbEnclosure.Name = "cmbEnclosure";
            this.cmbEnclosure.Size = new System.Drawing.Size(110, 21);
            this.cmbEnclosure.TabIndex = 13;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BackColor = System.Drawing.Color.LightGreen;
            this.btnAdd.Location = new System.Drawing.Point(600, 50);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(70, 23);
            this.btnAdd.TabIndex = 14;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Location = new System.Drawing.Point(680, 50);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(70, 23);
            this.btnUpdate.TabIndex = 15;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.BackColor = System.Drawing.Color.LightCoral;
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(760, 50);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(70, 23);
            this.btnDelete.TabIndex = 16;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // btnAdopt
            // 
            this.btnAdopt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdopt.BackColor = System.Drawing.Color.Gold;
            this.btnAdopt.Location = new System.Drawing.Point(720, 20);
            this.btnAdopt.Name = "btnAdopt";
            this.btnAdopt.Size = new System.Drawing.Size(130, 23);
            this.btnAdopt.TabIndex = 17;
            this.btnAdopt.Text = "Adopt Me!";
            this.btnAdopt.UseVisualStyleBackColor = false;
            this.btnAdopt.Visible = false;
            this.btnAdopt.Click += new System.EventHandler(this.BtnAdopt_Click);
            // 
            // grid
            // 
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Location = new System.Drawing.Point(20, 100);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(840, 530);
            this.grid.TabIndex = 18;
            // 
            // AnimalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 650);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.btnAdopt);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cmbEnclosure);
            this.Controls.Add(this.lblEnc);
            this.Controls.Add(this.cmbCity);
            this.Controls.Add(this.lblCity);
            this.Controls.Add(this.dtAdmission);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.cmbHealth);
            this.Controls.Add(this.lblHealth);
            this.Controls.Add(this.cmbGender);
            this.Controls.Add(this.lblGender);
            this.Controls.Add(this.txtSpecies);
            this.Controls.Add(this.lblSpecies);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AnimalForm";
            this.Text = "Manage Animals";
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblSpecies;
        private System.Windows.Forms.TextBox txtSpecies;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.ComboBox cmbGender;
        private System.Windows.Forms.Label lblHealth;
        private System.Windows.Forms.ComboBox cmbHealth;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtAdmission;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.ComboBox cmbCity;
        private System.Windows.Forms.Label lblEnc;
        private System.Windows.Forms.ComboBox cmbEnclosure;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdopt;
        private System.Windows.Forms.DataGridView grid;
    }
}
