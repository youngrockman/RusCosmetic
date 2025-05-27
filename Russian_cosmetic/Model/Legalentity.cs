using System;
using System.Collections.Generic;

namespace Russian_cosmetic.Model;

public partial class Legalentity
{
    public string ClientCode { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Inn { get; set; } = null!;

    public string AccountNumber { get; set; } = null!;

    public string Bik { get; set; } = null!;

    public string DirectorName { get; set; } = null!;

    public string ContactPerson { get; set; } = null!;

    public string ContactPhone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}
