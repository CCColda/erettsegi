using System;
using System.Collections.Generic;
using System.IO;

namespace tarsalgo
{
    internal class Atlepes {
        public int ora { get; }
        public int perc { get; }
        public int ember { get; }
        public string irany { get; }

        public Atlepes(int ora, int perc, int ember, string irany) {
            this.ora = ora;
            this.perc = perc;
            this.ember = ember;
            this.irany = irany;
        }
    }

    class Program
    {
        static List<Atlepes> atlepesek = new List<Atlepes>();
        static int szemely = 0;

        static void feladat1()
        {
            string[] sorok = File.ReadAllLines("ajto.txt");

            foreach (string sor in sorok)
            {
                string[] szavak = sor.Split(" ");

                atlepesek.Add(
                    new Atlepes(
                        int.Parse(szavak[0]),
                        int.Parse(szavak[1]),
                        int.Parse(szavak[2]),
                        szavak[3]
                    )
                );
            }
        }

        static void feladat2()
        {
            Console.WriteLine("2. feladat");

            for (int i = 0; i < atlepesek.Count; i += 1)
            {
                if (atlepesek[i].irany == "be")
                {
                    Console.WriteLine("Az első belépő: {0}", atlepesek[i].ember);
                    break;
                }
            }

            for (int i = atlepesek.Count - 1; i >= 0; i -= 1)
            {
                if (atlepesek[i].irany == "ki")
                {
                    Console.WriteLine("Az utolsó kilépő: {0}", atlepesek[i].ember);
                    break;
                }
            }
        }

        static void feladat3()
        {
            Dictionary<int, int> athaladasok = new Dictionary<int, int>();

            foreach (Atlepes lepes in atlepesek)
            {
                if (!athaladasok.ContainsKey(lepes.ember))
                    athaladasok[lepes.ember] = 1;
                else
                    athaladasok[lepes.ember] += 1;
            }

            List<int> keys = new List<int>(athaladasok.Keys);
            keys.Sort();

            string[] kimeneti_sorok = new string[keys.Count];

            for (int i = 0; i < keys.Count; i += 1)
                kimeneti_sorok[i] = $"{keys[i]} {athaladasok[keys[i]]}";

            File.WriteAllLines("athaladas.txt", kimeneti_sorok);
        }

        static void feladat4()
        {
            Console.WriteLine("4. feladat");

            HashSet<int> bent_levok = new HashSet<int>();

            foreach (Atlepes lepes in atlepesek)
            {
                if (lepes.irany == "be")
                    bent_levok.Add(lepes.ember);
                else
                    bent_levok.Remove(lepes.ember);
            }

            List<int> rendezett_bent_levok = new List<int>(bent_levok);
            rendezett_bent_levok.Sort();

            Console.WriteLine("A végén a társalgóban voltak: {0}", String.Join(" ", rendezett_bent_levok));
        }

        static void feladat5()
        {
            Console.WriteLine("5. feladat");

            int ora = 0;
            int perc = 0;
            int legtobb_fo = 0;

            int szamlalo = 0;

            foreach (Atlepes lepes in atlepesek) 
            {
                szamlalo += (lepes.irany == "be") ? 1 : (-1);

                if (szamlalo > legtobb_fo)
                {
                    legtobb_fo = szamlalo;
                    ora = lepes.ora;
                    perc = lepes.perc;
                }
            }

            Console.WriteLine("{0:00}:{1:00}-kor voltak a legtöbben a társalgóban.", ora, perc);
        }

        static void feladat6()
        {
            Console.WriteLine("6. feladat");
            Console.Write("Adja meg a személy azonosítóját! ");

            szemely = int.Parse(Console.ReadLine());
        }

        static void feladat7()
        {
            Console.WriteLine("7. feladat");

            int belepes_ora = 0;
            int belepes_perc = 0;
            foreach (Atlepes lepes in atlepesek)
            {
                if (lepes.ember == szemely)
                {
                    if (lepes.irany == "be")
                    {
                        belepes_ora = lepes.ora;
                        belepes_perc = lepes.perc;
                    }
                    else
                    {
                        Console.WriteLine("{0:00}:{1:00}-{2:00}:{3:00}", belepes_ora, belepes_perc, lepes.ora, lepes.perc);

                        belepes_ora = 0;
                        belepes_perc = 0;
                    }
                }
            }

            if (belepes_ora != 0 || belepes_perc != 0)
            {
                Console.WriteLine("{0:00}:{1:00}-", belepes_ora, belepes_perc);
            }
        }

        static void feladat8()
        {
            Console.WriteLine("8. feladat");

            int bent_toltott_percek = 0;

            int belepes_ora = 0;
            int belepes_perc = 0;
            foreach (Atlepes lepes in atlepesek)
            {
                if (lepes.ember == szemely)
                {
                    if (lepes.irany == "be")
                    {
                        belepes_ora = lepes.ora;
                        belepes_perc = lepes.perc;
                    }
                    else
                    {
                        bent_toltott_percek += (lepes.ora * 60 + lepes.perc) - (belepes_ora * 60 + belepes_perc);

                        belepes_ora = 0;
                        belepes_perc = 0;
                    }
                }
            }

            if (belepes_ora != 0 || belepes_perc != 0)
            {
                bent_toltott_percek += (15 * 60 + 0) - (belepes_ora * 60 + belepes_perc);
            }

            Console.Write("A(z) {0}. személy összesen {1} percet volt bent, a megfigyelés végén ", szemely, bent_toltott_percek);
            
            if (belepes_ora != 0 || belepes_perc != 0)
                Console.WriteLine("a társalgóban volt.");
            else
                Console.WriteLine("nem volt a társalgóban.");
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
            feladat8();
        }
    }
}
