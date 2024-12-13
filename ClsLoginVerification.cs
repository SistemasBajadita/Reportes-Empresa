using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reportes
{
	public class ClsLoginVerification
	{
		public ClsConnection con;

		public ClsLoginVerification(string connectionString)
		{
			con = new ClsConnection(connectionString);
		}

		public static string Encriptar(string password)
		{
			SHA256 sha256 = SHA256Managed.Create();
			ASCIIEncoding encoding = new ASCIIEncoding();
			StringBuilder sb = new StringBuilder();
			byte[] stream = sha256.ComputeHash(encoding.GetBytes(password));
			for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
			return sb.ToString();
		}

		public bool VerificarLogin(string userId, string password)
		{
			DataTable result = con.GetQuery($"select userid from users where userid='{userId}' and password='{password}'");
			if (result.Rows.Count > 0)
				return true;
			else
				return false;
		}

		public  void CambiarPassword(string userId, string password)
		{
			con.GetQuery($"update users set password='{ClsLoginVerification.Encriptar(password)}' where userid={userId};");
		}

		public void GuardarConfig(string userid, string modulo, string habilitado)
		{
			con.GetQuery($"update users_roles set {modulo}={habilitado} where userid={userid};");
		}

		public string CrearUsuario(string name, string password)
		{
			MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["log"].ConnectionString);
			MySqlCommand cmd = con.CreateCommand();
			cmd.CommandText = $"insert into users (name, password, active) values ('{name}', '{ClsLoginVerification.Encriptar(password)}',1)";

			string id = "";

			try
			{
				con.Open();
				cmd.ExecuteNonQuery();
				id = cmd.LastInsertedId.ToString();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "La Bajadita - Reportes", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				con.Close();
			}

			return id;
		}

		public void DeshabilitarHabilitarUsuario(string id, string valor)
		{
			MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["log"].ConnectionString);
			try
			{
				con.Open();
				MySqlCommand cmd = con.CreateCommand();
				cmd.CommandText = $"update users set active={valor} where userid={id}";
				cmd.ExecuteNonQuery();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				con.Close();
			}
		}

		public void CrearRoles(string id)
		{
			MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["log"].ConnectionString);
			MySqlCommand cmd = con.CreateCommand();
			cmd.CommandText = $"insert into users_roles values ({id}, 0,0,0,0,0,0,0,0,0,0,0,0,0,0)";

			try
			{
				con.Open();
				cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "La Bajadita - Reportes", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				con.Close();
			}
		}
	}
}
