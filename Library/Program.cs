using System;
using System.Collections.Generic;
using System.Linq;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();
            library.InitializeDate();
            User thisUser = new User();

            Console.WriteLine("WELCOME in Library!");
            bool autorization = false;
            while (!autorization)
            {
                Console.WriteLine("1 - Login");
                Console.WriteLine("2 - Registg");

                int UserLog = int.Parse(Console.ReadLine());

                switch (UserLog)
                {
                    case 1:
                        {
                            Console.WriteLine("Input your name: ");
                            thisUser.UserName = Console.ReadLine();
                            Console.WriteLine("Input your surname: ");
                            thisUser.UserSurname = Console.ReadLine();
                            //Console.WriteLine("Input your birthday: ");

                            foreach (var user in library.ListClasses.Users)
                                if (user.UserName == thisUser.UserName && user.UserSurname == thisUser.UserSurname)
                                {
                                    thisUser = user;
                                    autorization = true;
                                    break;
                                }

                            break;
                        }
                    case 2:
                        {
                            library.AddUser();

                            break;
                        }
                }

            }




            while (true)
            {
                Console.WriteLine($"Hi {thisUser.UserName}! ");
                Console.WriteLine("\n\nOptions:");
                Console.WriteLine("0 - LogOut:");
                Console.WriteLine("1 - Books in Library:");//кактб придумать еще удаление и поиск возможно в одном case(типо в библиотеке)
                Console.WriteLine("2 - Take book:");//а тут тож б все в одном типо вначале можно пропустить вход/рег, потом зайти сюда и если мы хахотим взять книгу, то напоминание о том чтоб войти
                Console.WriteLine("3 - Return book:");
                Console.WriteLine("4 - History for book:");
                Console.WriteLine("5 - Remove book:");
                //Возможность обмена с другими пользователями

                int GlobalOptions = int.Parse(Console.ReadLine());
                switch (GlobalOptions)
                {
                    case 0: return;
                    case 1:
                        {
                            while (true)
                            {
                                Console.WriteLine("1 - Show books:");
                                Console.WriteLine("2 - Add new book:");
                                Console.WriteLine("3 - Add book in library:");
                                Console.WriteLine("4 - Change status book:");

                                int optionsInLibrary = int.Parse(Console.ReadLine());

                                if (optionsInLibrary == 0) break;

                                switch (optionsInLibrary)
                                {
                                    case 1:
                                        {
                                            //Вот сдесь бы сделать так чтобы было 2 варика(1 - книги которые в библиотеке на данный момент
                                            //                                              2 - книги которые арендваны)


                                            Console.WriteLine("All books in Library:\n");

                                            var booksInLibrary = library.ListClasses.BooksInLibrary;
                                            var books = library.ListClasses.Books;
                                            var historyBooks = library.ListClasses.HistoryBooks;


                                            var booksGrouped = booksInLibrary.GroupBy(x => x.IDBook)
                                                .Select(g => new
                                                {
                                                    TemplateID = g.Key,
                                                    BookCount = g.Count(),
                                                    NotReturnedCount = g
                                                .Join(historyBooks, book => book.BookInLibraryID, history => history.IDBookInLibrary, (book, history) => history)
                                                .Count(history => !history.BookisRet)
                                                });

                                            Console.WriteLine("Books in Library:");
                                            foreach (var groupBook in booksGrouped)
                                            {
                                                foreach (var book in books)
                                                {
                                                    if (book.BookId == groupBook.TemplateID)
                                                        Console.WriteLine($"{book.BookName} | Total: {groupBook.BookCount} | Not Returned: {groupBook.NotReturnedCount}");
                                                }
                                            }


                                            Console.WriteLine("See details for every book? (Y/N)");
                                            string seeDetails = Console.ReadLine();
                                            if (seeDetails == "Y")
                                            {
                                                foreach (var bookInLib in booksInLibrary)
                                                {
                                                    Book book = books.FirstOrDefault(b => b.BookId == bookInLib.IDBook);
                                                    if (book != null)
                                                    {
                                                        Console.WriteLine($"{bookInLib.BookInLibraryID} {book.BookName}");

                                                        List<HistoryBook> allHistoryForThisIdBookInLib = historyBooks.Where(h => h.IDBookInLibrary == bookInLib.BookInLibraryID).ToList();
                                                        if (allHistoryForThisIdBookInLib.Count > 0)
                                                        {
                                                            foreach (var historyBook in allHistoryForThisIdBookInLib)
                                                            {
                                                                if (historyBook != null)
                                                                {
                                                                    historyBook.PrintInfo();
                                                                }

                                                            }
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("No history available for this book.");
                                                        }
                                                    }
                                                    Console.WriteLine("----------------");
                                                }
                                            }


                                            //можно выбрать книгу по id-iteration и посмотреть ее историю(во, а еще лучше чтоб для подробного скана книг там показывали для каждой и историю ее, но можно сделать и отдельно)

                                            break;
                                        }
                                    case 2:
                                        {
                                            library.NewBook();

                                            break;
                                        }
                                    case 3:
                                        {
                                            //если книга не доступна например
                                            int i = 1;
                                            Book thisBook = new Book();
                                            foreach (Book book in library.ListClasses.Books)
                                            {
                                                Console.Write("Select number book: " + i);
                                                book.PrintInfoBook();
                                                i++;
                                            }
                                            if (i != 1)
                                            {
                                                Console.Write("Input selected number of book: ");
                                                int selectedBookPos = int.Parse(Console.ReadLine());
                                                thisBook = library.ListClasses.Books[selectedBookPos - 1]; //
                                                if (thisBook.isStatusBook)
                                                {
                                                    Console.Write("Input how many book will be added: ");
                                                    int willBeAddedCount = int.Parse(Console.ReadLine());

                                                    for (int j = 0; j < willBeAddedCount; j++)
                                                        library.AddBookInLibrary(thisBook); //

                                                    if (willBeAddedCount > 0)
                                                        Console.WriteLine("Book was added in Library");
                                                }
                                                else
                                                    Console.WriteLine("Y can't added this book in lib");
                                            }

                                            break;
                                        }
                                    case 4:
                                        {
                                            int i = 1;
                                            Book thisBook = new Book();
                                            foreach (Book book in library.ListClasses.Books)
                                            {
                                                Console.Write("Select number book: " + i);
                                                book.PrintInfoBook();
                                                i++;
                                            }
                                            if (i != 1)
                                            {
                                                Console.Write("Input selected number of book: ");
                                                int selectedBookPos = int.Parse(Console.ReadLine());
                                                thisBook = library.ListClasses.Books[selectedBookPos - 1]; //
                                                library.StatusForBook(thisBook);
                                            }

                                            break;
                                        }
                                    default: break;
                                }


                            }

                            break;
                        }
                    case 2:
                        {
                            library.TakeBook(thisUser);

                            break;
                        }
                    case 3:
                        {
                            library.ReturnBook(thisUser);
                            break;
                        }
                    case 4:
                        {
                            foreach (var historyBooks in library.ListClasses.HistoryBooks)
                                historyBooks.PrintInfo();

                            break;
                        }
                    case 5:
                        {
                            Console.WriteLine("1 - Remove book");
                            Console.WriteLine("2 - Remove book in library");
                            int removeOption = int.Parse(Console.ReadLine());
                            switch (removeOption)
                            {
                                case 1:
                                    {
                                        library.RemoveFullBook();

                                        break;
                                    }
                                case 2:
                                    {
                                        var booksGrouped = library.ListClasses.BooksInLibrary.GroupBy(x => x.IDBook).Select(g => new { TemplateID = g.Key, BookCount = g.Count() });

                                        foreach(var books in booksGrouped)
                                            foreach(var book in library.ListClasses.Books)
                                                if(books.TemplateID == book.BookId)
                                                    Console.WriteLine(book.BookName + " " + books.BookCount);
                                        Console.WriteLine();
                                        library.RemoveBookFromLibrary();

                                        break;
                                    }
                                default:
                                    break;
                            }
                            break;
                        }
                    default:
                        break;
                }

            }
            Console.WriteLine("Close");
        }
    }
}
