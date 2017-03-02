namespace HanCaret
{
    partial class HanCaretForm
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
            this.m_label = new System.Windows.Forms.Label();
            this.m_timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // m_label
            // 
            this.m_label.BackColor = System.Drawing.Color.Transparent;
            this.m_label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_label.Font = new System.Drawing.Font("Dotum", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.m_label.Location = new System.Drawing.Point(0, 0);
            this.m_label.Margin = new System.Windows.Forms.Padding(0);
            this.m_label.Name = "m_label";
            this.m_label.Size = new System.Drawing.Size(25, 25);
            this.m_label.TabIndex = 0;
            // 
            // HanCaretForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(25, 25);
            this.ControlBox = false;
            this.Controls.Add(this.m_label);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HanCaretForm";
            this.ShowInTaskbar = false;
            this.Text = "Form1";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.HanCaretForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label m_label;
        private System.Windows.Forms.Timer m_timer;
    }
}

