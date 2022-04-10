
namespace Movie_Application.service.MovieService
{
    public interface IMovieService :IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<NewMovieDropDownVM> GetMovieDropDownVM();
        Task AddMovieAsync(NewMovieVM entity);
        Task UpdatedMovieAsync(NewMovieVM entity);
    }
}
