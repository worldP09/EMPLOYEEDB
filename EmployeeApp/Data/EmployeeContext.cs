using Microsoft.EntityFrameworkCore;
using EmployeeApp.Models;
using System.Collections.Generic;

namespace EmployeeApp.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
    }
}
