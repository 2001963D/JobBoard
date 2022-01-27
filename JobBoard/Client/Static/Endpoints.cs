using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobBoard.Client.Static
{
    public static class Endpoints
    {
        private static readonly string Prefix = "api";

        public static readonly string AppointmentsEndpoint = $"{Prefix}/appointments";
        public static readonly string EmployersEndpoint = $"{Prefix}/employers";
        public static readonly string EnquirysEndpoint = $"{Prefix}/enquirys";
        public static readonly string JSsEndpoint = $"{Prefix}/jSs";
        public static readonly string ListingsEndpoint = $"{Prefix}/listings";
        public static readonly string LocationsEndpoint = $"{Prefix}/locations";
        public static readonly string ReviewsEndpoint = $"{Prefix}/reviews";
    }
}
