using JobBoard.Shared.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobBoard.Server.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        Task Save(HttpContext httpContext);
        IGenericRepository<Appointment> Appointments { get; }
        IGenericRepository<Employer> Employers { get; }
        IGenericRepository<JS> JSs { get; }
        IGenericRepository<Listing> Listings { get; }
        IGenericRepository<Review> Reviews { get; }
        IGenericRepository<Enquiry> Enquirys { get; }
        IGenericRepository<Location> Locations { get; }
        IGenericRepository<Search> Searchs { get; }

  
    }
}