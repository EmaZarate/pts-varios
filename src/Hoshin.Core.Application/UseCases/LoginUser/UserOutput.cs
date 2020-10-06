using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Application.UseCases.LoginUser
{
    public sealed class UserOutput
    {
        public string JWT { get; set; }

        public UserOutput(string jwt)
        {
            JWT = jwt;
        }
    }
}
