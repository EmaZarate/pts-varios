using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.ApproveFinding
{
    public class TestDTO
    {
        public IFormFile File{ get; set; }
        public string Description { get; set; }
        public string FinalComment { get; set; }
        public ApproveFindingDTO finding { get; set; }
        [ModelBinder(BinderType = typeof(FormDataJsonBinder))]
        public ApproveFindingDTO Finding { get; set; }
    }
}
