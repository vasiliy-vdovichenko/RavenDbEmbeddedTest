using System;

namespace RavenDbEmbeddedTest
{
    public class ObjectModel
    {
        public ObjectModel()
        {
            Id = Guid.NewGuid().ToString("N");
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string ObjectName { get; set; }
        public DateTime Added { get; set; }
    }
}