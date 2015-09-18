using Raven.Client;
using Raven.Client.Embedded;

namespace RavenDbEmbeddedTest
{
    public class DocumentSessionFactory
    {
        private static IDocumentStore _store;
        private const string _defaultDatabase = "ObjectModels";

        static public IDocumentStore DocumentStore
        {
            get { return _store ?? (_store = CreateDocumentStore()); }
        }

        static public IDocumentSession CreateSession()
        {
            return DocumentStore.OpenSession(_defaultDatabase);
        }

        static private IDocumentStore CreateDocumentStore()
        {
            IDocumentStore store = new EmbeddableDocumentStore()
            {
                DataDirectory = "Data",
                DefaultDatabase = "ObjectModels",
                UseEmbeddedHttpServer = true
            };
            store.Initialize();
            store.DatabaseCommands.GlobalAdmin.EnsureDatabaseExists("ObjecModels");

            CreateIndexes(store);

            return store;
        }

        static private void CreateIndexes(IDocumentStore store)
        {
            new ObjectModel_ObjectName().Execute(store.DatabaseCommands.ForDatabase(_defaultDatabase), store.Conventions);
        }
    }
}
