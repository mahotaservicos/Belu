using System;

namespace BServices
{
	public class GestaoIntranet : Geral
	{
		private string conn_string;

		public GestaoIntranet(int tipoPlataforma, string strEmpresa, string strUtilizador, string strPassword) : base(strUtilizador, strPassword, strEmpresa, tipoPlataforma)
		{
		}

		public GestaoIntranet(object BSO, object plat) : base(BSO)
		{
		}

		private bool exportaArtigos(string database, string str_conn)
		{
			try
			{
				string str_query = string.Format("\r\n                    \r\n                    insert into OPENDATASOURCE('SQLNCLI','{0}').{1}.dbo.artigo (Codigo,Descricao,TipoArtigo, StkActual)\r\n\r\n                    select A.Artigo,A.Descricao,A.TipoArtigo,A.STKActual from dbo.Artigo a with(nolock)\r\n                        inner join OPENDATASOURCE('SQLNCLI','{0}').Intranet8.dbo.TipoArtigo ta on ta.codigo = a.TipoArtigo\r\n                        left outer join OPENDATASOURCE('SQLNCLI','{0}').{1}.dbo.artigo aa on aa.codigo = a.artigo\r\n                        where aa.codigo is null ;\r\n                ", str_conn, database);
				str_query = string.Concat(str_query, string.Format("\r\n                    insert into OPENDATASOURCE('SQLNCLI','{0}').{1}.ArtigoUnidades (Artigo,Base,Compra,Venda)\r\n                        select aa.Artigo,aa.UnidadeBase,aa.UnidadeCompra,aa.UnidadeVenda from OPENDATASOURCE('SQLNCLI','{0}').{1}.Artigo a\r\n                        inner join dbo.Artigo aa on aa.Artigo = a.Codigo\r\n                        left outer join OPENDATASOURCE('SQLNCLI','{0}').{1}.ArtigoUnidades au on au.Artigo = a.Codigo\r\n                        where au.Artigo is null ;\r\n                        \r\n                ", str_conn, database));
				str_query = string.Concat(str_query, string.Format("\r\n                    insert into OPENDATASOURCE('SQLNCLI','{0}').{1}.Linhas_ArtigoUnidades (Id_ArtigoUnidade,Artigo,UnidadeOrigem,UnidadeDestino,FactorConversao,UtilizaFormula)\r\n                        select id, a.Codigo, 'UN','UN',1,0 from Artigo a\r\n                        inner join OPENDATASOURCE('SQLNCLI','{0}').{1}.ArtigoUnidades au on au.Artigo = a.Codigo\r\n\r\n                        left outer join OPENDATASOURCE('SQLNCLI','{0}').{1}.Linhas_ArtigoUnidades au on au.Artigo = a.Codigo and\r\n                            au.UnidadeOrigem = 'UN' and au.UnidadeDestino = 'UN'\r\n                        where au.Artigo is null ;\r\n                        \r\n                ", str_conn, database));
				base.ExecutaQuery(str_query);
			}
			catch (Exception exception)
			{
			}
			return true;
		}

		public void Inicializar()
		{
			try
			{
				string server = base.GetParameter("Intranet_Server");
				string database = base.GetParameter("Intranet_Database");
				string user = base.GetParameter("Intranet_User");
				string password = base.GetParameter("Intranet_Password");
				string str_conn = string.Format("'Data Source={0};User ID={1};Password={2};", server, user, password);
				this.conn_string = string.Format("Server={0};Database={1};Trusted_Connection=false;user={2};password={3};MultipleActiveResultSets=true", new object[] { server, database, user, password });
				this.exportaArtigos(database, str_conn);
			}
			catch (Exception exception)
			{
				throw exception;
			}
		}
	}
}