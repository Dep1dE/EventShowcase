using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventShowcase.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventShowcase.DataAccess.Postgres.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasMany(u => u.Users).WithMany(e => e.Events);

            builder.HasMany(i => i.Images).WithOne(e => e.Event).HasForeignKey(i => i.EventId);
        }
    }
}
