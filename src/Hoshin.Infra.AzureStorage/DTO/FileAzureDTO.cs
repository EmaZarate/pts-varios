using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Infra.AzureStorage.DTO
{
    public class FileAzureDTO
    {
        public string Base64 { get; set; }
        public string FileName { get; set; }
        public bool IsInsert { get; set; }
        public string Url { get; set; }
    }
}