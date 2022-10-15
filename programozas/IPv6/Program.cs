using System;
using System.IO;
using System.Collections.Generic;

namespace IPv6
{
    class Program
    {
        static List<string> adatok = new List<string>();
        static int szam = 0;

        static void feladat1()
        {
            foreach (string sor in File.ReadAllLines("ip.txt"))
            {
                adatok.Add(sor);
            }
        }

        static void feladat2()
        {
            Console.WriteLine("2. feladat");
            Console.WriteLine("Az állományban {0} darab adatsor van.", adatok.Count);
            Console.WriteLine();
        }

        static void feladat3()
        {
            Console.WriteLine("3. feladat");

            string legalacsonyabb = adatok[0];

            for (int i = 1; i < adatok.Count; ++i)
                if (adatok[i].CompareTo(legalacsonyabb) == (-1))
                    legalacsonyabb = adatok[i];

            Console.WriteLine("A legalacsonyabb tárolt IP-cím: {0}", legalacsonyabb);
            Console.WriteLine();
        }

        static void feladat4()
        {
            Console.WriteLine("4. feladat");

            int dokumentacios = 0,
                globalis = 0,
                helyi = 0;

            foreach (string cim in adatok)
                if (cim.StartsWith("2001:0db8"))
                    dokumentacios++;
                else if (cim.StartsWith("2001:0e"))
                    globalis++;
                else if (cim.StartsWith("fc") || cim.StartsWith("fd"))
                    helyi++;

            Console.WriteLine("Dokumentációs cím: {0} darab", dokumentacios);
            Console.WriteLine("Globális cím: {0} darab", globalis);
            Console.WriteLine("Helyi cím: {0} darab", helyi);
            Console.WriteLine();
        }

        static void feladat5()
        {
            using (StreamWriter kimenet = new StreamWriter("sok.txt"))
            {
                for (int i = 0; i < adatok.Count; ++i)
                {
                    int nullak_szama = 0;

                    foreach (char betu in adatok[i])
                        if (betu == '0')
                            nullak_szama ++;

                    if (nullak_szama >= 18)
                        kimenet.WriteLine("{0} {1}", i + 1, adatok[i]);
                }
            }
        }

        static void feladat6()
        {
            Console.WriteLine("6. feladat");

            Console.Write("Kérek egy sorszámot: ");
            szam = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine(adatok[szam]);

            string[] szegmensek = adatok[szam].Split(':');
            for (int i = 0; i < 8; ++i)
            {
                szegmensek[i] = Convert.ToInt32(szegmensek[i], 0x10).ToString("x");
            }

            Console.WriteLine(String.Join(':', szegmensek));
            Console.WriteLine();
        }

        static void feladat7()
        {
            Console.WriteLine("7. feladat");

            string[] szegmensek = adatok[szam].Split(':');
            int leghosszabbCsoport = 0;
            int leghosszabbCsoportIndex = 0;

            int csoport = 0;
            int csoportIndex = 0;

            for (int i = 0; i < 8; ++i)
            {
                if (szegmensek[i] == "0000")
                {
                    if (csoportIndex == 0)
                        csoportIndex = i;

                    csoport += 1;

                    if (leghosszabbCsoport < csoport) {
                        leghosszabbCsoport = csoport;
                        leghosszabbCsoportIndex = csoportIndex;
                    }
                }
                else
                {
                    csoport = 0;
                    csoportIndex = 0;
                }
            }

            List<string> kimenet = new List<string>();

            for (int i = 0; i < 8; ++i)
            {
                if (i < leghosszabbCsoportIndex || i >= leghosszabbCsoportIndex + leghosszabbCsoport)
                    kimenet.Add(Convert.ToInt32(szegmensek[i], 0x10).ToString("x"));
                else if (i == leghosszabbCsoportIndex)
                    kimenet.Add("");
            }

            Console.WriteLine(String.Join(':', kimenet));
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            feladat1();
            feladat2();
            feladat3();
            feladat4();
            feladat5();
            feladat6();
            feladat7();
        }
    }
}
