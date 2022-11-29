namespace Demo.Models;

public class PagingLimitResponse
{
    public int Offset { get; set; } = default!;
    public int Limit { get; set; } = default!;
    public int Total { get; set; } = default!;
    public IEnumerable<Car> List { get; set; } = default!;
}

public class PagingNextPageResponse
{
    public string NextPage { get; set; } = default!;
    public int Total { get; set; } = default!;
    public IEnumerable<Car> List { get; set; } = default!;
}
