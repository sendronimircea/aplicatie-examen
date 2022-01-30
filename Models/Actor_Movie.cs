using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class Actor_Movie // definim clasa pentru a putea realiza relatia many to many dintre modelele Actor si Movie
                                //este un joining table
    {
        public int MovieId { get; set; } //Fkey pt modelul Movie

        public Movie Movie { get; set; }



        public int ActorId { get; set; } //Fkey pt modelul Actor

        public Actor Actor { get; set; }
    }



}