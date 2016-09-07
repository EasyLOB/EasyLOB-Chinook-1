using Chinook.Data;
using EasyLOB;
using Microsoft.OData.Edm;
using Microsoft.Practices.Unity;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;

namespace Chinook.OData
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            //config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            // OData

            config.MapODataServiceRoute("ODataRoute", null, GetEdmModelChinookConvention());

            config.Filters.Add(new EnableQueryAttribute()
            {
                PageSize = 100 // !!!
            });

            // Unity

            var container = new UnityContainer();

            UnityHelperChinook.RegisterMappings(container);

            UnityHelperAuditTrail.RegisterMappings(container);
            UnityHelperLog.RegisterMappings(container);

            config.DependencyResolver = new UnityResolver(container);
        }

        private static IEdmModel GetEdmModelChinookConvention()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();

            builder.EntitySet<AlbumDTO>("Album").EntityType
                .HasKey(x => x.AlbumId);
            builder.EntitySet<ArtistDTO>("Artist").EntityType
                .HasKey(x => x.ArtistId);
            builder.EntitySet<CustomerDTO>("Customer").EntityType
                .HasKey(x => x.CustomerId);
            builder.EntitySet<CustomerDocumentDTO>("CustomerDocument").EntityType
                .HasKey(x => x.CustomerDocumentId);
            builder.EntitySet<EmployeeDTO>("Employee").EntityType
                .HasKey(x => x.EmployeeId);
            builder.EntitySet<GenreDTO>("Genre").EntityType
                .HasKey(x => x.GenreId);
            builder.EntitySet<InvoiceDTO>("Invoice").EntityType
                .HasKey(x => x.InvoiceId);
            builder.EntitySet<InvoiceLineDTO>("InvoiceLine").EntityType
                .HasKey(x => x.InvoiceLineId);
            builder.EntitySet<MediaTypeDTO>("MediaType").EntityType
                .HasKey(x => x.MediaTypeId);
            builder.EntitySet<PlaylistDTO>("Playlist").EntityType
                .HasKey(x => x.PlaylistId);
            builder.EntitySet<PlaylistTrackDTO>("PlaylistTrack").EntityType
                .HasKey(x => x.PlaylistId)
                .HasKey(x => x.TrackId);
            builder.EntitySet<TrackDTO>("Track").EntityType
                .HasKey(x => x.TrackId);

            return builder.GetEdmModel();
        }
    }
}