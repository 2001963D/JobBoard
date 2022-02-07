using JobBoard.Server.Data;
using JobBoard.Server.IRepository;
using JobBoard.Server.Models;
using JobBoard.Shared.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JobBoard.Server.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<Appointment> _appointments;
        private IGenericRepository<Employer> _employers;
        private IGenericRepository<JS> _jSs; 
        private IGenericRepository<Listing> _listings;
        private IGenericRepository<Review> _reviews;
        private IGenericRepository<Enquiry> _enquirys;
        private IGenericRepository<Location> _locations;
        private IGenericRepository<Search> _searchs;


        private UserManager<ApplicationUser> _userManager;

        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IGenericRepository<Appointment> Appointments
            => _appointments ??= new GenericRepository<Appointment>(_context);
        public IGenericRepository<Employer> Employers
            => _employers ??= new GenericRepository<Employer>(_context);
        public IGenericRepository<JS> JSs
            => _jSs ??= new GenericRepository<JS>(_context);
        public IGenericRepository<Location> Locations
            => _locations ??= new GenericRepository<Location>(_context);
        public IGenericRepository<Listing> Listings
            => _listings ??= new GenericRepository<Listing>(_context);
        public IGenericRepository<Review> Reviews
            => _reviews ??= new GenericRepository<Review>(_context);
        public IGenericRepository<Enquiry> Enquirys
            => _enquirys ??= new GenericRepository<Enquiry>(_context);
        public IGenericRepository<Search> Searchs
            => _searchs ??= new GenericRepository<Search>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save(HttpContext httpContext)
        {
            //To be implemented
            //string user = "System";
            var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userId);

            var entries = _context.ChangeTracker.Entries()
                .Where(q => q.State == EntityState.Modified ||
                    q.State == EntityState.Added);

            foreach (var entry in entries)
            {
                ((BaseDomainModel)entry.Entity).DateUpdated = DateTime.Now;
                ((BaseDomainModel)entry.Entity).UpdatedBy = user.UserName;
                if (entry.State == EntityState.Added)
                {
                    ((BaseDomainModel)entry.Entity).DateCreated = DateTime.Now;
                    ((BaseDomainModel)entry.Entity).CreatedBy = user.UserName;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}