using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Domain.ParametrizationCriteria
{
    public class ParametrizationCriteria
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public int Id { get; set; }

        public ParametrizationCriteria()
        {

        }
        public ParametrizationCriteria(string name, string datatype)
        {
            this.Name = name;
            this.DataType = datatype;
        }

        public ParametrizationCriteria(string name, string datatype, int id)
        {
            this.Id = id;
            this.Name = name;
            this.DataType = datatype;
        }
    }
}
