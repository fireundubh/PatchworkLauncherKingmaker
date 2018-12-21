namespace PatchworkLauncher
{
	partial class LogForm
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
			this.guiPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.SuspendLayout();
			// 
			// guiPanel
			// 
			this.guiPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.guiPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.guiPanel.Location = new System.Drawing.Point(0, 0);
			this.guiPanel.Margin = new System.Windows.Forms.Padding(0);
			this.guiPanel.Name = "guiPanel";
			this.guiPanel.Size = new System.Drawing.Size(450, 350);
			this.guiPanel.TabIndex = 0;
			// 
			// LogForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(450, 350);
			this.ControlBox = false;
			this.Controls.Add(this.guiPanel);
			this.DoubleBuffered = true;
			this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LogForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Working...";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.LogForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel guiPanel;
	}
}