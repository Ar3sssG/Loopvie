using LoopvieCommon.Exceptions;
using Newtonsoft.Json;
using LoopvieCommon.Models.Error;

namespace LoopvieCommon.Helpers
{
    
    public static class ExceptionHelper
    {
        public static void ThrowBadRequest(string message = null)
        {
            throw new HttpException(400, message != null ? GetErrorModel(message) : "bad_request");
        }

        public static void ThrowUnauthorized(string message = null)
        {
            throw new HttpException(401, message != null ? GetErrorModel(message) : "unauthorized");
        }

        public static void ThrowPaymentRequired(string message = null)
        {
            throw new HttpException(402, message != null ? GetErrorModel(message) : "payment_required");
        }

        public static void ThrowResourseNotfound(string message = null)
        {
            throw new HttpException(404, message != null ? GetErrorModel(message) : "resource_not_found");
        }

        public static void ThrowConflict(string message = null)
        {
            throw new HttpException(409, message != null ? GetErrorModel(message) : "conflict_with_server_state");
        }

        public static void ThrowInternalServerError(string message = null)
        {
            throw new HttpException(500, message != null ? GetErrorModel(message) : "internal_server_error");
        }

        public static void ThrowServiceUnavailable(string message = null)
        {
            throw new HttpException(503, message != null ? GetErrorModel(message) : "service_unavailable");
        }

        public static void ThrowThrowedException(Exception exception)
        {
            throw exception;
        }

        private static string GetErrorModel(string message)
        {
            ErrorModel error = new ErrorModel
            {
                Key = message
            };

            return JsonConvert.SerializeObject(error);
        }


        //remove
        //public static void ReportInternalServerError(Exception ex)
        //{
        //    throw new HttpException(500, ex != null ? JsonObject(ex.Message) : "Problem occured: Internal Server Error");
        //}
        //public static void ReportServiceUnavailable(Exception ex)
        //{
        //    throw new HttpException(503, ex != null ? JsonObject(ex.Message) : "Problem occured: Server Unavailable");
        //}
        //public static void ReportConflict<T>(T model)
        //{
        //    throw new HttpException(409, model != null ? JsonConvert.SerializeObject(model) : "Conflict with data");
        //}
    }
}
