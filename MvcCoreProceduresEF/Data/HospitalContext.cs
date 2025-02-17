﻿using Microsoft.EntityFrameworkCore;
using MvcCoreProceduresEF.Models;

namespace MvcCoreProceduresEF.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options) { }

        public DbSet<Doctor> Doctores { get; set; }

        public DbSet<Enfermo> Enfermos { get; set; }       

        public DbSet<VistaEmpleado> VistasEmpleados { get; set; }

        public DbSet<Trabajador> Trabajadores { get; set; }
    }
}
