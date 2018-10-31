using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BookingAppStore.Models
{
    public class BookContext: DbContext
    {
        public DbSet<Book> Books { get; set; } // Если модель называется Book То свойство должно назваться во множественном числе
        public DbSet<Purchase> Purchases { get; set; }
    }

    public class BookDbInitializer: DropCreateDatabaseAlways<BookContext>
    {
        protected override void Seed(BookContext db)
        {
            db.Books.Add(new Book { Name = "Hi Bro", Author = "Arman Z", Pice = 100 });
            db.Books.Add(new Book { Name = "Viski hvatit", Author = "Garik E", Pice = 200 });
            db.Books.Add(new Book { Name = "Brat", Author = "Rach M", Pice = 300 });

            base.Seed(db);
        }
    }
}