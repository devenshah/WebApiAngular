using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebApplication3.Filters;

namespace WebApplication3.Configuration
{
    public class CustomHttpConfiguration : HttpConfiguration
    {
        public CustomHttpConfiguration()
        {
            ConfigureFilters();
            ConfigureRoutes();
            ConfigureJsonSerialization();
            ConfigureXmlSerialization();
        }

        private void ConfigureRoutes()
        {
            this.MapHttpAttributeRoutes();
            Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private void ConfigureJsonSerialization()
        {
            var jsonSettings = Formatters.JsonFormatter.SerializerSettings;
            jsonSettings.Formatting = Formatting.Indented;
            //For circular references in the objects turn Preserve Reference on 
            //http://johnnycode.com/2012/04/10/serializing-circular-references-with-json-net-and-entity-framework/
            //jsonSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        private void ConfigureXmlSerialization()
        {
            Formatters.XmlFormatter.UseXmlSerializer = true;
        }

        private void ConfigureFilters()
        {
            Filters.Add(new AuthorizeAttribute());
            Filters.Add(new RequireHttpsAttribute());
        }
    }
}