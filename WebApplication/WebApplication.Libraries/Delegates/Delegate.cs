using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Libraries.Delegates
{
    public delegate string PagingHtmlGenerator(int option, string actionLink = "", string displayText = "", bool active = false);
}
