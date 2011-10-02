using System.Windows.Forms;

namespace Tarantino.Deployer
{
	public partial class DeploymentOutput : Form
	{
		public DeploymentOutput()
		{
			InitializeComponent();
		}

		public string Output
		{
			get { return rtbOutput.Text; }
			set { rtbOutput.Text = value; }
		}
	}
}