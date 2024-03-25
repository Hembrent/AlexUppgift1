using System.Net.Mail;

namespace UserRegistration.Tests
{
    [TestClass]
    public class UserRegistrationServiceTest
    {
        private UserRegistrationService userService;

        [TestInitialize]
        public void Setup()
        {
            userService = new UserRegistrationService();
        }

        // Test User Input Validation
        [TestMethod]
        public void TestRegistration()
        {
            UserRegistrationService userRegistration = new UserRegistrationService();

            // Here we perform registration.
            bool registrationResult = userRegistration.RegisterUser("testUser");

            // Here we check if registration was successful.
            Assert.IsTrue(registrationResult);
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
        public void TestValidUsername_UsernameThatDoesNotMatchRegExShouldBeFalse()
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
        public void TestValidPassword_PasswordShouldBeAtLeast8CharactersLongAndHaveAtLeastOneSpecialCharacterShouldBeTrue()
        {
            UserRegistrationService userRegistration = new UserRegistrationService();

            // Performing validation with a totally normal password, utilizing at least one special character.
            bool validationResult = userRegistration.ValidatePassword("ValidPassword!");

            // Checking if validation was successful.
            Assert.IsTrue(validationResult);
        }

        [TestMethod]
        public void TestValidPassword_PasswordShouldNotBeBelow8CharactersShouldBeFalse()
        {
            UserRegistrationService userRegistration = new UserRegistrationService();

            // Performing validation with a VERY BAD PASSWORD.
            bool validationResult = userRegistration.ValidatePassword("bad!");

            // Checking if validation was successful.
            Assert.IsFalse(validationResult);
        }

        [TestMethod]
        public void TestValidPassword_PasswordShouldNotBeMissingASpecialCharacterShouldBeFalse()
        {
            UserRegistrationService userRegistration = new UserRegistrationService();

            // Performing validation with an invalid password.
            bool validationResult = userRegistration.ValidatePassword("ValidPassword");

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
        public void TestValidEmail_EmailShouldFollowValidFormatShouldBeTrue()
        {
            UserRegistrationService userRegistration = new UserRegistrationService();

            // Performing validation with a perfectly valid email address.
            bool validationResult = userRegistration.ValidateEmail("user@example.com");

            // Checking if validation was successful.
            Assert.IsTrue(validationResult);
        }

        [TestMethod]
        public void TestValidEmail_EmailShouldNotUseAnInvalidFormatShouldBeFalse()
        {
            UserRegistrationService userRegistration = new UserRegistrationService();

            // Performing validation with a very valid, not at all strange, stupid or suspicious email address.
            bool validationResult = userRegistration.ValidateEmail("userexample,com");

            // Checking if validation was successful
            Assert.IsFalse(validationResult);
        }

        [TestMethod]
        public void TestValidEmail_EmailShouldNotBeEmptyShouldBeFalse()
        {
            UserRegistrationService userRegistration = new UserRegistrationService();

            // Performing validation with an empty email.
            bool validationResult = userRegistration.ValidateEmail("");

            // Checking if validation was successful. Or should it not be successful if it's false?
            Assert.IsFalse(validationResult);
        }

        [TestMethod]
        public void TestValidEmail_EmailShouldNotBeCaseSensitiveShouldBeTrue()
        {
            UserRegistrationService userRegistration = new UserRegistrationService();

            // Performing validation with a very wonky email using both upper- and lowercase characters.
            bool validationResult = userRegistration.ValidateEmail("EmAiL@hOtMaIl.CoM");

            // Checking if validation was successful. Are you tired of reading this line yet?
            Assert.IsTrue(validationResult);
        }
    }
}