using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GanafWeb.Domain;

namespace GanafWeb.Logic.Abstract
{
    public interface IRequest
    {
        string MakeRequest(string username, string requestType, string urgency, string details);
        request GetRequest(string username);
        IEnumerable<request> GetRequest();
        string Authenticate(string username, string password);
        string AuthenticateAdmin(string username, string password);
        user GetUser(string username);
        user GetUser(int id);
        void Save();
    }
}
