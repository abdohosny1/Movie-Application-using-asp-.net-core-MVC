using Microsoft.AspNetCore.Mvc;
using Movie_Application.service.ProducerService;

namespace Movie_Application.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducerService _service;

        public ProducersController(IProducerService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GellAll();
            return View(data);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            var res = await _service.GetById(id);
            if(res == null)
            {
                return View("NotFound");

            }
            return View(res);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Producer producer)
        {
            if (ModelState.IsValid)
            {
                var res = new Producer()
                {
                    FullName = producer.FullName,
                    Bio = producer.Bio,
                    ProfileURL = producer.ProfileURL,
                };
               await _service.Add(res);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            if(Id == null)
            {
                return View("NotFound");

            }
            var res = await _service.GetById(Id);
            if(res == null)
            {
                return View("NotFound");

            }

            return View(res);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfileURL,FullName,Bio")] Producer producer)
        {
            if (id !=producer.Id)
            {
                return View("NotFound");

            }
           
            if (ModelState.IsValid)
            {

              await _service.update(producer);
                return RedirectToAction(nameof(Index));

            }

            return View(producer);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            if (Id == null)
            {
                return View("NotFound");

            }
            var res = await _service.GetById(Id);
            if (res == null)
            {
                return View("NotFound");

            }

            return View(res);
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
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

            await _service.delete(res);
                return RedirectToAction(nameof(Index));

           

        }



    }
}
