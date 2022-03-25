namespace bookit.Models;

public class DataModel
{
    public int id {get; set;}
    public string title {get; set;}
    public string author {get; set;}
    public int quantity {get; set;}

    public DataModel()
    {
        id = -1;
        title = "Empty";
        author = "Empty";
        quantity = 0;
    }

    public DataModel(int Id, string Title, string Author, int Quantity)
    {
        this.id = Id;
        this.title = Title;
        this.author = Author;
        this.quantity = Quantity;
    }
}