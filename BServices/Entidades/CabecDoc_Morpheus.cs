using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BServices.Entidades
{
	public class CabecDoc_Morpheus
	{
		public string CUSTOMER_ADDRESS
		{
			get;
			set;
		}

		public string CUSTOMER_NAME
		{
			get;
			set;
		}

		public string Entity_code
		{
			get;
			set;
		}

		public string fileFullName
		{
			get;
			set;
		}

		public string fileName
		{
			get;
			set;
		}

		public List<LinhasDocMorpheus> linhasDoc
		{
			get;
			set;
		}

		public string NUIT
		{
			get;
			set;
		}

		public DateTime Record_date
		{
			get;
			set;
		}

		public string Record_id
		{
			get;
			set;
		}

		public string User_code
		{
			get;
			set;
		}

		public CabecDoc_Morpheus()
		{
			this.linhasDoc = new List<LinhasDocMorpheus>();
		}

		public CabecDoc_Morpheus(string linha)
		{
			string[] itens = linha.Split(new char[] { ';' });
			this.linhasDoc = new List<LinhasDocMorpheus>();
			try
			{
				string codigoCliente = itens[15];
				this.Record_id = itens[0];
				this.Entity_code = itens[1];
				if (codigoCliente.Length <= 3)
				{
					this.User_code = itens[1];
				}
				else
				{
					this.User_code = (codigoCliente.Substring(0, 4).ToUpper() == "CVAN" ? codigoCliente : itens[1]);
				}
				this.CUSTOMER_NAME = itens[8];
				this.CUSTOMER_ADDRESS = itens[9];
				this.NUIT = itens[14];
				char chr = '\"';
				this.CUSTOMER_NAME = this.CUSTOMER_NAME.Replace(chr.ToString(), "");
				chr = '\"';
				this.CUSTOMER_ADDRESS = this.CUSTOMER_ADDRESS.Replace(chr.ToString(), "");
				this.Record_date = Convert.ToDateTime(itens[3]);
			}
			catch (Exception exception)
			{
				Exception ex = exception;
				throw new Exception(string.Format("Erro na leitura do Cabeçalho na linha {0} devido a {1}", linha, ex.Message));
			}
		}

		public void AdicionaLinha(LinhasDocMorpheus linha)
		{
			this.linhasDoc.Add(linha);
		}

		public CabecDoc daCabeDoc(string tipodoc, string serie)
		{
			CabecDoc cabecDoc = new CabecDoc()
			{
				tipodoc = tipodoc,
				serie = serie,
				entidade = this.User_code,
				tipoEntidade = "C",
				nuit = this.NUIT,
				nomeFac = (this.CUSTOMER_NAME.Length > 50 ? this.CUSTOMER_NAME.Substring(0, 50) : this.CUSTOMER_NAME),
				moradaFac = (this.CUSTOMER_ADDRESS.Length > 50 ? this.CUSTOMER_ADDRESS.Substring(0, 50) : this.CUSTOMER_ADDRESS),
				referencia = this.Record_id,
				requisicao = this.Record_id,
				cdu_prioridade = 2,
				cdu_nomeFicheiro = this.fileName,
				observacoes = string.Format("Cliente: {0} Nome: {1} Morada: {2} Nuit: {3}", new object[] { this.Entity_code, this.CUSTOMER_NAME, this.CUSTOMER_ADDRESS, this.NUIT })
			};
			return cabecDoc;
		}

		public void PreencheDados(string linha)
		{
			string[] itens = linha.Split(new char[] { ';' });
			this.linhasDoc = new List<LinhasDocMorpheus>();
			try
			{
				string codigoCliente = itens[15];
				this.Record_id = itens[0];
				this.Entity_code = itens[1];
				if (codigoCliente.Length <= 3)
				{
					this.User_code = itens[1];
				}
				else
				{
					this.User_code = (codigoCliente.Substring(0, 4).ToUpper() == "CVAN" ? codigoCliente : itens[1]);
				}
				this.CUSTOMER_NAME = itens[8];
				this.CUSTOMER_ADDRESS = itens[9];
				this.NUIT = itens[14];
				char chr = '\"';
				this.CUSTOMER_NAME = this.CUSTOMER_NAME.Replace(chr.ToString(), "");
				chr = '\"';
				this.CUSTOMER_ADDRESS = this.CUSTOMER_ADDRESS.Replace(chr.ToString(), "");
				this.Record_date = Convert.ToDateTime(itens[3]);
			}
			catch (Exception exception)
			{
				Exception ex = exception;
				throw new Exception(string.Format("Erro na leitura do Cabeçalho na linha {0} devido a {1}", linha, ex.Message));
			}
		}
	}
}