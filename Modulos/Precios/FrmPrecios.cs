using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aspose.Cells;

namespace Reportes.Modulos
{
	public partial class FrmPrecios : Form
	{
		//Historial de precios
		private Workbook historialExcel;
		private Worksheet hojaHistorial;
		private int filaHistorial = 1; // Comenzamos en la fila 1 porque 0 es para encabezados


		private static readonly List<MySqlConnection> mySqlConnections = new List<MySqlConnection>()
		{
			new MySqlConnection(ConfigurationManager.ConnectionStrings["labajadita1"].ToString()),
			new MySqlConnection(ConfigurationManager.ConnectionStrings["labajadita2"].ToString())
		};

		readonly List<MySqlConnection> empresas = mySqlConnections;

		public FrmPrecios()
		{
			InitializeComponent();
		}

		private void BtnSearchProduct_Click(object sender, EventArgs e)
		{
			FrmProducto frm = new FrmProducto(rbJardines.Checked ? "labajadita1" : "labajadita2")
			{
				SendProduct = GetProduct
			};
			frm.ShowDialog();
		}

		private void AplicarFiltros()
		{
			dgListaPrecios.Columns[0].ReadOnly = true;
			dgListaPrecios.Columns[1].ReadOnly = true;
			dgListaPrecios.Columns[2].ReadOnly = true;

			dgListaPrecios.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
			dgListaPrecios.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
			dgListaPrecios.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
		}

		decimal costoUCO = 0;

		public async Task SetProduct(string codigo)
		{
			TxtPrecioNuevo.Text = "";
			MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings[rbJardines.Checked ? "labajadita1" : "labajadita2"].ToString());
			try
			{
				await con.OpenAsync();

				//Con esto recupero el precio base
				MySqlCommand cmd = new MySqlCommand("select tblcatarticulos.cod1_art, des1_art, pre_iva, cos_uco " +
					"from tblcatarticulos " +
					"inner join tblundcospreart on tblcatarticulos.cod1_art=tblundcospreart.cod1_art " +
					"left join tblcodarticuloextra on tblcodarticuloextra.COD_ART=tblcatarticulos.COD1_ART " +
					$"where (tblcatarticulos.cod1_Art='{codigo}' or tblcodarticuloextra.CODART_EXTRA='{codigo}');", con);

				MySqlDataReader result = (MySqlDataReader)await cmd.ExecuteReaderAsync();

				if (result.Read())
				{
					decimal precioVenta = decimal.Parse(result.GetString(2));
					costoUCO = decimal.Parse(result.GetString(3));

					TxtCodigo.Text = result.GetString(0);
					lblDescripcion.Text = result.GetString(1);
					lblPrecio.Text = "$" + precioVenta.ToString();
					lblCosto.Text = "$" + costoUCO.ToString();

					decimal margen = ((precioVenta - costoUCO) / precioVenta) * 100;
					lblMargen.Text = margen.ToString("N2") + "%";
					result.Close();

					//Aqui cargo la lista de precios
					cmd.CommandText = $@"SELECT 
										CASE
											WHEN p.COD_PRECIO = '' THEN CONCAT('General ', p.cod_und)
											ELSE p.COD_PRECIO
										END AS Lista,
										p.PRE_IVA AS Precio,
										ROUND(
											(
												p.PRE_IVA - u.COS_UCO
											) / p.PRE_IVA * 100,
											2
										) AS Margen
									FROM 
										tblprecios p
									INNER JOIN 
										tblundcospreart u ON u.COD1_ART = p.COD1_ART AND u.COD_UND = p.COD_UND
									WHERE 
										p.COD1_ART = '{TxtCodigo.Text}'
									ORDER BY 
										CASE 
											WHEN p.EQV_UND=1 THEN 0
											ELSE 1
										END,
										p.COD_PRECIO;";

					DataTable precios = new DataTable();

					MySqlDataAdapter ad = new MySqlDataAdapter(cmd);

					ad.Fill(precios);

					//Por ultimo, muestro los precios
					dgListaPrecios.DataSource = precios;
					AplicarFiltros();

					TxtPrecioNuevo.Focus();
				}
				else
				{
					dgListaPrecios.DataSource = null;
					lblDescripcion.Text = "Producto no encontrado";
					lblPrecio.Text = "";
					lblCosto.Text = "";
					lblMargen.Text = "";
				}

			}
			catch (MySqlException ex)

			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				await con.CloseAsync();
			}
		}

		private async void GetProduct(string codigo)
		{
			await SetProduct(codigo);
		}

		private async void TxtCodigo_TextChanged(object sender, EventArgs e)
		{
			await Task.Delay(1000);
			await SetProduct(TxtCodigo.Text);
		}

		private async void BtnUpdate_Click(object sender, EventArgs e)
		{
			//Desahibilito los controles
			for (int i = 0; i < Controls.Count; i++)
			{
				Controls[i].Enabled = false;
			}

			if (string.IsNullOrWhiteSpace(TxtPrecioNuevo.Text))
			{
				MessageBox.Show("No dejes el cuadro del precio nuevo vacío si quieres cambiar el precio", "Cuidado",
					MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			try
			{
				foreach (var empresa in empresas)
				{
					using (var connection = empresa)
					{
						await connection.OpenAsync();

						using (var transaction = connection.BeginTransaction())
						{
							try
							{
								var cmd = connection.CreateCommand();
								cmd.Transaction = transaction;

								string codigo = TxtCodigo.Text;

								cmd.CommandText = $"select cod_art from tblcodarticuloextra where CODART_EXTRA='{codigo}'";

								var obj = await cmd.ExecuteScalarAsync();

								if (obj?.ToString() != null)
									codigo = obj.ToString();

								// Obtener el impuesto
								cmd.CommandText = "SELECT por_imp " +
												  "FROM tblimpuestos imp " +
												  "INNER JOIN tblcatarticulos art ON art.COD_IMP = imp.COD_IMP " +
												  $"WHERE COD1_ART = '{codigo}';";

								var impuestoResult = await cmd.ExecuteScalarAsync() ?? throw new Exception($"No existe el código {TxtCodigo.Text} en la base de datos de {connection.DataSource}");
								double imp = double.Parse(impuestoResult.ToString());

								// Actualizar los precios
								double precioBase = double.Parse(TxtPrecioNuevo.Text) / (1 + (imp / 100));

								cmd.CommandText = $"UPDATE tblundcospreart " +
												  $"SET PRE_UND = {precioBase}, PRE_IVA = {TxtPrecioNuevo.Text} " +
												  $"WHERE COD1_ART = '{codigo}' and eqv_und=1; " +
												  $"UPDATE tblprecios set pre_art={precioBase}, pre_iva={TxtPrecioNuevo.Text} " +
												  $"WHERE cod_lista=1 and cod1_art='{codigo}' and eqv_und=1";
								/*+
								$"UPDATE tblprecios " +
								$"SET PRE_ART = {precioBase}, PRE_IVA = {TxtPrecioNuevo.Text} " +
								$"WHERE COD1_ART = '{TxtCodigo.Text}' AND COD_LISTA = 1;";
								*/

								int principal = await cmd.ExecuteNonQueryAsync();

								if (principal == 0)
								{
									throw new Exception($"No se realizaron cambios para el código {TxtCodigo.Text}.");
								}

								int result = 0;

								//ahora actualizo las listas de precios
								for (int i = 1; i < dgListaPrecios.Rows.Count; i++)
								{
									precioBase = double.Parse(dgListaPrecios.Rows[i].Cells[3].Value.ToString()) / (1 + (imp / 100));

									if (dgListaPrecios.Rows[i].Cells[0].Value.ToString().ToUpper().Contains("GENERAL"))
									{
										string unidad = dgListaPrecios.Rows[i].Cells[0].Value.ToString().Split(' ')[1];

										cmd.CommandText = $"UPDATE tblundcospreart " +
												  $"SET PRE_UND = {precioBase}, PRE_IVA = {precioBase * (1 + (imp / 100))} " +
												  $"WHERE COD1_ART = '{codigo}' and cod_und='{unidad}'; " +
												  $"UPDATE tblprecios set pre_art={precioBase}, pre_iva={precioBase * (1 + (imp / 100))} " +
												  $"WHERE cod_lista=1 and cod1_art='{codigo}' and cod_und='{unidad}'";
										await cmd.ExecuteNonQueryAsync();
										continue;
									}

									cmd.CommandText = $"UPDATE tblprecios set pre_art={precioBase}," +
										$"pre_iva={dgListaPrecios.Rows[i].Cells[3].Value} where cod1_art='{codigo}' and cod_precio='{dgListaPrecios.Rows[i].Cells[0].Value}';";
									result = await cmd.ExecuteNonQueryAsync();
								}

								// Confirmar transacción
								await transaction.CommitAsync();

								if (principal >= 2)
								{
									// Registrar en Excel
									hojaHistorial.Cells[filaHistorial, 0].PutValue(codigo);
									hojaHistorial.Cells[filaHistorial, 1].PutValue(lblDescripcion.Text);
									hojaHistorial.Cells[filaHistorial, 2].PutValue(lblPrecio.Text.Replace("$", ""));
									hojaHistorial.Cells[filaHistorial, 3].PutValue(TxtPrecioNuevo.Text);
									hojaHistorial.Cells[filaHistorial, 4].PutValue(empresa.DataSource.ToString());
									filaHistorial++;
								}
							}
							catch (Exception ex)
							{
								// Revertir transacción en caso de error
								await transaction.RollbackAsync();
								MessageBox.Show($"Error al actualizar la base de datos: {ex.Message}", "Error",
									MessageBoxButtons.OK, MessageBoxIcon.Error);

								for (int i = 0; i < Controls.Count; i++)
								{
									Controls[i].Enabled = true;
								}

								return;
							}
						}
					}
				}

				// Habilitar los controles, y luego mostrar mensajes
				for (int i = 0; i < Controls.Count; i++)
				{
					Controls[i].Enabled = true;
				}

				await MostrarMensaje();
				await SetProduct(TxtCodigo.Text);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error general: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private async Task MostrarMensaje()
		{
			lblPrecioActualizado.Text = "Precio Actualizado!";
			lblPrecioActualizado.ForeColor = ColorTranslator.FromHtml("#2e0d23");
			await Task.Delay(3000);
			lblPrecioActualizado.ForeColor = Color.Black;
			lblPrecioActualizado.Text = "";
		}

		private void TxtPrecioNuevo_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != (char)Keys.Back)
			{
				e.Handled = true;
			}

			if (e.KeyChar == '.' && TxtPrecioNuevo.Text.Contains("."))
			{
				e.Handled = true;
			}
		}

		private async void TxtPrecioNuevo_TextChanged(object sender, EventArgs e)
		{
			MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings[rbJardines.Checked ? "labajadita1" : "labajadita2"].ConnectionString);

			if (dgListaPrecios.Columns.Count > 3)
			{
				dgListaPrecios.Columns.RemoveAt(3);
				dgListaPrecios.Columns.RemoveAt(3);
			}

			if (TxtPrecioNuevo.Text == "")
			{
				return;
			}

			if (dgListaPrecios.DataSource != null)
			{
				dgListaPrecios.Columns.Add("Column4", "Nuevo Precio");
				dgListaPrecios.Columns.Add("Column5", "Nuevo Margen");
			}

			decimal precio = decimal.Parse(TxtPrecioNuevo.Text);

			try
			{
				await con.OpenAsync();

				MySqlCommand cmd = con.CreateCommand();

				for (int i = 0; i < dgListaPrecios.Rows.Count; i++)
				{
					cmd.CommandText = "select des_precio " +
						"from tblprecios pre " +
						"inner join tblcatprecios listas on listas.COD_PRECIO=pre.COD_PRECIO " +
						$"where pre.cod1_art='{TxtCodigo.Text}' and pre.cod_precio='{dgListaPrecios.Rows[i].Cells[0].Value}';";

					var porcentaje = await cmd.ExecuteScalarAsync();

					if (decimal.TryParse(porcentaje == null ? "1" : porcentaje.ToString(), out decimal porc))
					{
						decimal precioNuevo = precio * porc;
						if (precio - precioNuevo >= 1)
						{
							precioNuevo = precio - 1;
						}

						decimal margenNuevo = (precioNuevo - costoUCO) / precioNuevo;

						dgListaPrecios.Rows[i].Cells[3].Value = precioNuevo;
						dgListaPrecios.Rows[i].Cells[4].Value = (margenNuevo * 100).ToString("N2");
					}
					else
					{
						decimal precioNuevo = decimal.Parse(TxtPrecioNuevo.Text);
						decimal margen = ((precioNuevo - costoUCO) / precioNuevo) * 100;

						dgListaPrecios.Rows[i].Cells[3].Value = precioNuevo;
						dgListaPrecios.Rows[i].Cells[4].Value = (margen).ToString("N2");
					}
				}
			}
			catch (ArgumentOutOfRangeException) { }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				await con.CloseAsync();
			}
		}

		private void TxtCodigo_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\'')
			{
				e.Handled = true;
			}
		}

		private void FrmPrincipal_Load(object sender, EventArgs e)
		{
			historialExcel = new Workbook();
			hojaHistorial = historialExcel.Worksheets[0];
			hojaHistorial.Name = "HistorialPrecios";

			// Encabezados
			hojaHistorial.Cells[0, 0].PutValue("Código");
			hojaHistorial.Cells[0, 1].PutValue("Descripción");
			hojaHistorial.Cells[0, 2].PutValue("Precio Anterior");
			hojaHistorial.Cells[0, 3].PutValue("Precio Nuevo");
			var columnStyle = hojaHistorial.Cells.Columns[2].GetStyle();
			columnStyle.Custom = "0.00";
			hojaHistorial.Cells.Columns[2].SetStyle(columnStyle);
			hojaHistorial.Cells.Columns[3].SetStyle(columnStyle);
		}

		object valorCeldaOriginal;

		private void ListaPrecios_EditadaCelda(object sender, DataGridViewCellEventArgs e)
		{
			// Obtener el precio base
			decimal PrecioBase = decimal.Parse(lblPrecio.Text.Substring(1));

			// Obtener el valor de la segunda columna en la fila editada
			object lista = dgListaPrecios.Rows[e.RowIndex].Cells[0].Value;
			object valorCelda = dgListaPrecios.Rows[e.RowIndex].Cells[3].Value;
			if (valorCelda != null && decimal.TryParse(valorCelda.ToString(), out decimal precioSeleccionado))
			{
				if (lista.ToString() == "ABARREY" || lista.ToString() == "General" || lista.ToString() == "ABARREY2")
					return;
				if (PrecioBase - precioSeleccionado >= 1)
				{
					MessageBox.Show("El precio introducido supera el limite de descuento", "LA BAJADITA - No se puede asignar ese precio",
						MessageBoxButtons.OK, MessageBoxIcon.Warning);
					dgListaPrecios.Rows[e.RowIndex].Cells[3].Value = valorCeldaOriginal.ToString();
					return;
				}
			}
			else
			{
				MessageBox.Show("El valor de la celda no es válido.");
			}
		}

		private void ListaPrecios_GuardarValor(object sender, DataGridViewCellCancelEventArgs e)
		{
			try
			{
				valorCeldaOriginal = dgListaPrecios.Rows[e.RowIndex].Cells[3].Value;
			}
			catch (ArgumentOutOfRangeException) { }
		}

		private void FrmPrecios_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (filaHistorial > 1) // Si hay cambios
			{
				string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "HistorialPrecios.xlsx");
				historialExcel.Save(path);

				// Abrir archivo con el sistema operativo
				try
				{
					System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
					{
						FileName = path,
						UseShellExecute = true // Para que lo abra con Excel o la app predeterminada
					});
				}
				catch (Exception ex)
				{
					MessageBox.Show("No se pudo abrir el archivo de historial: " + ex.Message);
				}
			}
		}
	}
}
