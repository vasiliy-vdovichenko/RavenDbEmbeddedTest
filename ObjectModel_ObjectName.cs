using System.Linq;
using Raven.Client.Indexes;

namespace RavenDbEmbeddedTest
{
    // ReSharper disable once InconsistentNaming
    public class ObjectModel_ObjectName : AbstractIndexCreationTask<ObjectModel>
    {
        public ObjectModel_ObjectName()
        {
            Map = objectModels => objectModels.Select(m => new { m.ObjectName });
        }
    }
}
