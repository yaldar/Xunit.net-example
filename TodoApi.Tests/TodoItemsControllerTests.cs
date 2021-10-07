using Xunit.Abstractions;
using Moq;
using TodoApi.Controllers;
using TodoApi.Models;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace UnitTests.TodoApi.Controllers
{
    public class TodoItemsControllerTests
    {
       
        private ITestOutputHelper logger;
        public TodoItemsControllerTests(ITestOutputHelper logger)
        {

            this.logger = logger;
        }

        [Fact]
        public async void TodoItemsController_DeleteTodoItem_GetsCorrectNumberOfTodos()
        {
            // Arrange
            var Item1 = new Mock<TodoItem>().Object; 
            //we can use a mock TodoItem object or a real one like the line below.
            var Item2 = new TodoItem { Id = 2, IsComplete = false, Name = "my second todo" };
            // NOTE: if your arrange part is the same for every test method then you can set it up in the test class constructor.
            var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: "unit_test_db")
            .Options;

            var context = new TodoContext(options);
            var controller = new TodoItemsController(context);

            context.TodoItems.Add(Item1);
            context.TodoItems.Add(Item2);
            await context.SaveChangesAsync();

            // Act
            var response = await controller.GetTodoItems();
            var todoCollection = response.Value;

            // Assert
            Assert.Equal(2, (todoCollection as ICollection<TodoItem>).Count);


        }

    }
}