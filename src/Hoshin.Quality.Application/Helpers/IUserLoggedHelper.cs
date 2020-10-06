using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.Helpers
{
    public interface IUserLoggedHelper
    {
        bool CheckForPermissionsToUpdateReassignOrClose(string responsibleUser);
    }
}
