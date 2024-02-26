
using IUE7VU_HFT_2022231.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUE7VU_HFT_2022231.Repository
{
    public class IUE7VUDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Workout> Workouts { get; set; }

        public IUE7VUDbContext()
        {
            //this.ChangeTracker
            //       .Clear();
            //this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite(@"Data Source=.\WorkoutContext.db;")
                .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(person => person
                .HasOne(person => person.Trainer)
                .WithMany(trainer => trainer.Clients)
                .HasForeignKey(person => person.TrainerId)
                .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<Workout>()
                .HasOne(persone => persone.Person)
                .WithMany(p => p.Workouts)
                .HasForeignKey(w => w.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Person>()
                .HasOne(p => p.Memberships)
                .WithOne(m => m.Person)
                .HasForeignKey<Membership>(m => m.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Trainer>().HasData(new Trainer[]
            {
                new Trainer("1#Béla#22#Male"),
                new Trainer("2#Géza#21#Male"),
                new Trainer("3#Feri#20#Male"),
                new Trainer("4#Regi#24#Female"),
                new Trainer("5#Sefi#21#Male"),

            });

            modelBuilder.Entity<Person>().HasData(new Person[]
            {
                new Person("1#Hunor#22#Male#1"),
                new Person("2#Laci#21#Male#5"),
                new Person("3#Reni#18#Female#3"),
                new Person("4#Bucsy#21#Male#2"),
                new Person("5#Marko#20#Male#5"),
                new Person("6#Caramel#20#Male#5"),
            });

            modelBuilder.Entity<Workout>().HasData(new Workout[]
            {
                new Workout("1#Chest#1,5#0,5#Hard#2022*11*26#1"),
                new Workout("2#Back#1,2#0,2#Extreme#2022*11*24#1"),
                new Workout("3#Shoulders#1#0#Easy#2022*11*15#3"),
                new Workout("4#Legs#2#0#Medium#2022*10*29#4"),
                new Workout("5#Biceps#2#0,2#Easy#2022*11*21#5"),
            });

            modelBuilder.Entity<Membership>().HasData(new Membership[]
            {
                new Membership("1#Monthly#2022*10*30#2022*11*30#Red#1"),
                new Membership("2#Weekly#2022*10*23#2022*10*30#Green#2"),
                new Membership("3#Daily#2022*10*29#2022*10*30#Pink#3"),
                new Membership("4#Monthly#2022*10*20#2022*11*20#Blue#4"),
                new Membership("5#Daily#2022*11*25#2022*11*26#Red#5"),
            });
        }
    }
}
