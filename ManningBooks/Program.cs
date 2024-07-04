using ManningBooks;

using Microsoft.Data.Sqlite;

using var keepAliveConnection = new SqliteConnection(
    CatalogContext.ConnectionString
);

keepAliveConnection.Open();

CatalogContext.SeedBooks();

var userRequests = new[] {
 ".NET in Action",
 "Grokking Simplicity",
 "API Design Patterns",
 "EF Core in Action",
};

// Modify Program.cs to start all user request at the same time and wait for all to finish

var tasks = new List<Task>();
foreach (var userRequest in userRequests)
{
    tasks.Add(
      CatalogContext.WriteBookToConsoleAsync(userRequest)
    );
}

Task.WaitAll(tasks.ToArray());