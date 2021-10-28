//pilotak.csv  http://www.infojegyzet.hu/vizsgafeladatok/okj-programozas/szoftverfejleszto-210209/
//név;születési_dátum;nemzetiség;rajtszám
//Lewis Hamilton;1985.01.07;brit;44
//Nick Heidfeld;1977.05.10;német;
using System;                     // Console
using System.IO;                  // StreamReader
using System.Text;                // Encoding
using System.Collections.Generic; // List<>, Dictionary<>
using System.Linq;                // from where select

class Pilota{
    public string név               {get; private set;}
    public string születési_dátum   {get; private set;}
    public string nemzetiség        {get; private set;}
    public int rajtszám             {get; private set;}
    public int év                   {get; private set;}

    public Pilota(string sor){
        var s = sor.Split(';');
        név             = s[0];
        születési_dátum = s[1];
        nemzetiség      = s[2];
        rajtszám        = (s[3].Length>0) ?  int.Parse(s[3]): 0;  
        év              = int.Parse( s[1].Substring(0,4));
    }
}

class Program {
    public static void Main (string[] args) {
        var lista = new List<Pilota>();              
        
        var f =  new StreamReader("pilotak.csv", Encoding.UTF8); 
        var elsosor = f.ReadLine();                   
        
        while (!f.EndOfStream){             
		    lista.Add(  new Pilota(f.ReadLine())  );                       
		}
        f.Close();                          

        // 3. feladat: Dolgozók száma: ? 
        Console.WriteLine($"3. feladat: {lista.Count} fő");

        // 4. feladat:
        var utolso_nev = lista.Last().név;
        Console.WriteLine($"4. feladat: {utolso_nev}");

        // 5. feladat:
        var xix = ( 
            from sor in lista 
            where sor.év < 1901 
            select (sor.név, sor.születési_dátum) 
            );
        Console.WriteLine(    $"5. feladat: ");
        foreach(var sor in xix){
            Console.WriteLine($"        {sor.név} ({sor.születési_dátum}.) ");
        }
        // 6. feladat legkisebb rajtszám pilótájának nemzetisége    
        var nemzetiség = ( 
            from sor in lista 
            where sor.rajtszám > 0  
            orderby sor.rajtszám 
            select sor.nemzetiség
            ).First();
        Console.WriteLine( $"6. feladat: {nemzetiség}");
        
        // 7. feladat
        var query = (
            from sor in lista 
            where sor.rajtszám > 0
            group sor by sor.rajtszám 
            into res
            where res.Count() > 1 
            select res.Key);

        Console.Write($"7. feladat:  ");
        foreach(var i in query){        
            Console.Write($" {i} ");
        }
        Console.WriteLine();
    }
}
