using eTickets.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; //pentru [Key]
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
                                    //clasa va prelua sau trimite date catre db. Va avea patru proprietati care vor corespunde celor patru coloane din tabelul din db
    public class Actor:IEntityBase //mosteneste din interfata mentionata
    {
        [Key]
        public int Id { get; set; } //identificatorul pentru clasa si tabela din DB

        [Display(Name = "Profile Picture URL")] //afisare in views Actors
        [Required(ErrorMessage ="Poza de profil este obligatorie")] //proprieatea obligatorie atunci cand se adauga un nou actor = validari
        public string ProfilePictureURL { get; set; } //proprietatile din modeulul Actor

        [Display(Name = "FullName")]
        [Required(ErrorMessage = "Numele este obligatoriu")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Numele trebuie sa fie intre 3 si 50 de caractere")]
        public string FullName { get; set; }

        [Display(Name = "Bio")]
        [Required(ErrorMessage = "Biografia este obligatorie")]
        public string Bio { get; set; }

        //schema relationala cu celelate modele
        public List<Actor_Movie> Actors_Movies { get; set; } //many to many Actors -- Movies
    }
}
