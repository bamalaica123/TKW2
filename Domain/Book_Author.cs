using TKW2.Models;
using TKW2.Models.Domain;

namespace TKW2.Domain
{
    public class Book_Author
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int AuthorId { get; set; }

        public Author Author { get; set; }
    }
}
