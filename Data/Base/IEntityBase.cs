using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Base // model de interfata
{
    public interface IEntityBase
    {
        int Id { get; set; } // proprietate comuna tuturor modelelor create
    }
}
