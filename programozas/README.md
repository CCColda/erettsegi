## Programozás feladatok

### Cheat sheet
- `main` alap
```cs
namespace projekt_neve {
    class Program {
        static void Main(string[] args) {    

        }
    }
}
```

- fix lista (fixed size array)
```cs
int[] lista = new int[10];
int[] lista2 = new int[5] {1, 2, 3, 4, 5};
```

- többdimenziós tömb (multidimensional fixed size array)
```cs
int[,] tomb_2d = new int[4, 5];
int[,,] tomb_3d = new int[4, 5, 6];
```

- jagged array
```cs
int[][] jagged_array = new int[3] {
    new int[2],
    new int[6],
    new int[1]
};
```

- lista
```cs
using System.Collections.Generic;

List<int> lista = new ();

int hossz = lista.Count;
int[] tomb = lista.ToArray();
```

- fájl beolvasása
```cs
using System.IO;

string[] lines = File.ReadLines("eleresi/ut.txt");
```

- fájl írása
```cs
using System.IO;

string[] adatok = {"sor1", "sor2", "sor3"};
File.WriteAllLines("eleresi/ut.txt", adatok);
```

- formázás
```cs
Console.WriteLine("Abc {0:00.00} {1} {0}", 28.9721, "hello");
```

- primitívek átalakítása
```cs
int egesz = 8;
double lebegopontos = (double)egesz;

int egesz_szovegbol = int.Parse("92");
```

- `foreach`
```cs
int[] szamok = {1, 2, 3};

foreach (int szam in szamok) {

}
```

- halmazok (`HashSet`)
```cs
using System.Collections.Generic;

HashSet<int> halmaz = new HashSet<int>();
halmaz.Add(8);
halmaz.Add(2);
halmaz.Add(1);
halmaz.Remove(2);

// listává: new List<int>(halmaz)
```

- asszociatív tömbök (`Dictonary`)
```cs
Dictionary<string, int> dict = new Dictionary<string, int>();

dict["asd"] = 9;

if (dict.ContainsKey("asd")) { }

// listává: new List<string>(dict.Keys);
// listává: new List<int>(dict.Values);
```

- listaösszefűzés (`Join`)
```cs
List<int> lista = new List<int>(new int[]{1, 2, 3});

string szoveg = String.Join(" ", lista);
```