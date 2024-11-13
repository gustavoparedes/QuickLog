namespace quickLog
{
    partial class Form_LabelManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_LabelManager));
            dataGridView1 = new DataGridView();
            groupBox1 = new GroupBox();
            btn_save = new Button();
            btn_delete = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(25, 22);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(499, 258);
            dataGridView1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btn_save);
            groupBox1.Controls.Add(btn_delete);
            groupBox1.Controls.Add(dataGridView1);
            groupBox1.Location = new Point(64, 42);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(675, 361);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Labels";
            // 
            // btn_save
            // 
            btn_save.Location = new Point(546, 191);
            btn_save.Name = "btn_save";
            btn_save.Size = new Size(103, 23);
            btn_save.TabIndex = 3;
            btn_save.Text = "Save";
            btn_save.UseVisualStyleBackColor = true;
            btn_save.Click += btn_save_Click;
            // 
            // btn_delete
            // 
            btn_delete.Location = new Point(546, 123);
            btn_delete.Name = "btn_delete";
            btn_delete.Size = new Size(103, 23);
            btn_delete.TabIndex = 2;
            btn_delete.Text = "Delete";
            btn_delete.UseVisualStyleBackColor = true;
            btn_delete.Click += btn_delete_Click;
            // 
            // Form_LabelManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_LabelManager";
            Text = "Quick Log Label Manager";
            Load += Form_LabelManager_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private GroupBox groupBox1;
        private Button btn_delete;
        private Button btn_save;
    }
}