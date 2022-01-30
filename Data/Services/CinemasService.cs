using eTickets.Data.Base;
using eTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class CinemasService : EntityBaseRepository<Cinema>, ICinemasService //mosteneste din EntityBaseRepository si ia ca parametru modelul Cinema + ICinemasService
    {
        public CinemasService(AppDbContext context) : base(context) // constructor care mosteneste contextul din clasa base
        {
        }
    }
}