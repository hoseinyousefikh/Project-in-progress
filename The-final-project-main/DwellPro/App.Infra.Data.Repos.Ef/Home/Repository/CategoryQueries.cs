namespace App.Infra.Data.Repos.Ef.Home.Repository
{
    public class CategoryQueries
    {
        public static string GetAllCategories = @"
        SELECT 
            c.Id AS Id, 
            c.Name, 
            c.ImageUrl,   
            c.IsDeleted
        FROM Categories c
        WHERE c.IsDeleted = 0";

        public static string GetSubCategoriesForCategory = @"
        SELECT 
            s.Id AS Id, 
            s.Name, 
            s.ImageUrl,    
            s.CategoryId
        FROM SubCategories s
        WHERE s.CategoryId = @CategoryId";

        public static string GetCategoryById = @"
        SELECT 
            c.Id AS Id, 
            c.Name, 
            c.ImageUrl,   
            c.IsDeleted
        FROM Categories c
        WHERE c.Id = @Id AND c.IsDeleted = 0";

        public static string GetSubCategoriesForCategoryId = @"
        SELECT 
            s.Id AS Id, 
            s.Name, 
            s.ImageUrl,   
            s.CategoryId
        FROM SubCategories s
        WHERE s.CategoryId = @CategoryId";
    }
}
