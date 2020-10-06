using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Domain.AzureStorageBlobSettings
{
    public class AzureStorageBlobSettings
    {
        public string StorageConnectionString { get; set; }
        public string UrlContainer { get; set; }
        public string ContainerFindingName { get; set; }
        public string ContainerCorrectiveActionName { get; set; }
        public string ContainerTaskName { get; set; }
        public string ContainerAuditName { get; set; }
    }
}
