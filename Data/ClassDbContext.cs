
using Microsoft.EntityFrameworkCore;
using Tarefas.Models;

namespace Tarefas.Data
{
    /// <summary>
    /// Objeto para representação da base na memoria
    /// </summary>
    public class ClassDbContext : DbContext
    {
        /// <summary>
        /// Representação da tabela Tarefas
        /// </summary>
        public DbSet<MdTarefas> objTbTarefas { get; set; }
        
        public DbSet<MdTipo> objTbTipo { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseSqlite(connectionString: "DataSource=app.db;Cache=Shared");


    }
}
