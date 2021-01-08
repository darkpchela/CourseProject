namespace BusinessLayer.Models
{
    public class CreateCollectionResultModel
    {
        public bool Succeed { get; set; }

        public int CollectionId { get; set; }

        public string Error { get; set; }
    }
}