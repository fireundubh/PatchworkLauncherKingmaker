namespace PatchworkLauncher
{
	partial class guiInputGameFolder
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
			this.tbLocation = new System.Windows.Forms.TextBox();
			this.lblSetPath = new System.Windows.Forms.Label();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.btnApply = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblWarningText = new System.Windows.Forms.Label();
			this.btnClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// tbLocation
			// 
			this.tbLocation.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.tbLocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.tbLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
			this.tbLocation.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.tbLocation.Location = new System.Drawing.Point(16, 55);
			this.tbLocation.Margin = new System.Windows.Forms.Padding(0);
			this.tbLocation.Name = "tbLocation";
			this.tbLocation.Size = new System.Drawing.Size(487, 24);
			this.tbLocation.TabIndex = 0;
			// 
			// lblSetPath
			// 
			this.lblSetPath.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lblSetPath.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.lblSetPath.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.lblSetPath.Location = new System.Drawing.Point(14, 34);
			this.lblSetPath.Name = "lblSetPath";
			this.lblSetPath.Size = new System.Drawing.Size(202, 16);
			this.lblSetPath.TabIndex = 1;
			this.lblSetPath.Text = "SET PATH TO GAME FOLDER";
			// 
			// btnBrowse
			// 
			this.btnBrowse.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnBrowse.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnBrowse.BackColor = System.Drawing.SystemColors.Control;
			this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnBrowse.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.btnBrowse.Location = new System.Drawing.Point(511, 51);
			this.btnBrowse.Margin = new System.Windows.Forms.Padding(0);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(75, 32);
			this.btnBrowse.TabIndex = 2;
			this.btnBrowse.Text = "BROWSE...";
			this.btnBrowse.UseVisualStyleBackColor = false;
			this.btnBrowse.Click += new System.EventHandler(this.button1_Click);
			// 
			// btnApply
			// 
			this.btnApply.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnApply.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnApply.BackColor = System.Drawing.SystemColors.Control;
			this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnApply.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnApply.Location = new System.Drawing.Point(511, 89);
			this.btnApply.Margin = new System.Windows.Forms.Padding(0);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(75, 32);
			this.btnApply.TabIndex = 3;
			this.btnApply.Text = "APPLY";
			this.btnApply.UseVisualStyleBackColor = false;
			this.btnApply.Click += new System.EventHandler(this.guiOkay_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCancel.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.btnCancel.Location = new System.Drawing.Point(433, 89);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(72, 32);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "CANCEL";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new System.EventHandler(this.guiCancel_Click);
			// 
			// lblWarningText
			// 
			this.lblWarningText.AutoSize = true;
			this.lblWarningText.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.lblWarningText.ForeColor = System.Drawing.Color.Red;
			this.lblWarningText.Location = new System.Drawing.Point(14, 93);
			this.lblWarningText.Name = "lblWarningText";
			this.lblWarningText.Size = new System.Drawing.Size(89, 16);
			this.lblWarningText.TabIndex = 5;
			this.lblWarningText.Text = "Warning Text";
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.BackColor = System.Drawing.SystemColors.Control;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnClose.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnClose.Location = new System.Drawing.Point(561, 12);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(24, 24);
			this.btnClose.TabIndex = 6;
			this.btnClose.Text = "X";
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// guiInputGameFolder
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(597, 133);
			this.ControlBox = false;
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.lblWarningText);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnApply);
			this.Controls.Add(this.btnBrowse);
			this.Controls.Add(this.lblSetPath);
			this.Controls.Add(this.tbLocation);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "guiInputGameFolder";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Set Game Path";
			this.Load += new System.EventHandler(this.guiInputGameFolder_Load);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.guiInputGameFolder_MouseDown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tbLocation;
		private System.Windows.Forms.Label lblSetPath;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.Button btnApply;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lblWarningText;
		private System.Windows.Forms.Button btnClose;
	}
}