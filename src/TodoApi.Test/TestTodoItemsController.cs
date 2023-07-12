using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TodoApi.Models;

namespace TodoApi.Test;

public class TestTodoItemsController : IClassFixture<WebApplicationFactory<Program>>
{
    public HttpClient _client;

    public TestTodoItemsController(WebApplicationFactory<Program> factory)
    {
        _client = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddDbContext<TodoContext>(options =>
                {
                    options.UseInMemoryDatabase("TodoList");
                });

                var sp = services.BuildServiceProvider();
                
                var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<TodoContext>();
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                db.TodoItems.AddRange(GetTodoTestList());
                db.SaveChanges();
            });
        }).CreateClient();
    }

    [Theory]
    [InlineData("/api/TodoItems",HttpStatusCode.OK)]
    [InlineData("/api/TodoItems/5",HttpStatusCode.NotFound)]
    public async Task TestGetRoutesStatusCode(string path, HttpStatusCode expectedStatusCode)
    {
        throw new NotImplementedException();
    }

    [Theory]
    [InlineData("/api/TodoItems/1",HttpStatusCode.NoContent)]
    [InlineData("/api/TodoItems/5",HttpStatusCode.NotFound)]
    public async Task TestDeleteRouteStatusCode(string path, HttpStatusCode expectedStatusCode)
    {
        throw new NotImplementedException();
    }


    [Theory]
    [InlineData("/api/TodoItems", "{\"name\": \"Minha Tarefa\"}", HttpStatusCode.Created)]
    [InlineData("/api/TodoItems", "{\"teste\": \"testando\"}", HttpStatusCode.BadRequest)]
    public async Task TestPostRouteStatusCode(string path, string jsonToAdd, HttpStatusCode expectedStatusCode)
    {
        throw new NotImplementedException();
    }

    [Theory]
    [InlineData("/api/TodoItems/1", "{\"id\": 1, \"name\": \"Minha Tarefa\"}", HttpStatusCode.OK)]
    [InlineData("/api/TodoItems/200", "{\"id\": 200, \"name\": \"testando\"}", HttpStatusCode.NotFound)]
    [InlineData("/api/TodoItems/5", "{\"test\": \"testando\"}", HttpStatusCode.BadRequest)]
    public async Task TestPutRouteStatusCode(string path, string jsonToAdd, HttpStatusCode expectedStatusCode)
    {
        throw new NotImplementedException();
    }

    public static List<TodoItem> GetTodoTestList()
    {
        return new List<TodoItem>()
        {
            new TodoItem()
            {
                Id = 1,
                Name = "Teste",
                IsComplete = false
            },
            new TodoItem()
            {
                Id = 2,
                Name = "Teste 2",
                IsComplete = false
            },
            new TodoItem()
            {
                Id = 3,
                Name = "Teste 3",
                IsComplete = true
            },
        };
    }
}