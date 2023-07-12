using Xunit;
using System.Net;
using System.Text.Json;
using FluentAssertions;
using TodoApi.Models;
using TestHelpers;
namespace TodoApi.Test.Test;

[Collection("Sequential")]
public class TodoItemsControllerTestTestGet
{
    [Trait("Category", "1. Corrija as rotas de buscar Tarefas")]
    [Theory(DisplayName = "Deve retornar o status code adequado nas rotas GET.")]
    [MemberData(nameof(DataTestTestGetRoutesStatusCode))]
    public async Task TestTestGetRoutesStatusCode(string path, HttpStatusCode expectedStatusCode, bool isCorrect)
    {
        var instance = Helpers.GetTestInstance();

        Func<Task> act = async () => await instance.TestGetRoutesStatusCode(path, expectedStatusCode);
        if (isCorrect)
        {
            await act.Should().NotThrowAsync<Xunit.Sdk.XunitException>();
        }
        else
        {
            await act.Should().ThrowAsync<Xunit.Sdk.XunitException>();
        }

        await act.Should().NotThrowAsync<NotImplementedException>();
    }

    public static TheoryData<string, HttpStatusCode, bool> DataTestTestGetRoutesStatusCode =>
        new()
        {
            {
                "/api/TodoItems",
                HttpStatusCode.OK,
                true
            },
            {
                "/api/TodoItems/5",
                HttpStatusCode.NotFound,
                true
            },
            {
                "/api/TodoItems/1",
                HttpStatusCode.OK,
                true
            },
            {
                "/api/TodoItems",
                HttpStatusCode.NotFound,
                false
            },
        };
}

[Collection("Sequential")]
public class TodoItemsControllerTestTestDelete
{
    [Trait("Category", "2. Corrija a rota de deletar tarefa")]
    [Theory(DisplayName = "Deve retornar o status code adequado nas rotas DELETE.")]
    [MemberData(nameof(DataTestTestDeleteRoutesStatusCode))]
    public async Task TestTestDeleteRouteStatusCode(string path, HttpStatusCode expectedStatusCode, bool isCorrect)
    {
        var instance = Helpers.GetTestInstance();

        Func<Task> act = async () => await instance.TestDeleteRouteStatusCode(path, expectedStatusCode);
        if (isCorrect)
        {
            await act.Should().NotThrowAsync<Xunit.Sdk.XunitException>();
        }
        else
        {
            await act.Should().ThrowAsync<Xunit.Sdk.XunitException>();
        }

        await act.Should().NotThrowAsync<NotImplementedException>();
    }

    public static TheoryData<string, HttpStatusCode, bool> DataTestTestDeleteRoutesStatusCode =>
        new()
        {
            {
                "/api/TodoItems/1",
                HttpStatusCode.NoContent,
                true
            },
            {
                "/api/TodoItems/5",
                HttpStatusCode.NotFound,
                true
            },
            {
                "/api/TodoItems/1",
                HttpStatusCode.NotFound,
                false
            },
        };
}

[Collection("Sequential")]
public class TodoItemsControllerTestTestPost
{
    [Trait("Category", "3. Corrija a rota de adicionar tarefas")]
    [Theory(DisplayName = "Deve retornar o status code adequado nas rotas POST.")]
    [MemberData(nameof(DataTestTestPostRoutesStatusCode))]
    public async Task TestTestPostRouteStatusCode(string path, string jsonToAdd, HttpStatusCode expectedStatusCode, bool isCorrect)
    {
        var instance = Helpers.GetTestInstance();

        Func<Task> act = async () => await instance.TestPostRouteStatusCode(path, jsonToAdd, expectedStatusCode);
        if (isCorrect)
        {
            await act.Should().NotThrowAsync<Xunit.Sdk.XunitException>();
        }
        else
        {
            await act.Should().ThrowAsync<Xunit.Sdk.XunitException>();
        }

        await act.Should().NotThrowAsync<NotImplementedException>();
    }
    public static TheoryData<string, string, HttpStatusCode, bool> DataTestTestPostRoutesStatusCode =>
        new()
        {
            {
                "/api/TodoItems",
                "{\"name\": \"Minha Tarefa\"}",
                HttpStatusCode.Created,
                true
            },
            {
                "/api/TodoItems",
                "{\"teste\": \"testando\"}",
                HttpStatusCode.BadRequest,
                true
            },
            {
                "/api/TodoItems",
                "{\"name\": \"Minha Tarefa\"}",
                HttpStatusCode.BadRequest,
                false
            },
        };
}

[Collection("Sequential")]
public class TodoItemsControllerTestTestPut
{
    [Trait("Category", "4. Corrija a rota de atualizar tarefas")]
    [Theory(DisplayName = "Deve retornar o status code adequado nas rotas PUT.")]
    [MemberData(nameof(DataTestTestPutRoutesStatusCode))]
    public async Task TestTestPutRouteStatusCode(string path, string jsonToUpdate, HttpStatusCode expectedStatusCode, bool isCorrect)
    {
        var instance = Helpers.GetTestInstance();

        Func<Task> act = async () => await instance.TestPutRouteStatusCode(path, jsonToUpdate, expectedStatusCode);
        if (isCorrect)
        {
            await act.Should().NotThrowAsync<Xunit.Sdk.XunitException>();
        }
        else
        {
            await act.Should().ThrowAsync<Xunit.Sdk.XunitException>();
        }

        await act.Should().NotThrowAsync<NotImplementedException>();
    }
    public static TheoryData<string, string, HttpStatusCode, bool> DataTestTestPutRoutesStatusCode =>
        new()
        {
            {
                "/api/TodoItems/1",
                "{ \"id\": 1, \"name\": \"Minha Tarefa\" }",
                HttpStatusCode.OK,
                true
            },
            {
                "/api/TodoItems/1",
                "{\"teste\": \"testando\"}",
                HttpStatusCode.BadRequest,
                true
            },
            {
                "/api/TodoItems/200",
                "{ \"id\": 200, \"name\": \"Minha Tarefa\"}",
                HttpStatusCode.BadRequest,
                false
            },
            {
                "/api/TodoItems/200",
                "{ \"id\": 200, \"name\": \"Minha Tarefa\"}",
                HttpStatusCode.NotFound,
                true
            },
        };
}
