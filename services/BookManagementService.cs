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
    /// TODO: validate user input!
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
        Console.WriteLine("Book ID (must be unique):");
        string idInput = ValidateInputID();
        Console.WriteLine("Book Title:");
        string titleInput = ValidateInput();
        Console.WriteLine("Book Author:");
        string authorInput = ValidateInput();
        Console.WriteLine("Book Genre:");
        string genreInput = ValidateInput();

        BookCollection.Add(idInput, new Book(titleInput, authorInput, genreInput, idInput));
    }

    /// <summary>
    /// Removes a book from BookCollection using the user's input as book ID.
    /// </summary>
    /// TODO: validate user input!
    private void RemoveBook()
    {
        Console.WriteLine("Which book would you like to delete?");
        string idToRemove = ValidateInput();
        Console.WriteLine($"{BookCollection[idToRemove].Title} REMOVED");
        BookCollection.Remove(idToRemove);

    }

    /// <summary>
    /// Simple method that loops until the user's input is not null.
    /// </summary>
    /// <returns>User's input. Never null.</returns>
    private string ValidateInput()
    {
        string userInput = Console.ReadLine();
        
        while(userInput == "")
        {
            Console.WriteLine("User input is null. Try again.");
            userInput = Console.ReadLine();
        };
        return userInput;
    }


    private string ValidateInputID()
    {
        string InputID = ValidateInput();
        bool isIDDuplicate = false;

        foreach(KeyValuePair<string, Book> book in BookCollection)
        {
            if(book.Key == InputID)
            {
                isIDDuplicate = true;
            }
        }
        while (isIDDuplicate)
        {
            isIDDuplicate = false;
            Console.WriteLine($"Book with ID {InputID} already exists. Please enter a unique ID.");
            InputID = ValidateInput();
            // Gross. I really need to find a better way to do this.
            foreach(KeyValuePair<string, Book> book in BookCollection)
            {
                if(book.Key == InputID)
                {
                    isIDDuplicate = true;
                }
            }
        }
        return InputID;
    }
}