


namespace Movie_Application.Controllers
{
    public class ActorController : Controller
    {
        private readonly IActorService _service;

        public ActorController(IActorService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var res = await _service.GellAll();
            return View(res);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfileURL,FullName,Bio")]Actor actor)
        {
            var res = new Actor()
            {
                ProfileURL = actor.ProfileURL,
                Bio = actor.Bio,
                FullName= actor.FullName,


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
            var res= await _service.GetById(id);

            if (res == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(res);
            }

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var data =await _service.GetById(Id);
            if (data == null)
            {
                return View("NotFound");

            }
            else
            {
                return View(data);
            }


        }


        [HttpPost]
        public async Task<IActionResult> Edit(int Id, [Bind("Id,ProfileURL,FullName,Bio")] Actor actor)
        {
            if (Id != actor.Id)
            {
                return View("NotFound");
            }


            if (ModelState.IsValid)
            {
               await _service.update(actor);
                return RedirectToAction(nameof(Index));

            }
           
            
                return View(actor);
            
           
          
        }



        [HttpGet]
        public async Task<IActionResult> Delete( int id)
        {
            if(id == null)
            {
                return View("NotFound");

            }
            var res=await _service.GetById(id);
            if (res == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(res);
            }
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            if(id == null)
            {
                return View("NotFound");
            }

            var res =await _service.GetById(id);
            _service.delete(res);
            return RedirectToAction(nameof(Index));

        }




    }
}
