using BaltaIoChallenge.WebApi.Exceptions.v1;
using BaltaIoChallenge.WebApi.Models.v1.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace BaltaIoChallenge.WebApi.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is CustomizedException)
                HandleCustomizedException(context);
            else
                ThrowUnknownError(context);
        }

        private static void ThrowUnknownError(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(new ResponseDto<string>($"Something went wrong. {context.Exception.Message}", 500));
        }

        private static void HandleCustomizedException(ExceptionContext context)
        {
            if (context.Exception is ObjectExistsException)
                HandleObjectExistsException(context);

            if (context.Exception is SpecificationException)
                HandleSpecificationException(context);

            if(context.Exception is EmailException)
                HandleEmailException(context);
        }

        private static void HandleSpecificationException(ExceptionContext context)
        {
            var specificationErrorException = context.Exception as SpecificationException;
            var statusCode = (int)HttpStatusCode.BadRequest;

            context.HttpContext.Response.StatusCode = statusCode;
            context.Result = new ObjectResult(GenerateResponse($"Validation errors: {specificationErrorException!.Message}", statusCode));
        }

        private static void HandleEmailException(ExceptionContext context)
        {
            var emailErrorException = context.Exception as EmailException;
            var statusCode = (int)HttpStatusCode.BadRequest;

            context.HttpContext.Response.StatusCode = statusCode;
            context.Result = new ObjectResult(GenerateResponse($"Invalid Email: {emailErrorException!.Message}", statusCode));
        }

        private static void HandleObjectExistsException(ExceptionContext context)
        {
            var validationErrorException = context.Exception as ObjectExistsException;
            var statusCode = (int)HttpStatusCode.BadRequest;

            context.HttpContext.Response.StatusCode = statusCode;
            context.Result = new ObjectResult(GenerateResponse(validationErrorException!.Message, statusCode));
        }

        private static ResponseDto<string> GenerateResponse(string message, int statusCode)
            => new(message, statusCode);
    }
}
