using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRegistration.Tests
{
    [TestClass]

    // Welcome to one of two test classes, they've been split to make it a little easier to read them all,
    // and they test somewhat different things. In order to run the tests, you'll want to hit right click on the MSTest class project itself,
    // the "UserRegistration.Tests" project, and hit "Run Tests." If you use Code instead of regular VS,
    // you may want to run your test in the Terminal instead, or perhaps download a convenient addon for a testing UI.
    // Once you have your tests run, just keep an eye on the test method names, they're pretty self-explanatory.
    public class UserRegistrationTests
    {
        // This is a very interesting class I came across during my work on this little solution. A means to access and retrieve information
        // about the tests within this context. In my case, I use it to tidily fetch the inputted username and then display it in a message.
        // This is a class I'd like to experiment more with, it seems very convenient.
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestRegisterUser_UserBeingAbleToBeRegisteredShouldBeTrue()
        {
            // Arrange the UserRegistrationService object.
            var userRegistration = new UserRegistrationService();

            // Act and create a variable to carry the registered user.
            bool registrationResult = userRegistration.RegisterUser("testuser", "password123!", "testuser@example.com");

            // Assert that it's true, that they have successfully been registered, then store it for a message.
            Assert.IsTrue(registrationResult, "Registration should succeed.");
            Assert.AreEqual("User 'testuser' has been successfully registered.", userRegistration.GetRegistrationConfirmationMessage("testuser"));
            
            // Output confirmation message comes in here, letting me read directly in my test that we have successfully registered!
            string confirmationMessage = userRegistration.GetRegistrationConfirmationMessage("testuser");
            TestContext.WriteLine("Registration confirmation: " + confirmationMessage);
        }

        [TestMethod]
        public void TestRegisterUser_RegistrationShouldNotPermitEmptyInputSpaces()
        {
            // Arrange
            var userRegistration = new UserRegistrationService();

            // Act on an empty input.
            bool registrationResult = userRegistration.RegisterUser("", "password123", "testuser@example.com");

            // Assert
            Assert.IsFalse(registrationResult, "Registration should fail due to invalid input.");
        }

        [TestMethod]
        public void TestRegisterUser_RegistrationShouldNotPermitInvalidEmailFormat()
        {
            // Arrange
            var userRegistration = new UserRegistrationService();

            // Act on an invalid email format.
            bool registrationResult = userRegistration.RegisterUser("Username", "password123", "testuserexamplecom");

            // Assert
            Assert.IsFalse(registrationResult, "Registration should fail due to invalid input.");
        }

        [TestMethod]
        public void TestRegisterUser_RegistrationShouldNotPermitInvalidPasswordFormat()
        {
            // Arrange
            var userRegistration = new UserRegistrationService();

            // Act on an invalid password format.
            bool registrationResult = userRegistration.RegisterUser("Username", "password", "testuser@example.com");

            // Assert
            Assert.IsFalse(registrationResult, "Registration should fail due to invalid input.");
        }

        [TestMethod]
        public void TestRegisterUser_TakenUsernamesShouldNotBeAvailableForNewUsers()
        {
            // Arrange
            var userRegistration = new UserRegistrationService();
            userRegistration.RegisterUser("existinguser", "password123!", "existinguser@example.com");

            // Act on EXISTINGUSER and the fact that they already exist.
            bool registrationResult = userRegistration.RegisterUser("existinguser", "password456!", "newuser@example.com");

            // Assert
            Assert.IsFalse(registrationResult, "Registration should fail due to username already taken.");
        }
    }
}
