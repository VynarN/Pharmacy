namespace Pharmacy.Domain.Entites
{
    public class Image
    {
        public int Id { get; set; }

        public byte[] ImageData { get; set; }

        public int MedicamentId { get; set; }

        public Medicament Medicament { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Image);
        }

        public bool Equals(Image image)
        {
            var AreArraysEqual = true;
            if (ImageData.Length == image.ImageData.Length)
            {
                for (int i = 0; i < ImageData.Length; i++)
                {
                    if (ImageData[i] != image.ImageData[i])
                    {
                        AreArraysEqual = false;
                        break;
                    }
                }
            }
            else
            {
                AreArraysEqual = false;
            }
            return image != null && AreArraysEqual;
        }

        public override int GetHashCode()
        {
            return ImageData.GetHashCode() * 13;
        }
    }
}
