using BaltaIoChallenge.WebApi.Exceptions.v1;
using BaltaIoChallenge.WebApi.Models.v1.ValueObjects;
using System.Text.RegularExpressions;

namespace BaltaIoChallenge.WebApi.Models.ValueObjects
{
    public partial class Email : ValueObject
    {
        private const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        public Email(string address)
        {
            IsValidEmail(address);

            Address = address.ToLower();
        }

        public string Address { get; } = string.Empty;

        public static implicit operator Email(string address) => new(address);

        public static implicit operator string(Email email) => email.ToString();

        public override string ToString() => Address;

        private static void IsValidEmail(string address)
        {
            if (string.IsNullOrEmpty(address)) throw new EmailException("E-mail cannot be null.");

            if (address.Length < 5) throw new EmailException("E-mail cannot contain less than five characters.");

            if (!EmailRegex().IsMatch(address)) throw new EmailException("Invalid E-mail format.");
        }

        [GeneratedRegex(Pattern)]
        private static partial Regex EmailRegex();
    }
}
