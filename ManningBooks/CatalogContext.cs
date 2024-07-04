using Microsoft.EntityFrameworkCore;

namespace ManningBooks;

/* CatalogContext is a DbContext that holds the Book class 
{EF Core will only know that a class represents an entity 
and relates to a table if it’s part of a DbContext. The DbContext, 
or database context, is EF Core’s way of knowing about entities and the relationships between them.}
*/
public class CatalogContext : DbContext
{