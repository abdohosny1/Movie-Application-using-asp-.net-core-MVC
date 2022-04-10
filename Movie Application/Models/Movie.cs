using Movie_Application.Data;

namespace Movie_Application.Models
{
    public class Movie :IEntityBase
    {
        public Movie()
        {
            actor_Movies = new List<Actor_Movie>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Decription { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MovieCategory movieCategory { get; set; }

        //Relation
        public List<Actor_Movie> actor_Movies { get; set; }

        //cinema
        public int CinemaId { get; set; }
        [ForeignKey("CinemaId")]

        public Cinema Cinema { get; set; }


        //producer

        public int ProducerId { get; set; }
        [ForeignKey("ProducerId")]

        public Producer Producer { get; set; }
    }
}
