using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmacy.Application.Common.Constants;
using System;
using System.Net;

namespace Pharmacy.Api.Auxiliary
{
    public static class ControllersAuxiliary
    {
        public static ObjectResult LogExceptionAndReturnError(Exception ex, ILogger logger, HttpResponse response)
        {
            logger.LogError(ex, ex.Message);

            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return new ObjectResult(ExceptionStrings.Exception);
        }
    }
}
