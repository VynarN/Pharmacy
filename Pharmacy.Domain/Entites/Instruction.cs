namespace Pharmacy.Domain.Entites
{
    public class Instruction
    {
        public int Id { get; set; }

        public string ActiveSubstance { get; set; }

        public string AuxiliarySubstances { get; set; }

        public string MedicamentForm { get; set; }

        public string PhysicoChemicalProperties { get; set; }

        public string PharmacotherapeuticGroup { get; set; }

        public string Pharmacodynamics { get; set; }

        public string Pharmacokinetics { get; set; }

        public string Indication { get; set; }

        public string Contraindication { get; set; }

        public string InteractionWithOthersMedicaments { get; set; }

        public string FeaturesOfApplication { get; set; }

        public string ApplicationMethodAndDose { get; set; }

        public string Overdose { get; set; }

        public string AverseReactions { get; set; }

        public string ExpirationDate { get; set; }

        public string StorageConditions { get; set; }

        public string Packaging { get; set; }

        public bool ByPrescription { get; set; }
    }
}
