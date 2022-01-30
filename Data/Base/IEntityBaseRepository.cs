using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace eTickets.Data.Base
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new() // interfata generica astfel ca primeste parametrul T de tip clasa
                                                                                  //serviciile care vor mosteni din aceasta interfata trebuie sa mosteneasca si din IEntityBase
                                                                                  // definim metodele
    {
        Task<IEnumerable<T>> GetAllAsync();

        //metoda generica pentru a afisa toate filmele, care are parametru un array cu numele includeProperties

        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);


        Task<T> GetByIdAsync(int id);

        Task AddAsync(T entity);

        Task UpdateAsync(int id, T entity);

        Task DeleteAsync(int id);
    }
}