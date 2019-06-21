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
		public Form1()
		{
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
	}
}
