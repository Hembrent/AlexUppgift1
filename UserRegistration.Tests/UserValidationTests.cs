using System.Net.Mail;

namespace UserRegistration.Tests
{
    [TestClass]

    // Welcome to one of two test classes, they've been split to make it a little easier to read them all,
    // and they test somewhat different things. In order to run the tests, you'll want to hit right click on the MSTest class project itself,
    // the "UserRegistration.Tests" project, and hit "Run Tests." If you use Code instead of regular VS,
    // you may want to run your test in the Terminal instead, or perhaps download a convenient addon for a testing UI.
    // Once you have your tests run, just keep an eye on the test method names, they're pretty self-explanatory.
    public class UserValidationTest
    {
        private UserRegistrationService userService;

        [TestInitialize]
        public void Setup()
        {
            userService = new UserRegistrationService();
        }

        [TestMethod]
        public void TestValidUsername_UsernameContainingLettersAndNumbersShouldBeTrue()
        {
            UserRegistrationService userRegistration = new UserRegistrationService();

            // Here we perform the validation itself, with an alphanumeric name.
            bool validationResult = userRegistration.ValidateUsername("validUser123");

            // Here we check if validation was successful.
            Assert.IsTrue(validationResult);
        }

        [TestMethod]
        public void TestValidUsername_UsernameContainingOnlyLettersShouldBeTrue()
        {
            UserRegistrationService userRegistration = new UserRegistrationService();

            // Here we perform the validation, now with all letters.
            bool validationResult = userRegistration.ValidateUsername("validUser");

            // Here we check if validation was successful.
            Assert.IsTrue(validationResult);
        }

        [TestMethod]
        public void TestValidUsername_UsernameContainingSomeInternationalLettersShouldBeTrue()
        {
            UserRegistrationService userRegistration = new UserRegistrationService();

            // Here we perform the validation, now with all letters.
            bool validationResult = userRegistration.ValidateUsername("vålidÜsér");

            // Here we check if validation was successful.
            Assert.IsTrue(validationResult);
        }

        [TestMethod]
        public void TestValidUsername_UsernameContainingOnlyNumbersShouldBeTrue()
        {
            UserRegistrationService userRegistration = new UserRegistrationService();

            // Here we perform the validation, this time with all numbers.
            bool validationResult = userRegistration.ValidateUsername("123456");

            // Here we check if validation was successful.
            Assert.IsTrue(validationResult);
        }

        [TestMethod]
        public void TestValidUsername_UsernameThatDoesNotMatchValidFormatShouldBeFalse()
        {
            UserRegistrationService userRegistration = new UserRegistrationService();

            // Perform validation with an invalid username, as can be seen with the use of special characters instead of numbers.
            bool validationResult = userRegistration.ValidateUsername("BAD_USERNAME!");

            // Checking if validation failed.
            Assert.IsFalse(validationResult);
        }

        [TestMethod]
        public void TestValidUsername_ShortNamesAreInvalidAndShouldBeFalse()
        {
            UserRegistrationService userRegistration = new UserRegistrationService();

            // Checking validation with a short username.
            bool validationResult = userRegistration.ValidateUsername("dgf");

            // Checking if validation failed.
            Assert.IsFalse(validationResult);
        }

        [TestMethod]
        public void TestValidUsername_LongNamesAreInvalidAndShouldBeFalse()
        {
            UserRegistrationService userRegistration = new UserRegistrationService();

            // Checking validation with a long username.
            bool validationResult = userRegistration.ValidateUsername("LoooooooooooooooooooooooooooooongName");

            // Checking if validation failed.
            Assert.IsFalse(validationResult);
        }

        [TestMethod]
        public void TestValidPassword_PasswordWithAtLeast8CharactersAndHavingAtLeastOneSpecialCharacterShouldBeTrue()
        {
            UserRegistrationService userRegistration = new UserRegistrationService();

            // Performing validation with a totally normal password, utilizing at least one special character.
            bool validationResult = userRegistration.ValidatePassword("ValidPassword!");

            // Checking if validation was successful.
            Assert.IsTrue(validationResult);
        }

        [TestMethod]
        public void TestValidPassword_PasswordBelow8CharactersShouldBeFalse()
        {
            UserRegistrationService userRegistration = new UserRegistrationService();

            // Performing validation with a VERY BAD PASSWORD.
            bool validationResult = userRegistration.ValidatePassword("bad!");

            // Checking if validation was successful.
            Assert.IsFalse(validationResult);
        }

        [TestMethod]
        public void TestValidPassword_PasswordMissingASpecialCharacterShouldBeFalse()
        {
            UserRegistrationService userRegistration = new UserRegistrationService();

            // Performing validation with an invalid password. It lacks a special character like !.
            bool validationResult = userRegistration.ValidatePassword("InvalidPassword");

            // Checking if validation was successful.
            Assert.IsFalse(validationResult);
        }

        [TestMethod]
        public void TestValidPassword_PasswordWithOnlySpecialCharactersShouldBeTrue()
        {
            UserRegistrationService userRegistration = new UserRegistrationService();

            // Performing validation with a password containing only special characters, don't blame me if you didn't want this.
            bool validationResult = userRegistration.ValidatePassword("!@#$%^&*");

            // Checking if validation was successful.
            Assert.IsTrue(validationResult);
        }

        [TestMethod]
        public void TestValidEmail_EmailFollowingValidFormatShouldBeTrue()
        {
            UserRegistrationService userRegistration = new UserRegistrationService();

            // Performing validation with a perfectly valid email address.
            bool validationResult = userRegistration.ValidateEmail("user@example.com");

            // Checking if validation was successful.
            Assert.IsTrue(validationResult);
        }

        [TestMethod]
        public void TestValidEmail_EmailUsingAnInvalidFormatShouldBeFalse()
        {
            UserRegistrationService userRegistration = new UserRegistrationService();

            // Performing validation with a very valid, not at all strange, stupid or suspicious email address.
            bool validationResult = userRegistration.ValidateEmail("userexample,com");

            // Checking if validation was successful
            Assert.IsFalse(validationResult);
        }

        [TestMethod]
        public void TestValidEmail_EmailThatIsEmptyShouldBeFalse()
        {
            UserRegistrationService userRegistration = new UserRegistrationService();

            // Performing validation with an empty email.
            bool validationResult = userRegistration.ValidateEmail("");

            // Checking if validation was successful. Or should it not be successful if it's false?
            Assert.IsFalse(validationResult);
        }

        [TestMethod]
        public void TestValidEmail_EmailThatIsNotCaseSensitiveShouldBeTrue()
        {
            UserRegistrationService userRegistration = new UserRegistrationService();

            // Performing validation with a very wonky email using both upper- and lowercase characters.
            bool validationResult = userRegistration.ValidateEmail("EmAiL@hOtMaIl.CoM");

            // Checking if validation was successful. Are you tired of reading this line yet?
            Assert.IsTrue(validationResult);
        }


    }
}