using BServices.Helpers;
using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace BServices.Entidades
{
	public class LinhasDocMorpheus
	{
		public string artigo
		{
			get;
			set;
		}

		public string iva
		{
			get;
			set;
		}

		public double precoUnitario
		{
			get;
			set;
		}

		public double quantidade
		{
			get;
			set;
		}

		public string unidades
		{
			get;
			set;
		}

		public LinhasDocMorpheus()
		{
		}

		public LinhasDocMorpheus(string linha)
		{
			string[] itens = linha.Split(new char[] { ';' });
			NumberFormatInfo dbNumberFormat = (new CultureInfo("en-US")).NumberFormat;
			try
			{
				this.artigo = itens[4];
				this.quantidade = double.Parse(itens[5], dbNumberFormat);
				this.unidades = itens[6];
				this.precoUnitario = double.Parse(itens[7], dbNumberFormat);
				if (this.artigo.Contains("X") || this.artigo.Contains("x") || this.artigo.Contains("C") || this.artigo.Contains("c"))
				{
					char chr = 'X';
					string str = this.artigo.Replace(chr.ToString(), "");
					chr = 'x';
					string str1 = str.Replace(chr.ToString(), "");
					chr = 'C';
					string str2 = str1.Replace(chr.ToString(), "");
					chr = 'c';
					this.artigo = str2.Replace(chr.ToString(), "");
					string tempNumeroUnidades = this.unidades.StripNonNumeric();
					if (tempNumeroUnidades == "")
					{
						tempNumeroUnidades = "1";
					}
					double numeroUnidades = Convert.ToDouble(tempNumeroUnidades);
					this.unidades = this.unidades.StripAlpha();
					this.quantidade = this.quantidade * numeroUnidades;
					this.precoUnitario = this.precoUnitario / numeroUnidades;
				}
			}
			catch (Exception exception)
			{
				Exception ex = exception;
				throw new Exception(string.Format("Erro na leitura da linha {0} devido a {1}", linha, ex.Message));
			}
		}
	}
}