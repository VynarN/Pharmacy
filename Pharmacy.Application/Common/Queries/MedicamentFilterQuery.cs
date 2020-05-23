namespace Pharmacy.Application.Common.Queries
{
    public class MedicamentFilterQuery
    {
        public string SearchValue { get; set; }

        public string Categories { get; set; }

        public string ApplicationMethods { get; set; }

        public string MedicamentForms { get; set; }

        public string AllowedFor { get; set; }

        public int PriceFrom { get; set; }

        public int PriceTo { get; set; } = 1;

        public bool OrderByPrice { get; set; }

        public bool InDescOrder { get; set; }
    }
}
