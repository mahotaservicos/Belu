using System;
using System.Runtime.CompilerServices;

namespace BServices.Entidades
{
	public class MovimentoBanco
	{
		public string conta
		{
			get;
			set;
		}

		public string descritivo
		{
			get;
			set;
		}

		public int seq
		{
			get;
			set;
		}

		public double valor
		{
			get;
			set;
		}

		public MovimentoBanco()
		{
		}
	}
}