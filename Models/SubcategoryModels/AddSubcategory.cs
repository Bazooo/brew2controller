namespace CSharpMongoGraphqlSubscriptions.Models.SubcategoryModels
{
    public class AddSubcategory
    {
        public string Name { get; set; } = null!;

        public int Rank { get; set; }

        public string CategoryId { get; set; } = null!;
    }

    public partial class Subcategory
    {
        public Subcategory(AddSubcategory addSubcategory)
        {
            this.Name = addSubcategory.Name;
            this.Rank = addSubcategory.Rank;
            this.CategoryId = addSubcategory.CategoryId;
        }
    }
}
