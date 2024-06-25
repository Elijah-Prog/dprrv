using ManningBooks;

using Microsoft.EntityFrameworkCore; 

/*The DbContext, or database context, is EF Core’s 
way of knowing about entities and the relationships between them.
*/

using var dbContext = new CatalogContext();

//code to test out the relationship between Book and Rating

var efBook = new Book("EF Core in Action");
efBook.Ratings.Add(new Rating { Comment = "Great!" });
efBook.Ratings.Add(new Rating { Stars = 4 });
dbContext.Add(efBook);
dbContext.SaveChanges();

var efRatings = (from b in dbContext.Books
                 where b.Title == "EF Core in Action"
                 select b.Ratings
                ).FirstOrDefault();

foreach (var book in dbContext.Books
           .Include(b => b.Ratings))
{
  Console.WriteLine($"Book \"{book.Title}\" has id {book.Id}");
  book.Ratings.ForEach(r =>
    Console.WriteLine(
    $"\t{r.Stars} stars: {r.Comment ?? "-blank-"}"));
}