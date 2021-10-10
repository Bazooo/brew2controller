namespace CSharpMongoGraphqlSubscriptions.Models.TogglerModels
{
    public class UpdateToggler
    {
        public string Id { get; set; } = null!;

        public string PhysicalId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int Rank { get; set; }

        public bool Interactive { get; set; }

        public string SubcategoryId { get; set; } = null!;
    }

    public partial class Toggler
    {
        public Toggler(UpdateToggler updateToggler)
        {
            this.Id = updateToggler.Id;
            this.PhysicalId = updateToggler.PhysicalId;
            this.Name = updateToggler.Name;
            this.Description = updateToggler.Description;
            this.Rank = updateToggler.Rank;
            this.Interactive = updateToggler.Interactive;
            this.SubcategoryId = updateToggler.SubcategoryId;
        }
    }
}
