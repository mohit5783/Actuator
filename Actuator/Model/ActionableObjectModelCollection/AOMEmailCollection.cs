using Actuator.Model.ActionableObjectsModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.ActionableObjectModelCollection
{
    public class AOMEmailCollection : ObservableCollection<AOMEmail>
    {
        public AOMEmail AddAOMEmail(int id, MailMessage email)
        {
            AOMEmail NewAOMEmail = new AOMEmail()
            {
                AOMEmailId = id,
                Email = email
            };
            base.Add(NewAOMEmail);
            return NewAOMEmail;
        }
    }
}
