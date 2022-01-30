using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace eTickets.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new() //EntityBaserepository implementeaza interfata IEntityBaseRepository
    {

        private readonly AppDbContext _context; //injectam DBcontext
        public EntityBaseRepository(AppDbContext context) // constructor
        {
            _context = context;
        }

        //metoda generica de adaugare date in modele, urmata de salvarea lor in db

        public async Task AddAsync(T entity)
        { 
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        //metoda generica de stergere date in modele, urmata de salvarea stergerii in db
        //trebuie sa setam starea entitatii la deleted

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id); // luam id 
            EntityEntry entityEntry = _context.Entry(entity);
            entityEntry.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        //metoda generica pentru a afisa toate datele din modelul precizat

        public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();


        

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>(); // interogare in db, informatiile fiind transmise in variabila query
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)); //info din query se for afisa sub forma unei liste
            return await query.ToListAsync();
        }


        //metoda generica pentru a afisa toate datele din modelul precizat

        public async Task<T> GetByIdAsync(int id)  => await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);

        //metoda generica pentru a modifica datele din db
        //folosim Microsoft.EntityFrameworkCore.ChangeTracking pentru a defini noua entitate
        //va lua paramentru entity
        //trebuie sa setam starea entitatii la modified

        public async Task  UpdateAsync(int id, T entity)
        {
            EntityEntry entityEntry = _context.Entry(entity);
            entityEntry.State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }
    }
}