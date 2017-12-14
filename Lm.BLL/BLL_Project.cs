using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.DAL;
using Lm.Model;

namespace Lm.BLL
{
    public class BLL_Project
    {
        #region dbContext
        public DbHelperEfSql<tb_Project> dbContext { get; set; }
        public BLL_Project()
        {
            dbContext = new DbHelperEfSql<tb_Project>();
        }
        #endregion
        #region  查询 Search Entity

        //查询所有项目
        public IList<tb_Project> GetListByAll()
        {
            return dbContext.SearchByAll();
        }

        /// <summary>
        /// 根据ID获取
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public tb_Project GetObjectById(int iid)
        {
            return dbContext.SearchBySingle(c => c.Id == iid);
        }

        /// <summary>
        /// 分页记录
        /// </summary>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <param name="iTotalRecord"></param>
        /// <returns></returns>
        public IList<tb_Project> GetPageList(int iPageIndex, int iPageSize, ref int iTotalRecord)
        {
            return dbContext.SearchByPageCondition(true, c => c.Project_Start, iPageIndex, iPageSize, ref iTotalRecord);
        }
        #endregion

    }
}
