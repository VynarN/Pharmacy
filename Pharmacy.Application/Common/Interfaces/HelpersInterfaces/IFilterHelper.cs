using System.Linq;

namespace Pharmacy.Application.Common.Interfaces.HelpersInterfaces
{
    public interface IFilterHelper<EntityType, FilterType> where EntityType: class 
                                                           where FilterType: class
    {
        IQueryable<EntityType> Filter(IQueryable<EntityType> entities, FilterType filter);
    }
}
