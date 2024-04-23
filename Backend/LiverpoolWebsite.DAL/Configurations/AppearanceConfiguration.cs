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
    public class AppearanceConfiguration : IEntityTypeConfiguration<Appearance>
    {
        public void Configure(EntityTypeBuilder<Appearance> builder)
        {
            builder.HasKey(x => new { x.PlayerId, x.MatchId});
            builder.HasOne(x => x.Player)
                .WithMany(x => x.Appearances)
                .HasForeignKey(x => x.PlayerId);

            builder.HasOne(x => x.Match)
                .WithMany(x => x.Appearances)
                .HasForeignKey(x => x.MatchId);
        }
    }
}
