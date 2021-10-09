namespace CSharpMongoGraphqlSubscriptions.Models.CategoryModels
{
    public class AddCategory
    {
        public string Name { get; set; } = null!;

        public string Color { get; set; } = null!;

        public int Rank { get; set; }
    }

    public partial class Category
    {
        public Category(AddCategory addCategory)
        {
            this.Name = addCategory.Name;
            this.Color = addCategory.Color;
            this.Rank = addCategory.Rank;
        }
    }
}
