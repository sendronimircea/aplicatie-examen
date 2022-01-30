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
    public class NewMovieVM //definim o clasa noua necesara pentru crearea unui nou film. Aceasta va contine toate proprietatile necesare 
    {
        public int Id { get; set; }

        [Display(Name = "Movie name")]
        [Required(ErrorMessage = "Numele este obligatoriu")]
        public string Name { get; set; }

        [Display(Name = "Movie description")]
        [Required(ErrorMessage = "Descrierea filmului este obligatorie")]
        public string Description { get; set; }

        [Display(Name = "Pretul in $")]
        [Required(ErrorMessage = "Mentionarea pretului este obligatorie")]
        public double Price { get; set; }

        [Display(Name = "Movie poster URL")]
        [Required(ErrorMessage = "Inserarea unui poster este obligatorie")]
        public string ImageURL { get; set; }

        [Display(Name = "Movie start date")]
        [Required(ErrorMessage = "Inserarea datei de rulare este obligatorie")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Movie end date")]
        [Required(ErrorMessage = "Inserarea datei de sfarsit este obligatorie")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Select a category")]
        [Required(ErrorMessage = "Inserarea categoriei filmului este obligatorie")]
        public MovieCategory MovieCategory { get; set; }

        //Relationships
        [Display(Name = "Select actor(s)")]
        [Required(ErrorMessage = "Inserarea unui/unor actori este obligatorie")]
        public List<int> ActorIds { get; set; }

        [Display(Name = "Select a cinema")]
        [Required(ErrorMessage = "Inserarea unui cinema este obligatorie")]
        public int CinemaId { get; set; }

        [Display(Name = "Select a producer")]
        [Required(ErrorMessage = "Inserarea unui producator este obligatorie")]
        public int ProducerId { get; set; }
    }
}