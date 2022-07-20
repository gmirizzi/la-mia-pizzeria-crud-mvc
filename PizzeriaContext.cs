using Microsoft.EntityFrameworkCore;
using la_mia_pizzeria_static.Models;

public class PizzeriaContext : DbContext
{
    public DbSet<Pizza> Pizzas { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=pizze-db;Integrated Security=True");
    }
}