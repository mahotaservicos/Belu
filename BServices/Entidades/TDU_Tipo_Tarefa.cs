using System;
using System.Data;
using System.Runtime.CompilerServices;

namespace BServices.Entidades
{
	public class TDU_Tipo_Tarefa
	{
		public string cdu_descricao
		{
			get;
			set;
		}

		public string cdu_doc
		{
			get;
			set;
		}

		public int cdu_estado
		{
			get;
			set;
		}

		public string cdu_grupo
		{
			get;
			set;
		}

		public string cdu_tarefa_tipo
		{
			get;
			set;
		}

		public string cdu_tipo
		{
			get;
			set;
		}

		public TDU_Tipo_Tarefa()
		{
		}

		public TDU_Tipo_Tarefa(DataRow item)
		{
			try
			{
				this.cdu_tipo = Convert.ToString(item["cdu_tipo"]);
				this.cdu_descricao = Convert.ToString(item["cdu_descricao"]);
				this.cdu_grupo = Convert.ToString(item["cdu_grupo"]);
				this.cdu_doc = Convert.ToString(item["cdu_doc"]);
				this.cdu_tarefa_tipo = Convert.ToString(item["cdu_tarefa_tipo"]);
				this.cdu_estado = Convert.ToInt32(item["cdu_estado"]);
			}
			catch (Exception exception)
			{
				throw exception;
			}
		}
	}
}