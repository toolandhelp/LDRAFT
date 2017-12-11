
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Model;
using Lm.DAL;

namespace Lm.BLL
{
    public class Bll_ClientMessage
    {
        #region dbContext
        public DbHelperEfSql<tb_ClientMessage> dbContext { get; set; }

        public Bll_ClientMessage()
        {
            dbContext = new DbHelperEfSql<tb_ClientMessage>();
        }
        #endregion

        #region  查询 Search Entity

        //查询所有
        public IList<tb_ClientMessage> GetListByAll()
        {
            return dbContext.SearchByAll();
        }
        #endregion

        #region
        public bool AddOrUpdate(tb_ClientMessage item)
        {
            return item.Id >= 0 ? dbContext.Add(item) : dbContext.Update(item, c => c.Id == item.Id);

            //if (item.Id == 0)
            //{
            //    return dbContext.Add(item);
            //}
            //else
            //{
            //    return dbContext.Update(item, c => c.Id == item.Id);
            //}
        }
        #endregion
    }
}
