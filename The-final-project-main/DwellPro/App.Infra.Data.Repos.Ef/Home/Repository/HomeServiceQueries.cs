using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Ef.Home.Repository
{
    public class HomeServiceQueries
    {
        public static string GetAllHomeServices = @"
        SELECT 
            hs.Id AS Id, 
            hs.Name, 
            hs.ImageUrl,   
            hs.Description,
            hs.BasePrice,
            hs.ViewCount,
            hs.IsDeleted,
            hs.SubCategoryId
        FROM HomeServices hs
        WHERE hs.IsDeleted = 0";

        public static string GetHomeServiceById = @"
        SELECT 
            hs.Id AS Id, 
            hs.Name, 
            hs.ImageUrl,   
            hs.Description,
            hs.BasePrice,
            hs.ViewCount,
            hs.IsDeleted,
            hs.SubCategoryId
        FROM HomeServices hs
        WHERE hs.Id = @Id AND hs.IsDeleted = 0";

        public static string GetSubCategoryForHomeService = @"
        SELECT 
            s.Id AS Id, 
            s.Name, 
            s.ImageUrl,   
            s.CategoryId
        FROM SubCategories s
        WHERE s.Id = @SubCategoryId";

        public static string GetExpertHomeServicesForHomeService = @"
        SELECT 
            ehs.Id AS Id, 
            ehs.HomeServiceId, 
            ehs.ExpertId
        FROM ExpertSkills ehs
        WHERE ehs.HomeServiceId = @HomeServiceId";
    }
}
