namespace Movie_Application.service.MovieService
{
    public class MovieService :EntityBaseRepository<Movie>,IMovieService
    {
        private readonly ApplicationDbContext _context;
        public MovieService(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task AddMovieAsync(NewMovieVM data)
        {
            var newMovie = new Movie()
            {
               Decription=data.Decription,
               EndDate=data.EndDate,
               ImageUrl=data.ImageUrl,
               ProducerId=data.ProducerId,
               StartDate=data.StartDate,
               Price=data.Price,
               CinemaId=data.CinemaId,
               Name=data.Name,
               movieCategory=data.movieCategory,
            };

          await  _context.Movies.AddAsync(newMovie);
          await _context.SaveChangesAsync();

            //add actor
            foreach (var item in data.actor_id)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = newMovie.Id,
                    ActoId = item,
                };
                await _context.Actor_Movies.AddAsync(newActorMovie);

            }
            await _context.SaveChangesAsync();
         

        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var res= await _context.Movies
                     .Include(e=>e.Cinema)
                     .Include(p=>p.Producer)
                     .Include(ac=>ac.actor_Movies)
                     .ThenInclude(a=>a.Actor)
                     .FirstOrDefaultAsync(i=>i.Id==id);

            return   res;
        }

        public async Task<NewMovieDropDownVM> GetMovieDropDownVM()
        {
            var res = new NewMovieDropDownVM()
            {
                Actors = await _context.Actors.OrderBy(e => e.FullName).ToListAsync(),
                Producers = await _context.Producers.OrderBy(e => e.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(e => e.Name).ToListAsync()
            };
            return res;
        }

        public async Task UpdatedMovieAsync(NewMovieVM data)
        {
            var dbMovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbMovie != null)
            {

                dbMovie.Decription = data.Decription;
                dbMovie.EndDate = data.EndDate;
                dbMovie.ImageUrl = data.ImageUrl;
                dbMovie.ProducerId = data.ProducerId;
                dbMovie.StartDate = data.StartDate;
                dbMovie.Price = data.Price;
                dbMovie.CinemaId = data.CinemaId;
                dbMovie.Name = data.Name;
                dbMovie.movieCategory = data.movieCategory;
                
                await _context.SaveChangesAsync();

            }

            //remove exisitng actor
            var existingActot = _context.Actor_Movies.Where(e => e.MovieId == data.Id).ToList();
            _context.Actor_Movies.RemoveRange(existingActot);
            await _context.SaveChangesAsync();

            //add actor
            foreach (var item in data.actor_id)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = data.Id,
                    ActoId = item,
                };
                await _context.Actor_Movies.AddAsync(newActorMovie);

            }
            await _context.SaveChangesAsync();

        }
    }
}
