using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T7DLLPack_tests
{
	public partial class Form1 : Form
	{
		private readonly bool close = false;
		public Form1(bool autoclose)
		{
			close = autoclose;
			InitializeComponent();
			sideBar1.Close();
			sideBar1.AddControl(label1);
			sideBar1.AddControl(button2);
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			sideBar1.OpenSideBar();
		}

		private void Button2_Click(object sender, EventArgs e)
		{
			MessageBox.Show("HELLO!");
		}

		private void Form1_Shown(object sender, EventArgs e)
		{
			if (close)
			{
				Close();
			}
		}
	}
}
