using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace intSoft.MVC.Core.ActionResults
{
    public class StandardJsonActionResult : JsonResult
    {
        private readonly bool _isCamelCase;

        public StandardJsonActionResult(bool isCamelCase=true)
        {
            _isCamelCase = isCamelCase;
            ErrorMessages = new List<string>();
        }

        public StandardJsonActionResult(object data)
        {
            ErrorMessages = new List<string>();
            Data = data;
        }

        public IList<string> ErrorMessages { get; private set; }

        public void AddError(string errorMessage)
        {
            ErrorMessages.Add(errorMessage);
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var response = context.HttpContext.Response;
            response.ContentType = string.IsNullOrEmpty(ContentType) ? "application/json" : ContentType;

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            SerializeData(response);
        }

        protected virtual void SerializeData(HttpResponseBase response)
        {
            if (ErrorMessages.Any())
            {
                var originalData = Data;
                Data = new
                {
                    Success = false,
                    OriginalData = originalData,
                    ErrorMessage = string.Join("\n", ErrorMessages),
                    ErrorMessages = ErrorMessages.ToArray()
                };

                response.StatusCode = 400;
            }

            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };

            if (_isCamelCase)
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            response.Write(JsonConvert.SerializeObject(Data, settings));
        }
    }
}