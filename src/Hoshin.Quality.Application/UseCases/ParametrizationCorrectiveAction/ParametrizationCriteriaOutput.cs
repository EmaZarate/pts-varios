using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.ParametrizationCorrectiveAction
{
    public sealed class ParametrizationCorrectiveActionOutput
    {
        public int ParametrizationCorrectiveActionId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Value { get; set; }

        public ParametrizationCorrectiveActionOutput(int id, string name, string code, int value)
        {
            this.ParametrizationCorrectiveActionId = id;
            this.Name = name;
            this.Code = code;
            this.Value = value;
        }
    }
}
