namespace CSharpMongoGraphqlSubscriptions.Models.GaugeModels
{
    public class UpdateGauge
    {
        public string Id { get; set; } = null!;

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
        public Gauge(UpdateGauge updateGauge)
        {
            this.Id = updateGauge.Id;
            this.PhysicalId = updateGauge.PhysicalId;
            this.Name = updateGauge.Name;
            this.Description = updateGauge.Description;
            this.Type = updateGauge.Type;
            this.Rank = updateGauge.Rank;
            this.Interactive = updateGauge.Interactive;
            this.SubcategoryId = updateGauge.SubcategoryId;
        }
    }
}
