using Flunt.Notifications;
using System.Text.Json.Serialization;

namespace BaltaIoChallenge.WebApi.Models.v1.Dtos
{
    public class ResponseDto<T>
    {
        protected ResponseDto() { }

        public ResponseDto(
            string message
            , int status
            , IEnumerable<Notification>? notifications = null)
        {
            Message = message;
            Status = status;
        }

        public ResponseDto(
            string message
            , T data
            , int status = 200
            )
        {
            Message = message;
            Status = status;
            Data = data;
        }

        public string Message { get; set; } = string.Empty;

        public int Status { get; set; } = 200;

        public bool IsSuccess => Status is >= 200 and <= 299;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; }
    }
}
