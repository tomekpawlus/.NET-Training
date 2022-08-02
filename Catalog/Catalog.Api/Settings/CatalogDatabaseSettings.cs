using System;
namespace Catalog.Api.Settings
{
    public class CatalogDatabaseSettings
    {

        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string ItemCollectionName { get; set; } = null!;

    }
}