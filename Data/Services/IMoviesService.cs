using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public interface IMoviesService : IEntityBaseRepository<Movie> //mosteneste din interfata IEntityBaseRepository cu paramentru entitatea Movies 
                                                                    // la care adaugam suplimentar aceste metode pentru serviciul IMoviesService
    {   
        Task<Movie> GetMovieByIdAsync(int id); //metoda pentru a obtine informatii despre Movies

        Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues(); // metoda pentru a crea un drop list atunci cand cream un film nou

        Task AddNewMovieAsync(NewMovieVM data); //metoda pentru a crea un film nou care ia ca parametru modelul NewMovieVM

        Task UpdateMovieAsync(NewMovieVM data); //metoda pentru a modifica un film care ia ca parametru modelul NewMovieVM
    }
}