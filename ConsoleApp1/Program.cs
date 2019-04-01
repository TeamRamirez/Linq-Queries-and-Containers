using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Type in one of the following commands:");
            Console.WriteLine("SimpleLinq");
            Console.WriteLine("ListArtists");
            Console.WriteLine("ListArtistsAlbum");

            prompt();
        }

        static void prompt()
        {
            Console.Write("Please enter a command: ");
            string command = Console.ReadLine();

            if (command == "SimpleLinq")
                simpleLinq();
            else if (command == "ListArtists")
                listArtists();
            else if (command == "ListArtistsAlbum")
                listArtistsAlbum();
            else if (command == "1")
                findArtistsAlbum();
            else if (command == "2")
                arrayList();
            else if (command == "3")
                tList();

        }

        static void simpleLinq()
        {
            List<int> intArray = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            int intSum = intArray.Sum();
            Console.WriteLine($"Simple Sum of int array: {intSum.ToString()}");

            var intSumOddOnly = intArray.Where(x => x % 2 == 1);
            Console.WriteLine($"Sum only Odd Numbers of int array: {intSumOddOnly.Sum().ToString()}");

            intArray[1] = 201;
            intSumOddOnly = intArray.Where(x => x % 2 == 1);
            Console.WriteLine($"Re-evaluate Sum only Odd Numbers of int array: {intSumOddOnly.Sum().ToString()}");

            int intDoubleSum = intArray.Select(x => x * 2).Sum();
            Console.WriteLine($"Doubling the Sum of All Numbers of int array: {intDoubleSum.ToString()}");

            int intSelectX2AndMin = intArray.Select(x => x * 10).Min();
            Console.WriteLine($"Selecting Min Value and Multiplying by 2 of int array: {intSelectX2AndMin.ToString()}");
        }


        static void listArtists()
        {
            using (var db = new chinookContext())
            {
                //List of Artists
                Console.WriteLine("===List of Artists===");
                var artists = db.Artists.Select(x => x.Name).ToList();
                foreach (string artist in artists)
                {
                    Console.WriteLine($"Name: {artist}");
                }
            }
        }

        static void listArtistsAlbum()
        {
            using (var db = new chinookContext())
            {
                //List of Artists
                Console.WriteLine("===List of Artists and Albums===");
                //var artistsAlbums = db.Artists.Join(db.Albums, x => x.ArtistId, y => y.ArtistId, (x, y) => x).ToList();

                var artistsAlbums = (from art in db.Artists
                                     join alb in db.Albums on art.ArtistId equals alb.ArtistId
                                     select new { art.Name, alb.Title })
                                     .OrderBy(x => x.Name).ThenBy(y => y.Title);

                // var aa = artistsAlbums;

                foreach (var list in artistsAlbums)
                {
                    Console.WriteLine($"Name: {list.Name} - Album: {list.Title}");
                }

                prompt();

            }
        }



        static void findArtistsAlbum()
        {
            using (var db = new chinookContext())
            {
                Console.WriteLine("Enter In Part Of The Name of the Artist: ");

                string name = Console.ReadLine();

                Console.WriteLine("===List of Artists and Albums===");

                var artistsAlbums = (from art in db.Artists
                                     join alb in db.Albums on art.ArtistId equals alb.ArtistId
                                     select new { art.Name, alb.Title })
                                     .Where(i => i.Name.ToLower().Contains(name.ToLower()))
                                     .OrderBy(x => x.Name)
                                     .ThenBy(y => y.Title);

                // var aa = artistsAlbums;

                foreach (var list in artistsAlbums)
                {
                    Console.WriteLine($"Name: {list.Name} - Album: {list.Title}");
                }

                prompt();

            }

        }

        static void arrayList()
        {
            //Single dimensional array that can contain pretty much anything
            //ArrayList = ArrayList <- this is what I prefer
            //IList = ArrayList
            //ICollection = ArrayList
            //IEnumerable = ArrayList

            ArrayList arrayList1 = new ArrayList();
            arrayList1.Add(1);
            arrayList1.Add("Two");
            arrayList1.Add(3);
            arrayList1.Add(4);
            arrayList1.Add(4.5);

            Console.WriteLine($"ArrayList1: {string.Join(", ", arrayList1.ToArray())}");

            IList arrayList2 = new ArrayList() { 100, 200, 300, 400};

            Console.WriteLine($"ArrayList2: {string.Join(", ", ((ArrayList)arrayList2).ToArray())}");

            arrayList1.AddRange(arrayList2);
            
            Console.WriteLine($"ArrayList1 with ArrayList2: {string.Join(", ", arrayList1.ToArray())}");

            ArrayList arrayList3 = new ArrayList();
            arrayList3.Add(500);
            arrayList3.Add(600);
            arrayList3.Add(700);
            arrayList3.Add(800);

            arrayList3.InsertRange(0, arrayList2);//0 = where to start arrayList2 in arrayList3

            Console.WriteLine($"ArrayList3 with ArrayList2: {string.Join(", ", arrayList3.ToArray())}");

            prompt();
        }

        static void tList()
        {
            //https://www.tutorialsteacher.com/csharp/csharp-list

            IList<int> intList = new List<int>() { 1,2,3,4,5,6 };
            intList.Add(7);
            intList.Add(8);
            intList.Add(9);
            intList.Add(10);

            Console.WriteLine($"List<int>: {string.Join(", ", intList.ToArray())}");

            IList<string> stringList = new List<string>() { "one", "two", "three" };
            stringList.Add("Four");
            stringList.Add("Five");
            stringList.Add("Six");

            Console.WriteLine($"List<string>: {string.Join(", ", stringList.ToArray())}");

            prompt();
        }
    }
}
