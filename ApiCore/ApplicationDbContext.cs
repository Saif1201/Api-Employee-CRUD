using ApiCore.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCore
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
		{
          
		}
		public DbSet<Employee> Employees { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Employee>().HasData(
				new Employee() { Id = 20, Name = "Ayesha", Salary = 45000, UAN = "qwertyuiop12345", CreatedDate = DateTime.Now },
				new Employee() { Id = 21, Name = "Ayesha", Salary = 45000, UAN = "poiuytrewq54321", CreatedDate = DateTime.Now },
				new Employee() { Id = 22, Name = "Ayesha", Salary = 45000, UAN = "mnbvcxz12345", CreatedDate = DateTime.Now }
				);
		}
	}
}
