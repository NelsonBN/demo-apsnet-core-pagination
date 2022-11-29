using Demo;
using Demo.Models;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<Repository>();


var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();



app.MapGet("all-data",
    Ok<List<Car>> (Repository repository, int? total)
        => TypedResults.Ok(repository.Generate(total ?? 100)));


app.MapGet("page-limit",
    Results<Ok<PagingLimitResponse>, BadRequest<string>> (
        Repository repository,
        int? offset,
        int? limit
    ) =>
    {
        if(offset < 0)
        {
            return TypedResults.BadRequest($"Min for {nameof(offset)} is 0");
        }

        if(limit < 0)
        {
            return TypedResults.BadRequest($"Min for {nameof(limit)} is 0");
        }
        else if(limit > 100)
        {
            return TypedResults.BadRequest($"Max for {nameof(limit)} is 100");
        }

        offset ??= 0;
        limit ??= 100;

        var list = repository.List();

        return TypedResults.Ok(new PagingLimitResponse()
        {
            Offset = offset.Value,
            Limit = limit.Value,
            Total = list.Count,
            List = list.Skip(offset.Value)
                       .Take(limit.Value)
        });
    });


app.MapGet("next-page",
    Results<Ok<PagingNextPageResponse>, BadRequest<string>> (
        Repository repository,
        int? offset,
        int? limit
    ) =>
    {
        if(offset < 0)
        {
            return TypedResults.BadRequest($"Min for {nameof(offset)} is 0");
        }

        if(limit < 0)
        {
            return TypedResults.BadRequest($"Min for {nameof(limit)} is 0");
        }
        else if(limit > 100)
        {
            return TypedResults.BadRequest($"Max for {nameof(limit)} is 100");
        }

        var list = repository.List();

        offset ??= 0;
        limit = Math.Min(limit ?? 100, list.Count);


        var end = offset + limit is
                var max
                 && max < list.Count ?
                    max :
                    list.Count;


        return TypedResults.Ok(new PagingNextPageResponse()
        {
            NextPage = (end < list.Count ? $"?{nameof(offset)}={end}&{nameof(limit)}={limit}" : null)!,
            Total = list.Count,
            List = list.Skip(offset.Value).Take(end.Value)
        });
    });


app.Run();
