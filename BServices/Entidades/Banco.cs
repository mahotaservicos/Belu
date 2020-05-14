using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BServices.Entidades
{
	public class Banco
	{
		public string conta
		{
			get;
			set;
		}

		public DateTime data
		{
			get;
			set;
		}

		public string empresa
		{
			get;
			set;
		}

		public List<MovimentoBanco> listaMov
		{
			get;
			set;
		}

		public string moeda
		{
			get;
			set;
		}

		public Banco()
		{
			this.listaMov = new List<MovimentoBanco>();
		}
	}
}