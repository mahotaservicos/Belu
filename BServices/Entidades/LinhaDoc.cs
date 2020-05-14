using BServices.Helpers;
using System;
using System.Data;
using System.Runtime.CompilerServices;

namespace BServices.Entidades
{
	public class LinhaDoc
	{
		public double precunit;

		public string id;

		public float desconto1;

		public string armazem
		{
			get;
			set;
		}

		public string artigo
		{
			get;
			set;
		}

		public bool CDU_CampoEspecial
		{
			get;
			set;
		}

		public string CDU_CampoEspecialDescricao
		{
			get;
			set;
		}

		public DateTime dataSaida
		{
			get;
			set;
		}

		public double factorConv
		{
			get;
			set;
		}

		public bool Fechado
		{
			get;
			set;
		}

		public string lote
		{
			get;
			set;
		}

		public int numLinhaOrigem
		{
			get;
			set;
		}

		public double precoNegociado
		{
			get;
			set;
		}

		public double precoOrg
		{
			get;
			set;
		}

		public double quantidade
		{
			get;
			set;
		}

		public string unidade
		{
			get;
			set;
		}

		public LinhaDoc()
		{
		}

		public LinhaDoc(DataRow dr)
		{
			this.numLinhaOrigem = StringHelper.DaInt32(dr["NumLinha"]);
			this.factorConv = StringHelper.DaDouble(dr["FactorConv"]);
			this.quantidade = StringHelper.DaDouble(dr["Quantidade"]);
			this.artigo = StringHelper.DaString(dr["Artigo"]);
			this.armazem = StringHelper.DaString(dr["Armazem"]);
			this.lote = StringHelper.DaString(dr["Lote"]);
			this.unidade = StringHelper.DaString(dr["Unidade"]);
			this.precoOrg = StringHelper.DaDouble(dr["CDU_PrecOrig"]);
			this.precoNegociado = StringHelper.DaDouble(dr["cdu_precoNegociado"]);
			this.CDU_CampoEspecial = Convert.ToBoolean(dr["CDU_CampoEspecial"]);
			this.CDU_CampoEspecialDescricao = StringHelper.DaString(dr["CDU_CampoEspecialDescricao"]);
			this.Fechado = Convert.ToBoolean(dr["Fechado"]);
			this.precunit = StringHelper.DaDouble(dr["precunit"]);
			this.id = StringHelper.DaString(dr["id"]);
			this.desconto1 = (float)StringHelper.DaDouble(dr["Desconto1"]);
			this.dataSaida = Convert.ToDateTime(dr["DataSaida"]);
		}
	}
}