namespace ManningBooks;

public class Book{
    public int Id {get; set;}
    public string Title {get; set;}

    //  Add ratings to the Book class
    public List<Rating> Ratings 
        {get; } 
        = new();

    //  Book constructor requires non-null string title
    public Book(string title){
        Title = title;
    }
}