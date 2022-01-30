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
    public class CinemasController : Controller // clasa Cinemascontroller mosteneste din clasa de baza controller
    {
        private readonly ICinemasService _service; // trebuie sa declaram dbcontextul pentru a face legatura dintre events si db

        public CinemasController(ICinemasService service) //constructor
        {
            _service = service;
        }

        public async Task<IActionResult> Index()  // va afisa pagina index din Views\Cinemas\Index
        {
            var allCinemas = await _service.GetAllAsync();// folosim metoda GetAllAsync
            return View(allCinemas); //parametru care contine informatiile din tabelul cinema
        }


        //Get: Cinemas/Create
        // va afisa pagina create din Views\Cinemas\Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] // ca urmare a requestului din formularul create din pagina Views\Cinemas\Create

        public async Task<IActionResult> Create([Bind("Logo,Name,Description")] Cinema cinema) // adaugam proprietatile din view in modelul Cinema
        {
            if (!ModelState.IsValid) return View(cinema); //daca modelul nu este corect, revenim la pagina initiala
            await _service.AddAsync(cinema); //daca e corect, adaugam filmul in db cu metoda AddAsync cu parametru Cinema
            return RedirectToAction(nameof(Index)); //dupa adaugare, revenim la pagina initiala
        }

        //Get: Cinemas/Details/1
        //definim actiunea Details cu parametru id
        public async Task<IActionResult> Details(int id) 
        {
            var cinemaDetails = await _service.GetByIdAsync(id);  //verificam daca exista film cu acel id
            if (cinemaDetails == null) return View("NotFound"); //daca nu gasim id, afiseaza view not found
            return View(cinemaDetails); //daca gasim id, afiseaza detaliile filmului
        }

        //Get: Cinemas/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound"); //daca nu exista id, afiseaza pagina NotFound din Shared Folder
            return View(cinemaDetails); // daca nu, redirectioneaza catre pagina index
        }

        [HttpPost] // actiunea Edit din Views/Cinemas/Edit
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,Name,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid) return View(cinema); //daca modelul introdus e corect
            await _service.UpdateAsync(id, cinema); //foloseste metoda update cu doi parametrii: id si cinema
            return RedirectToAction(nameof(Index)); //apoi afiseaza pagina Index
        }

        //Get: Cinemas/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id); //verificam daca exista
            if (cinemaDetails == null) return View("NotFound"); //daca nu exista, afiseaza notfound view
            return View(cinemaDetails); //daca exista, afiseaza detaliile filmului
        }

        [HttpPost, ActionName("Delete")] // actiunea Delete din Views/Cinemas/Delete
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");

            await _service.DeleteAsync(id); // folosim metoda DeleteAsync cu parametru ID
            return RedirectToAction(nameof(Index)); //dupa stergere, ne intoarcem la pagina index
        }
    }
}