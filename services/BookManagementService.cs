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
        // The next Console.WriteLine will print on the same line.
        Console.Write("Welcome to the book management system. ");
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
        Console.WriteLine($"You currently have {BookCollection.Count} books in your system.");
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
    private void ParseUserInput(string input)
    {
        Enum.TryParse(input, out ValidInputs parsedInput);
        switch(parsedInput)
        {
            case ValidInputs.DISPLAY_ALL_BOOKS:
                DisplayAllBooks();
                break;
            case ValidInputs.DISPLAY_BOOK_BY_ID:
                DisplayBookByID();
                break;
            case ValidInputs.ADD_BOOK:
                AddBook();
                break;
            case ValidInputs.REMOVE_BOOK:
                RemoveBook();
                break;
            case ValidInputs.EXIT:
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Input is invalid. Type a number between 1 and 5.");
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
    /// Takes in user input. Loops until the user's input is not null, empty, or whitespace.
    /// </summary>
    /// <returns>User's input. Never null.</returns>
    private string ValidateInputNotEmpty()
    {
        string userInput;
        do
        {
            userInput = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("User input is null or empty. Try again.");
            }
        } while(string.IsNullOrWhiteSpace(userInput));
        return userInput;
    }

    /// <summary>
    /// Checks if user input matches the ID of an existing book.
    /// </summary>
    /// <returns>An empty string if user input doesn't match a book's ID. Returns user input otherwise.</returns>
    private string ValidateInputMatchID()
    {
        string inputID;
        inputID = ValidateInputNotEmpty();

        if (!BookCollection.ContainsKey(inputID))
        {
            Console.WriteLine($"There is no book with ID {inputID}. Type 1 to see all valid book IDs.");
            return "";
        }
        return inputID;
    }

    /// <summary>
    /// Calls DisplayBook using the user's input as book ID.
    /// </summary>
    private void DisplayBookByID()
    {
        Console.WriteLine("What is the ID of the book you'd like to look up?");
        string inputID = ValidateInputMatchID();
        // ValidateInputMatchID can only return "" if the user's input doesn't match a book ID.
        if(inputID != "")
        {
            DisplayBook(BookCollection[inputID]);
        }
    }

    /// <summary>
    /// Takes in user input. Loops until user input doesn't match an existing book ID.
    /// </summary>
    /// <returns>User input. Will never match an existing book ID.</returns>
    private string ValidateInputNotMatchID()
    {
        string inputID;
        do
        {
            inputID = ValidateInputNotEmpty();
            if (BookCollection.ContainsKey(inputID))
            {
                Console.WriteLine($"Book with ID {inputID} already exists. Please enter a unique ID.");
            }
        } while (BookCollection.ContainsKey(inputID));
        return inputID;
    }

    /// <summary>
    /// Adds a book to BookCollection based on user input. Inputs cannot be null.
    /// </summary>
    private void AddBook()
    {
        Console.WriteLine("Book ID (must be unique):");
        string idInput = ValidateInputNotMatchID();
        Console.WriteLine("Book Title:");
        string titleInput = ValidateInputNotEmpty();
        Console.WriteLine("Book Author:");
        string authorInput = ValidateInputNotEmpty();
        Console.WriteLine("Book Genre:");
        string genreInput = ValidateInputNotEmpty();

        BookCollection.Add(idInput, new Book(titleInput, authorInput, genreInput, idInput));
    }
    
    /// <summary>
    /// Removes a book from BookCollection using the user's input as book ID.
    /// </summary>
    private void RemoveBook()
    {
        Console.WriteLine("Which book would you like to delete?");
        string idToRemove = ValidateInputMatchID();
        if(idToRemove != "")
        {
            Console.WriteLine($"{BookCollection[idToRemove].Title} REMOVED");
            BookCollection.Remove(idToRemove);
        }
    }
}