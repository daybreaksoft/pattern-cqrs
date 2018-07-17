namespace AspNetCore.EF.Sample.Data.Entities
{
    public class ConstEntity
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string DisplayText { get; set; }

        public string Description { get; set; }

        public bool Enabled { get; set; }
    }
}
