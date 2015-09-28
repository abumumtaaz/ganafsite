using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GanafWeb.Domain;
using GanafWeb.Logic.Abstract;

namespace GanafWeb.Logic.Concrete
{
    public class ContactUs : IContactUs
    {
        readonly GanafDBEntities _entity = new GanafDBEntities();

        public string Register(Domain.user user)
        {
            try
            {
                _entity.users.Add(user);
                Save();
                return "Registration was successful";
            }
            catch (Exception)
            {
                return "An error occured?ERROR";
            }
        }

        public string Contact(Domain.contact contact)
        {
            try
            {
                _entity.contacts.Add(contact);
                Save();
                return "Message successfully sent";
            }
            catch (Exception)
            {
                return "An error occured, please retry?ERROR";
            }
        }


        public void Save()
        {
            _entity.SaveChanges();
        }
    }
}
