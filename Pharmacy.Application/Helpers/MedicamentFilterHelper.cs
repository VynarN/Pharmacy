using Pharmacy.Application.Common.Interfaces.HelpersInterfaces;
using Pharmacy.Application.Common.Queries;
using Pharmacy.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pharmacy.Application.Helpers
{
    public class MedicamentFilterHelper : IFilterHelper<Medicament, MedicamentFilterQuery>
    {
        public IQueryable<Medicament> Filter(IQueryable<Medicament> medicaments, MedicamentFilterQuery filter)
        {
            return FilterByCategory(filter.Categories,
                                    FilterByMedicamentForm(filter.MedicamentForms,
                                    FilterByApplicationMethod(filter.ApplicationMethods,
                                    FilterByAllowedFor(filter.AllowedFor, medicaments))));
        }

        public IQueryable<Medicament> FilterByCategory(string categories, IQueryable<Medicament> medicaments)
        {
            if (!string.IsNullOrEmpty(categories))
            {
                var filteredMedicaments = new List<Medicament>();

                var parsedCategories = categories.Split(",");

                foreach (var categoryId in parsedCategories)
                {
                    filteredMedicaments.AddRange(medicaments.Where(med => med.CategoryId == int.Parse(categoryId)));
                }

                return filteredMedicaments.AsQueryable();
            }

            return medicaments;
        }

        public IQueryable<Medicament> FilterByMedicamentForm(string medicamentsForms, IQueryable<Medicament> medicaments)
        {
            if (!string.IsNullOrEmpty(medicamentsForms))
            {
                var filteredMedicaments = new List<Medicament>();

                var parsedMedicamentsForms = medicamentsForms.Split(",");

                foreach (var medicamentFormId in parsedMedicamentsForms)
                {
                    filteredMedicaments.AddRange(medicaments.Where(med => med.MedicamentFormId == int.Parse(medicamentFormId)));
                }

                return filteredMedicaments.AsQueryable();
            }

            return medicaments;
        }

        public IQueryable<Medicament> FilterByApplicationMethod(string applicationMethods, IQueryable<Medicament> medicaments)
        {
            if (!string.IsNullOrEmpty(applicationMethods))
            {
                var filteredMedicaments = new List<Medicament>();

                var parsedApplicationMethods = applicationMethods.Split(",");

                foreach (var applicationMethodId in parsedApplicationMethods)
                {
                    filteredMedicaments.AddRange(medicaments.Where(med => med.ApplicationMethodId == int.Parse(applicationMethodId)));
                }

                return filteredMedicaments.AsQueryable();
            }

            return medicaments;
        }

        public IQueryable<Medicament> FilterByAllowedFor(string allowedForOptions, IQueryable<Medicament> medicaments)
        {
            if (!string.IsNullOrEmpty(allowedForOptions))
            {
                var filteredMedicaments = new List<Medicament>();

                var parsedAllowedForOptions = allowedForOptions.ToLower().Split(",");

                foreach(var allowedForOption in parsedAllowedForOptions)
                {
                    filteredMedicaments.AddRange(FilterByAllowedForOption(allowedForOption, medicaments));
                }

                return filteredMedicaments.AsQueryable();
            }

            return medicaments;
        }

        static IQueryable<Medicament> FilterByAllowedForOption(string allowedForOption, IQueryable<Medicament> medicaments) => allowedForOption switch
        {
            "forchildren"   => medicaments.Where(med => med.AllowedForEntity.ForChildren),
            "foradult"      => medicaments.Where(med => med.AllowedForEntity.ForAdults),
            "fornurses"     => medicaments.Where(med => med.AllowedForEntity.ForNurses),
            "forpregnants"  => medicaments.Where(med => med.AllowedForEntity.ForPregnants),
            "fordrivers"    => medicaments.Where(med => med.AllowedForEntity.ForDrivers),
            "forallergist"  => medicaments.Where(med => med.AllowedForEntity.ForAllergist),
            "fordiabetics"  => medicaments.Where(med => med.AllowedForEntity.ForDiabetics),
                        _   => throw new ArgumentException(allowedForOption, nameof(allowedForOption))
        };
    }
}
