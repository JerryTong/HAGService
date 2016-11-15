using System.Web.Http.ExceptionHandling;

namespace HAGService.Handler
{
    public class GlobalExceptionHandler : ExceptionHandler    
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            //Write all exception handling logic here. Eg., Log into database/server, send mail.
        }

    }
}H