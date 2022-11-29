using System;

namespace Demo.Models;

public class Car
{
    public Guid Id { get; set; } = default!;

    public string Model { get; set; } = default!;
    public string Manufacturer { get; set; } = default!;
    public string Fuel { get; set; } = default!;
    public string Type { get; set; } = default!;

}
