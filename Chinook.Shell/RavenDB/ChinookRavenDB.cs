using Chinook.Data;
using EasyLOB.Library;
using Raven.Client;
using Raven.Client.Document;

namespace Chinook.Shell
{
    public class ChinookRavenDB
    {
        private IDocumentStore _documentStore;

        public IDocumentStore DocumentStore { get { return _documentStore; } }

        public ChinookRavenDB()
        {
            _documentStore = new DocumentStore
            {
                Url = LibraryHelper.AppSettings<string>("RavenDB.Chinook.Url"),
                DefaultDatabase = LibraryHelper.AppSettings<string>("RavenDB.Chinook.Database")
            };

            // entity/1
            //_documentStore.Conventions
            //    .DocumentKeyGenerator = (dbname, commands, entity) => _documentStore.Conventions.GetTypeTagName(entity.GetType()) + "/";

            // DocumentId = entity/1
            _documentStore.Conventions
                .FindIdentityProperty = property => property.Name == "DocumentId";

            // entity/1 instead of entities/1
            _documentStore.Conventions
                .FindTypeTagName = type => type.Name;

            _documentStore.Initialize();
        }
    }
}