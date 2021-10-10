namespace CSharpMongoGraphqlSubscriptions.Models.GaugeModels
{
    public class AddGauge
    {
        public string PhysicalId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public GaugeType Type { get; set; }

        public int Rank { get; set; }

        public bool Interactive { get; set; }

        public string SubcategoryId { get; set; } = null!;
    }

    public partial class Gauge
    {
        public Gauge(AddGauge addGauge)
        {
            this.PhysicalId = addGauge.PhysicalId;
            this.Name = addGauge.Name;
            this.Description = addGauge.Description;
            this.Type = addGauge.Type;
            this.Rank = addGauge.Rank;
            this.Interactive = addGauge.Interactive;
            this.SubcategoryId = addGauge.SubcategoryId;
        }
    }
}
