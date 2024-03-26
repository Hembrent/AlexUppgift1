using System.Text.RegularExpressions;
using System.Net.Mail;

namespace UserRegistration;

public class UserRegistrationService
{
    private readonly List<User> _registeredUsers;

    public UserRegistrationService()
    {
        _registeredUsers = new List<User>(); // Our list of registered users!
    }

    public bool RegisterUser(string username, string password, string email)
    {
        // Validates user information, somewhat updated from an earlier version where I once only used the username.
        if (!ValidateUsername(username) || !ValidatePassword(password) || !ValidateEmail(email))
        {
            return false; // Registration failed due to invalid input, very simple.
        }

        // Checks if the username is already taken through the use of OrdinalIgnoreCase,
        // which not only checks that the letters are exactly the same but also ignores case sensitivity.
        // StringComparison is very literal, it compares two strings, that being two usernames.
        if (_registeredUsers.Any(user => user.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
        {
            return false; // The username is available.
        }

        // Creates a new User object and adds it to the list of registered users!
        var newUser = new User { Username = username, Password = password, Email = email };
        _registeredUsers.Add(newUser);

        return true; // And done, registration is successful.
    }

    public string GetRegistrationConfirmationMessage(string username)
    {
        return $"User '{username}' has been successfully registered."; // What it says on the tin, it's just a confirmation message.
    }

    public bool ValidateUsername(string username)
    {
        // Checks if username length is between 5 and 20 characters, otherwise throwing a false and denying the username.
        if (username.Length < 5 || username.Length > 20)
        {
            return false;
        }

        // Checks if username consists only of letters or digits, by utilizing Char and looping on the item 'c' through 'username'.
        // In doing so, we can check if each position of the username is either a letter or a digit, and thus enforce an alphanumeric name.
        // Even if it's all aaaa or 1111.
        // I tried RegEx once, but it just didn't want to work out how I wanted it to. For some reason, it would play with only numbers
        // or only letters, but not a fully alphanumeric username. This worked much simpler.
        foreach (char c in username)
        {
            if (!char.IsLetter(c) && !char.IsDigit(c))
            {
                return false;
            }
        }

        // Username is valid if it passes all checks, simple as that.
        return true;
    }

    public bool ValidatePassword(string password)
    {
        // Checking if password length is at least 8 characters, very simple stuff.
        if (password.Length < 8)
        {
            return false;
        }

        // Defining a set of special characters, because I couldn't think of a simpler way to check for them.
        // Would RegEx here too, but I have even less of a clue how to cover all characters. "!-?" perhaps?
        string specialCharacters = "!@#$%^&*()-_+=[]{}|;:,.<>?";

        // Checking if a password contains at least one special character,
        // utilizing .Any to run through the 'password' with my 'specialCharacters' variable.
        // Hitting it with a ! at the start means we're running a negative check, so if there ISN'T a special character, we throw it out.
        if (!password.Any(c => specialCharacters.Contains(c)))
        {
            return false;
        }

        // The password meets all criteria, huzzah.
        return true;
    }

    public bool ValidateEmail(string email)
    {
        // Checking if the email address is null or empty, in which case we dismiss it and return a false.
        if (string.IsNullOrEmpty(email))
        {
            return false;
        }

        // Covering for case sensitivity, emails should all default to lowercase. Nobody's gonna write 'EmAiL@hOtMaIl.CoM'.
        // And if they do, I'm going to dismiss it and them for being funny little guys.
        email = email.ToLower();

        try
        {
            // Utilizing the System.Net.Mail, we can create a MailAddress instance with the provided email.
            // An excellent and easy way to handle the formatting of emails, and the simulation of providing emails.
            MailAddress mailAddress = new MailAddress(email);

            // If the email is valid, it returns true.
            return true;
        }
        catch (FormatException)
        {
            // But if the email address is invalid, a FormatException is thrown and we return false.
            // Hadn't tried on the Mail class yet, so I wanted to see how this works.
            return false;
        }
    }
}
