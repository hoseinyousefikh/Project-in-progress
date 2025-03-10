using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Ef.Home.Repository
{
     public class SubCategoryQueries
    {
        public static string GetAllSubCategories = @"
        SELECT 
            s.Id AS Id, 
            s.Name, 
            s.ImageUrl,   
            s.IsDeleted,
            s.CategoryId
        FROM SubCategories s
        WHERE s.IsDeleted = 0";

        public static string GetSubCategoryById = @"
        SELECT 
            s.Id AS Id, 
            s.Name, 
            s.ImageUrl,   
            s.IsDeleted,
            s.CategoryId
        FROM SubCategories s
        WHERE s.Id = @Id AND s.IsDeleted = 0";

        public static string GetHomeServicesForSubCategory = @"
        SELECT 
            hs.Id AS Id, 
            hs.Name, 
            hs.SubCategoryId
        FROM HomeServices hs
        WHERE hs.SubCategoryId = @SubCategoryId";

        public static string GetCategoryForSubCategory = @"
        SELECT 
            c.Id AS Id, 
            c.Name, 
            c.ImageUrl
        FROM Categories c
        WHERE c.Id = @CategoryId";
    }
}
