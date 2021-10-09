namespace CSharpMongoGraphqlSubscriptions.Models.CategoryModels
{
    public class UpdateCategory
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Color { get; set; } = null!;
        
        public int Rank { get; set; }
    }

    public partial class Category
    {
        public Category(UpdateCategory updateCategory)
        {
            Id = updateCategory.Id;
            Name = updateCategory.Name;
            Color = updateCategory.Color;
            Rank = updateCategory.Rank;
        }
    }
}