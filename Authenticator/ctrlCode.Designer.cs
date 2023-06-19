namespace Authenticator
{
    partial class ctrlCode
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
            components = new System.ComponentModel.Container();
            progressBar1 = new ProgressBar();
            textBox1 = new TextBox();
            tmrRefresh = new System.Windows.Forms.Timer(components);
            lblUserName = new Label();
            SuspendLayout();
            // 
            // progressBar1
            // 
            progressBar1.Dock = DockStyle.Top;
            progressBar1.Location = new Point(0, 38);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(258, 10);
            progressBar1.TabIndex = 4;
            // 
            // textBox1
            // 
            textBox1.Dock = DockStyle.Top;
            textBox1.Location = new Point(0, 15);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(258, 23);
            textBox1.TabIndex = 3;
            // 
            // tmrRefresh
            // 
            tmrRefresh.Tick += tmrRefresh_Tick;
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Dock = DockStyle.Top;
            lblUserName.Location = new Point(0, 0);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(63, 15);
            lblUserName.TabIndex = 5;
            lblUserName.Text = "User name";
            // 
            // ctrlCode
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(progressBar1);
            Controls.Add(textBox1);
            Controls.Add(lblUserName);
            Name = "ctrlCode";
            Size = new Size(258, 58);
            Load += ctrlCode_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ProgressBar progressBar1;
        private TextBox textBox1;
        private System.Windows.Forms.Timer tmrRefresh;
        private Label lblUserName;
    }
}
