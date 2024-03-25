using System.Text.RegularExpressions;
using System.Net.Mail;

namespace UserRegistration;

public class UserRegistrationService
{
    private readonly List<string> _usernames = new List<string>();

    public bool RegisterUser(string username)
    {
        if (string.IsNullOrEmpty(username))
        {
            throw new ArgumentException("Username cannot be null or empty.", nameof(username)); // Pretty self-explanatory in its message.
        }

        foreach (var nameInList in _usernames)
        {
            if (nameInList.ToUpper().Equals(username.ToUpper()))
            {
                return false; // Username already exists.
            }
        }

        _usernames.Add(username);
        return true; // Registration successful.
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
