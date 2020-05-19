namespace Pharmacy.Domain.Entites
{
    public class CommentResponse
    {
        public int CommentId { get; set; }
        public Comment Comment { get; set; }

        public int CommentResponseId { get; set; }
    }
}
