using System.Runtime.InteropServices;
using clinic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace clinic.Data
{

	public class AppDBContext : DbContext 
	{
		public AppDBContext (DbContextOptions  options) : base (options)
		{

		}
		public DbSet<Patient> patients { get; set;  }
        public DbSet<Doctor> doctors { get; set; }  
        public DbSet<Contact> contacts { get; set; }
        public DbSet<Admin>   admins { get; set;}
        public DbSet<Comment> comments { get; set; }    
        public DbSet<Ticket>  tickets { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Patient config 

			modelBuilder.Entity<Patient> ().HasKey(p => p.Id);	

			modelBuilder.Entity<Patient>
				().Property(p=>p.Fname).HasColumnType("VARCHAR").HasMaxLength(30).IsRequired();

            modelBuilder.Entity<Patient>
                ().Property(p => p.Email).HasColumnType("VARCHAR").HasMaxLength(255).IsRequired();
            modelBuilder.Entity<Patient>
                ().Property(p => p.Lname).HasColumnType("VARCHAR").HasMaxLength(30).IsRequired();
            modelBuilder.Entity<Patient>
             ().Property(p => p.Password).HasColumnType("VARCHAR").HasMaxLength(20).IsRequired();
            modelBuilder.Entity<Patient>
                ().Property(p => p.Phone).HasColumnType("VARCHAR").HasMaxLength(11).IsRequired();


            // Doctor  config  

            modelBuilder.Entity<Doctor>().HasKey(p => p.Id);

            modelBuilder.Entity<Doctor>
                ().Property(p => p.Fname).HasColumnType("VARCHAR").HasMaxLength(30).IsRequired();

            modelBuilder.Entity<Doctor>
                ().Property(p => p.Email).HasColumnType("VARCHAR").HasMaxLength(255).IsRequired();
            modelBuilder.Entity<Doctor>
                ().Property(p => p.Lname).HasColumnType("VARCHAR").HasMaxLength(30).IsRequired();
            modelBuilder.Entity<Doctor>
             ().Property(p => p.Password).HasColumnType("VARCHAR").HasMaxLength(20).IsRequired();
            modelBuilder.Entity<Doctor>
                ().Property(p => p.Phone).HasColumnType("VARCHAR").HasMaxLength(11).IsRequired();
            modelBuilder.Entity<Doctor>
                ().Property(p => p.Dr_picture).HasColumnType("VARCHAR"); 
            // Contact  config   

            modelBuilder.Entity<Contact>().HasKey(c => c.Id); 
            modelBuilder.Entity<Contact>().Property(c=>c.Email).HasColumnType("VARCHAR").HasMaxLength(255).IsRequired();
            modelBuilder.Entity<Contact>().Property(c => c.Name).HasColumnType("VARCHAR").HasMaxLength(60).IsRequired();
            modelBuilder.Entity<Contact>
              ().Property(c => c.Phone).HasColumnType("VARCHAR").HasMaxLength(11).IsRequired();
            modelBuilder.Entity<Contact>
                ().Property(c => c.Message).HasColumnType("VARCHAR").HasMaxLength(255).IsRequired();

            // Doctor Contact relations 
            modelBuilder.Entity<Doctor>().HasMany(d => d.contacts).
                WithOne(d => d.doctor).HasForeignKey(c => c.D_id).HasPrincipalKey(d=>d.Id);


            // Doctors Data 
            modelBuilder.Entity<Doctor>().HasData(LoadDoctors());

            // admin Ticket 

            modelBuilder.Entity<Admin>()
                .HasMany(a => a.tickets).WithOne(a => a.admin)
                .HasForeignKey(t => t.A_id).IsRequired(false);
            // Doctor  Ticket 
            modelBuilder.Entity<Doctor>()
          .HasMany(a => a.tickets).WithOne(a => a.Doctor)
          .HasForeignKey(t => t.D_id).IsRequired(true);

            modelBuilder.Entity<Patient>()
          .HasMany(a => a.tickets).WithOne(a => a.patient)
          .HasForeignKey(t => t.P_id).IsRequired(true);


            // *

            modelBuilder.Entity<Doctor>()
            .HasMany(a => a.admins).WithOne(a => a.doctor)
             .HasForeignKey(t => t.D_id).IsRequired(true);


            modelBuilder.Entity<Comment>().HasKey(c => c.Id);
            modelBuilder.Entity<Comment>()
                .Property(c => c.D_Email).HasColumnType("varchar").HasMaxLength(50);

            modelBuilder.Entity<Comment>().HasKey(c => c.Id);
            modelBuilder.Entity<Comment>()
                .Property(c => c.Massege).HasColumnType("varchar"); 
            modelBuilder.Entity<Patient>()
                .HasMany(p => p.comments).
                WithOne(c => c.patient).HasForeignKey(c => c.P_ID).IsRequired(); 


        }



        private static List<Doctor> LoadDoctors()
        {
            return new List<Doctor>
            {

                new Doctor {Id = 1 ,  Fname = "Samwel" , Lname
                = "Marzouk" , Email = "DrSamwel@gmail.com" 
                , Password="Sa147258" , Phone = "01005415303"} , 



                new Doctor { Id = 2 , Fname = "Mina" , Lname
                = "Marzouk" , Email = "DrMina@gmail.com"
                , Password="Ma147258" , Phone = "01005415404"}

            }; 
        }
    }
}
