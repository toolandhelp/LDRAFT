
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.DAL;
using Lm.Model;

namespace Lm.BLL
{
    public class BLL_Menu_A
    {
        #region dbContext
        public DbHelperEfSql<tb_Menu_A> dbContext { get; set; }
        public BLL_Menu_A()
        {
            dbContext = new DbHelperEfSql<tb_Menu_A>();
        }
        #endregion

        public IList<tb_Menu_A> GetMenu_A_ListByAll()
        {
            return dbContext.SearchByAll();
        }
    }
}
