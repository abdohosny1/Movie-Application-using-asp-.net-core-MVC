namespace Movie_Application.Data.viewModel
{
    public class NewMovieDropDownVM
    {
        public NewMovieDropDownVM()
        {
            Actors = new List<Actor>();
            Producers = new List<Producer>();
            Cinemas = new List<Cinema>();
        }
        public List<Actor> Actors { get; set; }
        public List<Producer> Producers { get; set; }
        public List<Cinema> Cinemas { get; set; }
    }
}
