using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Entities.Entities;


namespace VeterinaryClinic.Data.Context
{

    public partial class VeterinaryClinicContext :  Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext
    {
        public VeterinaryClinicContext(DbContextOptions<VeterinaryClinicContext> options)
         : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointment { get; set; }
        public virtual DbSet<DocumentType> DocumentType { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Owner> Owner { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<Race> Race { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<Specialty> Specialty { get; set; }
        public virtual DbSet<Specie> Specie { get; set; }
        public virtual DbSet<WorkStation> WorkStation { get; set; }




    }
}
