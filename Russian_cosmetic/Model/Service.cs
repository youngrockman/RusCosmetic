using System;
using System.Collections.Generic;

namespace Russian_cosmetic.Model;

public partial class Service
{
    public int ServiceId { get; set; }

    public string ServiceName { get; set; } = null!;

    public string ServiceCode { get; set; } = null!;

    public string ExecutionTime { get; set; } = null!;

    public decimal? DeviationValue { get; set; }

    public string? DeviationUnit { get; set; }

    public decimal StandardPrice { get; set; }

    public decimal? SpecialPriceRuscosm { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    
    public virtual ICollection<Orderservice> Orderservices { get; set; } = new List<Orderservice>();
}
