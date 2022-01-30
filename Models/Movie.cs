using eTickets.Data;
using eTickets.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class Movie : IEntityBase //clasa Movie mosteneste din interfata IEntityBase
    {
        [Key]
        public int Id { get; set; } //identificatorul pentru clasa si tabela din DB

        public string Name { get; set; } //proprietatile din modelul Movie

        public string Description { get; set; }

        public double Price { get; set; }

        public string ImageURL { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public MovieCategory MovieCategory { get; set; } //se compleateaza cu enumerarea din Data/MovieCategory

        //schema relationala cu celelalte modele
        public List<Actor_Movie> Actors_Movies { get; set; } //many to many Actors -- Movies

        
        public int CinemaId { get; set; }
        [ForeignKey("CinemaId")] //definim Fkey pt modelul Cinema

        public Cinema Cinema { get; set; }//relatie one to many Cinema -- Movies


        public int ProducerId { get; set; }
        [ForeignKey("ProducerId")] //definim Fkey pt modelul Producer

        public Producer Producer { get; set; }// relatie one to many Producer -- Movies


    }
}
