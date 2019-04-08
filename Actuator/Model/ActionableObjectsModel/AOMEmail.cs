using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.ComponentModel;

namespace Actuator.Model.ActionableObjectsModel
{

	public class AOMEmail
	{
        public string DataType { get; set; } = "string";
        public int AOMEmailId
        {
            get;
            set;
        }
        public string ShortText
		{
			get
			{
				return "Configure your Email Message";
			}
			set
			{
				ShortText = "Configure your Email Message";
			}
		}
		public MailMessage Email
		{
			get;
			set;
		}
		public SmtpClient Client
		{
			get;
			set;
		}
		public void SendEmail(SmtpClient sc, MailMessage ml)
		{
			sc.Send(ml);
		}

	}
}
