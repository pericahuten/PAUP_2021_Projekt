using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paup_2021_ServisVozila.Models
{
    public class VIN
    {
		private static int transliterate(char c)
		{
			return "0123456789.ABCDEFGH..JKLMN.P.R..STUVWXYZ".IndexOf(c) % 10;
		}

		private static char getCheckDigit(String vin)
		{
			String map = "0123456789X";
			String weights = "8765432X098765432";
			int sum = 0;
			for (int i = 0; i < 17; ++i)
			{
				sum += transliterate(vin[i]) * map.IndexOf(weights[i]);
			}
			return map[sum % 11];
		}

		public static bool validateVIN(String vin)
		{
			if (vin.Length != 17) return false;
			return getCheckDigit(vin) == vin[8];
		}
	}
}