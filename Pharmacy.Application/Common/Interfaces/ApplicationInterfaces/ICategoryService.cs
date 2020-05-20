using Pharmacy.Domain.Entites;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.ApplicationInterfaces
{
    public interface ICategoryService
    {
        IQueryable<Category> GetCategories();

        Task CreateCategory(string category);

        Task DeleteCategory(Category category);
    }
}
