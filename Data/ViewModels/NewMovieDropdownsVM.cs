using eTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.ViewModels
{
    public class NewMovieDropdownsVM // cream o clasa necesara pentru a afisa lista cu Actori, Producatori si Cinemauri disponibile atunci cand vom crea un film nou
    {
        public NewMovieDropdownsVM() //definim un constructor pentru instantiere
        {
            Producers = new List<Producer>();
            Cinemas = new List<Cinema>();
            Actors = new List<Actor>();
        }

        public List<Producer> Producers { get; set; }

        public List<Cinema> Cinemas { get; set; }

        public List<Actor> Actors { get; set; }
    }
}