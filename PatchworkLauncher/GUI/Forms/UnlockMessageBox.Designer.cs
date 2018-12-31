namespace PatchworkLauncher
{
	partial class UnlockMessageBox
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
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
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
			this.tblLayout = new System.Windows.Forms.TableLayoutPanel();
			this.btnUnlock = new System.Windows.Forms.Button();
			this.lblMessage = new System.Windows.Forms.Label();
			this.tblLayout.SuspendLayout();
			this.SuspendLayout();
			//
			// tblLayout
			//
			this.tblLayout.ColumnCount = 1;
			this.tblLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tblLayout.Controls.Add(this.btnUnlock, 0, 1);
			this.tblLayout.Controls.Add(this.lblMessage, 0, 0);
			this.tblLayout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tblLayout.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
			this.tblLayout.Location = new System.Drawing.Point(0, 0);
			this.tblLayout.Margin = new System.Windows.Forms.Padding(0);
			this.tblLayout.Name = "tblLayout";
			this.tblLayout.RowCount = 2;
			this.tblLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tblLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tblLayout.Size = new System.Drawing.Size(288, 145);
			this.tblLayout.TabIndex = 0;
			//
			// btnUnlock
			//
			this.btnUnlock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnUnlock.BackColor = System.Drawing.SystemColors.ControlLight;
			this.btnUnlock.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnUnlock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnUnlock.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.btnUnlock.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnUnlock.Location = new System.Drawing.Point(10, 110);
			this.btnUnlock.Margin = new System.Windows.Forms.Padding(10);
			this.btnUnlock.Name = "btnUnlock";
			this.btnUnlock.Size = new System.Drawing.Size(268, 25);
			this.btnUnlock.TabIndex = 0;
			this.btnUnlock.Text = "UNLOCK";
			this.btnUnlock.UseVisualStyleBackColor = false;
			this.btnUnlock.Click += new System.EventHandler(this.btnUnlock_Click);
			//
			// lblMessage
			//
			this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblMessage.AutoSize = true;
			this.lblMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblMessage.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.lblMessage.Location = new System.Drawing.Point(3, 0);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Padding = new System.Windows.Forms.Padding(10);
			this.lblMessage.Size = new System.Drawing.Size(282, 100);
			this.lblMessage.TabIndex = 1;
			this.lblMessage.Text = "Body Text";
			this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			//
			// UnlockMessageBox
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(288, 145);
			this.ControlBox = false;
			this.Controls.Add(this.tblLayout);
			this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UnlockMessageBox";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Running Processes";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UnlockMessageBox_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UnlockMessageBox_FormClosed);
			this.tblLayout.ResumeLayout(false);
			this.tblLayout.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tblLayout;
		private System.Windows.Forms.Button btnUnlock;
		private System.Windows.Forms.Label lblMessage;
	}
}