using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reportes.Modulos
{
	public partial class ProgressProductsCocina : Form
	{
		public ProgressProductsCocina()
		{
			InitializeComponent();
		}

		public void UpdateProgress(int value)
		{
			if (Progreso.InvokeRequired)
			{
				Progreso.Invoke(new Action(() => Progreso.Value = value));
			}
			else
			{
				Progreso.Value = value;
			}
		}

		public void SetMessage(string text)
		{
			Invoke(new Action(() =>
			{
				label2.Text=text;
			}));
		}
	}
}
