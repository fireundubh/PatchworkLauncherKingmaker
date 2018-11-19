﻿namespace PatchworkLauncher
{
	partial class guiHome
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
			this.guiGameName = new System.Windows.Forms.Label();
			this.guiGameVersion = new System.Windows.Forms.Label();
			this.btnLaunchWithMods = new System.Windows.Forms.Button();
			this.btnLaunchNoMods = new System.Windows.Forms.Button();
			this.btnActiveMods = new System.Windows.Forms.Button();
			this.guiPwVersion = new System.Windows.Forms.Label();
			this.lblVersion = new System.Windows.Forms.Label();
			this.btnSetGamePath = new System.Windows.Forms.Button();
			this.btnTestRun = new System.Windows.Forms.Button();
			this.lblPatchwork = new System.Windows.Forms.LinkLabel();
			this.btnHelp = new System.Windows.Forms.Button();
			this.tbArguments = new System.Windows.Forms.TextBox();
			this.lblArguments = new System.Windows.Forms.Label();
			this.btnClearArguments = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.guiGameIcon = new System.Windows.Forms.PictureBox();
			this.btnPatreon = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.guiGameIcon)).BeginInit();
			this.SuspendLayout();
			// 
			// guiGameName
			// 
			this.guiGameName.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.guiGameName.BackColor = System.Drawing.Color.Transparent;
			this.guiGameName.Font = new System.Drawing.Font("Times New Roman", 38F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.guiGameName.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.guiGameName.Location = new System.Drawing.Point(54, 29);
			this.guiGameName.Name = "guiGameName";
			this.guiGameName.Size = new System.Drawing.Size(422, 45);
			this.guiGameName.TabIndex = 14;
			this.guiGameName.Text = "Pathfinder: Kingmaker";
			// 
			// guiGameVersion
			// 
			this.guiGameVersion.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.guiGameVersion.BackColor = System.Drawing.Color.Transparent;
			this.guiGameVersion.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.guiGameVersion.ForeColor = System.Drawing.SystemColors.ControlLight;
			this.guiGameVersion.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.guiGameVersion.Location = new System.Drawing.Point(490, 66);
			this.guiGameVersion.Margin = new System.Windows.Forms.Padding(0);
			this.guiGameVersion.Name = "guiGameVersion";
			this.guiGameVersion.Size = new System.Drawing.Size(120, 18);
			this.guiGameVersion.TabIndex = 1;
			this.guiGameVersion.Text = "2.0.0.0";
			this.guiGameVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnLaunchWithMods
			// 
			this.btnLaunchWithMods.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnLaunchWithMods.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnLaunchWithMods.BackColor = System.Drawing.SystemColors.Control;
			this.btnLaunchWithMods.FlatAppearance.BorderColor = System.Drawing.Color.Black;
			this.btnLaunchWithMods.FlatAppearance.BorderSize = 0;
			this.btnLaunchWithMods.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Azure;
			this.btnLaunchWithMods.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
			this.btnLaunchWithMods.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnLaunchWithMods.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnLaunchWithMods.Location = new System.Drawing.Point(11, 88);
			this.btnLaunchWithMods.Margin = new System.Windows.Forms.Padding(0);
			this.btnLaunchWithMods.Name = "btnLaunchWithMods";
			this.btnLaunchWithMods.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
			this.btnLaunchWithMods.Size = new System.Drawing.Size(312, 50);
			this.btnLaunchWithMods.TabIndex = 1;
			this.btnLaunchWithMods.Text = "LAUNCH WITH MODS";
			this.btnLaunchWithMods.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnLaunchWithMods.UseVisualStyleBackColor = false;
			this.btnLaunchWithMods.Click += new System.EventHandler(this.btnLaunchWithMods_Click);
			// 
			// btnLaunchNoMods
			// 
			this.btnLaunchNoMods.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnLaunchNoMods.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnLaunchNoMods.BackColor = System.Drawing.SystemColors.Control;
			this.btnLaunchNoMods.FlatAppearance.BorderColor = System.Drawing.Color.Black;
			this.btnLaunchNoMods.FlatAppearance.BorderSize = 0;
			this.btnLaunchNoMods.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Azure;
			this.btnLaunchNoMods.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
			this.btnLaunchNoMods.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnLaunchNoMods.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnLaunchNoMods.Location = new System.Drawing.Point(11, 143);
			this.btnLaunchNoMods.Margin = new System.Windows.Forms.Padding(0);
			this.btnLaunchNoMods.Name = "btnLaunchNoMods";
			this.btnLaunchNoMods.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
			this.btnLaunchNoMods.Size = new System.Drawing.Size(534, 50);
			this.btnLaunchNoMods.TabIndex = 4;
			this.btnLaunchNoMods.Text = "LAUNCH WITHOUT MODS";
			this.btnLaunchNoMods.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnLaunchNoMods.UseVisualStyleBackColor = false;
			this.btnLaunchNoMods.Click += new System.EventHandler(this.btnLaunchNoMods_Click);
			// 
			// btnActiveMods
			// 
			this.btnActiveMods.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnActiveMods.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnActiveMods.BackColor = System.Drawing.SystemColors.Control;
			this.btnActiveMods.FlatAppearance.BorderColor = System.Drawing.Color.Black;
			this.btnActiveMods.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Azure;
			this.btnActiveMods.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
			this.btnActiveMods.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.btnActiveMods.Location = new System.Drawing.Point(490, 227);
			this.btnActiveMods.Margin = new System.Windows.Forms.Padding(0);
			this.btnActiveMods.Name = "btnActiveMods";
			this.btnActiveMods.Size = new System.Drawing.Size(120, 24);
			this.btnActiveMods.TabIndex = 8;
			this.btnActiveMods.Text = "ACTIVE MODS";
			this.btnActiveMods.UseVisualStyleBackColor = false;
			this.btnActiveMods.Click += new System.EventHandler(this.btnActiveMods_Click);
			// 
			// guiPwVersion
			// 
			this.guiPwVersion.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.guiPwVersion.AutoSize = true;
			this.guiPwVersion.BackColor = System.Drawing.Color.Transparent;
			this.guiPwVersion.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.guiPwVersion.ForeColor = System.Drawing.SystemColors.ControlLight;
			this.guiPwVersion.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.guiPwVersion.Location = new System.Drawing.Point(168, 233);
			this.guiPwVersion.Margin = new System.Windows.Forms.Padding(0);
			this.guiPwVersion.Name = "guiPwVersion";
			this.guiPwVersion.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.guiPwVersion.Size = new System.Drawing.Size(25, 14);
			this.guiPwVersion.TabIndex = 13;
			this.guiPwVersion.Text = "???";
			this.guiPwVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblVersion
			// 
			this.lblVersion.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.lblVersion.BackColor = System.Drawing.Color.Transparent;
			this.lblVersion.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.lblVersion.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.lblVersion.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblVersion.Location = new System.Drawing.Point(114, 233);
			this.lblVersion.Margin = new System.Windows.Forms.Padding(0);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblVersion.Size = new System.Drawing.Size(53, 14);
			this.lblVersion.TabIndex = 12;
			this.lblVersion.Text = "Version:";
			this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnSetGamePath
			// 
			this.btnSetGamePath.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnSetGamePath.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnSetGamePath.BackColor = System.Drawing.SystemColors.Control;
			this.btnSetGamePath.FlatAppearance.BorderColor = System.Drawing.Color.Black;
			this.btnSetGamePath.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Azure;
			this.btnSetGamePath.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
			this.btnSetGamePath.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.btnSetGamePath.Location = new System.Drawing.Point(364, 227);
			this.btnSetGamePath.Margin = new System.Windows.Forms.Padding(0);
			this.btnSetGamePath.Name = "btnSetGamePath";
			this.btnSetGamePath.Size = new System.Drawing.Size(120, 24);
			this.btnSetGamePath.TabIndex = 7;
			this.btnSetGamePath.Text = "SET GAME PATH";
			this.btnSetGamePath.UseVisualStyleBackColor = false;
			this.btnSetGamePath.Click += new System.EventHandler(this.btnChangeFolder_Click);
			// 
			// btnTestRun
			// 
			this.btnTestRun.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnTestRun.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnTestRun.BackColor = System.Drawing.SystemColors.Control;
			this.btnTestRun.FlatAppearance.BorderColor = System.Drawing.Color.Black;
			this.btnTestRun.FlatAppearance.BorderSize = 0;
			this.btnTestRun.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Azure;
			this.btnTestRun.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
			this.btnTestRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnTestRun.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnTestRun.Location = new System.Drawing.Point(550, 88);
			this.btnTestRun.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btnTestRun.Name = "btnTestRun";
			this.btnTestRun.Size = new System.Drawing.Size(60, 105);
			this.btnTestRun.TabIndex = 3;
			this.btnTestRun.Text = "TEST RUN";
			this.btnTestRun.UseVisualStyleBackColor = false;
			this.btnTestRun.Click += new System.EventHandler(this.btnTestRun_Click);
			// 
			// lblPatchwork
			// 
			this.lblPatchwork.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.lblPatchwork.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.lblPatchwork.Location = new System.Drawing.Point(12, 233);
			this.lblPatchwork.Margin = new System.Windows.Forms.Padding(0);
			this.lblPatchwork.Name = "lblPatchwork";
			this.lblPatchwork.Size = new System.Drawing.Size(101, 14);
			this.lblPatchwork.TabIndex = 11;
			this.lblPatchwork.TabStop = true;
			this.lblPatchwork.Text = "Patchwork";
			this.lblPatchwork.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblPatchwork.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// btnHelp
			// 
			this.btnHelp.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnHelp.BackColor = System.Drawing.SystemColors.Control;
			this.btnHelp.FlatAppearance.BorderColor = System.Drawing.Color.Black;
			this.btnHelp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Azure;
			this.btnHelp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
			this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnHelp.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnHelp.Location = new System.Drawing.Point(559, 9);
			this.btnHelp.Margin = new System.Windows.Forms.Padding(0);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(24, 24);
			this.btnHelp.TabIndex = 9;
			this.btnHelp.Text = "?";
			this.btnHelp.UseVisualStyleBackColor = false;
			this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
			// 
			// tbArguments
			// 
			this.tbArguments.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.tbArguments.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.tbArguments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbArguments.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::PatchworkLauncher.Properties.Settings.Default, "Arguments", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.tbArguments.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.tbArguments.Location = new System.Drawing.Point(170, 200);
			this.tbArguments.Margin = new System.Windows.Forms.Padding(0);
			this.tbArguments.Name = "tbArguments";
			this.tbArguments.Size = new System.Drawing.Size(376, 21);
			this.tbArguments.TabIndex = 5;
			this.tbArguments.Text = global::PatchworkLauncher.Properties.Settings.Default.Arguments;
			this.tbArguments.Leave += new System.EventHandler(this.tbArguments_Leave);
			// 
			// lblArguments
			// 
			this.lblArguments.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.lblArguments.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.lblArguments.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.lblArguments.Location = new System.Drawing.Point(10, 203);
			this.lblArguments.Name = "lblArguments";
			this.lblArguments.Size = new System.Drawing.Size(157, 14);
			this.lblArguments.TabIndex = 14;
			this.lblArguments.Text = "Command Line Arguments";
			this.lblArguments.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnClearArguments
			// 
			this.btnClearArguments.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnClearArguments.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnClearArguments.BackColor = System.Drawing.SystemColors.ControlLight;
			this.btnClearArguments.FlatAppearance.BorderSize = 0;
			this.btnClearArguments.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Azure;
			this.btnClearArguments.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
			this.btnClearArguments.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.btnClearArguments.Location = new System.Drawing.Point(550, 200);
			this.btnClearArguments.Margin = new System.Windows.Forms.Padding(0);
			this.btnClearArguments.Name = "btnClearArguments";
			this.btnClearArguments.Size = new System.Drawing.Size(60, 21);
			this.btnClearArguments.TabIndex = 6;
			this.btnClearArguments.Text = "CLEAR";
			this.btnClearArguments.UseVisualStyleBackColor = false;
			this.btnClearArguments.Click += new System.EventHandler(this.btnClearArguments_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnClose.BackColor = System.Drawing.SystemColors.Control;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnClose.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnClose.Location = new System.Drawing.Point(586, 9);
			this.btnClose.Margin = new System.Windows.Forms.Padding(0);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(24, 24);
			this.btnClose.TabIndex = 10;
			this.btnClose.Text = "X";
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// guiGameIcon
			// 
			this.guiGameIcon.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.guiGameIcon.InitialImage = null;
			this.guiGameIcon.Location = new System.Drawing.Point(11, 34);
			this.guiGameIcon.Name = "guiGameIcon";
			this.guiGameIcon.Size = new System.Drawing.Size(37, 33);
			this.guiGameIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.guiGameIcon.TabIndex = 8;
			this.guiGameIcon.TabStop = false;
			// 
			// btnPatreon
			// 
			this.btnPatreon.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnPatreon.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnPatreon.FlatAppearance.BorderSize = 0;
			this.btnPatreon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnPatreon.Image = global::PatchworkLauncher.Properties.Resources.patreon;
			this.btnPatreon.Location = new System.Drawing.Point(325, 88);
			this.btnPatreon.Margin = new System.Windows.Forms.Padding(0);
			this.btnPatreon.Name = "btnPatreon";
			this.btnPatreon.Size = new System.Drawing.Size(221, 50);
			this.btnPatreon.TabIndex = 2;
			this.btnPatreon.UseVisualStyleBackColor = false;
			this.btnPatreon.Click += new System.EventHandler(this.btnPatreon_Click);
			// 
			// guiHome
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.Color.Black;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.ClientSize = new System.Drawing.Size(620, 261);
			this.Controls.Add(this.btnPatreon);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnClearArguments);
			this.Controls.Add(this.lblArguments);
			this.Controls.Add(this.tbArguments);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.lblPatchwork);
			this.Controls.Add(this.btnTestRun);
			this.Controls.Add(this.btnSetGamePath);
			this.Controls.Add(this.guiGameIcon);
			this.Controls.Add(this.lblVersion);
			this.Controls.Add(this.guiPwVersion);
			this.Controls.Add(this.btnActiveMods);
			this.Controls.Add(this.btnLaunchNoMods);
			this.Controls.Add(this.btnLaunchWithMods);
			this.Controls.Add(this.guiGameVersion);
			this.Controls.Add(this.guiGameName);
			this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(811, 679);
			this.MinimizeBox = false;
			this.Name = "guiHome";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Patchwork Launcher";
			this.Load += new System.EventHandler(this.guiHome_Load);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.guiHome_MouseDown);
			((System.ComponentModel.ISupportInitialize)(this.guiGameIcon)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label guiGameName;
		private System.Windows.Forms.Label guiGameVersion;
		private System.Windows.Forms.Button btnLaunchWithMods;
		private System.Windows.Forms.Button btnLaunchNoMods;
		private System.Windows.Forms.Button btnActiveMods;
		private System.Windows.Forms.Label guiPwVersion;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.PictureBox guiGameIcon;
		private System.Windows.Forms.Button btnSetGamePath;
		private System.Windows.Forms.Button btnTestRun;
		private System.Windows.Forms.LinkLabel lblPatchwork;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.TextBox tbArguments;
		private System.Windows.Forms.Label lblArguments;
		private System.Windows.Forms.Button btnClearArguments;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnPatreon;
	}
}

