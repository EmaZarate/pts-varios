using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Domain.FindingsState
{
    public class FindingsState
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
        public bool Active { get; set; }

        public FindingsState()
        {

        }
        public FindingsState(string code, string name, string colour, bool active)
        {
            this.Code = code;
            this.Name = name;
            this.Colour = colour;
            this.Active = active;
        }

        public FindingsState(string code, string name, string colour, bool active, int id)
        {
            this.Id = id;
            this.Code = code;
            this.Name = name;
            this.Colour = colour;
            this.Active = active;
        }
    }
}
