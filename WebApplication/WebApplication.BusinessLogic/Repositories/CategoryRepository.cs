using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Models.Models;

namespace WebApplication.BusinessLogic.Repositories
{
    public class CategoryRepository
    {
        PortalEntities dc = new PortalEntities();
        public IList<product_Categories> FindAll()
        {
            return dc.product_Categories.ToList<product_Categories>();
        }
        public product_Categories FindById(int Id)
        {
            return dc.product_Categories.Where(c => c.ID == Id).FirstOrDefault();
        }
        public product_Categories FindByGuid(System.Guid Guid)
        {
            //IList<product_Categories> categories = dc.product_Categories.ToList();
            //if (categories.Count() > 0)
            //{

            //}
            return dc.product_Categories.Where(c => c.GUID == Guid).FirstOrDefault<product_Categories>();
        }
        /// <summary>
        /// Find all category have status "hien thi" with value of Status = 1
        /// </summary>
        /// <returns></returns>
        public IList<product_Categories> FindAllVisible()
        {
            return dc.product_Categories.Where(c => c.Status == 1).ToList<product_Categories>();
        }

        public IList<product_Categories> GetAllRootCategory()
        {
            IList<product_Categories> ret = new List<product_Categories>();
            ret = dc.product_Categories.Where(c => c.GUID != Guid.Empty && c.ParentID == Guid.Empty && c.Status == 1).ToList();
            return ret;
        }

        public IList<product_Categories> GetChildCategory(Guid ParentGuid)
        {
            IList<product_Categories> ret = new List<product_Categories>();
            ret = dc.product_Categories.Where(c => c.ParentID == ParentGuid).ToList();
            return ret;
        }
    }
}
