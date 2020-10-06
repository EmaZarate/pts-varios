using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDParametrizationCriteria
{
    public class ParametrizationCriteriaDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DataType { get; set; }
    }
}
