using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Belutecnica
{
	[ComVisible(true)]
	[Guid("CB9CDA01-4581-4B9F-B77E-D7F5ACC7F3DE")]
	public interface IProxy
	{
		double CalculaAvalicao(dynamic bso, DateTime data, string tipo, string artigo, string fornecedor, string idLinhaOrigem, double preco);

		bool DaCertificao(dynamic bso, string tipo, string artigo, string fornecedor);

		bool EnviaEmail(dynamic bso, dynamic pso, dynamic objVenda, string email_to, string mensaguem, ref string str_aviso, ref string str_erro);

		bool FazVerificacaoDeFundos(dynamic bso, dynamic pso, string modulo, string tipoDoc, ref string str_aviso, ref string str_erro);

		void GestaoIntranet(dynamic bso, dynamic pso);

		void GestaoIntranet(int tipoPlataforma, string strEmpresa, string strUtilizador, string strPassword);

		bool ValidaAlteracoesDocumentoOriginal(dynamic bso, dynamic pso, dynamic _objCompra, ref string str_msg, ref string str_msgEmail, ref string str_aviso, ref string str_erro);

		bool ValidaLinhasOrcamento(dynamic bso, dynamic pso, string objCompra, ref string str_aviso, ref string str_erro);
	}
}