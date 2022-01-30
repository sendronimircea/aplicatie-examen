using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducersService _service; // trebuie sa declaram dbcontextul pentru a face legatura dintre events si db

        public ProducersController(IProducersService service) //constructor
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        { 
            var allProducers = await _service.GetAllAsync();
            return View(allProducers);
        }

        //GET: producerscontroller/details/1 = id
        //definim o noua actiune = Details care va afisa informatii despre producatorul selectat

        public async Task<IActionResult> Details(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id); // verificam daca producatorul exista in db
            if (producerDetails == null) return View("NotFound"); //daca nu exista, afiseaza fereastra Notfound din Folderul Shared
            return View(producerDetails); //daca exista, afiseaza informatiile despre producator
        }


        //GET: producers/create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] // ca urmare a requestului din formularul create din pagina Views\Producers\Create
        public async Task<IActionResult> Create([Bind("ProfilePictureURL,FullName,Bio")] Producer producer) // adaugam proprietatile din view in modelul Producer
        {
            if (!ModelState.IsValid) return View(producer); //daca modelul nu este corect, revenim la pagina initiala

            await _service.AddAsync(producer); //daca e corect, adaugam producatorul in db
            return RedirectToAction(nameof(Index)); //dupa adaugare, revenim la pagina initiala
        }


        //GET: producers/edit/1
        //definim actiunea de modificare
        public async Task<IActionResult> Edit(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id); //verificam daca producatorul exista in db
            if (producerDetails == null) return View("NotFound"); //daca nu gasim id, afiseaza view not found din Shared Folder
            return View(producerDetails);//daca gasim id, afiseaza detaliile producatorului
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilePictureURL,FullName,Bio")] Producer producer)
        {
            if (!ModelState.IsValid) return View(producer);

            if (id == producer.Id) // verificam ID din db cu cel din request
            {
                await _service.UpdateAsync(id, producer); // daca este acelasi, folosim metoda UpdateAsync
                return RedirectToAction(nameof(Index)); // daca nu, redirectioneaza catre pagina index
            }
            return View(producer);
        }

        //GET: producers/delete/1
        //definim actiunea delete

        public async Task<IActionResult> Delete(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id); //verificam daca exista id respectiv
            if (producerDetails == null) return View("NotFound"); //daca nu exista, afiseaza notfound view
            return View(producerDetails); //daca exista, afiseaza detaliile producatorului
        }

        [HttpPost, ActionName("Delete")] // actiunea Delete din Views/Producers/Delete
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null) return View("NotFound");

            await _service.DeleteAsync(id); // folosim metoda DeleteAsync cu parametru ID
            return RedirectToAction(nameof(Index)); //dupa stergere, ne intoarcem la pagina index
        }
    }
}
