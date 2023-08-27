using DataLibrary.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary
{
    public  class DbWPFContext:DbContext
    {
        public DbWPFContext(DbContextOptions<DbWPFContext> option):base(option)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                var typeconfiguration = Assembly.Load("DataLibrary").GetTypes().Where(q => q.GetInterface(typeof(IEntityTypeConfiguration<>).FullName) != null);
                foreach (var type in typeconfiguration)
                {
                    if (!type.FullName.Contains("EntityTypeConfiguration"))
                    {
                        dynamic configuration = Activator.CreateInstance(type);
                        modelBuilder.ApplyConfiguration(configuration);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<ToDo> ToDos { get; set; }

        public DbSet<Menmo> Menmos { get; set; }
    }
}
