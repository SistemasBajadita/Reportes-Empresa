﻿using System;
using System.Windows.Forms;

namespace Reportes
{
	internal static class Program
	{
		/// <summary>
		/// Punto de entrada principal para la aplicación.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new FrmLogin());
		}

		//Parametro para saber que empresa se escogio. Como es global, se usara en todas las formas
		public static int Empresa;
	}
}
