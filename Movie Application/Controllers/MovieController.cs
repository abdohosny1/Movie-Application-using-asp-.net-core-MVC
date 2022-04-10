using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Movie_Application.service.MovieService;

namespace Movie_Application.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _service;

        public MovieController(IMovieService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GellAll(e=>e.Cinema);
            return View(data);
        }

        public async Task<IActionResult> Filter(string searchString)
        {

            var data = await _service.GellAll(e => e.Cinema);
            if (!string.IsNullOrEmpty(searchString))
            {
                var filterResult = data.Where(e => e.Name.Contains(searchString)
                || e.Decription.Contains(searchString)).ToList();
                return View("Index", filterResult);

            }
            return View("Index", data);
        }
        

        public async Task<IActionResult> Details(int id)
        {
            var data = await _service.GetMovieByIdAsync(id);
            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            var movieDropData = await _service.GetMovieDropDownVM();
            ViewBag.Cinemas = new SelectList(movieDropData.Cinemas,"Id","Name");
            ViewBag.Producers = new SelectList(movieDropData.Actors,"Id","FullName");
            ViewBag.Actors = new SelectList(movieDropData.Actors,"Id", "FullName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            if (ModelState.IsValid)
            {
                await _service.AddMovieAsync(movie);
                return RedirectToAction(nameof(Index));
            }
            var movieDropData = await _service.GetMovieDropDownVM();
            ViewBag.Cinemas = new SelectList(movieDropData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropData.Actors, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropData.Actors, "Id", "FullName");
            return View(movie);

        }
        public async Task<IActionResult> Edit(int id)
        {
            var res=await _service.GetMovieByIdAsync(id);
            if (res == null)
            {
                return View("NotFound");
            }
            var response = new NewMovieVM()
            {
                Id = res.Id,
                Name = res.Name,
                Decription = res.Decription,
                EndDate = res.EndDate,
                ImageUrl = res.ImageUrl,
                StartDate = res.StartDate,
                CinemaId = res.CinemaId,
                ProducerId = res.ProducerId,
                Price = res.Price,
                movieCategory = res.movieCategory,
                actor_id = res.actor_Movies.Select(n => n.ActoId).ToList(),
            };
            var movieDropData = await _service.GetMovieDropDownVM();
            ViewBag.Cinemas = new SelectList(movieDropData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropData.Actors, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropData.Actors, "Id", "FullName");
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,NewMovieVM movie)
        {
            if(id!=movie.Id) return View("NotFound");

            if (ModelState.IsValid)
            {
                await _service.UpdatedMovieAsync(movie);
                return RedirectToAction(nameof(Index));
            }
            var movieDropData = await _service.GetMovieDropDownVM();
            ViewBag.Cinemas = new SelectList(movieDropData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropData.Actors, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropData.Actors, "Id", "FullName");
            return View(movie);

        }



    }
}
