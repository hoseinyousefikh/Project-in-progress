using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Ef.Home.Repository
{
    public static class CityQueries
    {
        public static string GetCityById = @"
        SELECT 
            c.Id AS Id, 
            c.Name, 
            c.IsDeleted
        FROM Cities c
        WHERE c.Id = @Id AND c.IsDeleted = 0";

        public static string GetUsersForCityId = @"
        SELECT 
            u.Id AS Id, 
            u.FirstName, 
            u.LastName, 
            u.CityId
        FROM AspNetUsers u
        WHERE u.CityId = @CityId";



        public static string GetAllCities = @"
        SELECT 
            c.Id AS Id, 
            c.Name, 
            c.IsDeleted
        FROM Cities c
        WHERE c.IsDeleted = 0";

        public static string GetUsersForCity = @"
        SELECT 
            u.Id AS Id, 
            u.FirstName, 
            u.LastName, 
            u.CityId
        FROM AspNetUsers u
        WHERE u.CityId = @CityId";

    }
}
