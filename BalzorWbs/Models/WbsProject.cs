namespace BalzorWbs.Models;

public class WbsProject
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public List<WbsTask> RootTasks { get; set; } = new();

    public decimal TotalCharge => RootTasks.Sum(t => t.TotalCharge);
    public decimal TotalConsumed => RootTasks.Sum(t => t.TotalConsumed);
    public decimal TotalRaf => RootTasks.Sum(t => t.TotalRaf);

    public decimal ConsumptionPercent => TotalCharge > 0 ? Math.Round(100 * TotalConsumed / TotalCharge, 0) : 0;

    public decimal AdvancePercent => TotalCharge > 0 ? Math.Round(100 * (TotalConsumed + TotalRaf) / TotalCharge, 0) : 0;

    public DateTime LatestDelivery => RootTasks.Count == 0 ? DateTime.Today : RootTasks.Max(t => t.LatestDelivery);
}
