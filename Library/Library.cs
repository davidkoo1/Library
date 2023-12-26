using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{

    public class Library
    {
        public ListClasses ListClasses;
        public void InitializeDate()
        {
            Json.Provide(out ListClasses listClassesObject);
            ListClasses = listClassesObject;
        } 


        private int GetNewUserId()
        {
            return ListClasses.Users.Count > 0 ? ListClasses.Users.Max(x => x.UserID) + 1 : 1;
        }
        public void AddUser()
        {
            User newUser = new User();
            Console.WriteLine("Input name: ");
            newUser.UserName = Console.ReadLine();
            Console.WriteLine("Input surname: ");
            newUser.UserSurname = Console.ReadLine();
            Console.WriteLine("Input Adres: ");
            newUser.UserAdres = Console.ReadLine();
            Console.WriteLine("Input Birthday(dd-mm-yy): ");
            newUser.UserBirthDay = DateTime.Parse(Console.ReadLine());

            newUser.UserID = GetNewUserId();

            ListClasses.Users.Add(newUser);
            Json.WriteDown(ListClasses);
        }

        private int GetNewBookId()
        {
            return ListClasses.Books.Count > 0 ? ListClasses.Books.Max(x => x.BookId) + 1 : 1;
        }
        public void NewBook()
        {
            Book newBook = new Book();
            Console.Write("\nWrite name for new book: ");
            newBook.BookName = Console.ReadLine();
            Console.Write("Who author for new book: ");
            newBook.BookAuthor = Console.ReadLine();
            Console.Write("Write genre for new book: ");
            newBook.BookGenre = Console.ReadLine();

            newBook.BookId = GetNewBookId();
            newBook.isStatusBook = true;

            ListClasses.Books.Add(newBook);
            Json.WriteDown(ListClasses);
            Console.WriteLine("Book was added");
        }

        private int GetNewBookinLibraryId()
        {
            return ListClasses.BooksInLibrary.Count > 0 ? ListClasses.BooksInLibrary.Max(x => x.BookInLibraryID) + 1 : 1;
        }
        public void AddBookInLibrary(Book thisBook)
        {
            BookInLibrary addBookInLibrary = new BookInLibrary();
            addBookInLibrary.BookInLibraryID = GetNewBookinLibraryId();
            addBookInLibrary.IDBook = thisBook.BookId;

            ListClasses.BooksInLibrary.Add(addBookInLibrary);
            Json.WriteDown(ListClasses);

        }

        public void TakeBook(User user)
        {
            Book searchBook = new Book();
            Console.WriteLine("Input book name: ");
            searchBook.BookName = Console.ReadLine();
            Console.WriteLine("Input book author: ");
            searchBook.BookAuthor = Console.ReadLine();
            Console.WriteLine("Input book genre: ");
            searchBook.BookGenre = Console.ReadLine();

            Book selectedBook = ListClasses.Books.FirstOrDefault(b => b.BookName == searchBook.BookName &&
                                                b.BookAuthor == searchBook.BookAuthor &&
                                                b.BookGenre == searchBook.BookGenre);

            BookInLibrary bookInLibrary = ListClasses.BooksInLibrary.FirstOrDefault(b => b.IDBook == selectedBook.BookId &&
                                                                 !ListClasses.HistoryBooks.Any(hb => hb.IDBookInLibrary == b.BookInLibraryID &&
                                                                                         !hb.BookisRet));


            if (bookInLibrary != null)
            {
                HistoryBook takeBook = new HistoryBook();
                takeBook.IDUser = user.UserID;
                takeBook.IDBookInLibrary = bookInLibrary.BookInLibraryID;
                takeBook.DataBookTake = DateTime.Now.ToString();
                takeBook.DataBookRet = null;

                ListClasses.HistoryBooks.Add(takeBook);
                Json.WriteDown(ListClasses);
            }
            else
                Console.WriteLine("Все книги такого типа в данный момент кем-то заняты");
        }

        public void ReturnBook(User thisUser)
        {
            
            List<HistoryBook> rentedBooks = ListClasses.HistoryBooks.Where(x => x.IDUser == thisUser.UserID && !x.BookisRet).ToList();
            if (rentedBooks.Count > 0)
            {
                int i = 1;
                foreach (var rentedBook in rentedBooks)
                {

                    BookInLibrary bookInLibrary = ListClasses.BooksInLibrary.FirstOrDefault(x => x.BookInLibraryID == rentedBook.IDBookInLibrary);
                    if (bookInLibrary != null)
                    {
                        Book book = ListClasses.Books.FirstOrDefault(x => x.BookId == bookInLibrary.IDBook);
                        if (book != null)
                        {
                            string bookName = book.BookName;
                            Console.WriteLine($"{i} Book Name: {bookName} " + rentedBook.DataBookTake + " " + rentedBook.BookisRet);
                            i++;
                        }
                    }

                }

                Console.WriteLine("Select number what book u want returned");
                int selectNumberForReturnBook = int.Parse(Console.ReadLine());
                HistoryBook selected = rentedBooks[selectNumberForReturnBook - 1];
                selected.DataBookRet = DateTime.Now.ToString();
                Json.WriteDown(ListClasses);
            }
            //////////
        }

        public void StatusForBook(Book thisBookStatusChang)
        {

            thisBookStatusChang.isStatusBook = !thisBookStatusChang.isStatusBook;
            Json.WriteDown(ListClasses);
        }

        public void RemoveFullBook()
        {
            foreach(var book in ListClasses.Books)
                Console.WriteLine(book.BookName);

            Console.WriteLine("Input name of the book to delete: ");
            string bookNameForRemove = Console.ReadLine();
            Book delBook = ListClasses.Books.FirstOrDefault(x => x.BookName == bookNameForRemove);

            if (delBook != null)
            {
                ListClasses.Books.Remove(delBook);

                var books = ListClasses.BooksInLibrary.Where(y => y.IDBook == delBook.BookId).ToList();
                foreach(var book in books)
                    ListClasses.HistoryBooks.RemoveAll(x => x.IDBookInLibrary == book.BookInLibraryID);

                ListClasses.BooksInLibrary.RemoveAll(x => x.IDBook == delBook.BookId);

                Json.WriteDown(ListClasses);
            }
            else
            {
                Console.WriteLine("Book not found.");
            }

        }
        public void RemoveBookFromLibrary()
        {

            Book searchBook = new Book();
            Console.WriteLine("Input book name: ");
            searchBook.BookName = Console.ReadLine();
            Console.WriteLine("Input book author: ");
            searchBook.BookAuthor = Console.ReadLine();
            Console.WriteLine("Input book genre: ");
            searchBook.BookGenre = Console.ReadLine();

            searchBook = ListClasses.Books.FirstOrDefault(b => b.BookName == searchBook.BookName &&
                                                b.BookAuthor == searchBook.BookAuthor &&
                                                b.BookGenre == searchBook.BookGenre);
            var books = ListClasses.BooksInLibrary.Where(x => x.IDBook == searchBook.BookId).ToList();
            int i = 1;
            foreach (var book in books)
            {
                Console.WriteLine(i + " " + searchBook.BookName + " " + book.BookInLibraryID);
                i++;
            }
            Console.WriteLine("Input selected book number: ");
            int selectedIndex = int.Parse(Console.ReadLine());

            BookInLibrary selectedRemoveBook = books[selectedIndex - 1];

            ListClasses.BooksInLibrary.Remove(selectedRemoveBook);
            ListClasses.HistoryBooks.RemoveAll(x => x.IDBookInLibrary == selectedRemoveBook.BookInLibraryID);

            Json.WriteDown(ListClasses);
        }

        public void SearchBook()//Тут бы я насамом деле бы хотел сделать так чтоб..не важно что я вводил мне искали по всем полям
        {

        }

    }
}
