using System;
using System.Windows.Forms;

namespace PatchworkLauncher
{
	public partial class UnlockMessageBox : Form
	{
		#region Constructors and Destructors

		public UnlockMessageBox()
		{
			this.InitializeComponent();
		}

		public UnlockMessageBox(string message)
		{
			this.InitializeComponent();
			this.MessageText = message;
		}

		public UnlockMessageBox(string messageText, string buttonText)
		{
			this.InitializeComponent();
			this.MessageText = messageText;
			this.ButtonText = buttonText;
		}

		#endregion

		#region Public Properties

		public string ButtonText
		{
			get
			{
				return this.btnUnlock.Text;
			}
			set
			{
				this.btnUnlock.Text = value;
			}
		}

		public string MessageText
		{
			get
			{
				return this.lblMessage.Text;
			}
			set
			{
				this.lblMessage.Text = value;
			}

		}

		#endregion

		#region Methods

		private void btnUnlock_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void UnlockMessageBox_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.DialogResult != DialogResult.OK)
			{
				e.Cancel = true;
			}
		}

		#endregion
	}
}
