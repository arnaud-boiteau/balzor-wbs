using System;
using System.Collections.Generic;
using System.Linq;

namespace BalzorWbs.Models;

public class WbsTask
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Title { get; set; } = string.Empty;

    public decimal Charge
    {
        get => _charge;
        set => _charge = Math.Max(0, value);
    }

    public decimal Consumed
    {
        get => _consumed;
        set => _consumed = Math.Max(0, value);
    }

    public decimal Raf
    {
        get => _raf;
        set => _raf = Math.Max(0, value);
    }

    public DateTime DeliveryDate { get; set; } = DateTime.Today;

    public List<WbsTask> Children { get; set; } = new();

    private decimal _charge;
    private decimal _consumed;
    private decimal _raf;

    public bool IsLeaf => Children.Count == 0;

    public decimal TotalCharge => IsLeaf ? Charge : Children.Sum(c => c.TotalCharge);
    public decimal TotalConsumed => IsLeaf ? Consumed : Children.Sum(c => c.TotalConsumed);
    public decimal TotalRaf => IsLeaf ? Raf : Children.Sum(c => c.TotalRaf);

    public decimal ConsumptionPercent => TotalCharge > 0 ? Math.Round(100 * TotalConsumed / TotalCharge, 0) : 0;

    public decimal AdvancePercent => TotalCharge > 0 ? Math.Round(100 * (TotalConsumed + TotalRaf) / TotalCharge, 0) : 0;

    public DateTime LatestDelivery => IsLeaf ? DeliveryDate : Children.Max(c => c.LatestDelivery);
}
