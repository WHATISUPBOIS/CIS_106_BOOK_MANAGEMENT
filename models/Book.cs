/// <summary>
/// A model that represents a book. None of the book's properties should be null.
/// </summary>
/// <author>Colson Leonardi</author>
public class Book
{
    public string Title {get; set;}
    public string Author {get; set;}
    public string Genre {get; set;}
    public string ID {get; set;}

/// <summary>
/// Book constructor.
/// </summary>
/// <param name="title">The title of the book.</param>
/// <param name="author">The book author's first and last name.</param>
/// <param name="genre">The book's genre. eg. Fantasy, Sci-Fi</param>
/// <param name="id">The book's unique ID.</param>
    public Book (string title, string author, string genre, string id)
    {
        Title = title;
        Author = author;
        Genre = genre;
        ID = id;
    }
}