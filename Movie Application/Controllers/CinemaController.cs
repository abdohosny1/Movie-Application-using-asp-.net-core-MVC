using Microsoft.AspNetCore.Mvc;
using Movie_Application.service.cinemaService;

namespace Movie_Application.Controllers
{
    public class CinemaController : Controller
    {
        private readonly ICinemaService _service;

        public CinemaController(ICinemaService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GellAll();
            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Logo,Name,Decription")] Cinema cinema)
        {
            var res = new Cinema()
            {
                Decription = cinema.Decription,
                Logo = cinema.Logo,
                Name = cinema.Name,
            };
            if (ModelState.IsValid)
            {

                await _service.Add(res);
                return RedirectToAction(nameof(Index));

            }
            return View();
        }


        [HttpGet]

        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return View("NotFound");

            }
            var res = await _service.GetById(id);
            if (res == null)
            {
                return View("NotFound");
            }
            return View(res);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null) return View("NotFound");

            var res = await _service.GetById(id);
            if (res == null) return View("NotFound");

            return View(res);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,Name,Decription")] Cinema cinema)
        {
            if (id != cinema.Id) return View("NotFound");


            if (ModelState.IsValid)
            {
                await _service.update(cinema);
                return RedirectToAction(nameof(Index));
            }
            return View(cinema);



        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) return View("NotFound");

            var res = await _service.GetById(id);
            if (res == null) return View("NotFound");

            return View(res);

        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            if (id == null) return View("NotFound");

            var res = await _service.GetById(id);
            if(res==null) return View("NotFound");
             await _service.delete(res);
            return RedirectToAction(nameof(Index));
            



        }
    }
}