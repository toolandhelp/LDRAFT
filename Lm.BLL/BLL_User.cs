using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Model;
using Lm.DAL;

namespace Lm.BLL
{
  public   class BLL_User
    {
        #region dbContext
        public DbHelperEfSql<Users> dbContext { get; set; }
        public BLL_User()
        {
            dbContext = new DbHelperEfSql<Users>();
        }
        #endregion
        #region  查询 Search Entity

        /// <summary>
        /// 根据账号获取单条数据
        /// </summary>
        /// <param name="sAccount"></param>
        /// <returns></returns>
        public Users GetObjectByAccount(string sAccount)
        {
            return dbContext.SearchBySingle(c => c.Account == sAccount);
        }

        /// <summary>
        /// 查询所有用户
        /// </summary>
        /// <returns></returns>
        public IList<Users> GetListByAll()
        {
            return dbContext.SearchByAll();
        }

        /// <summary>
        /// 根据用户ID获取
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public Users GetObjectById(Guid gid)
        {
            return dbContext.SearchBySingle(c => c.Id == gid);
        }

        /// <summary>
        /// 登陆用户
        /// </summary>
        /// <param name="sUserName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Users LoginUsers(string sUserName, string password)
        {
            return dbContext.SearchBySingle(c => c.Account == sUserName && c.Password == password);
        }


        #endregion
    }
}
