using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Application.Common.Validators;
using Pharmacy.Domain.Entites;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repository;

        public CategoryService(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task CreateCategory(string category)
        {
            StringArgumentValidator.ValidateStringArgument(category, nameof(category));

            var correctedCategory = char.ToUpper(category[0]) + category.Substring(1).ToLower();
            
            await _repository.Create(new Category() { Name = correctedCategory });
        }

        public async Task DeleteCategory(Category category)
        {
            await _repository.Delete(category);
        }

        public IQueryable<Category> GetCategories()
        {
            return _repository.GetAllQueryable();
        }
    }
}
