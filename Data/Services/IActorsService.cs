using eTickets.Data.Base;
using eTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{

    //interfata IActorsService mosteneste metodele din IEntityBaseRepository

    public interface IActorsService:IEntityBaseRepository<Actor>
    {
        
    }
}