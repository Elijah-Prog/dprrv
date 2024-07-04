using Microsoft.EntityFrameworkCore;

namespace ManningBooksApi;

// CatalogContext adapted to work in the ManningBooksApi project

public class CatalogContext : DbContext
{
  public const string ConnectionString =
    "DataSource=manningbooks;mode=memory;cache=shared";

  public DbSet<Book> Books { get; set; }
  public DbSet<Rating> Ratings {get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options
      .UseSqlite(ConnectionString);

  public static void SeedBooks()
  {
    using var dbContext = new CatalogContext();
    dbContext.Database.EnsureCreated();
    dbContext.Add(new Book("Grokking Simplicity"));
    dbContext.Add(new Book("API Design Patterns"));
    var efBook = new Book("EF Core in Action");
    efBook.Ratings.Add(new Rating { Comment = "Great!" });
    efBook.Ratings.Add(new Rating { Stars = 4 });
    dbContext.Add(efBook);
    dbContext.SaveChanges();
  }
}
