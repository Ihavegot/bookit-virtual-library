namespace bkit.Models
{
    public class BorrowModel
    {

        public int borrowId { get; set; }
        public string userId { get; set; }
        public int bookId { get; set; }

        public BorrowModel()
        {
            borrowId = -1;
            userId = "Empty";
            bookId = -1;
        }

        public BorrowModel(int Id, string User, int Book)
        {
            this.borrowId = Id;
            this.userId = User;
            this.bookId = Book;
        }
    }
}
