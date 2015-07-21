using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Admin.BusinessLogic
{
    public class StatusProductViewModels
    {
    
        public int StatusID{get;set;}
        public string Value{get;set;}
        public static List<StatusProductViewModels> GetListStatusOptions()
        {
            List<StatusProductViewModels> ret = new List<StatusProductViewModels>();
            StatusProductViewModels option1 = new StatusProductViewModels()
            {
                StatusID = 1,
                Value = "Còn hàng"
            };
            StatusProductViewModels option2 = new StatusProductViewModels()
            {
                StatusID = 2,
                Value = "Hết hàng"
            };
            StatusProductViewModels option3 = new StatusProductViewModels()
            {
                StatusID = 3,
                Value = "Cho phép đặt hàng"
            };
            ret.Add(option1);
            ret.Add(option2);
            ret.Add(option3);
            return ret;
        }

        public static string GetValueOfStatus(int? StatusId)
        {
            if (StatusId == null)
            {
                return "Còn hàng";
            }
            switch (StatusId)
            {
                case 1: return "Còn hàng";
                case 2: return "Hết hàng";
                default: return "Cho phép đặt hàng";
            }
        }
    }
}