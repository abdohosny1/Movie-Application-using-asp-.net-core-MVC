namespace Movie_Application.service.cinemaService
{
    public class CinemaService :EntityBaseRepository<Cinema>,ICinemaService
    {
        public CinemaService(ApplicationDbContext context) : base(context) { }
    }
}
