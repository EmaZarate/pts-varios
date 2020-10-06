using Hoshin.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Domain.UserCorrectiveAction
{
    public class UserCorrectiveAction
    {
        public string UserID { get; set; }
        public int CorrectiveActionID { get; set; }
        public User Users { get; set; }
    }
}
