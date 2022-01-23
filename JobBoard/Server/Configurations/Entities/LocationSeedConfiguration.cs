using JobBoard.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobBoard.Server.Configurations.Entities
{
    public class LocationSeedConfiguration : IEntityTypeConfiguration<Location>

    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasData(
                new Location
                {
                    LocationId = 1,
                    Name = "Ang Mo Kio"
                },
                new Location
                {
                    LocationId = 2,
                    Name = "Tampines"
                }
                );

        }
    }
}
