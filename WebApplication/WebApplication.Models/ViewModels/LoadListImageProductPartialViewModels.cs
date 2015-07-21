using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Models.ViewModels
{
    public class LoadListImageProductPartialViewModels
    {
        public int ProductId;
        public IEnumerable<WebApplication.Models.Models.share_Images> Images;
    }
}
