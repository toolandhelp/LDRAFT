using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.DAL;
using Lm.Model;

namespace Lm.BLL
{
    public  class BLL_NewsCenter
    {
        #region dbContext
        public DbHelperEfSql<tb_NewsCenter> dbContext { get; set; }

        public BLL_NewsCenter()
        {
            dbContext = new DbHelperEfSql<tb_NewsCenter>();
        }
        #endregion

        #region  查询 Search Entity

        //查询所有
        public IList<tb_NewsCenter> GetListByAll()
        {
            return dbContext.SearchByAll();
        }

        /// <summary>
        /// 根据ID获取
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public tb_NewsCenter GetObjectById(int iid)
        {
            return dbContext.SearchBySingle(c => c.ID == iid);
        }
        #endregion
    }
}
