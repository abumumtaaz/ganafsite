using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GanafWeb.Domain;

namespace GanafWeb.Logic.Abstract
{
    public interface IContactUs
    {
        string Contact(contact contact);
        string Register(user user);
        void Save();
    }
}
