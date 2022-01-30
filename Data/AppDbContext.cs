 using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;




namespace eTickets.Data
{

    public class AppDbContext: DbContext   //mosteneste clasa DbContext din Entity Framework
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) //constructor care are ca parametru DbContextOptions,
                                                                                    //paramentru cu numele de options
        {
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)//instructiuni pentru translatorul dinter DB si Models
        {
            modelBuilder.Entity<Actor_Movie>().HasKey(am => new //definim combinatie de primary keys
                                                                         // am = Actors_Movies, prescurtarea variabilei
            {
                am.ActorId,  //parametru
                am.MovieId
            });

            //definim modelul Actor_Movie drept joining tabel si relatia dintre Movie si Actor_Movie
            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Movie).WithMany(am => am.Actors_Movies).HasForeignKey(m => m.MovieId);//tipul relatiei si definim Fkey

            //definim modelul Actor_Movie drept joining tabel si relatia dintre Actor si Actor_Movie
            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Actor).WithMany(am => am.Actors_Movies).HasForeignKey(m => m.ActorId);//tipul relatiei si definim Fkey


            base.OnModelCreating(modelBuilder);
        }

        //definim numele tabelelor din DB

        public DbSet<Actor> Actors { get; set; } //numele va fi Actors etc.
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor_Movie> Actors_Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Producer> Producers { get; set; }


    }
}
