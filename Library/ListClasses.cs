using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    [Serializable]
    public class ListClasses
    {
        public List<Book> Books { get; set; }
        public List<BookInLibrary> BooksInLibrary { get; set; }
        public List<HistoryBook> HistoryBooks { get; set; }
        public List<User> Users { get; set; }


        public ListClasses()
        {
            Books = new List<Book>();
            BooksInLibrary = new List<BookInLibrary>();
            HistoryBooks = new List<HistoryBook>();
            Users = new List<User>();
        }
    }
}
