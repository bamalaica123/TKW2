using System.ComponentModel.DataAnnotations;
using TKW2.Models;
using TKW2.Models.Domain;
namespace TKW2.Domain
{
    public class Publishers
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; }
    }
}
