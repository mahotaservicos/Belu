using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace BServices.Entidades
{
	public class CabecDoc
	{
		public string cdu_estado
		{
			get;
			set;
		}

		public string cdu_nomeFicheiro
		{
			get;
			set;
		}

		public int cdu_prioridade
		{
			get;
			set;
		}

		public bool cdu_retira
		{
			get;
			set;
		}

		public string condPag
		{
			get;
			set;
		}

		public DateTime data
		{
			get;
			set;
		}

		public DateTime dataCarga
		{
			get;
			set;
		}

		public double descEntidade
		{
			get;
			set;
		}

		public double descFinanceiro
		{
			get;
			set;
		}

		public string entidade
		{
			get;
			set;
		}

		public bool fechado
		{
			get;
			set;
		}

		public string filial
		{
			get;
			set;
		}

		public string fluxo
		{
			get;
			set;
		}

		public string grupo
		{
			get;
			set;
		}

		public string idCabec
		{
			get;
			set;
		}

		public List<LinhaDoc> linhasDoc
		{
			get;
			set;
		}

		public string modoPag
		{
			get;
			set;
		}

		public string moradaFac
		{
			get;
			set;
		}

		public string nomeFac
		{
			get;
			set;
		}

		public string nuit
		{
			get;
			set;
		}

		public string nuitFac
		{
			get;
			set;
		}

		public int numDoc
		{
			get;
			set;
		}

		public string observacoes
		{
			get;
			set;
		}

		public string referencia
		{
			get;
			set;
		}

		public string requisicao
		{
			get;
			set;
		}

		public string serie
		{
			get;
			set;
		}

		public string tipodoc
		{
			get;
			set;
		}

		public string tipoEntidade
		{
			get;
			set;
		}

		public string utilizador
		{
			get;
			set;
		}

		public CabecDoc(dynamic campos)
		{
			this.entidade = (string)campos[1].valor;
			this.tipoEntidade = (string)campos[2].valor;
			this.condPag = (string)campos[3].valor;
			this.modoPag = (string)campos[4].valor;
			this.fluxo = (string)campos[5].valor;
			this.descEntidade = (double)campos[6].valor;
			this.descFinanceiro = (double)campos[7].valor;
			//this.dataCarga = (DateTime)typeof(DateTime).ParseExact(campos[8].valor, "dd/MM/yyyy", CultureInfo.InvariantCulture);
			this.requisicao = (string)campos[9].valor;
			this.referencia = (string)campos[10].valor;
			this.observacoes = (string)campos[11].valor;
			this.grupo = (string)campos[12].valor;
			this.utilizador = (string)campos[13].valor;
			this.idCabec = (string)campos[14].valor;
			this.filial = (string)campos[15].valor;
			this.cdu_prioridade = (int)campos[16].valor;
		}

		public CabecDoc()
		{
		}
	}
}