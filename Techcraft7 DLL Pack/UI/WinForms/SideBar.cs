using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Techcraft7_DLL_Pack.UI.WinForms
{
	public partial class SideBar : UserControl
	{
		public SideBar()
		{
			InitializeComponent();
		}

		private void CloseButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		public void AddControl(Control c)
		{
			flowLayoutPanel1.Controls.Add(c);
			flowLayoutPanel1.Update();
			Update();
		}

		public void RemoveControl(Control c)
		{
			flowLayoutPanel1.Controls.Remove(c);
			flowLayoutPanel1.Update();
			Update();
		}

		public void Close()
		{
			Visible = false;
			Enabled = false;
			SendToBack();
		}

		public void OpenSideBar()
		{
			Visible = true;
			Enabled = true;
			BringToFront();
		}

		private void SideBar_VisibleChanged(object sender, EventArgs e)
		{
			foreach (Control c in flowLayoutPanel1.Controls)
			{
				c.Size = new Size(Size.Width, c.Size.Height);
				flowLayoutPanel1.Update();
				Update();
			}
		}
	}
}
