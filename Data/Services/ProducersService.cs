using eTickets.Data.Base;
using eTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class ProducersService : EntityBaseRepository<Producer>, IProducersService //mosteneste metodele generice din EntityBaseRepository cu paramentru Producer + din IProducersService
    {
        public ProducersService(AppDbContext context) : base(context) // legatura cu baza de date prin intermediul constructorului generic din base
        {

        }
    }
}
