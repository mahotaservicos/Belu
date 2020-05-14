using System;
using BServices.Helpers;
using Interop.ErpBS900;
using Interop.StdPlatBS900;
using Interop.StdBE900;
using ADODB;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Interop.StdBESql900;
using System.Runtime.InteropServices;


namespace BServices
{
    public class Geral : IDisposable
    {
        public StdBSInterfPub plat { get; set; }
        public ErpBS bso { get; set; }

        public Geral(dynamic bso)
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            this.bso = (ErpBS) bso;
        }

        public Geral(dynamic bso, dynamic pso)
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            this.bso = bso;
            this.plat =  pso;
        }

        public Geral(string userPrimavera, string passUserPrimavera, string empresa, int tipoEmpPRI)
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            AbrirMotorPrimavera(userPrimavera, passUserPrimavera, empresa, tipoEmpPRI);
        }

        public ErpBS MotorERP()
        {
            return bso;
        }

        /// <summary>
        /// Método para resolução das assemblies.
        /// </summary>
        /// <param name="sender">Application</param>
        /// <param name="args">Resolving Assembly Name</param>
        /// <returns>Assembly</returns>
        static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string assemblyFullName;
            System.Reflection.AssemblyName assemblyName;
            string PRIMAVERA_COMMON_FILES_FOLDER = PrimaveraConstHelper.pastaConfigV900;//pasta dos ficheiros comuns especifica da versão do ERP PRIMAVERA utilizada.
            assemblyName = new System.Reflection.AssemblyName(args.Name);
            assemblyFullName = System.IO.Path.Combine(System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFilesX86), PRIMAVERA_COMMON_FILES_FOLDER), assemblyName.Name + ".dll");
            if (System.IO.File.Exists(assemblyFullName))
                return System.Reflection.Assembly.LoadFile(assemblyFullName);
            else
                return null;
        }

        public Boolean AbrirMotorPrimavera(string userPrimavera, string passUserPrimavera, string empresa, int tipoEmpPRI)
        {
            try
            {
                StdBSConfApl objAplConf = new StdBSConfApl();
                StdPlatBS Plataforma = new StdPlatBS();
                ErpBS MotorLE = new ErpBS();

                EnumTipoPlataforma objTipoPlataforma = new EnumTipoPlataforma();
                objTipoPlataforma = EnumTipoPlataforma.tpEmpresarial;

                objAplConf.Instancia = "Default";
                objAplConf.AbvtApl = "ERP";
                objAplConf.PwdUtilizador = passUserPrimavera;
                objAplConf.Utilizador = userPrimavera;
                objAplConf.LicVersaoMinima = "9.00";

                StdBETransaccao objStdTransac = new StdBETransaccao();

                try
                {
                    Plataforma.AbrePlataformaEmpresa(ref empresa, ref objStdTransac, ref objAplConf, ref objTipoPlataforma, "");
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                if (Plataforma.Inicializada)
                {

                    plat = Plataforma.InterfacePublico;

                    bool blnModoPrimario = true;

                    MotorLE.AbreEmpresaTrabalho(tipoEmpPRI == 0 ? EnumTipoPlataforma.tpEmpresarial : EnumTipoPlataforma.tpProfissional,
                        ref empresa, ref userPrimavera, ref passUserPrimavera, ref objStdTransac, "Default", ref blnModoPrimario);
                    MotorLE.set_CacheActiva(true);

                    bso = MotorLE;

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable ConsultaSQLDatatable(string querySql)
        {

            try
            {
                DataTable dt = new DataTable();

                string connectionString = plat.BaseDados.DaConnectionStringNET(plat.BaseDados.DaNomeBDdaEmpresa(bso.Contexto.CodEmp),
                    "Default");

                SqlConnection con = new SqlConnection(connectionString);

                SqlDataAdapter da = new SqlDataAdapter(querySql, con);

                SqlCommandBuilder cb = new SqlCommandBuilder(da);

                da.Fill(dt);

                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string GetParameter(string Name)
        {
            try
            {
                string result = "";
                string query = string.Format("select * from TDU_Parametros where CDU_Parametro = '{0}' ", Name);
                DataTable dt = ConsultaSQLDatatable(query);

                if (dt.Rows.Count > 0)
                {
                    result = dt.Rows[0]["CDU_Valor"].ToString();
                }
                else
                {
                    new Exception("O parametro {0} não se encontra configurado na tabela de TDU_Parametros");
                }

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public int ExecutaQuery(string querySQL)
        {
            try
            {
                DataTable dt = new DataTable();

                string connectionString = plat.BaseDados.DaConnectionStringNET(plat.BaseDados.DaNomeBDdaEmpresa(bso.Contexto.CodEmp),
                    "Default");

                SqlConnection con = new SqlConnection(connectionString);

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.Connection = con;
                command.CommandText = querySQL;

                return command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public object F4Event(string query, string Coluna, string nomeTabela)
        {
            try
            {
                return plat.Listas.GetF4SQL(nomeTabela, query, Coluna);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Connection AbrirLigacaoXLS(string CaminhoExcel)
        {
            Connection oCon = new Connection();
            oCon = new Connection();
            oCon.ConnectionTimeout = 30;
            oCon.Open(("Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
                            + (CaminhoExcel + ";Extended Properties=\"Excel 8.0;IMEX=1;TypeGuessRows=0;ImportMixedTypes=Text\"")));
            return oCon;
        }

        public void FecharLigacaoXLS(ref Connection ligacao)
        {
            ligacao.Close();
        }

        public void AddLinhaVazia(DataTable dt)
        {
            try
            {
                DataRow dr = dt.NewRow();
                dt.Rows.InsertAt(dr, 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable daListaTabela(string tabela, int maximo = 0, string campos = "", string filtros = "", string juncoes = "", string ordenacao = "")
        {
            try
            {
                string strSql = "select ";

                DataTable dt;

                if (maximo > 0) strSql = strSql + string.Format("Top {0} ", maximo);

                if ((campos.Length > 0))
                {
                    strSql = (strSql + campos);
                }
                else
                {
                    strSql = (strSql + " * ");
                }

                strSql = (strSql + (" from " + tabela));
                if ((juncoes.Length > 0))
                {
                    strSql = (strSql + (" " + juncoes));
                }

                if ((filtros.Length > 0))
                {
                    strSql = (strSql + (" where " + filtros));
                }

                if ((ordenacao.Length > 0))
                {
                    strSql = (strSql + (" order by " + ordenacao));
                }

                dt = this.ConsultaSQLDatatable(strSql);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public TDU_Email DaDadoServidorEmail(string user = "")
        {
            TDU_Email tDUEmail;
            TDU_Email email = new TDU_Email();
            try
            {
                DataTable dt = this.daListaTabela("TDU_DadosEmail", 0, "*", string.Format("CDU_UserPrim= '{0}'", user), "", "");
                if (dt.Rows.Count > 0)
                {
                    email = new TDU_Email()
                    {
                        CDU_fromStr = StringHelper.DaString(dt.Rows[0]["CDU_fromStr"]),
                        CDU_userSSL = StringHelper.DaString(dt.Rows[0]["CDU_userSSL"]),
                        CDU_passUserSSL = StringHelper.DaString(dt.Rows[0]["CDU_passUserSSL"]),
                        CDU_portaSmtpServer = StringHelper.DaInt32(dt.Rows[0]["CDU_portaSmtpServer"]),
                        CDU_smtpServer = StringHelper.DaString(dt.Rows[0]["CDU_smtpServer"]),
                        CDU_ssl = StringHelper.DaBoolean(dt.Rows[0]["CDU_ssl"]),
                        CDU_UserPrim = StringHelper.DaString(dt.Rows[0]["CDU_UserPrim"]),
                        CDU_CC1 = StringHelper.DaString(dt.Rows[0]["CDU_CC1"]),
                        CDU_CC2 = StringHelper.DaString(dt.Rows[0]["CDU_CC2"]),
                        CDU_CC3 = StringHelper.DaString(dt.Rows[0]["CDU_CC3"]),
                        CDU_CC4 = StringHelper.DaString(dt.Rows[0]["CDU_CC4"]),
                        CDU_CC5 = StringHelper.DaString(dt.Rows[0]["CDU_CC5"]),
                        CDU_CC6 = StringHelper.DaString(dt.Rows[0]["CDU_CC6"])
                    };
                }
                tDUEmail = email;
            }
            catch (Exception exception)
            {
                throw new Exception(string.Concat("<DaDadosServidorEmail>_", exception.Message));
            }
            return tDUEmail;
        }


        public void escreveErro(string pastaLog, string name, string logMessage)
        {
            try
            {
                using (StreamWriter w = File.AppendText(string.Concat(pastaLog, "\\", string.Format("erro_{0}.log", name))))
                {
                    this.Log(logMessage, w);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        protected void escreveLog(string logMessage)
        {
            string ficheiro;
            try
            {
                ficheiro = GetParameter("Log_PastaErro");

                using (StreamWriter w = File.AppendText(ficheiro + "\\" + string.Format("log_{0}.txt", DateTime.Now.ToString("ddMMyy"))))
                {
                    Log(logMessage, w);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //throw new NotImplementedException();
        }

        public string DaMes(int mes)
        {
            switch (mes)
            {
                case 1: return "Janeiro";
                case 2: return "Fevreiro";
                case 3: return "Março";
                case 4: return "Abril";
                case 5: return "Maio";
                case 6: return "Junho";
                case 7: return "Julho";
                case 8: return "Agosto";
                case 9: return "Setembro";
                case 10: return "Outubro";
                case 11: return "Novembro";
                case 12: return "Dezembro";

            }
            return "";
        }

        public void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), logMessage);
            }
            catch (Exception ex)
            {
            }
        }

        public void DrillDownDocumentoGCP(string documento)
        {
            try
            {
                documento = documento.Replace("Importado - ", "");

                DataTable dt;
                string query = string.Format("select TipoDoc, Serie, NumDoc,Filial from cabecdoc where tipodoc + ' '+ convert(nvarchar,numdoc) +'/'+serie ='{0}' and filial = '000' ", documento);
                string tipodoc, serie, filial, modulo = "V";
                int numdoc;

                dt = ConsultaSQLDatatable(query);
                if (dt.Rows.Count > 0)
                {
                    tipodoc = dt.Rows[0]["TipoDoc"].ToString();
                    serie = dt.Rows[0]["Serie"].ToString();
                    filial = dt.Rows[0]["Filial"].ToString();
                    numdoc = Convert.ToInt32(dt.Rows[0]["NumDoc"]);

                    DrillDownDocumentoGCP(modulo, filial, tipodoc, serie, numdoc);

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DrillDownDocumentoGCP(string strModulo, string strFilial, string strTipodoc, string strSerie, int intNumDoc)
        {
            StdBESqlCampoDrillDown objCampoDrillDown = new StdBESqlCampoDrillDown();
            StdBEValoresStr objParam = new StdBEValoresStr();

            try
            {
                objCampoDrillDown.set_ModuloNotificado("GCP");
                objCampoDrillDown.set_Tipo(EnumTipoDrillDownListas.tddlEventoAplicacao);
                objCampoDrillDown.set_Evento("GCP_EditarDocumento");

                objParam.InsereNovo("Modulo", strModulo);
                objParam.InsereNovo("Filial", strFilial);
                objParam.InsereNovo("Tipodoc", strTipodoc);
                objParam.InsereNovo("Serie", strSerie);
                objParam.InsereNovo("NumDocInt", intNumDoc.ToString());

                plat.DrillDownLista(objCampoDrillDown, objParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Marshal.ReleaseComObject(objCampoDrillDown);
                Marshal.ReleaseComObject(objParam);
            }
        }


        public void Dispose()
        {

            //_empresaErp.Dispose();
            //_comercial.Dispose();

        }



    }
}