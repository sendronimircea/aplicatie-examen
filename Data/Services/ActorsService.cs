using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using eTickets.Data.Base;

namespace eTickets.Data.Services
{
    public class ActorsService : EntityBaseRepository<Actor>, IActorsService //mosteneste din EntityBaseRepository si ia ca parametru modelul Actor
    {
       
        public ActorsService(AppDbContext context) : base(context) { } // constructor care mosteneste din contextul din clasa base

    }
}