using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TodoApi.Test;
using TodoApi.Models;

namespace TestHelpers 
{
    public static class Helpers
    {
        public static TestTodoItemsController GetTestInstance()
        {
            var factory = new WebApplicationFactory<TodoApi.Program>();
            TestTodoItemsController instance = new(factory);
            instance._client = factory.WithWebHostBuilder(builder => {
                builder.ConfigureServices(services =>
                {
                    services.AddDbContext<TodoContext>(options =>
                    {
                        options.UseInMemoryDatabase("TodoList");
                    });

                    var sp = services.BuildServiceProvider();

                    using (var scope = sp.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        var db = scopedServices.GetRequiredService<TodoContext>();
                        db.Database.EnsureDeleted();
                        db.Database.EnsureCreated();
                        db.TodoItems.AddRange(GetTodoTestList());
                        db.SaveChanges();
                    }
                });
            }).CreateClient();

            return instance;
        }


        public static List<TodoItem>  GetTodoTestList()
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
}
