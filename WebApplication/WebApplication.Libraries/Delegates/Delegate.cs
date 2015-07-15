using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebApplication.Libraries.Delegates
{
    public delegate string PagingHtmlGenerator(int option, MvcHtmlString actionLink = null, bool active = false);
}
