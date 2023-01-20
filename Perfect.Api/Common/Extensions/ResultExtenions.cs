﻿using Perfect.Api.Common.Models;

namespace Perfect.Api.Common.Extensions
{
    public static class ResultExtenions
    {
        public static IResult ToEnvelope(this CSharpFunctionalExtensions.Result result)
        {
            if (result.IsSuccess)
            {
                return Results.Ok();
            }

            return Results.BadRequest(new Envelope(result.Error));
        }

        public static IResult ToEnvelope<T>(this CSharpFunctionalExtensions.Result<T> result)
        {
            if (result.IsSuccess)
            {
                return Results.Ok(result.Value);
            }

            return Results.BadRequest(new Envelope(result.Error));
        }
    }
}