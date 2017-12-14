using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.CommonLib;
using Lm.Model;
using Lm.DAL;


namespace Lm.BLL
{
    public class BLL_UploadFile
    {
        #region dbContext
        public DbHelperEfSql<tb_UploadFile> dbContext { get; set; }

        public BLL_UploadFile()
        {
            dbContext = new DbHelperEfSql<tb_UploadFile>();
        }
        #endregion


        #region 查询

        /// <summary>
        /// 根据Id 获取一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public tb_UploadFile GetObjectById(int id)
        {
            return dbContext.SearchBySingle(o => o.Id == id);
        }

        /// <summary>
        /// 根据项目ID 到 文件list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<tb_UploadFile> GetListByProId(int id)
        {
            return dbContext.SearchByAll().Where(o => o.DirId == id).ToList();
        }

        #endregion
    }
}
