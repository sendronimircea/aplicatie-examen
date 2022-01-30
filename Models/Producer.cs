using eTickets.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class Producer:IEntityBase // clasa Producer mosteneste din interfata mentionata
    {
        [Key]
        public int Id { get; set; } //identificatorul pentru clasa si tabela din DB

        [Display(Name = "Profile Picture URL")] //afisare in views Producers
        [Required(ErrorMessage = "Este necesara inserarea unei poze de profil")]

        public string ProfilePictureURL { get; set; } //proprietatile din modelul Producer


        [Display(Name = "FullName")]
        [Required(ErrorMessage = "Este necesar sa mentionati numele complet")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Numele trebuie sa fie intre 3 si 50 de caractere")]

        public string FullName { get; set; }

        [Display(Name = "Bio")]
        [Required(ErrorMessage = "Este necesar sa mentionati biografia persoanei")]

        public string Bio { get; set; }

        //schema relationala cu celelate modele
        public List<Movie> Movies { get; set; } //one to many Producers -- Movies

    }
}
