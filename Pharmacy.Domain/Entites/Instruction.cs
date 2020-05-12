using System;
using System.Collections;

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

        public int MedicamentId { get; set; }
        public Medicament Medicament { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Instruction instruction &&
                   ActiveSubstance == instruction.ActiveSubstance &&
                   AuxiliarySubstances == instruction.AuxiliarySubstances &&
                   MedicamentForm == instruction.MedicamentForm &&
                   PhysicoChemicalProperties == instruction.PhysicoChemicalProperties &&
                   PharmacotherapeuticGroup == instruction.PharmacotherapeuticGroup &&
                   Pharmacodynamics == instruction.Pharmacodynamics &&
                   Pharmacokinetics == instruction.Pharmacokinetics &&
                   Indication == instruction.Indication &&
                   Contraindication == instruction.Contraindication &&
                   InteractionWithOthersMedicaments == instruction.InteractionWithOthersMedicaments &&
                   FeaturesOfApplication == instruction.FeaturesOfApplication &&
                   ApplicationMethodAndDose == instruction.ApplicationMethodAndDose &&
                   Overdose == instruction.Overdose &&
                   AverseReactions == instruction.AverseReactions &&
                   ExpirationDate == instruction.ExpirationDate &&
                   StorageConditions == instruction.StorageConditions &&
                   Packaging == instruction.Packaging &&
                   ByPrescription == instruction.ByPrescription &&
                   MedicamentId == instruction.MedicamentId;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(ActiveSubstance);
            hash.Add(AuxiliarySubstances);
            hash.Add(MedicamentForm);
            hash.Add(PhysicoChemicalProperties);
            hash.Add(PharmacotherapeuticGroup);
            hash.Add(Pharmacodynamics);
            hash.Add(Pharmacokinetics);
            hash.Add(Indication);
            hash.Add(Contraindication);
            hash.Add(InteractionWithOthersMedicaments);
            hash.Add(FeaturesOfApplication);
            hash.Add(ApplicationMethodAndDose);
            hash.Add(Overdose);
            hash.Add(AverseReactions);
            hash.Add(ExpirationDate);
            hash.Add(StorageConditions);
            hash.Add(Packaging);
            hash.Add(ByPrescription);
            hash.Add(MedicamentId);
            return hash.ToHashCode();
        }
    }
}
