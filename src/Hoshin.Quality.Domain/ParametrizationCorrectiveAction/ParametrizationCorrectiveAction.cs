using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Domain.ParametrizationCorrectiveAction
{
    public class ParametrizationCorrectiveAction
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }

        public ParametrizationCorrectiveAction()
        {

        }
        public ParametrizationCorrectiveAction(string name, string code, int value)
        {
            this.Name = name;
            this.Code = code;
            this.Value = value;
        }

        public ParametrizationCorrectiveAction(string name, string code, int value, int id)
        {
            this.Id = id;
            this.Name = name;
            this.Code = code;
            this.Value = value;
        }
    }
}
