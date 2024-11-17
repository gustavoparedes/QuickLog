namespace quickLog
{
    partial class Form_Comment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Comment));
            textBox_Comments = new TextBox();
            btn_SaveComment = new Button();
            groupBox1 = new GroupBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // textBox_Comments
            // 
            textBox_Comments.Location = new Point(30, 22);
            textBox_Comments.Multiline = true;
            textBox_Comments.Name = "textBox_Comments";
            textBox_Comments.Size = new Size(564, 273);
            textBox_Comments.TabIndex = 0;
            // 
            // btn_SaveComment
            // 
            btn_SaveComment.Location = new Point(299, 383);
            btn_SaveComment.Name = "btn_SaveComment";
            btn_SaveComment.Size = new Size(175, 23);
            btn_SaveComment.TabIndex = 1;
            btn_SaveComment.Text = "Save Comment";
            btn_SaveComment.UseVisualStyleBackColor = true;
            btn_SaveComment.Click += btn_SaveComment_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textBox_Comments);
            groupBox1.Location = new Point(91, 67);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(625, 310);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Comments";
            // 
            // Form_Comment
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox1);
            Controls.Add(btn_SaveComment);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_Comment";
            Text = "Quick Log Add Comments";
            Load += Form_Comment_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox textBox_Comments;
        private Button btn_SaveComment;
        private GroupBox groupBox1;
    }
}