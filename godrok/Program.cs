using System;
using System.IO;
using System.Collections.Generic;

namespace godrok
{
	class Program
	{
		static List<int> melysegek = new ();
		
		static int tavolsag;
		static int godor_kezdet;
		static int godor_veg;

		public static void Main(string[] args)
		{
			feladat1();
			feladat2();
			feladat3();
			feladat4();
			feladat5();
			feladat6();
		}

		static void feladat1()
		{
			Console.WriteLine("1. feladat");

			foreach (string line in File.ReadAllLines("melyseg.txt"))
			{
				melysegek.Add(int.Parse(line));
			}

			Console.WriteLine("A fájl adatainak száma: {0}", melysegek.Count);
			Console.WriteLine();
		}

		static void feladat2()
		{
			Console.WriteLine("2. feladat");

			Console.Write("Adjon meg egy távolságértéket! ");
			tavolsag = int.Parse(Console.ReadLine());

			Console.WriteLine("Ezen a helyen a felszín {0} méter mélyen van.", melysegek[tavolsag - 1]);
			Console.WriteLine();
		}

		static void feladat3()
		{
			Console.WriteLine("3. feladat");

			float erintetlen = 0.0f;

			foreach (int melyseg in melysegek)
				if (melyseg == 0)
					erintetlen += 1.0f;

			Console.WriteLine("Az érintetlen terület aránya {0:00.00}%.", (erintetlen / (float)melysegek.Count) * 100.0f);
			Console.WriteLine();
		}

		static void feladat4()
		{
			List<string> godrok = new();
			List<int> utolso_godor_melysegei = new();

			foreach (int melyseg in melysegek)
			{
				if (melyseg != 0)
				{
					utolso_godor_melysegei.Add(melyseg);
				}
				else if (utolso_godor_melysegei.Count > 0)
				{
					godrok.Add(string.Join(' ', utolso_godor_melysegei));
					utolso_godor_melysegei.Clear();
				}
			}

			// utolso_godor_melysegei itt üres, és nem kell
			// fájlba írni, mert:
			// "Tudjuk, hogy az első és az utolsó méteren
			//  sértetlen a felszín, tehát ott biztosan
			//  a 0 szám áll."

			File.WriteAllLines("godrok.txt", godrok.ToArray());
		}

		static void feladat5()
		{
			Console.WriteLine("5. feladat");

			int godrok_szama = 0;
			bool godor = false;
			
			foreach (int melyseg in melysegek)
			{
				if (!godor && melyseg > 0)
				{
					godor = true;
					godrok_szama += 1;
				}

				if (godor && melyseg == 0)
				{
					godor = false;
				}
			}

			Console.WriteLine("A gödrök száma: {0}", godrok_szama);
			Console.WriteLine();
		}

		static void feladat6()
		{
			Console.WriteLine("6. feladat");

			if (melysegek[tavolsag - 1] != 0)
			{
				feladat6a();
				feladat6b();
				feladat6c();
				feladat6d();
				feladat6e();
			}
			else
			{
				Console.WriteLine("Az adott helyen nincs gödör.");
			}

			Console.WriteLine();
		}

		static void feladat6a()
		{
			Console.WriteLine("a)");

			for (int i = tavolsag + 1; i < melysegek.Count; ++i)
			{
				if (melysegek[i] == 0)
				{
					godor_veg = i - 1;
					break;
				}
			}

			for (int i = tavolsag - 1; i > 0; --i)
			{
				if (melysegek[i] == 0)
				{
					godor_kezdet = i + 1;
					break;
				}
			}

			Console.WriteLine("A gödör kezdete: {0} méter, a gödör vége: {1} méter.", godor_kezdet, godor_veg);
		}

		static void feladat6b()
		{
			Console.WriteLine("b)");

			bool folyamatosanMelyul = true;

			bool monotonMelyul = true;
			for (int i = godor_kezdet + 1; i <= godor_veg; ++i)
			{
				if (melysegek[i - 1] < melysegek[i] && monotonMelyul)
				{
					monotonMelyul = false;
				}

				if (melysegek[i - 1] > melysegek[i] && !monotonMelyul)
				{
					folyamatosanMelyul = false;
					break;
				}
			}

			if (folyamatosanMelyul)
				Console.WriteLine("Folyamatosan mélyül.");
			else
				Console.WriteLine("Nem mélyül folyamatosan.");
		}

		static void feladat6c()
		{
			Console.WriteLine("c)");

			int legmelyebb = 0;
			for (int i = godor_kezdet; i <= godor_veg; ++i)
				if (melysegek[i] > legmelyebb)
					legmelyebb = melysegek[i];

			Console.WriteLine("A legnagyobb mélysége {0} méter.", legmelyebb);
		}

		static void feladat6d()
		{
			Console.WriteLine("d)");

			const int szelesseg = 10;
			int ossz_melyseg = 0;

			for (int i = godor_kezdet; i <= godor_veg; ++i)
				ossz_melyseg += melysegek[i];

			Console.WriteLine("Térfogata {0} m^3.", ossz_melyseg * szelesseg);
		}

		static void feladat6e()
		{
			Console.WriteLine("e)");

			const int szelesseg = 10;
			int ossz_melyseg = 0;

			for (int i = godor_kezdet; i <= godor_veg; ++i)
				ossz_melyseg += melysegek[i] - 1;

			Console.WriteLine("A vízmennyiség {0} m^3.", ossz_melyseg * szelesseg);
		}
	}
}