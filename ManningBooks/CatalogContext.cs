using Microsoft.EntityFrameworkCore;

namespace ManningBooks;

/* CatalogContext is a DbContext that holds the Book class 
{EF Core will only know that a class represents an entity 
and relates to a table if it’s part of a DbContext. The DbContext, 
or database context, is EF Core’s way of knowing about entities and the relationships between them.}
*/
public class CatalogContext : DbContext
{

    public const string ConnectionString =
        "DataSource=manningbooks;mode=memory;cache=shared";
    public DbSet<Book> Books { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite(ConnectionString);


    // Static method to seed Books in the database in CatalogContext
    public static void SeedBooks()
    {
        using var dbContext = new CatalogContext();
        if (!dbContext.Database.EnsureCreated())
        {
            return;
        }

        dbContext.Add(new Book("Grokking Simplicity"));
        dbContext.Add(new Book("API Design Patterns"));
        var efBook = new Book("EF Core in Action");
        efBook.Ratings.Add(new Rating { Comment = "Great!" });
        efBook.Ratings.Add(new Rating { Stars = 4 });
        dbContext.Add(efBook);
        dbContext.SaveChanges();
    }

    // Async static method to find a Book by its title and write its data to the console

    public static async Task
      WriteBookToConsoleAsync(string title)
    {
        using var dbContext = new CatalogContext();
        var book = await dbContext.Books
          .Include(b => b.Ratings)
          .FirstOrDefaultAsync(b => b.Title == title);
        if (book == null)
        {
            Console.WriteLine(@$"""{title}"" not found.");
        }
        else
        {
            Console.WriteLine(@$"Book ""{book.Title}"" has id {book.Id}");
            book.Ratings.ForEach(r =>
              Console.WriteLine(
              $"\t{r.Stars} stars: {r.Comment ?? "-blank-"}"));
        }
    }


}