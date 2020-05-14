using Pharmacy.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.ApplicationInterfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();

        Task CreateService(string category);
    }
}
