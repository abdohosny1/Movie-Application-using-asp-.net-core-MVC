using Movie_Application.Data;

namespace Movie_Application.Models
{
    public class NewMovieVM 
    {
        public int Id { get; set; }
        public NewMovieVM()
        {
            actor_id = new List<int>();
        }
    
        [Required(ErrorMessage ="Name Is Requried")]
        [Display(Name = "Name Movie")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Decription Is Requried")]
        [Display(Name = "Decription Movie")]
        public string Decription { get; set; }

        [Required(ErrorMessage = "Price Is Requried")]
        [Display(Name = "Price  Is $")]
        public double Price { get; set; }
        [Required(ErrorMessage = "ImageURL Is Requried")]
        [Display(Name = "Image Movie")]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "StartDate Is Requried")]
        [Display(Name = "StartDate Movie")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "EndDate Is Requried")]
        [Display(Name = "EndDate Movie")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "movie Category Is Requried")]
        [Display(Name  = "Select Category Movie")]
        public MovieCategory movieCategory { get; set; }

         [Required(ErrorMessage = "movie Actor(s) Is Requried")]
        [Display(Name = "Select Actor")]
        public List<int> actor_id { get; set; }
        [Required(ErrorMessage = "movie Cinema Is Requried")]
        [Display(Name = "Select Cinema")]
        public int CinemaId { get; set; }
  


      

        public int ProducerId { get; set; }
   
    }
}
