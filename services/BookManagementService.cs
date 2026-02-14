using System.Collections;
/// <summary>
/// A service that can modify and display a dictionary of books through user input.
/// </summary>
/// <author>Colson Leonardi</author>
public class BookManagementService
{
    private Dictionary<string, Book> BookCollection = new Dictionary<string, Book>()
    {
        {"1984", new Book("1984", "jorjor wel", "Non-Fiction", "1984")},
        {"451", new Book("Fahrenheit 451", "Ray Bradbury", "Non-Fiction", "451")}
    };

    /// <summary>
    /// Continually runs PromptUser until program exits.
    /// </summary>
    public void OnStart()
    {
        Console.WriteLine($"Welcome to the book management system. You currently have {BookCollection.Count()} books in your system.");
        do
        {
            PromptUser();
        } while(true);
    }

    /// <summary>
    /// Asks the user what option they want to select. Calls ParseUserInput with whatever the user inputs.
    /// </summary>
    private void PromptUser()
    {
        Console.WriteLine("What would you like to do?");
        Console.WriteLine("1. Display Books");
        Console.WriteLine("2. Display Book by Book ID");
        Console.WriteLine("3. Add New Book");
        Console.WriteLine("4. Remove Book by Book ID");
        Console.WriteLine("5. Exit");
        ParseUserInput(Console.ReadLine());
    }

    /// <summary>
    /// Call book related methods or exit the program depending on user input.
    /// </summary>
    /// <param name="input">The user's input.</param>
    // TODO: validate user input and make this use enums... somehow
    private void ParseUserInput(string input)
    {
        switch(input)
        {
            case "1":
                DisplayAllBooks();
                break;
            case "2":
                DisplayBookByID();
                break;
            case "3":
                AddBook();
                break;
            case "4":
                RemoveBook();
                break;
            case "5":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Input is invalid. Try again.");
                break;
        }
    }

    /// <summary>
    /// Displays a book.
    /// </summary>
    /// <param name="book">Book to display.</param>
    private void DisplayBook(Book book)
    {
        Console.WriteLine($"ID: {book.ID}");
        Console.WriteLine($"Title: {book.Title}");
        Console.WriteLine($"Author: {book.Author}");
        Console.WriteLine($"Genre: {book.Genre}");
        Console.WriteLine("---------------------------");
    }

    /// <summary>
    /// Iterates through BookCollection and calls DisplayBook on every book.
    /// </summary>
    private void DisplayAllBooks()
    {
        Console.WriteLine("BOOKS AVAILABLE:");
        foreach(KeyValuePair<string, Book> entry in BookCollection)
        {
            DisplayBook(entry.Value);
        }
    }

    /// <summary>
    /// Calls DisplayBook using the user's input as book ID.
    /// </summary>
    private void DisplayBookByID()
    {
        Console.WriteLine("What is the ID of the book you'd like to look up?");
        string inputID = Console.ReadLine();
        DisplayBook(BookCollection[inputID]);
    }

    /// <summary>
    /// Adds a book to BookCollection based on user input. Inputs cannot be null.
    /// </summary>
    private void AddBook()
    {
        Console.WriteLine("Book ID:");
        string idInput = InputNotNull();
        Console.WriteLine("Book Title:");
        string titleInput = InputNotNull();
        Console.WriteLine("Book Author:");
        string authorInput = InputNotNull();
        Console.WriteLine("Book Genre:");
        string genreInput = InputNotNull();

        BookCollection.Add(idInput, new Book(titleInput, authorInput, genreInput, idInput));
    }

    /// <summary>
    /// Removes a book from BookCollection using the user's input as book ID.
    /// </summary>
    private void RemoveBook()
    {
        Console.WriteLine("Which book would you like to delete?");
        string idToRemove = InputNotNull();
        Console.WriteLine($"{BookCollection[idToRemove].Title} REMOVED");
        BookCollection.Remove(idToRemove);

    }

    /// <summary>
    /// Simple method that loops until the user's input is not null.
    /// </summary>
    /// <returns>User's input. Never null.</returns>
    private string InputNotNull()
    {
        string userInput;
        do
        {
            userInput = Console.ReadLine();
        } while(userInput == "");
        return userInput;
    }
}