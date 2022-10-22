using System.Collections.Generic;
using System.IO;

namespace Sorozatok
{
	internal class ListaElem
	{
		public string datum { get; }
		public string cim { get; }
		public string szam { get; }
		public int hossz { get; } /* perc */
		public bool megnezte { get; } /* true: igen */

		public ListaElem(string[] sorok, int kezdo_index)
		{
			datum = sorok[kezdo_index + 0];
			cim = sorok[kezdo_index + 1];

			szam = sorok[kezdo_index + 2];
		
			hossz = Convert.ToInt32(sorok[kezdo_index + 3]);
			megnezte = sorok[kezdo_index + 4] == "1";
		}
	}
	class Program
	{
		static List<ListaElem> adatok = new List<ListaElem>();

		static void feladat1()
		{
			string[] sorok = File.ReadAllLines("lista.txt");
			for (int i = 0; i < sorok.Length; i += 5)
			{
				adatok.Add(new ListaElem(sorok, i));
			}
		}

		static void feladat2()
		{
			Console.WriteLine("2. feladat");
			
			int ismert_datumok = 0;
			foreach (ListaElem epizod in adatok)
				if (epizod.datum != "NI")
					ismert_datumok ++;
			
			Console.WriteLine("A listában {0} db vetítési dátummal rendelkező epizód van.", ismert_datumok);
			Console.WriteLine();
		}

		static void feladat3()
		{
			Console.WriteLine("3. feladat");

			int latott_epizodok = 0;
			foreach (ListaElem epizod in adatok)
				if (epizod.megnezte)
					latott_epizodok ++;

			float latott_szazalek = ((float)latott_epizodok / (float)adatok.Count) * 100.0f;

			Console.WriteLine("A listában lévő epizódok {0:#0.00}%-át látta.", latott_szazalek);
			Console.WriteLine();
		}

		static void feladat4()
		{
			Console.WriteLine("4. feladat");

			int ossz_perc = 0;
			foreach (ListaElem epizod in adatok)
				if (epizod.megnezte)
					ossz_perc += epizod.hossz;

			int nap = ossz_perc / (60 * 24);
			int ora = (ossz_perc % (60 * 24)) / 60;
			int perc = ossz_perc % 60;

			Console.WriteLine("Sorozatnézéssel {0} napot {1} órát és {2} percet töltött.", nap, ora, perc);
			Console.WriteLine();
		}

		static void feladat5()
		{
			Console.WriteLine("5. feladat");
			Console.Write("Adjon meg egy dátumot! Dátum= ");

			string latott_datum = Console.ReadLine() ?? "";

			foreach (ListaElem epizod in adatok)
				if (!epizod.megnezte && epizod.datum.CompareTo(latott_datum) <= 0)
					Console.WriteLine("{0}\t{1}", epizod.szam, epizod.cim);
		
			Console.WriteLine();
		}

		/* 6. feladat */
		public static string Hetnapja(int ev, int ho, int nap)
		{
			string[] napok = {"v", "h", "k", "sze", "cs", "p", "szo"};
			int[] honapok = {0, 3, 2, 5, 0, 3, 5, 1, 4, 6, 2, 4};

			if (ho < 3)
				ev = ev - 1;
			
			return napok[(ev + (ev / 4) - (ev / 100) +
				(ev / 400) + honapok[ho - 1] + nap) % 7];
		}

		static void feladat7()
		{
			Console.WriteLine("7. feladat");
			Console.Write("Adja meg a hét egy napját (például cs)! Nap= ");

			string megadott_nap = Console.ReadLine() ?? "";

			HashSet<string> megadott_napi_sorozatok = new HashSet<string>();

			foreach (ListaElem epizod in adatok)
			{
				if (epizod.datum != "NI")
				{
					string[] datum_elemek = epizod.datum.Split('.');

					int ev = Convert.ToInt32(datum_elemek[0]),
						honap = Convert.ToInt32(datum_elemek[1]),
						nap = Convert.ToInt32(datum_elemek[2]);
				
					if (Hetnapja(ev, honap, nap) == megadott_nap)
						megadott_napi_sorozatok.Add(epizod.cim);
				}
			}

			if (megadott_napi_sorozatok.Count == 0)
				Console.WriteLine("Az adott napon nem kerül adásba sorozat.");
			else
				foreach (string cim in megadott_napi_sorozatok)
					Console.WriteLine(cim);

			Console.WriteLine();
		}

		static void feladat8()
		{
			using (StreamWriter summa = new StreamWriter("summa.txt"))
			{
				for (int i = 0; i < adatok.Count; )
				{
					int epizodok = 0;
					int ossz_hossz = 0;

					for (int j = i; j < adatok.Count && adatok[j].cim == adatok[i].cim; ++j)
					{
						epizodok ++;
						ossz_hossz += adatok[j].hossz;
					}

					summa.WriteLine("{0} {1} {2}", adatok[i].cim, ossz_hossz, epizodok);

					i += epizodok;
				}
			}
		}

		public static void Main(string[] args)
		{
			feladat1();
			feladat2();
			feladat3();
			feladat4();
			feladat5();
			/* feladat6: Hetnapja */
			feladat7();
			feladat8();
		}
	}
}