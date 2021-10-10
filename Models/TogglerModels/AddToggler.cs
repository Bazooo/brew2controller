namespace CSharpMongoGraphqlSubscriptions.Models.TogglerModels
{
    public class AddToggler
    {
        public string PhysicalId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int Rank { get; set; }

        public bool Interactive { get; set; }

        public string SubcategoryId { get; set; } = null!;
    }

    public partial class Toggler
    {
        public Toggler(AddToggler addToggler)
        {
            this.PhysicalId = addToggler.PhysicalId;
            this.Name = addToggler.Name;
            this.Description = addToggler.Description;
            this.Rank = addToggler.Rank;
            this.Interactive = addToggler.Interactive;
            this.SubcategoryId = addToggler.SubcategoryId;
        }
    }
}
