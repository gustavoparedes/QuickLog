namespace quickLog
{
    partial class Form_SetLabel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_SetLabel));
            groupBox1 = new GroupBox();
            btn_SetLabel = new Button();
            dataGridView1 = new DataGridView();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btn_SetLabel);
            groupBox1.Controls.Add(dataGridView1);
            groupBox1.Location = new Point(56, 46);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(675, 392);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Set Label";
            // 
            // btn_SetLabel
            // 
            btn_SetLabel.Location = new Point(270, 303);
            btn_SetLabel.Name = "btn_SetLabel";
            btn_SetLabel.Size = new Size(103, 23);
            btn_SetLabel.TabIndex = 1;
            btn_SetLabel.Text = "Set Label";
            btn_SetLabel.UseVisualStyleBackColor = true;
            btn_SetLabel.Click += btn_SetLabel_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(69, 22);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(499, 258);
            dataGridView1.TabIndex = 0;
            // 
            // Form_SetLabel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_SetLabel";
            Text = "Quick Log Set Label";
            Load += Form_SetLabel_Load;
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private DataGridView dataGridView1;
        private Button btn_SetLabel;
    }
}