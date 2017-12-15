using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Model;
using Lm.DAL;

namespace Lm.BLL
{
   public  class BLL_CommentMessage
    {
        #region dbContext
        public DbHelperEfSql<tb_CommentMessage> dbContext { get; set; }

        public BLL_CommentMessage()
        {
            dbContext = new DbHelperEfSql<tb_CommentMessage>();
        }
        #endregion

        #region  查询 Search Entity

        //查询所有
        public IList<tb_CommentMessage> GetListByAll()
        {
            return dbContext.SearchByAll();
        }
        #endregion


        public IList<tb_CommentMessage> GetListByNewId(int iid)
        {
            return dbContext.SearchByAll().Where(o => o.CommentariesID == iid).ToList();
        }

        #region
        public bool AddOrUpdate(tb_CommentMessage item)
        {
            return item.Id >= 0 ? dbContext.Add(item) : dbContext.Update(item, c => c.Id == item.Id);
        }
        #endregion
    }
}
