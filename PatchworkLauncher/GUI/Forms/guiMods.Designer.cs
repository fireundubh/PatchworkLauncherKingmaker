namespace PatchworkLauncher
{
	partial class guiMods
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
			this.guiInstructionsGridView = new System.Windows.Forms.DataGridView();
			this.guiOn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.guiName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.guiTarget = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.guiRequirements = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.guiPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.btnMoveUp = new System.Windows.Forms.Button();
			this.btnMoveDown = new System.Windows.Forms.Button();
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.guiInstructionsGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// guiInstructionsGridView
			// 
			this.guiInstructionsGridView.AllowUserToAddRows = false;
			this.guiInstructionsGridView.AllowUserToDeleteRows = false;
			this.guiInstructionsGridView.AllowUserToOrderColumns = true;
			this.guiInstructionsGridView.AllowUserToResizeRows = false;
			this.guiInstructionsGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.guiInstructionsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
			this.guiInstructionsGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
			this.guiInstructionsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.guiInstructionsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.guiOn,
            this.guiName,
            this.guiTarget,
            this.guiRequirements,
            this.guiPath});
			this.guiInstructionsGridView.Location = new System.Drawing.Point(12, 12);
			this.guiInstructionsGridView.MultiSelect = false;
			this.guiInstructionsGridView.Name = "guiInstructionsGridView";
			this.guiInstructionsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.guiInstructionsGridView.Size = new System.Drawing.Size(500, 257);
			this.guiInstructionsGridView.TabIndex = 0;
			// 
			// guiOn
			// 
			this.guiOn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.guiOn.DataPropertyName = "IsEnabled";
			this.guiOn.HeaderText = "On";
			this.guiOn.Name = "guiOn";
			this.guiOn.Width = 27;
			// 
			// guiName
			// 
			this.guiName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.guiName.DataPropertyName = "Name";
			this.guiName.HeaderText = "Name";
			this.guiName.Name = "guiName";
			this.guiName.ReadOnly = true;
			this.guiName.Width = 60;
			// 
			// guiTarget
			// 
			this.guiTarget.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.guiTarget.DataPropertyName = "Target";
			this.guiTarget.HeaderText = "Target";
			this.guiTarget.Name = "guiTarget";
			this.guiTarget.ReadOnly = true;
			this.guiTarget.Width = 63;
			// 
			// guiRequirements
			// 
			this.guiRequirements.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.guiRequirements.DataPropertyName = "Requirements";
			this.guiRequirements.HeaderText = "Requirements";
			this.guiRequirements.Name = "guiRequirements";
			this.guiRequirements.ReadOnly = true;
			this.guiRequirements.Width = 97;
			// 
			// guiPath
			// 
			this.guiPath.DataPropertyName = "PatchLocation";
			this.guiPath.HeaderText = "Path";
			this.guiPath.Name = "guiPath";
			this.guiPath.ReadOnly = true;
			this.guiPath.Width = 54;
			// 
			// btnMoveUp
			// 
			this.btnMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMoveUp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnMoveUp.BackColor = System.Drawing.SystemColors.Control;
			this.btnMoveUp.FlatAppearance.BorderColor = System.Drawing.Color.Black;
			this.btnMoveUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Azure;
			this.btnMoveUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
			this.btnMoveUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnMoveUp.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.btnMoveUp.Location = new System.Drawing.Point(523, 64);
			this.btnMoveUp.Name = "btnMoveUp";
			this.btnMoveUp.Size = new System.Drawing.Size(111, 34);
			this.btnMoveUp.TabIndex = 6;
			this.btnMoveUp.Text = "MOVE UP";
			this.btnMoveUp.UseVisualStyleBackColor = false;
			this.btnMoveUp.Click += new System.EventHandler(this.guiMoveUp_Click);
			// 
			// btnMoveDown
			// 
			this.btnMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMoveDown.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnMoveDown.BackColor = System.Drawing.SystemColors.Control;
			this.btnMoveDown.FlatAppearance.BorderColor = System.Drawing.Color.Black;
			this.btnMoveDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Azure;
			this.btnMoveDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
			this.btnMoveDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnMoveDown.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.btnMoveDown.Location = new System.Drawing.Point(523, 104);
			this.btnMoveDown.Name = "btnMoveDown";
			this.btnMoveDown.Size = new System.Drawing.Size(111, 34);
			this.btnMoveDown.TabIndex = 7;
			this.btnMoveDown.Text = "MOVE DOWN";
			this.btnMoveDown.UseVisualStyleBackColor = false;
			this.btnMoveDown.Click += new System.EventHandler(this.guiMoveDown_Click);
			// 
			// btnRemove
			// 
			this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRemove.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnRemove.BackColor = System.Drawing.SystemColors.Control;
			this.btnRemove.FlatAppearance.BorderColor = System.Drawing.Color.Black;
			this.btnRemove.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Azure;
			this.btnRemove.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
			this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnRemove.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.btnRemove.Location = new System.Drawing.Point(523, 235);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(111, 34);
			this.btnRemove.TabIndex = 9;
			this.btnRemove.Text = "REMOVE";
			this.btnRemove.UseVisualStyleBackColor = false;
			this.btnRemove.Click += new System.EventHandler(this.guiRemove_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAdd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnAdd.BackColor = System.Drawing.SystemColors.Control;
			this.btnAdd.FlatAppearance.BorderColor = System.Drawing.Color.Black;
			this.btnAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Azure;
			this.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
			this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAdd.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.btnAdd.Location = new System.Drawing.Point(523, 195);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(111, 34);
			this.btnAdd.TabIndex = 8;
			this.btnAdd.Text = "ADD";
			this.btnAdd.UseVisualStyleBackColor = false;
			this.btnAdd.Click += new System.EventHandler(this.guiAdd_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.BackColor = System.Drawing.SystemColors.Control;
			this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Black;
			this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Azure;
			this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnClose.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.btnClose.Location = new System.Drawing.Point(610, 12);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(24, 24);
			this.btnClose.TabIndex = 11;
			this.btnClose.Text = "X";
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new System.EventHandler(this.guiClose_Click);
			// 
			// guiMods
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.Color.Black;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.ClientSize = new System.Drawing.Size(646, 281);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnRemove);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.btnMoveDown);
			this.Controls.Add(this.btnMoveUp);
			this.Controls.Add(this.guiInstructionsGridView);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "guiMods";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Active Mods";
			this.Load += new System.EventHandler(this.guiMods_Load);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.guiMods_MouseDown);
			((System.ComponentModel.ISupportInitialize)(this.guiInstructionsGridView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView guiInstructionsGridView;
		private System.Windows.Forms.Button btnMoveUp;
		private System.Windows.Forms.Button btnMoveDown;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.DataGridViewCheckBoxColumn guiOn;
		private System.Windows.Forms.DataGridViewTextBoxColumn guiName;
		private System.Windows.Forms.DataGridViewTextBoxColumn guiTarget;
		private System.Windows.Forms.DataGridViewTextBoxColumn guiRequirements;
		private System.Windows.Forms.DataGridViewTextBoxColumn guiPath;
	}
}

