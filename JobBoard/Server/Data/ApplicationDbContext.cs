using IdentityServer4.EntityFramework.Options;
using JobBoard.Server.Models;
using JobBoard.Shared.Domain;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobBoard.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Employer> Employers { get; set; }

        public DbSet<Listing> Listings { get; set; }
        public DbSet<JS> Js { get; set; }
    }
}
