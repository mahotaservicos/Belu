using System;
using System.Data;

namespace BServices
{
	public class GestaoAvaliacao : Geral
	{
		public GestaoAvaliacao(object BSO) : base(BSO)
		{
		}

		public double CalculaPrazo(DateTime dataDoc, DateTime dataVenc)
		{
			return (dataDoc - dataVenc).TotalDays;
		}

		public bool ValidaLinhaCertificao_Artigo(string artigo)
		{
			bool flag;
			try
			{
				flag = (base.ConsultaSQLDatatable(string.Format("select CDU_Certificao from Artigo where Artigo = '{0}' and CDU_Certificao = 1", artigo)).Rows.Count <= 0 ? false : true);
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return flag;
		}

		public bool ValidaLinhaCertificao_ArtigoFornecedor(string artigo, string fornecedor)
		{
			bool flag;
			try
			{
				flag = (base.ConsultaSQLDatatable(string.Format("select CDU_Certificao from ArtigoFornecedor where Artigo = '{0}' and Fornecedor = '{1}' and CDU_Certificao = 1", artigo, fornecedor)).Rows.Count <= 0 ? false : true);
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return flag;
		}

		public double ValidaLinhaPrazoEncomenda(DateTime data, string idLinhaOrigem)
		{
			double num;
			try
			{
				DataTable dt = base.ConsultaSQLDatatable(string.Format("select cd.DataVencimento from CabecCompras cd with(nolock)\r\n                    inner join LinhasCompras ld with(nolock) on ld.IdCabecCompras = cd.Id\r\n                    where ld.id ='{0}'", idLinhaOrigem));
				if (dt.Rows.Count <= 0)
				{
					throw new Exception("Linha pai nao existe");
				}
				DateTime dataVenc = Convert.ToDateTime(dt.Rows[0]["DataVencimento"]);
				double diasEntrega = this.CalculaPrazo(data, dataVenc);
				if (diasEntrega >= 0 && diasEntrega <= 15)
				{
					num = 10;
				}
				else if (diasEntrega >= 16 && diasEntrega <= 30)
				{
					num = 8;
				}
				else if (diasEntrega < 31 || diasEntrega > 40)
				{
					num = (diasEntrega <= 40 ? -1 : 0);
				}
				else
				{
					num = 3;
				}
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return num;
		}

		public double ValidaLinhaPreco(string idLinhaOrigem, double preco)
		{
			double num;
			try
			{
				DataTable dt = base.ConsultaSQLDatatable(string.Format("select Preco_Min,Preco_Max,Preco_Medio, PCMedio from View_CMP_ValidaPrecoFornecedores where idLinhaOrigem = '{0}'", idLinhaOrigem));
				if (dt.Rows.Count <= 0)
				{
					num = -1;
				}
				else
				{
					double num1 = Convert.ToDouble(dt.Rows[0]["Preco_Min"]);
					Convert.ToDouble(dt.Rows[0]["Preco_Max"]);
					num = Math.Round(num1 / preco * 10, 0);
				}
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return num;
		}
	}
}