
using Movie_Application.Data.Base;

namespace Movie_Application.Models
{
    public class Actor :IEntityBase
    {
        public Actor()
        {
            actor_Movies = new List<Actor_Movie>();
        }
        [Key]
        public int Id { get; set; }
        [Display(Name = "ProfileURL")]
        [Required(ErrorMessage ="ImageUrl Is Requried")]

        public string ProfileURL { get; set; }
        [Display(Name = "FullName")]
        [Required(ErrorMessage = "Name Is Requried")]
        [StringLength(50,MinimumLength =3,ErrorMessage ="Name Length Must Be Between 3 and 50")]

        public string FullName { get; set; }
        [Required(ErrorMessage = "Bio Is Requried")]
        [Display(Name = "Bio")]

        public string Bio { get; set; }

        //Relation
        public List<Actor_Movie> actor_Movies { get; set; }
    }
}
