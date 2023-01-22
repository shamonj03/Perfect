using CSharpFunctionalExtensions;
using Perfect.Api.Common.Models;

namespace Perfect.Api.Common.Extensions
{
    public static class ResultExtenions
    {
        public static Microsoft.AspNetCore.Http.IResult ToEnvelope<T>(this Result<T> result)
        {
            if (result.IsSuccess)
            {
                return Results.Ok(result.Value);
            }

            return Results.BadRequest(new Envelope(result.Error));
        }

        public static Microsoft.AspNetCore.Http.IResult ToEnvelope<T, TRespone>(this Result<T> result, Func<T, TRespone> map)
        {
            if (result.IsSuccess)
            {
                return Results.Ok(result.Map(map).Value);
            }

            return Results.BadRequest(new Envelope(result.Error));
        }

        public static async Task<Microsoft.AspNetCore.Http.IResult> ToEnvelope<T, TRespone>(this Task<Result<T>> task, Func<T, TRespone> map)
        {
            var result = await task;

            if (result.IsSuccess)
            {
                return Results.Ok(result.Map(map).Value);
            }

            return Results.BadRequest(new Envelope(result.Error));
        }
    }
}
