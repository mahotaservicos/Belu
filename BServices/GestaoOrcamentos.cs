using BServices.Helpers;
using Interop.ErpBS900;
using Interop.GcpBE900;
using Interop.StdBE900;
using Interop.StdPlatBS900;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;

namespace BServices
{
    public class GestaoOrcamentos : Geral
    {
        public StdBSInterfPub pso
        {
            get;
            set;
        }

        public GestaoOrcamentos(object BSO, object plat) : base(BSO)
        {
            this.pso = (StdBSInterfPub)plat;
        }

        public bool EnviaEmail(dynamic _objCompra, string email_to, string mensaguem, ref string str_aviso, ref string str_erro)
        {
            bool flag;
            string source = "GestaoOrcamentos.EnviaEmails";
            string log = base.GetParameter("Mail_log");
            string idioma = "2070";
            try
            {
                GcpBEDocumentoCompra objCompra = (GcpBEDocumentoCompra)_objCompra;
                
                base.GetParameter("Mail_Report");
                
                string email_mensaguem = base.GetParameter("Mail_Mensaguem");
                string email_cc = base.GetParameter("Mail_CC");
                string email_bcc = base.GetParameter("Mail_BCC");
                
                base.GetParameter("Mail_TipoContacto");
                
                string email_assunto = base.GetParameter("Mail_Assunto");
                string email_body = "";
                
                Dictionary<string, string> dicionario = new Dictionary<string, string>()
                {
                    { "Artigo_2070", "Artigo" },
                    { "Descricao_2070", "Descrição" },
                    { "Unidade_2070", "Unidade" },
                    { "Quantidade_2070", "Quantidade" },
                    { "PrecUnit_2070", "Preço Unit." },
                    { "Total_2070", "Total" },
                    { "ContaCBL_2070", "Conta" },
                    { "CCustoCBL_2070", "Area de Negocio" }
                };

                string primPar = base.GetParameter("Mail_primPar");
                string segPar = base.GetParameter("Mail_segPar");
                string terPar = base.GetParameter("Mail_terPar");

                email_body = string.Concat(email_body, " \r\n                    <html xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:word' xmlns:m='http://schemas.microsoft.com/office/2004/12/omml' xmlns='http://www.w3.org/TR/REC-html40'><head><meta http-equiv=Content-Type content='text/html; charset=iso-8859-1'><meta name=Generator content='Microsoft Word 15 (filtered medium)'><!--[if gte mso 9]><xml>\r\n                            <o:shapedefaults v:ext='edit' spidmax='1026' /></xml><![endif]--><!--[if gte mso 9]><xml><o:shapelayout v:ext='edit'><o:idmap v:ext='edit' data='1' />\r\n                         </o:shapelayout></xml><![endif]-->\r\n                        <style>\r\n                            body {\r\n                                font: normal 10px Verdana, Arial, sans-serif;\r\n                                font-size:11.0pt;\r\n                                font-family:'Calibri',sans-serif;\r\n                                mso-fareast-language:PT\r\n                            }\r\n                        </style>\r\n\r\n                    </head><body lang=PT > ");
                email_body = string.Concat(email_body, string.Format("<div> \r\n                        <p><span>{0}</span></p>\r\n                        {1}\r\n                        <p><span>{2}</span></p>\r\n                        <p><span>{3}</span></p>\r\n                    </div>", new object[] { primPar, mensaguem, segPar, terPar }));
                email_body = string.Concat(email_body, "<div><table border=2 cellspacing=10 cellpadding=10 style='border-collapse:collapse'>");
                email_body = string.Concat(email_body, string.Format("<tr>\r\n                         <td width=100 valign=top style='width:100pt;color:white;background:#5C6BC0;padding:0cm 5.4pt 0cm 5.4pt'><p align=center style='text-align:center'><b>{0}</b></p></td>\r\n                         <td width=100 valign=top style='width:100pt;color:white;background:#5C6BC0;padding:0cm 5.4pt 0cm 5.4pt'><p align=center style='text-align:center'><b>{1}</b></p></td>\r\n                         <td width=100 valign=top style='width:100pt;color:white;background:#5C6BC0;padding:0cm 5.4pt 0cm 5.4pt'><p align=center style='text-align:center'><b>{2}</b></p></td>\r\n                         <td width=100 valign=top style='width:100pt;color:white;background:#5C6BC0;padding:0cm 5.4pt 0cm 5.4pt'><p align=center style='text-align:center'><b>{3}</b></p></td>\r\n                         <td width=100 valign=top style='width:100pt;color:white;background:#5C6BC0;padding:0cm 5.4pt 0cm 5.4pt'><p align=center style='text-align:center'><b>{4}</b></p></td>\r\n                         <td width=100 valign=top style='width:100pt;color:white;background:#5C6BC0;padding:0cm 5.4pt 0cm 5.4pt'><p align=center style='text-align:center'><b>{5}</b></p></td>\r\n                         <td width=100 valign=top style='width:100pt;color:white;background:#5C6BC0;padding:0cm 5.4pt 0cm 5.4pt'><p align=center style='text-align:center'><b>{6}</b></p></td>\r\n                         <td width=100 valign=top style='width:100pt;color:white;background:#5C6BC0;padding:0cm 5.4pt 0cm 5.4pt'><p align=center style='text-align:center'><b>{7}</b></p></td>\r\n                     </tr>\r\n                ", new object[] { dicionario[string.Concat("Artigo_", idioma)], dicionario[string.Concat("Descricao_", idioma)], dicionario[string.Concat("Unidade_", idioma)], dicionario[string.Concat("Quantidade_", idioma)], dicionario[string.Concat("PrecUnit_", idioma)], dicionario[string.Concat("Total_", idioma)], dicionario[string.Concat("ContaCBL_", idioma)], dicionario[string.Concat("CCustoCBL_", idioma)] }));
                
                for (int i = 1; i <= objCompra.get_Linhas().NumItens; i++)
                {
                    int num = i;
                    GcpBELinhaDocumentoCompra linha = objCompra.get_Linhas().get_Edita(ref num);
                    
                    short ano = (short)objCompra.get_DataDoc().Year;
                    string cCustoCBL = linha.get_CCustoCBL();
                    string contaCBL = linha.get_ContaCBL();

                    string centroNome = base.bso.Contabilidade.CentrosCusto.DaValorAtributo(ano, cCustoCBL, "Descricao").ToString(); 
                    string contaNome = base.bso.Contabilidade.PlanoContas.DaValorAtributo(ano, contaCBL, "Descricao").ToString();
                    email_body = string.Concat(email_body, string.Format("\r\n                            <tr>\r\n                                <td width=100 valign=top style='width:100pt;padding:0cm 5.4pt 0cm 5.4pt'><p align=right style='text-align:left'>{0}</p></td>\r\n                                <td width=100 valign=top style='width:100pt;padding:0cm 5.4pt 0cm 5.4pt'><p align=right style='text-align:left'>{1}</p></td>\r\n                                <td width=100 valign=top style='width:100pt;padding:0cm 5.4pt 0cm 5.4pt'><p align=right style='text-align:left'>{2}</p></td>\r\n                                <td width=100 valign=top style='width:100pt;padding:0cm 5.4pt 0cm 5.4pt'><p align=right style='text-align:left'>{3}</p></td>\r\n                                <td width=100 valign=top style='width:100pt;padding:0cm 5.4pt 0cm 5.4pt'><p align=right style='text-align:left'>{4}</p></td>\r\n                                <td width=100 valign=top style='width:100pt;padding:0cm 5.4pt 0cm 5.4pt'><p align=right style='text-align:left'>{5}</p></td>\r\n                                <td width=100 valign=top style='width:100pt;padding:0cm 5.4pt 0cm 5.4pt'><p align=right style='text-align:left'>{6} - {7}</p></td>\r\n                                <td width=100 valign=top style='width:100pt;padding:0cm 5.4pt 0cm 5.4pt'><p align=right style='text-align:left'>{8} - {9}</p></td>\r\n                            </tr>\r\n                        ", new object[] { linha.get_Artigo(), linha.get_Descricao(), linha.get_Unidade(), linha.get_Quantidade(), linha.get_PrecUnit(), linha.get_PrecoLiquido(), linha.get_ContaCBL(), contaNome, linha.get_CCustoCBL(), centroNome }));
                }
                
                email_body = string.Concat(email_body, " </table> <p></p> </div>");
                email_body = string.Concat(email_body, "</body></html>");
                email_mensaguem = email_body;
                
                if (email_to.Length <= 0)
                {
                    throw new Exception(string.Format("O email não foi enviado porque não existe nenhum contacto associado ao utilizador!", new object[0]));
                }
                
                this.EnviaEmailNativo(email_to, email_mensaguem, email_cc, email_bcc, email_assunto);
                
                flag = true;
            }
            catch (Exception exception)
            {
                Exception ex = exception;
                base.escreveErro(log, source, string.Format("<{0}>_{1}", source, ex.Message));
                flag = false;
            }
            return flag;
        }

        private void EnviaEmailNativo(string email_to, string email_mensaguem, string email_cc, string email_bcc, string email_assunto)
        {
            int i;
            string source = "GestaoOrcamentos.EnviaEmailNativo";
            string log = base.GetParameter("Mail_log");
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                TDU_Email dadosEmail = base.DaDadoServidorEmail(base.bso.Contexto.UtilizadorActual);
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(dadosEmail.CDU_userSSL, dadosEmail.CDU_portaSmtpServer);
                mail.From = new MailAddress(dadosEmail.CDU_fromStr);
                string[] strArrays = email_to.Split(new char[] { ';' });
                for (i = 0; i < (int)strArrays.Length; i++)
                {
                    string item = strArrays[i];
                    mail.To.Add(item);
                }
                if (email_cc.Length > 0)
                {
                    strArrays = email_cc.Split(new char[] { ';' });
                    for (i = 0; i < (int)strArrays.Length; i++)
                    {
                        string item = strArrays[i];
                        mail.CC.Add(item);
                    }
                }
                if (email_bcc.Length > 0)
                {
                    strArrays = email_bcc.Split(new char[] { ';' });
                    for (i = 0; i < (int)strArrays.Length; i++)
                    {
                        string item = strArrays[i];
                        mail.Bcc.Add(item);
                    }
                }
                if (dadosEmail.CDU_CC1.Length > 0)
                {
                    mail.CC.Add(dadosEmail.CDU_CC1);
                }
                if (dadosEmail.CDU_CC2.Length > 0)
                {
                    mail.CC.Add(dadosEmail.CDU_CC2);
                }
                if (dadosEmail.CDU_CC3.Length > 0)
                {
                    mail.CC.Add(dadosEmail.CDU_CC3);
                }
                if (dadosEmail.CDU_CC4.Length > 0)
                {
                    mail.CC.Add(dadosEmail.CDU_CC4);
                }
                if (dadosEmail.CDU_CC5.Length > 0)
                {
                    mail.CC.Add(dadosEmail.CDU_CC5);
                }
                if (dadosEmail.CDU_CC6.Length > 0)
                {
                    mail.CC.Add(dadosEmail.CDU_CC6);
                }
                mail.Subject = email_assunto;
                mail.IsBodyHtml = true;
                mail.Body = email_mensaguem;
                SmtpServer.Host = dadosEmail.CDU_smtpServer;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new NetworkCredential(dadosEmail.CDU_userSSL, dadosEmail.CDU_passUserSSL);
                SmtpServer.EnableSsl = dadosEmail.CDU_ssl;
                SmtpServer.Send(mail);
            }
            catch (Exception exception)
            {
                Exception ex = exception;
                base.escreveErro(log, source, string.Format("<{0}>_{1}", source, ex.Message));
            }
        }

        public bool FazVerificacaoDeFundos(string modulo, string tipodoc, ref string str_aviso, ref string str_erro)
        {
            bool flag;
            string source = "GestaoOrcamentos.FazVerificacaoDeFundos";
            string log = base.GetParameter("PastaErro");
            try
            {
                string str = string.Format("select CDU_TipoDoc from Tdu_VerificacaoFundos_Documentos where CDU_Modulo='{0}' and CDU_Documento= '{1}'", modulo, tipodoc);
                flag = (base.bso.Consulta(ref str).Vazia() ? false : true);
            }
            catch (Exception exception)
            {
                Exception ex = exception;
                base.escreveErro(log, source, string.Format("<{0}>_{1}", source, ex.Message));
                flag = false;
            }
            return flag;
        }

        public bool ValidaAlteracoesDocumentoOriginal(dynamic _objCompra, ref string str_msg, ref string str_msgEmail, ref string str_aviso, ref string str_erro)
        {
            bool flag;
            string source = "GestaoOrcamentos.ValidaAlteracoesDocumentoOriginal";
            string log = base.GetParameter("PastaErro");
            
            double precoAnterior = 0;
            double quantAnterior = 0;
            string erro_linha = "";
            string erro_linhaEmail = "";

            string user = base.bso.Contexto.UtilizadorActual;
            try
            {
                GcpBEDocumentoCompra objCompra = (GcpBEDocumentoCompra)_objCompra;
                str_msg = "";
                str_msgEmail = "";
                string documento = string.Format("{0} {1}/{2}", objCompra.get_Tipodoc(), objCompra.get_NumDoc(), objCompra.get_Serie());
                string documentoOrc = "";
                for (int i = 1; i <= objCompra.get_Linhas().NumItens; i++)
                {
                    erro_linha = "";
                    erro_linhaEmail = "";
                    int num = i;
                    GcpBELinhaDocumentoCompra linha = objCompra.get_Linhas().get_Edita(ref num);
                    string idLinhaOrigem = linha.get_IDLinhaOriginal().Replace("{", "").Replace("}", "");

                    DataTable dt = base.ConsultaSQLDatatable(
                        string.Format("select [Documento],[DocumentoOrc],[id],[Artigo],[unidade],[Quantidade],[PrecUnit] from View_Linhas_Documento_OCP where id= '{0}'", 
                        idLinhaOrigem
                        )
                    );

                    if (dt.Rows.Count > 0)
                    {
                        precoAnterior = StringHelper.DaDouble(dt.Rows[0]["PrecUnit"]);
                        quantAnterior = StringHelper.DaDouble(dt.Rows[0]["Quantidade"]);
                        documentoOrc = StringHelper.DaString(dt.Rows[0]["DocumentoOrc"]);
                        if (linha.get_PrecUnit() > precoAnterior)
                        {
                            erro_linha = string.Concat(string.Format(new CultureInfo("pt-PT", false), "Preço Orçamentado = {0:N2}", new object[] { precoAnterior }), Environment.NewLine);
                            erro_linha = string.Concat(erro_linha, string.Format(new CultureInfo("pt-PT", false), "Preço = {0:N2} ", new object[] { linha.get_PrecUnit() }), Environment.NewLine, Environment.NewLine);
                            erro_linhaEmail = string.Format(new CultureInfo("pt-PT", false), "<p>Preço Orçamentado = {0:N2}</p>", new object[] { precoAnterior });
                            erro_linhaEmail = string.Concat(erro_linhaEmail, string.Format(new CultureInfo("pt-PT", false), "<p>Preço = {0:N2} </p> </br>", new object[] { linha.get_PrecUnit() }));
                        }
                        if (linha.get_Quantidade() > quantAnterior)
                        {
                            erro_linha = string.Concat(erro_linha, string.Format(new CultureInfo("pt-PT", false), "Quantidade Orçamentada = {0:N2}", new object[] { quantAnterior }), Environment.NewLine);
                            erro_linha = string.Concat(erro_linha, string.Format(new CultureInfo("pt-PT", false), "Quantidade = {0:N2}", new object[] { linha.get_Quantidade() }), Environment.NewLine, Environment.NewLine);
                            erro_linhaEmail = string.Format(new CultureInfo("pt-PT", false), "<p>Quantidade Orçamentada = {0:N2}</p>", new object[] { quantAnterior });
                            erro_linhaEmail = string.Concat(erro_linhaEmail, string.Format(new CultureInfo("pt-PT", false), "<p>Quantidade = {0:N2} </p> </br>", new object[] { linha.get_Quantidade() }));
                        }
                        if (erro_linha.Length > 0)
                        {
                            object[] str = new object[] { i, documento, null, null, null, null, null };
                            DateTime dataDoc = objCompra.get_DataDoc();
                            str[2] = dataDoc.ToString("dd/MM/yyyy");
                            str[3] = user;
                            str[4] = Environment.NewLine;
                            str[5] = erro_linha;
                            str[6] = documentoOrc;
                            str_msg = string.Concat(str_msg, string.Format("A Linha ({0}) do documento {1} do dia {2} pelo(a) {3} \r\n                                falhou na validação com o documento {6} orçamentado. Assim, a vide abaixo o detalhe: {4} {5}", str));
                            str_msg = string.Concat(str_msg, Environment.NewLine);
                            object[] objArray = new object[] { i, documento, null, null, null, null, null };
                            dataDoc = objCompra.get_DataDoc();
                            objArray[2] = dataDoc.ToString("dd/MM/yyyy");
                            objArray[3] = user;
                            objArray[4] = "</br>";
                            objArray[5] = erro_linhaEmail;
                            objArray[6] = documentoOrc;
                            str_msgEmail = string.Concat(str_msgEmail, string.Format("<p><b>A Linha ({0}) do documento {1} do dia {2} pelo(a) {3} \r\n                                falhou na validação com o documento {6} orçamentado. Assim, a vide abaixo o detalhe:</b></p> {4} {5}", objArray));
                        }
                    }
                }
                flag = (str_msg.Length <= 0 ? true : false);
            }
            catch (Exception exception)
            {
                Exception ex = exception;
                str_erro = string.Concat(str_erro, ex.Message, Environment.NewLine);
                base.escreveErro(log, source, string.Format("<{0}>_{1}", source, ex.Message));
                flag = true;
            }
            return flag;
        }

        public bool ValidaLinhasOrcamento(dynamic _objCompra, ref string str_aviso, ref string str_erro)
        {
            bool flag;
            string source = "GestaoOrcamentos.ValidaLinhasOrcamento";
            string log = base.GetParameter("PastaErro");
            bool cancel = false;
            try
            {
                GcpBEDocumentoCompra objCompra = (GcpBEDocumentoCompra)_objCompra;
                for (int i = 1; i <= objCompra.get_Linhas().NumItens; i++)
                {
                    int num = i;
                    GcpBELinhaDocumentoCompra edita = objCompra.get_Linhas().get_Edita(ref num);
                    if (edita.get_ContaCBL().Length == 0)
                    {
                        cancel = true;
                    }
                    if (edita.get_CCustoCBL().Length == 0)
                    {
                        cancel = true;
                    }
                    if (edita.get_IDObra().Length == 0)
                    {
                        cancel = true;
                    }
                }
                flag = cancel;
            }
            catch (Exception exception)
            {
                Exception ex = exception;
                base.escreveErro(log, source, string.Format("<{0}>_{1}", source, ex.Message));
                flag = true;
            }
            return flag;
        }
    }
}