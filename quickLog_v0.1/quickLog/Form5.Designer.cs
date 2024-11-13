namespace quickLog
{
    partial class About_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About_Form));
            btn_Close = new Button();
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // btn_Close
            // 
            btn_Close.Location = new Point(257, 233);
            btn_Close.Name = "btn_Close";
            btn_Close.Size = new Size(125, 23);
            btn_Close.TabIndex = 1;
            btn_Close.Text = "Close";
            btn_Close.UseVisualStyleBackColor = true;
            btn_Close.Click += btn_Close_Click;
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.Control;
            textBox1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox1.Location = new Point(117, 45);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(418, 171);
            textBox1.TabIndex = 2;
            textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // About_Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(634, 278);
            Controls.Add(textBox1);
            Controls.Add(btn_Close);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "About_Form";
            Text = "Quick Log About";
            Load += About_Form_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btn_Close;
        private TextBox textBox1;
    }
}