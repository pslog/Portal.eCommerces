using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.BusinessLogic.BusinessLogic
{
    public class StatusCategoryViewModel
    {
        public int StatusID { get; set; }
        public string Value { get; set; }
        public static List<StatusCategoryViewModel> GetListStatusOptions()
        {
            List<StatusCategoryViewModel> ret = new List<StatusCategoryViewModel>();
            StatusCategoryViewModel option1 = new StatusCategoryViewModel()
            {
                StatusID = 1,
                Value = "Hiển thị"
            };
            StatusCategoryViewModel option2 = new StatusCategoryViewModel()
            {
                StatusID = 2,
                Value = "Ẩn"
            };

            ret.Add(option1);
            ret.Add(option2);
            return ret;
        }

        public static string GetValueOfStatus(int? StatusId)
        {
            if (StatusId == null)
            {
                return "Hiển thị";
            }
            switch (StatusId)
            {
                case 1: return "Hiển thị";
                case 2: return "Ẩn";
                default: return "Hiển thị";
            }
        }
    }
}