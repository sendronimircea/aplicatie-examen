using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService //clasa mosteneste din interfata EntityBaseRepository cu parametru Movie + din interfata IMoviesService
                                                                                //definim metodele pe care le va folosi clasa MoviesService
    {
        private readonly AppDbContext _context;
        public MoviesService(AppDbContext context) : base(context) //constructor care mosteneste din clasa base
        {
            _context = context;
        }

        public async Task AddNewMovieAsync(NewMovieVM data) // implementarea metodei prin care se creeaza un film nou
        {
            var newMovie = new Movie() // cream un obiect newMovie nou care sa aiba urmatoarele proprietati
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                CinemaId = data.CinemaId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                MovieCategory = data.MovieCategory,
                ProducerId = data.ProducerId
            };
            await _context.Movies.AddAsync(newMovie); //adaugam proprietatile in context
            await _context.SaveChangesAsync(); //apoi le salvam

            //adaugam actorii in filmul nou creat

            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie() // cream un obiect newActorMovie nou
                {
                    MovieId = newMovie.Id, // va prelua ID din obiectul newMovie nou creat
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);//adaugam proprietatile in context
            }
            await _context.SaveChangesAsync();//apoi le salvam
        }

        //metoda pentru a obtine informatii despre Movies
        //facem referire la ce proprietati dorim sa se afiseze din celelate tabele 

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDetails = await _context.Movies
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
                .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor) // din cauza relatiei many to many luam numele actorului din tabela Actors_Movies, care la randul ei o ia din tabelul Actors 
                .FirstOrDefaultAsync(n => n.Id == id);

            return movieDetails;
        }

        //metoda pentru a crea un droplist atunci cand vom crea un film nou
        public async Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues()
        {
            var response = new NewMovieDropdownsVM()
            {
                Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync(),
                Producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync()
            };

            return response; //variabila response va intoarce toti actorii, producatorii si cinemaurile sub forma de lista
        }


        //implementarea metodei de actualizare a filmelor

        public async Task UpdateMovieAsync(NewMovieVM data)
        {
            var dbMovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == data.Id); // vaariabila care care preia ID filmului

            if (dbMovie != null) //daca exista filmul, modificam proprietatile 
            {
                dbMovie.Name = data.Name;
                dbMovie.Description = data.Description;
                dbMovie.Price = data.Price;
                dbMovie.ImageURL = data.ImageURL;
                dbMovie.CinemaId = data.CinemaId;
                dbMovie.StartDate = data.StartDate;
                dbMovie.EndDate = data.EndDate;
                dbMovie.MovieCategory = data.MovieCategory;
                dbMovie.ProducerId = data.ProducerId;
                await _context.SaveChangesAsync(); //salvam modificarile
            }

            //stergem actorii care exista in db

            var existingActorsDb = _context.Actors_Movies.Where(n => n.MovieId == data.Id).ToList();
            _context.Actors_Movies.RemoveRange(existingActorsDb);
            await _context.SaveChangesAsync();

            //adaugam actorii in noul film
            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = data.Id,
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync(); //salvam modificarile
        }
    }
}
