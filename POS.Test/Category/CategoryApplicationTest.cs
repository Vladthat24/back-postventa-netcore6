using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POS.Aplication.Dtos.Category.Request;
using POS.Aplication.Interfaces;
using POS.Utilites.Static;

namespace POS.Test.Category
{
    [TestClass]
    public class CategoryApplicationTest
    {
        private static WebApplicationFactory<Program>? _factory = null;
        private static IServiceScopeFactory? _scopeFactory = null;

        [ClassInitialize]
        public static void Initalize(TestContext _testContext)
        {
            _factory = new CustomWebAplicationFactory();
            _scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
        }

        [TestMethod]
        public async Task RegisterCategory_WhenSendingNullValuesOrEmpty_ValidationErrors()
        {
            using var scope= _scopeFactory?.CreateScope();
            var context = scope?.ServiceProvider.GetService<ICategoryApplication>();

            //Arrange
            var name = "";
            var descripcion = "";
            var state = 1;
            var expected = ReplyMessage.MESSAGE_VALIDATE;

            //Act
            var result = await context!.RegisterCategory(new CategoryRequestDto()
            {
                Name = name,
                Description = descripcion,
                State = state
            });

            var current = result.Message;

            //Assert
            Assert.AreEqual(expected, current);
        }

    }
}
