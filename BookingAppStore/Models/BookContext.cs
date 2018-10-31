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
            db.Books.Add(new Book { Name = "Hi Bro", Author = "Arman Z", Price = 100 });
            db.Books.Add(new Book { Name = "Viski hvatit", Author = "Garik E", Price = 200 });
            db.Books.Add(new Book { Name = "Brat", Author = "Rach M", Price = 300 });

            base.Seed(db);
        }
    }
}