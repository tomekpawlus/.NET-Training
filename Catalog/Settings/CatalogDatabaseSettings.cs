using System;
namespace Catalog.Settings
{
    public class CatalogDatabaseSettings
    {

        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string ItemCollectionName { get; set; } = null!;

    }
}