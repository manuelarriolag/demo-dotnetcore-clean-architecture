using GreetingsApp.Adapters.Controllers;
using GreetingsCore.Model;
using NetArchTest.Rules;
using NUnit.Framework;

namespace GreetingsUnitTest
{
    public class ArchitectureTests
    {
        [Test]
        public void Core_Must_Not_Depend_On_App_Layer()
        {
            var result = Types.InAssembly(typeof(Greeting).Assembly)
                .That()
                .ResideInNamespace("GreetingsCore")
                .ShouldNot()
                .HaveDependencyOn("GreetingsApp")
                .GetResult();

            Assert.IsTrue(result.IsSuccessful, "GreetingsCore should not depend on GreetingsApp.");
        }

        [Test]
        public void Domain_Model_Must_Not_Depend_On_Adapters()
        {
            var result = Types.InAssembly(typeof(Greeting).Assembly)
                .That()
                .ResideInNamespace("GreetingsCore.Model")
                .ShouldNot()
                .HaveDependencyOn("GreetingsCore.Adapters")
                .GetResult();

            Assert.IsTrue(result.IsSuccessful, "GreetingsCore.Model should not depend on GreetingsCore.Adapters.");
        }

        [Test]
        public void Core_Ports_Must_Not_Depend_On_App_Layer()
        {
            var result = Types.InAssembly(typeof(Greeting).Assembly)
                .That()
                .ResideInNamespace("GreetingsCore.Ports")
                .ShouldNot()
                .HaveDependencyOn("GreetingsApp")
                .GetResult();

            Assert.IsTrue(result.IsSuccessful, "GreetingsCore.Ports should not depend on GreetingsApp.");
        }

        [Test]
        [Explicit("Aspirational AaC rule. Enable after moving DB access out of Controllers.")]
        public void Controllers_Should_Not_Depend_On_Core_Adapters_Db()
        {
            var result = Types.InAssembly(typeof(GreetingsController).Assembly)
                .That()
                .ResideInNamespace("GreetingsApp.Adapters.Controllers")
                .ShouldNot()
                .HaveDependencyOn("GreetingsCore.Adapters.Db")
                .GetResult();

            Assert.IsTrue(result.IsSuccessful,
                "Controllers should depend on Ports/Facades, not directly on Core Adapters.Db.");
        }
    }
}
