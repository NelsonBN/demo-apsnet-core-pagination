using Bogus;
using Demo.Models;

namespace Demo;

public class Repository
{
    private List<Car> _list = new();

    public List<Car> Generate(int total) => _list = new Faker<Car>()
        .RuleFor(r => r.Id, s => s.Random.Uuid())
        .RuleFor(r => r.Model, s => s.Vehicle.Model())
        .RuleFor(r => r.Manufacturer, s => s.Vehicle.Manufacturer())
        .RuleFor(r => r.Fuel, s => s.Vehicle.Fuel())
        .RuleFor(r => r.Type, s => s.Vehicle.Type())
        .Generate(total);

    public List<Car> List() => _list;
}
