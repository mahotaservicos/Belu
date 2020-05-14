using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BServices.Helpers
{
	public static class StringHelper
	{
		public static bool DaBoolean(object s)
		{
			if (s is DBNull)
			{
				return false;
			}
			return Convert.ToBoolean(s.ToString());
		}

		public static double DaDouble(object s)
		{
			if (s is DBNull)
			{
				return 0;
			}
			return Convert.ToDouble(s);
		}

		public static float DaFloat(object s)
		{
			if (s is DBNull)
			{
				return 0f;
			}
			return float.Parse(s.ToString());
		}

		public static int DaInt32(object s)
		{
			int num;
			try
			{
				num = (!(s is DBNull) ? Convert.ToInt32(s) : 0);
			}
			catch
			{
				return 0;
			}
			return num;
		}

		public static string DaString(object s)
		{
			string str;
			try
			{
				str = (!(s is DBNull) ? Convert.ToString(s) : "");
			}
			catch
			{
				return "";
			}
			return str;
		}

		public static string StripAlpha(this string self)
		{
			return new string((
				from c in self
				where char.IsLetter(c)
				select c).ToArray<char>());
		}

		public static string StripNonNumeric(this string self)
		{
			string str = new string(self.Where<char>((char c) => {
				if (char.IsDigit(c) || c == '.')
				{
					return true;
				}
				return c == ',';
			}).ToArray<char>());
			return new string(self.Where<char>((char c) => {
				if (char.IsDigit(c) || c == '.')
				{
					return true;
				}
				return c == ',';
			}).ToArray<char>());
		}
	}
}