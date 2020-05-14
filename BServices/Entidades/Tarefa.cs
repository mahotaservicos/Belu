using System;
using System.Data;
using System.Data.SqlClient;

namespace BServices.Entidades
{
	public class Tarefa
	{
		public int id;

		public string CDU_DocID;

		public string CDU_LinhaDocId;

		public string CDU_Grupo;

		public string CDU_OBS;

		public string CDU_tipo;

		public string CDU_estado;

		private SqlConnection con = new SqlConnection();

		private SqlCommand cmd = new SqlCommand();

		private SqlDataReader data;

		private SqlDataAdapter adapter = new SqlDataAdapter();

		private SqlParameter parameter = new SqlParameter();

		private string strConn;

		public Tarefa(string strConn)
		{
			this.con = new SqlConnection(strConn);
		}

		public void createTasks(string CDU_DocID, string CDU_LinhaDocId, string CDU_Grupo, string CDU_OBS, string cdu_tipo, int cdu_estado = 2)
		{
			try
			{
				CDU_DocID = CDU_DocID.Replace("{", "").Replace("}", "");
				if (!this.existeTasks(CDU_DocID, CDU_LinhaDocId, CDU_Grupo, cdu_tipo))
				{
					string strSql = "INSERT INTO [dbo].[TDU_Tarefas]([CDU_Id],[CDU_DocID],[CDU_LinhaDocId], CDU_Grupo,[CDU_Obs] ,[CDU_Tipo],cdu_status) VALUES ";
					strSql = string.Concat(new string[] { strSql, "(NEWID(),'", CDU_DocID, "','", CDU_LinhaDocId, "','", CDU_Grupo, "','", CDU_OBS, "','", cdu_tipo, "', ", cdu_estado.ToString(), ")" });
					this.cmd.CommandType = CommandType.Text;
					this.cmd.CommandText = strSql;
					this.cmd.Connection = this.con;
					this.con.Open();
					this.cmd.ExecuteNonQuery();
					this.con.Close();
				}
				else
				{
					this.updateTasks(CDU_DocID, CDU_LinhaDocId, CDU_Grupo, CDU_OBS, cdu_tipo);
				}
			}
			catch (Exception exception)
			{
				throw exception;
			}
		}

		public bool deleteTasks(string CDU_DocID)
		{
			string strSql = string.Concat("delete TDU_Tarefas ", "where CDU_DocID='", CDU_DocID, "'");
			this.cmd.CommandType = CommandType.Text;
			this.cmd.CommandText = strSql;
			this.cmd.Connection = this.con;
			this.con.Open();
			this.cmd.ExecuteNonQuery();
			this.con.Close();
			return true;
		}

		public bool existeTasks(string CDU_DocID, string CDU_LinhaDocId, string CDU_Grupo, string cdu_tipo)
		{
			bool tem = false;
			string strSql = "select * from TDU_Tarefas  WITH (NOLOCK) ";
			strSql = string.Concat(new string[] { strSql, "where CDU_DocID='", CDU_DocID, "' and CDU_LinhaDocId='", CDU_LinhaDocId, "' and CDU_Grupo='", CDU_Grupo, "' and " });
			strSql = string.Concat(strSql, "CDU_Tipo='", cdu_tipo, "'");
			this.cmd.CommandType = CommandType.Text;
			this.cmd.CommandText = strSql;
			this.cmd.Connection = this.con;
			this.con.Open();
			this.con.Close();
			return tem;
		}

		public void updateTasks(string CDU_DocID, string CDU_LinhaDocId, string CDU_Grupo, string CDU_OBS, string cdu_tipo)
		{
			string strSql = string.Concat("update TDU_Tarefas set ", "CDU_DocID='", CDU_DocID, "',");
			strSql = string.Concat(strSql, "CDU_LinhaDocId='", CDU_LinhaDocId, "',");
			strSql = string.Concat(strSql, "CDU_Grupo='", CDU_Grupo, "',");
			strSql = string.Concat(strSql, "CDU_OBS='", CDU_OBS, "',");
			strSql = string.Concat(strSql, "cdu_tipo='", cdu_tipo, "', ");
			strSql = string.Concat(strSql, "cdu_status=2 ");
			strSql = string.Concat(new string[] { strSql, "where CDU_DocID='", CDU_DocID, "' and CDU_LinhaDocId='", CDU_LinhaDocId, "' and CDU_Grupo='", CDU_Grupo, "' and " });
			strSql = string.Concat(strSql, "CDU_Tipo='", cdu_tipo, "'");
			this.cmd.CommandType = CommandType.Text;
			this.cmd.CommandText = strSql;
			this.cmd.Connection = this.con;
			this.con.Open();
			this.cmd.ExecuteNonQuery();
			this.con.Close();
		}
	}
}