using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class MoviesController : Controller // clasa actorscontroller mosteneste din clasa de baza controller
    {
        private readonly IMoviesService _service; // trebuie sa declaram dbcontextul pentru a face legatura dintre events si db

        public MoviesController(IMoviesService service) //constructor
        {
            _service = service;
        }

        public async Task<IActionResult> Index()// returneaza o lista cu toate filmele prin folosirea metodei GetAllAsync 
        {
            var allMovies = await _service.GetAllAsync(n => n.Cinema); 
            return View(allMovies);
        }

        //implementam metoda cu numele Filter de cautare filme 
        public async Task<IActionResult> Filter(string searchString) // ia parametru stringul searchString din viewul _Layout
        {
            var allMovies = await _service.GetAllAsync(n => n.Cinema);

            if (!string.IsNullOrEmpty(searchString)) // verificam daca s-au introdus date in camp
            {
                //cauta filme dupa numele filmelor din db sau daca in descrierea lor apare acest nume, comparandu/le cu stringu-l introdus de user
                //se va afisa pagina Index cu rezultatele din varabila filteredResult

                var filteredResult = allMovies.Where(n => n.Name.Contains(searchString) || n.Description.Contains(searchString)).ToList(); 
                return View("Index", filteredResult);
            }

            return View("Index", allMovies); //daca nu au fost introduse date, afisam toate filmele
        }

        //GET: Movies/Details/1
        //definim actiunea cu numele Details care ia ca parametru Id
        public async Task<IActionResult> Details(int id)
        {
            var movieDetail = await _service.GetMovieByIdAsync(id); //variabila movieDetail primeste filmul in functie de id mentionat ca parametru in actiunea Details
            return View(movieDetail); //afisam informatiile despre film
        }

        //GET: Movies/Create
        //definim o noua actiune cu numele Create pentru a crea filme noi

        public async Task<IActionResult> Create()
        {
            var movieDropdownsData = await _service.GetNewMovieDropdownsValues(); //vom folosi metoda GetNewMovieDropdownsValues si vom crea trei liste
                                                                                    //folosim ViweBag pentru a transfera date de la controller la view

            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name"); // lista va lua ca parametru  variabila movieDropdownsData.Cinemas si id + numele cinemaului
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            return View();
        }

        [HttpPost] // ca urmare a requestului din formularul create din pagina Views\Movies\Create
        public async Task<IActionResult> Create(NewMovieVM movie) // metoda are ca parametru modelul NewMovieVM
        {
            if (!ModelState.IsValid) // verificam modelul daca e corespunzator
            {
                var movieDropdownsData = await _service.GetNewMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

                return View(movie);
            }

            await _service.AddNewMovieAsync(movie); //daugam noul film in db
            return RedirectToAction(nameof(Index)); //dupa adaugare ne intoarcem la pagina index
        }

        //implementam metoda de modificare a filmelor
        //GET: Movies/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var movieDetails = await _service.GetMovieByIdAsync(id); //verificam daca exista in db
            if (movieDetails == null) return View("NotFound");//daca nu, afiseaza pagina NotFound din Shared

            var response = new NewMovieVM() //construim un raspuns la solicitarea de editare cu urmatoarele proprietati
            {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                Price = movieDetails.Price,
                StartDate = movieDetails.StartDate,
                EndDate = movieDetails.EndDate,
                ImageURL = movieDetails.ImageURL,
                MovieCategory = movieDetails.MovieCategory,
                CinemaId = movieDetails.CinemaId,
                ProducerId = movieDetails.ProducerId,
                ActorIds = movieDetails.Actors_Movies.Select(n => n.ActorId).ToList(), // din cauza relatiei many to many trebuie sa arata din ce tabel este preluat ID, respectiv ActorMovies, care la randul lui preia din Actors
            };

            var movieDropdownsData = await _service.GetNewMovieDropdownsValues();
            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            return View(response);
        }

        //implementam metoda de modificare a filmelor prin POST
        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMovieVM movie)
        {
            if (id != movie.Id) return View("NotFound"); //verificam id/urile, daca nu sunt identice -> NotFound

            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _service.GetNewMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

                return View(movie);
            }

            await _service.UpdateMovieAsync(movie); //salvam filmul
            return RedirectToAction(nameof(Index)); //ne intoarcem la index
        }
    }
}
