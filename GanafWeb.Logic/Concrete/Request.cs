using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GanafWeb.Domain;
using GanafWeb.Logic.Abstract;
using GanafWeb.Logic.Constants;

namespace GanafWeb.Logic.Concrete
{
    public class Request : IRequest
    {

        GanafDBEntities _entity = new GanafDBEntities();

        public string MakeRequest(string username, string requestType, string urgency, string details)
        {
            return "Not implemented";
        }

        public request GetRequest(string username)
        {
            try
            {
                var rq = (from x in _entity.requests where (x.username == username) select x).FirstOrDefault();
                return rq;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<request> GetRequest()
        {
            var rq = from x in _entity.requests select x;
            return rq;
        }

        public string Authenticate(string username, string password)
        {
            try
            {
                var result =
                    (from x in _entity.users where (x.username == username && x.password == password) select x).First();
                SessionParameters.SetUserAuthenticated(true);
                SessionParameters.SetCurrentUser(result.username);
                return result.userType;
            }
            catch (Exception)
            {
                SessionParameters.SetUserAuthenticated(false);
                SessionParameters.SetCurrentUser(null);
                return "Unknown User?ERROR";
            }
        }

        public string AuthenticateAdmin(string username, string password)
        {
            try
            {
                var result =
                    (from x in _entity.users where (x.username == username && x.password == password) select x).First();
                SessionParameters.SetUserAuthenticated(true);
                SessionParameters.SetCurrentUser(result.username);
                return result.userType;
            }
            catch (Exception)
            {
                _entity.users.Add(new user
                {
                    firstname = "Ganaf Admin",
                    lastname = "Ganaf Admin",
                    email = "ganaftech@gmail.com",
                    phone = "07061543553",
                    username = username,
                    password = password,
                    userType = UserTypes.ADMIN
                });
                Save();
                SessionParameters.SetUserAuthenticated(true);
                SessionParameters.SetCurrentUser(username);
                return UserTypes.ADMIN;
            }
        }

        public user GetUser(int id)
        {
            try
            {
                return _entity.users.Find(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public user GetUser(string username)
        {
            try
            {
                var result = (from x in _entity.users where x.username == username select x).First();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Save()
        {
            _entity.SaveChanges();
        }
    }
}
