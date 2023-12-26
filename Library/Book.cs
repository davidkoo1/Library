using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    [Serializable]
    public class Book
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string BookAuthor { get; set; }
        public string BookGenre { get; set; } //enum(Боевик, драма, литература)->предлагать список
        public bool isStatusBook { get; set; }

        public void PrintInfoBook()
        {
            Console.WriteLine("\nName: " + BookName);
            Console.WriteLine("Author: " + BookAuthor);
            Console.WriteLine("Genre: " + BookGenre);
            Console.WriteLine("Status: " + isStatusBook);
            Console.WriteLine("-----------------------------------------------");
        }
    }
}
