using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.FindingsStates
{
    public sealed class FindingsStatesOutput
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
        public bool Active { get; set; }

        public FindingsStatesOutput(int id, string code, string name, string colour, bool active)
        {
            this.Id = id;
            this.Code = code;
            this.Name = name;
            this.Colour = colour;
            this.Active = active;
        }
    }
}
