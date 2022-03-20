namespace bookit.Models;

public class DataModel
{
    public int Id {get; set;}
    public string Title {get; set;}
    public string Author {get; set;}
    public int Quantity {get; set;}

    public DataModel()
    {
        Id = -1;
        Title = "Empty";
        Author = "Empty";
        Quantity = 0;
    }

    public DataModel(int Id, string Title, string Author, int Quantity)
    {
        this.Id = Id;
        this.Title = Title;
        this.Author = Author;
        this.Quantity = Quantity;
    }
}