using BaltaIoChallenge.WebApi.Models.v1.Dtos.LocalizationDto.LocalizationManagementDto;
using Flunt.Notifications;
using Flunt.Validations;
using System.Text.RegularExpressions;

namespace BaltaIoChallenge.WebApi.Specifications.v1
{
    public static partial class LocalizationManagementRequestSpecification
    {
        private const string StatePattern = @"^[a-zA-Z]*$";
        private const string StringWithoutNumbersPattern = @"^[^0-9]+$";

        public static Contract<Notification> Ensure(LocalizationManagementRequestDto request)
            => new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(request.Id, "Id", "Id field must have a value.")
                .IsNotNullOrEmpty(request.State, "State", "State field must have a value.")
                .IsTrue(OnlyLettersRegex().IsMatch(request.State), "State", "State must have only letters.")
                .IsTrue(request.State.Length == 2, "State", "State should only have 2 characters")
                .IsNotNullOrEmpty(request.City, "City", "City field must have a value.")
                .IsTrue(StringWithoutNumbersRegex().IsMatch(request.City), "City", "City cannot contain numbers.");


        [GeneratedRegex(StatePattern)]
        private static partial Regex OnlyLettersRegex();

        [GeneratedRegex(StringWithoutNumbersPattern)]
        private static partial Regex StringWithoutNumbersRegex();
    }
}
