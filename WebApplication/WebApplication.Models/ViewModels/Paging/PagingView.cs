using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Models.ViewModels
{
    public class PagingView<T>
    {
        public IEnumerable<T> Items { get; set; }
        public PagingRouteValue RouteValue { get; set; }
    }
}
