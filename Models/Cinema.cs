using eTickets.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class Cinema : IEntityBase // clasa mosteneste din interfata IEntityBase
    {
        [Key]
        public int Id { get; set; } //identificatorul pentru clasa si tabela din DB

        [Display(Name = "Cinema Logo")] //afisare in views Cinemas
        [Required(ErrorMessage = "Logo-ul este obligatoriu")] //proprieatea obligatorie atunci cand se adauga un nou film = validari

        public string Logo { get; set; } //proprietatile din modelul Cinema

        [Display(Name = "Cinema Name")]
        [Required(ErrorMessage = "Numele este obligatoriu")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Numele trebuie sa fie intre 3 si 50 de caractere")]

        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Descrierea este obligatorie")]
        public string Description { get; set; }

        //schema relationala cu celelate modele
        public List<Movie> Movies { get; set; } //one to many Cinema -- Movies
    }
}
