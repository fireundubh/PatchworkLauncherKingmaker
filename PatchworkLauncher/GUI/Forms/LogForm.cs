using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Patchwork.Utility;
using Patchwork.Utility.Binding;
using PatchworkLauncher.Extensions;

namespace PatchworkLauncher
{
	public partial class LogForm : Form
	{
		#region Constructors and Destructors

		public LogForm(ProgressObject list)
		{
			this.InitializeComponent();

			this.List = Bindable.List(new ProgressList(list));
		}

		#endregion

		#region Public Properties

		public IBindable<IList<ProgressObject>> List { get; }

		#endregion

		#region Methods

		private Control BuildProgressBar(ProgressObject progressObject)
		{
			Label topLabel = this.CreateTopLabel(progressObject);

			Label bottomLabel = this.CreateBottomLabel(progressObject);

			ProgressBar progressBar = this.CreateProgressBar();

			FlowLayoutPanel flowLayoutPanel = this.CreateFlowLayoutPanel();

			progressObject.TaskTitle.Binding = GuiBindings.Bind(topLabel, x => x.Text).ToBinding(BindingMode.FromTarget);
			progressObject.TaskText.Binding = GuiBindings.Bind(bottomLabel, x => x.Text).ToBinding(BindingMode.FromTarget);
			progressObject.Total.Binding = GuiBindings.Bind(progressBar, x => x.Maximum).ToBinding(BindingMode.FromTarget);
			progressObject.Current.Binding = GuiBindings.Bind(progressBar, x => x.Value).ToBinding(BindingMode.FromTarget);

			progressBar.Maximum = progressObject.Total.Value;
			progressBar.Value = progressObject.Current.Value;

			flowLayoutPanel.Controls.AddRange(new[]
			{
				(Control) topLabel,
				bottomLabel,
				progressBar
			});

			return flowLayoutPanel;
		}

		private FlowLayoutPanel CreateFlowLayoutPanel()
		{
			var flowLayoutPanel = new FlowLayoutPanel();
			flowLayoutPanel.FlowDirection = FlowDirection.TopDown;
			flowLayoutPanel.Margin = new Padding(10, 5, 10, 5);
			flowLayoutPanel.Width = this.guiPanel.Width - 30;
			flowLayoutPanel.BorderStyle = BorderStyle.None;
			return flowLayoutPanel;
		}

		private Label CreateBottomLabel(ProgressObject progressObject)
		{
			var bottomLabel = new Label();
			bottomLabel.Width = this.guiPanel.Width - 30;
			bottomLabel.AutoSize = true;
			bottomLabel.TextChanged += (sender, args) => progressObject.TaskText.Value = bottomLabel.Text;
			return bottomLabel;
		}

		private Label CreateTopLabel(ProgressObject progressObject)
		{
			var topLabel = new Label();
			topLabel.Margin = new Padding(0, 5, 0, 5);
			topLabel.Font = new Font(this.Font.FontFamily, 12f, FontStyle.Bold);
			topLabel.Width = this.guiPanel.Width - 30;
			topLabel.AutoSize = true;
			topLabel.TextChanged += (sender, args) => progressObject.TaskTitle.Value = topLabel.Text;
			return topLabel;
		}

		private ProgressBar CreateProgressBar()
		{
			var progressBar = new ProgressBar();
			progressBar.Margin = new Padding(5, 5, 0, 0);
			progressBar.Width = this.guiPanel.Width - 30;
			progressBar.AutoSize = true;
			progressBar.Style = ProgressBarStyle.Continuous;
			return progressBar;
		}

		private void LogForm_Load(object sender, EventArgs e)
		{
			this.guiPanel.FlowDirection = FlowDirection.TopDown;
			this.guiPanel.Controls.Clear();

			Func<ProgressObject, Control> progressTemplate = this.BuildProgressBar;

			this.List.Binding = this.guiPanel.Controls.CastList().ProjectList(progressTemplate).ToBindable().WithDispatcher(this.DispatchAction).ToBinding(BindingMode.FromTarget);
		}

		private void DispatchAction(Action act)
		{
			this.InvokeIfRequired(() => act());
		}

		#endregion
	}
}
