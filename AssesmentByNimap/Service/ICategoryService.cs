using AssesmentByNimap.Models;

namespace AssesmentByNimap.Service
{
    public interface ICategoryService
    {

        List<Category> GetAllCategories();
        int AddCategory(Category category);
        Category GetCategoryById(int categoryId);
        int UpdateCategory(Category category);
        int DeleteCategory(int categoryId);
    }
}
