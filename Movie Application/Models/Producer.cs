namespace Movie_Application.Models
{
    public class Producer : IEntityBase
    {
        public Producer()
        {
            Movies = new List<Movie>();
        }
        [Key]
        public int Id { get; set; }

        [Display(Name = "Image")]
        [Required(ErrorMessage = "ImageUrl Is Requried")]

        public string ProfileURL { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name Is Requried")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name Length Must Be Between 3 and 50")]

        public string FullName { get; set; }
        [Display(Name = "Bio")]
        [Required(ErrorMessage = "Bio Is Requried")]


        public string Bio { get; set; }

        //relation

       public  List<Movie> Movies { get; set; }
    }
}
