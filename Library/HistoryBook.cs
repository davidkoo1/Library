using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    [Serializable]
    public class HistoryBook
    {
        public int IDUser { get; set; }
        public int IDBookInLibrary { get; set; }
        public string DataBookTake { get; set; }
        public string DataBookRet { get; set; }
        public bool BookisRet
        {
            get
            {
                if (string.IsNullOrWhiteSpace(DataBookRet)) return false;
                return true;
            }
        }

        //метод который следит не прошло ли 5 месяцов (в разработке, еще думаю)
        public bool MissingBook()
        {
            bool isMissing = false;

            return isMissing;
        }

        public void PrintInfo()
        {
            Console.ResetColor();
            if (BookisRet)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
                Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($"{IDUser}, {IDBookInLibrary}, {DataBookTake}, {DataBookRet}, {BookisRet}");
            Console.ResetColor();

        }
    }
}
