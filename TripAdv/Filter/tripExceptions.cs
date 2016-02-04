using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;


public class tripExceptions : ExceptionFilterAttribute
{

    class ErrorMessage
    {
        public string Message { get; set; }
    }

    class WarningMessage
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }


    /// <summary>
    /// Odchyceni vyjimek typu WSException a odeslani klientovi kod: 400 Bad Request + JSON s popisem chyby

    /// </summary>
    /// <param name="context"></param>
    public override void OnException(HttpActionExecutedContext context)
    {
        // jedna se o vyjimku vyvolanou pri testovani claims
        if (context.Exception.GetType().Name == "SecurityException")
        {
            throw new HttpResponseException(context.Request.CreateResponse(HttpStatusCode.Unauthorized));
        }

        if (context.Exception.Message == "Forbidden")
        {
            throw new HttpResponseException(context.Request.CreateResponse(HttpStatusCode.Forbidden, new ErrorMessage() { Message = context.Exception.InnerException.Message }));
        }

        if (context.Exception.Message == "Warning")
        {
            string status = (context.Exception.Data["status"] != null) ? context.Exception.Data["status"].ToString() : string.Empty;
            WarningMessage warningMessage = new WarningMessage() { Status = status, Message = context.Exception.InnerException.Message };
            throw new HttpResponseException(context.Request.CreateResponse(HttpStatusCode.OK, warningMessage));
        }
        

    }
}