using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using intSoft.MVC.Core.Helpers.UiGrid;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace intSoft.MVC.Core.Extensions
{
    public static class UiGridDefinitionExtensions
    {
        public static string ToJson(this UiGridDefinition gridDefinition)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return JsonConvert.SerializeObject(gridDefinition, settings);
        }
    }
}
