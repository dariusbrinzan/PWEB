using LiverpoolWebsite.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolWebsite.DAL.Configurations
{
    class CompetitionConfiguration : IEntityTypeConfiguration<Competition>
    {
        public void Configure(EntityTypeBuilder<Competition> builder)
        {
            builder.HasKey(x => x.CompetitionId);

            builder.Property(x => x.CompetitionId).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired();
        }
    }
} 
