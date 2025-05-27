namespace Russian_cosmetic.Model;

public partial class Orderservice
{
    public int OrderId { get; set; }
    public int ServiceId { get; set; }

    public virtual Order Order { get; set; } = null!;
    public virtual Service Service { get; set; } = null!;
}