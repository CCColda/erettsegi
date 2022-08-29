using System;
using System.Collections.Generic;
using System.IO;

namespace sudoku
{
    internal class Utasitas
    {
        public int szam { get; }
        public int sor { get;  }
        public int oszlop { get; }

        public Utasitas(int szam, int sor, int oszlop)
        {
            this.szam = szam;
            this.sor = sor;
            this.oszlop = oszlop;
        }
    };
    class Program
    {
        private static string fajl_nev;
        private static int sor;
        private static int oszlop;

        private static int[][] adatbazis = new int[9][];
        private static List<Utasitas> utasitasok = new List<Utasitas>();

        static void Main(string[] args)
        {
            feladat1();
            feladat2();
            feladat3();
            feladat4();
            feladat5();
        }

        static void feladat1()
        {
            Console.WriteLine("1. feladat");

            Console.Write("Adja meg a bemeneti fájl nevét! ");
            fajl_nev = Console.ReadLine();

            Console.Write("Adja meg egy sor számát! ");
            sor = int.Parse(Console.ReadLine());

            Console.Write("Adja meg egy oszlop számát! ");
            oszlop = int.Parse(Console.ReadLine());
            
            Console.WriteLine();
        }
    
        static void feladat2()
        {
            string[] fajl_sorok = File.ReadAllLines(fajl_nev);
            
            for (int i = 0; i < 9; ++i)
            {
                string[] szamok = fajl_sorok[i].Split(" ");
                adatbazis[i] = new int[9];

                for (int j = 0; j < 9; ++j)
                {
                    adatbazis[i][j] = int.Parse(szamok[j]);
                }
            }

            for (int i = 9; i < fajl_sorok.Length; ++i)
            {
                string[] utasitas_szamok = fajl_sorok[i].Split(" ");

                utasitasok.Add(new Utasitas(
                    int.Parse(utasitas_szamok[0]),
                    int.Parse(utasitas_szamok[1]),
                    int.Parse(utasitas_szamok[2])
                ));
            }
            
        }

        static void feladat3()
        {
            Console.WriteLine("3. feladat");
            feladat3a();
            feladat3b();
            Console.WriteLine();
        }

        static void feladat3a()
        {
            if (adatbazis[sor - 1][oszlop - 1] == 0)
            {
                Console.WriteLine("Az adott helyet még nem töltötték ki.");
            }
            else
            {
                Console.WriteLine($"Az adott helyen szereplő szám: {adatbazis[sor][oszlop]}");
            }
        }

        static void feladat3b()
        {
            int resztablazat = ((sor - 1) / 3 * 3 + (oszlop - 1) / 3) + 1;
            Console.WriteLine($"A hely a(z) {resztablazat} résztáblázathoz tartozik.");
        }

        static void feladat4()
        {
            Console.WriteLine("4. feladat");

            float nullak_szama = 0.0f;
            foreach (int[] sor in adatbazis)
            {
                foreach (int szam in sor)
                {
                    if (szam == 0)
                    {
                        nullak_szama += 1.0f;
                    }
                }
            }

            float nullak_aranya = (nullak_szama / 81.0f) * 100.0f;

            Console.WriteLine($"Az üres helyek aránya: {nullak_aranya.ToString("00.0")}%");
            Console.WriteLine();
        }

        static bool sorbanKeres(int sor, int keresett_szam)
        {
            foreach (int szam in adatbazis[sor - 1])
            {
                if (szam == keresett_szam)
                {
                    return true;
                }
            }

            return false;
        }

        static bool oszlopbanKeres(int oszlop, int keresett_szam)
        {
            foreach (int[] sor in adatbazis)
            {
                if (sor[oszlop - 1] == keresett_szam)
                {
                    return true;
                }
            }

            return false;
        }

        static bool resztablabanKeres(int sor, int oszlop, int keresett_szam)
        {
            int kezdo_sor = (sor - 1) - ((sor - 1) % 3);
            int kezdo_oszlop = (oszlop - 1) - ((oszlop - 1) % 3);

            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    if (adatbazis[i+kezdo_sor][j+kezdo_oszlop] == keresett_szam)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        static void feladat5()
        {
            Console.WriteLine("5. feladat");

            foreach(Utasitas utasitas in utasitasok)
            {
                Console.WriteLine($"A kiválasztott sor: {utasitas.sor} oszlop: {utasitas.oszlop} a szám: {utasitas.szam}");

                if (adatbazis[utasitas.sor - 1][utasitas.oszlop - 1] != 0)
                {
                    Console.WriteLine("A helyet már kitöltötték.");
                    Console.WriteLine();
                    continue;
                }

                if (sorbanKeres(utasitas.sor, utasitas.szam))
                {
                    Console.WriteLine("Az adott sorban már szerepel a szám");
                    Console.WriteLine();
                    continue;
                }

                if (oszlopbanKeres(utasitas.oszlop, utasitas.szam))
                {
                    Console.WriteLine("Az adott oszlopban már szerepel a szám");
                    Console.WriteLine();
                    continue;
                }

                if (resztablabanKeres(utasitas.sor, utasitas.oszlop, utasitas.szam))
                {
                    Console.WriteLine("Az adott résztáblázatban már szerepel a szám");
                    Console.WriteLine();
                    continue;
                }

                Console.WriteLine("A lépés megtehető");
                Console.WriteLine();
            }
        }
    }
}
