namespace DemoMvc6
{
    using DemoMvc6.Domain;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class DemoContext: DbContext
    {
       public virtual DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-QKJJ83R;Database=DemoMvc6;Integrated Security=False;User ID=sa;Password=sa;");
        }
    }
}
