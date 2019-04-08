using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Actuator.Model.Template
{
    public class rightMenuViewModel
    {
        public clsDesignActionCollection designActions
        {
            get;
            set;
        }

        public rightMenuViewModel()
        {
            LoadRightSideMenu();
        }

        private void LoadRightSideMenu()
        {
            designActions = new clsDesignActionCollection();
            if (File.Exists("DesignMenu.xml"))
            {
                XElement root = XElement.Load("DesignMenu.xml");
                root.Elements("Menu").All<XElement>(xe =>
                {
                    designActions.AddDesignAction(xe.Attribute("Name").Value.ToUpper(),
                    xe.Attribute("Icon").Value,
                    xe.Attribute("ShortText").Value,
                    Convert.ToBoolean(xe.Attribute("HasChild").Value),
                    xe.Attribute("MenuType").Value);
                    return true;
                });
            }
        }
    }
}
