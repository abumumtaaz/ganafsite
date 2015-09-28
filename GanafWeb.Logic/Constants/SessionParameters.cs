using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanafWeb.Logic.Constants
{
    public class SessionParameters
    {
        private static bool _userAuthenticated = false;
        private static string _currentUser;

        public static void SetUserAuthenticated(bool state)
        {
            _userAuthenticated = state;
        }

        public static bool GetUserAuthenticated()
        {
            return _userAuthenticated;
        }

        public static void SetCurrentUser(string current)
        {
            _currentUser = current;
        }

        public static string GetCurrentUser()
        {
            return _currentUser;
        }
    }
}
