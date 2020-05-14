using BServices;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Belutecnica
{
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[Guid("384A53F1-FEAC-4CFA-ABB8-D100A2E75191")]
	public class Proxy
	{
		public Proxy()
		{
		}

		public double CalculaAvalicao(dynamic bso, DateTime data, string tipo, string artigo, string fornecedor, string idLinhaOrigem, double preco)
		{
			GestaoAvaliacao cls = new GestaoAvaliacao(bso);
			if (tipo == "prazo")
			{
				return cls.ValidaLinhaPrazoEncomenda(data, idLinhaOrigem);
			}
			if (tipo != "preco")
			{
				return -1;
			}
			return cls.ValidaLinhaPreco(idLinhaOrigem, preco);
		}

		public bool DaCertificao(dynamic bso, string tipo, string artigo, string fornecedor)
		{
			GestaoAvaliacao cls = new GestaoAvaliacao(bso);
			if (tipo == "artigo")
			{
				return cls.ValidaLinhaCertificao_Artigo(artigo);
			}
			if (tipo != "fornecedor")
			{
				return false;
			}
			return cls.ValidaLinhaCertificao_ArtigoFornecedor(artigo, fornecedor);
		}

		public bool EnviaEmail(dynamic bso, dynamic pso, dynamic objCompra, string email_to, string mensaguem, ref string str_aviso, ref string str_erro)
		{
			GestaoOrcamentos cls = new GestaoOrcamentos(bso, pso);
			return (bool)cls.EnviaEmail(objCompra, email_to, mensaguem, str_aviso, str_erro);
		}

		public bool FazVerificacaoDeFundos(dynamic bso, dynamic pso, string modulo, string tipoDoc, ref string str_aviso, ref string str_erro)
		{
			return (new GestaoOrcamentos(bso, pso)).FazVerificacaoDeFundos(modulo, tipoDoc, ref str_aviso, ref str_erro);
		}

		public void GestaoIntranet(dynamic bso, dynamic pso)
		{
			try
			{
				(new GestaoIntranet(bso, pso)).Inicializar();
			}
			catch (Exception exception)
			{
				throw exception;
			}
		}

		public void GestaoIntranet(int tipoPlataforma, string strEmpresa, string strUtilizador, string strPassword)
		{
			try
			{
				(new GestaoIntranet(tipoPlataforma, strEmpresa, strUtilizador, strPassword)).Inicializar();
			}
			catch (Exception exception)
			{
				throw exception;
			}
		}

		public bool ValidaAlteracoesDocumentoOriginal(dynamic bso, dynamic pso, dynamic _objCompra, ref string str_msg, ref string str_msgEmail, ref string str_aviso, ref string str_erro)
		{
			GestaoOrcamentos cls = new GestaoOrcamentos(bso, pso);
			return (bool)cls.ValidaAlteracoesDocumentoOriginal(_objCompra, str_msg, str_msgEmail, str_aviso, str_erro);
		}

		public bool ValidaLinhasOrcamento(dynamic bso, dynamic pso, dynamic objCompra, ref string str_aviso, ref string str_erro)
		{
			return (bool)(new GestaoOrcamentos(bso, pso)).ValidaLinhasOrcamento(objCompra, str_aviso, str_erro);
		}
	}
}