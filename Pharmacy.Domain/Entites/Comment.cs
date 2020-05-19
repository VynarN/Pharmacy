using Pharmacy.Domain.Common.ValueObjects;

namespace Pharmacy.Domain.Entites
{
    public class Comment: AuditableEntity
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int Depth { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int MedicamentId { get; set; }
        public Medicament Medicament { get; set; }
    }
}
