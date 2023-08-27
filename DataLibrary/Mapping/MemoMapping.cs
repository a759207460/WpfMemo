using DataLibrary.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Mapping
{
    public class MemoMapping:EntityTypeConfiguration<Menmo>
    {
        public override void Configure(EntityTypeBuilder<Menmo> builder)
        {
            builder.ToTable("Menmo");
            builder.HasKey(x => x.Id).IsClustered();
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Title);
            builder.Property(x => x.Content);
            builder.Property(x => x.Status);
            builder.Property(x => x.CreateTime);
            builder.Property(x => x.UpdateTime);
        }
    }
}
