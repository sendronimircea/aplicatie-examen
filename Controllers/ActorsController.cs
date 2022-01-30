using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class ActorsController : Controller // clasa actorscontroller mosteneste din clasa de baza controller
    {
        private readonly IActorsService _service; // trebuie sa declaram dbcontextul pentru a face legatura dintre events si db

        public ActorsController(IActorsService service) //constructor
        {
            _service = service;
        }

        public async Task<IActionResult> Index() // va afisa pagina index din Views\Actors\Index
        {
            var data = await _service.GetAllAsync();
            return View(data); //parametru data care contine informatiile din tabelul actor se va transmite paginii view
        }

        //definim actiunea de creare a unui nou actor

        public IActionResult Create() // va afisa pagina create din Views\Actors\Create
        {
            return View();
        }


        [HttpPost] // ca urmare a requestului din formularul create din pagina Views\Actors\Create

        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Actor actor) // adaugam proprietatile din view in modelul Actor
        {
            if (!ModelState.IsValid) //daca modelul nu este corect, revenim la pagina initiala
            {
                return View(actor);
            }
            await _service.AddAsync(actor); //daca e corect, adaugam actorul in db
            return RedirectToAction(nameof(Index)); //dupa adaugare, revenim la pagina initiala
        }

        //Get: Actors/Details/1
        public async Task<IActionResult> Details(int id) //parametru id
        {
            var actorDetails = await _service.GetByIdAsync(id); //verificam daca exista actor cu acel id

            if (actorDetails == null) return View("NotFound"); //daca nu exista afisam empty
            return View(actorDetails); //daca exista, afisam detaliile actorului. Pagina de tip view va prelua datele prin parametrul actorDetails
        }

        //Get: Actors/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id); //verificam daca exista id pentru actorul respectiv
            if (actorDetails == null) return View("NotFound"); //daca nu gasim id, afiseaza view not found
            return View(actorDetails); //daca gasim id, afiseaza detaliile actorului. Pagina de tip view va prelua datele prin parametrul actorDetails
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            if (!ModelState.IsValid) //daca modelul introdus e corect
            {
                return View(actor);
            }
            await _service.UpdateAsync(id, actor); // daca este acelasi, folosim metoda UpdateAsync
            return RedirectToAction(nameof(Index)); // daca nu, redirectioneaza catre pagina index
        }

        //Get: Actors/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id); //verificam daca exista
            if (actorDetails == null) return View("NotFound"); //daca nu exista, afiseaza notfound view
            return View(actorDetails); //daca exista, afiseaza detaliile actorului. Pagina de tip view va prelua datele prin parametrul actorDetails
        }

        [HttpPost, ActionName("Delete")] // actiunea Delete din Views/Actors/Delete
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");

            await _service.DeleteAsync(id); // folosim metoda DeleteAsync cu parametru ID
            return RedirectToAction(nameof(Index)); //dupa stergere, ne intoarcem la pagina index
        }
    }
}
 