using System.ComponentModel.DataAnnotations;

namespace Movie_Application.Models
{
    public class Cinema : IEntityBase
    {

        public Cinema()
        {
            Movies = new List<Movie>();
        }
        [Key]

        public int Id { get; set; }

        [Required(ErrorMessage = "Logo Is Requried")]

        public string Logo { get; set; }
        [Required(ErrorMessage = "Name Is Requried")]

        public string Name { get; set; }
        [Required(ErrorMessage = "Name Is Requried")]

        public string Decription { get; set; }

        //relation

        public List<Movie> Movies { get; set; }

    }
}
