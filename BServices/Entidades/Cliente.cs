using System;
using System.Runtime.CompilerServices;

namespace BServices.Entidades
{
	public class Cliente
	{
		public string cdu_armazem
		{
			get;
			set;
		}

		public bool cdu_integra
		{
			get;
			set;
		}

		public bool cdu_retira
		{
			get;
			set;
		}

		public string cliente
		{
			get;
			set;
		}

		public string nome
		{
			get;
			set;
		}

		public Cliente(dynamic campos)
		{
			this.cliente = (string)campos[1].valor;
			this.nome = (string)campos[2].valor;
			this.cdu_armazem = (string)campos[3].valor;
		}

		public Cliente()
		{
		}
	}
}