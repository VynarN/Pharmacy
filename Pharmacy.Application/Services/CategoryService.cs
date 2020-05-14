using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Application.Common.Validators;
using Pharmacy.Domain.Entites;
using System.Collections.Generic;
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

        public async Task CreateService(string category)
        {
            StringArgumentValidator.ValidateStringArgument(category, nameof(category));

            var correctedCategory = char.ToUpper(category[0]) + category.Substring(1).ToLower();
            
            await _repository.Create(new Category() { Name = correctedCategory });
        }

        public IEnumerable<Category> GetCategories()
        {
            return _repository.GetAllQueryable().AsNoTracking();
        }
    }
}
