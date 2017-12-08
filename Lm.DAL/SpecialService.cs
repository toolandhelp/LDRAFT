using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using Lm.CommonLib;
using Lm.Model;

namespace Lm.DAL
{
    /// <summary>
    /// 特殊数据层，针对不完善的事情
    /// </summary>
    public class SpecialService : BaseServiceEf<lmEntities>
    {

        #region
        public bool Update(Hashtable ht, string tableName, string fieldPkName, int id)
        {
            if (ht == null || string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(fieldPkName))
            {
                return false;
            }
            bool isFlag = false;
            try
            {
                #region
                StringBuilder sb = new StringBuilder("update " + tableName + " set ");
                IList<object> list = new List<object>();
                IDictionaryEnumerator ienum = ht.GetEnumerator();
                int index = 0;
                while (ienum.MoveNext())
                {
                    sb.Append(ienum.Key.ToString() + "={" + index + "},");
                    list.Add(ienum.Value);
                    index++;
                }
                sb = new StringBuilder(sb.ToString().TrimEnd(','));
                object[] objArray = (object[])list.ToArray<object>();
                sb.Append(" where " + fieldPkName + "=" + id);
                int num = _context.Database.ExecuteSqlCommand(sb.ToString(), objArray);
                isFlag = num > 0 ? true : false;
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                isFlag = false;
            }
            return isFlag;
        }

        public bool Update(Hashtable ht, string tableName, string fieldPkName, Guid id)
        {
            if (ht == null || string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(fieldPkName))
            {
                return false;
            }
            bool isFlag = false;
            try
            {
                #region
                StringBuilder sb = new StringBuilder("update " + tableName + " set ");
                IList<object> list = new List<object>();
                IDictionaryEnumerator ienum = ht.GetEnumerator();
                int index = 0;
                while (ienum.MoveNext())
                {
                    sb.Append(ienum.Key.ToString() + "={" + index + "},");
                    list.Add(ienum.Value);
                    index++;
                }
                sb = new StringBuilder(sb.ToString().TrimEnd(','));
                object[] objArray = (object[])list.ToArray<object>();
                sb.Append(" where " + fieldPkName + "='" + id + "'");
                int num = _context.Database.ExecuteSqlCommand(sb.ToString(), objArray);
                isFlag = num > 0 ? true : false;
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                isFlag = false;
            }
            return isFlag;
        }

        public bool Add(Hashtable ht, string tableName)
        {
            if (ht == null || string.IsNullOrEmpty(tableName))
            {
                return false;
            }
            bool isFlag = false;
            try
            {
                #region
                StringBuilder sb = new StringBuilder("INSERT INTO " + tableName + " (");
                IList<object> list = new List<object>();
                IDictionaryEnumerator ienum = ht.GetEnumerator();
                while (ienum.MoveNext())
                {
                    sb.Append(ienum.Key.ToString() + ",");
                    list.Add(ienum.Value);
                }
                sb = new StringBuilder(sb.ToString().TrimEnd(','));
                sb.Append(") values (");
                for (int i = 0; i < ht.Keys.Count; i++)
                {
                    sb.Append("{" + i + "},");
                }
                sb = new StringBuilder(sb.ToString().TrimEnd(','));
                sb.Append(")");
                object[] objArray = (object[])list.ToArray<object>();
                int num = _context.Database.ExecuteSqlCommand(sb.ToString(), objArray);
                isFlag = num > 0 ? true : false;
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                isFlag = false;
            }
            return isFlag;
        }
        #endregion

        #region SQL
        public int UpdateBySQL(Hashtable ht, string tableName, string whereFiledName, int whereFiledVlaue)
        {
            return DbHelperSql.DynamicUpdate(ht, tableName, whereFiledName, whereFiledVlaue, 1);
        }
        public int AddBySQL(Hashtable ht, string tableName)
        {
            return DbHelperSql.DynamicInsert(ht, tableName);
        }

        public static int LogicDelBySQL(string tableName, string filedPKName, int filedPKVlaue, List<object> ids)
        {
            return -1;
        }
        #endregion

        #region EF 执行Sql
        /// <summary>
        /// EF 执行 Sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public bool ExecuteNonQuery(string sql, params object[] parameters)
        {
            try
            {
                return _context.Database.ExecuteSqlCommand(sql, parameters) > 0;
            }
            catch (Exception ex)
            {
                MessageLog.AddErrorLogDalSpecial("ExecuteSqlCommand(EF 执行 Sql)", ex.Message);
                return false;
            }
        }
        #endregion

        #region  暂时先删除
        /*
        /// <summary>
        /// 保存角色信息，同时保存菜单权限及用户
        /// </summary>
        /// <param name="model"></param>
        /// <param name="MenuIds"></param>
        /// <param name="eIds"></param>
        /// <returns></returns>
        public bool AddOrUpdateRole(ts_Role model, string[] MenuIds, int[] eIds)
        {
            try
            {
                if (model == null) return false;
                #region 角色
                if (model.Id == Guid.Empty)
                {
                    model.Id = Guid.NewGuid();
                    _context.ts_Role.Add(model);
                }
                else
                {
                    var old = _context.ts_Role.FirstOrDefault(c => c.Id == model.Id);
                    if (old == null) return false;
                    old.Name = model.Name;
                    old.Mark = model.Mark;
                }
                #endregion
                var roldId = model.Id;
                #region 菜单权限
                var oldList = _context.ts_AuthMenu.Where(c => c.RoleId == roldId).ToList();
                MenuIds = MenuIds.Where(c => !string.IsNullOrEmpty(c)).ToArray();
                if (oldList != null)
                {
                    _context.ts_AuthMenu.RemoveRange(oldList.Where(c => !MenuIds.Contains(c.MenuCode)));
                    if (MenuIds != null)
                        MenuIds = MenuIds.Where(c => !oldList.Select(d => d.MenuCode).Contains(c)).ToArray();
                }
                if (MenuIds != null)
                    _context.ts_AuthMenu.AddRange(MenuIds.Select(c => new ts_AuthMenu() { MenuCode = c, RoleId = roldId }));
                #endregion
                #region 用户
                var oldUserList = _context.td_Employee.Where(c => c.role == roldId && !eIds.Contains(c.eId)).ToList();
                foreach (var item in oldUserList)
                    item.role = null;
                var newUserList = _context.td_Employee.Where(c => c.role != roldId && eIds.Contains(c.eId)).ToList();
                foreach (var item in newUserList)
                    item.role = roldId;
                #endregion
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageLog.AddErrorLogDalSpecial("AddOrUpdateRole(级联更新角色、菜单权限及用户)", ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 更新角色用户
        /// </summary>
        /// <param name="eIds"></param>
        /// <param name="purId"></param>
        /// <returns></returns>
        public bool UpdateAuthRoleUser(int[] eIds, Guid purId)
        {
            try
            {
                if (eIds == null || purId == Guid.Empty) return false;

                var oldUserList = _context.td_Employee.Where(c => c.role == purId && !eIds.Contains(c.eId)).ToList();
                foreach (var item in oldUserList)
                    item.role = null;
                var newUserList = _context.td_Employee.Where(c => c.role != purId && eIds.Contains(c.eId)).ToList();
                foreach (var item in newUserList)
                    item.role = purId;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageLog.AddErrorLogDalSpecial("UpdateAuthRoleUser(更新角色用户)", ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 获取用户可以查看公共文件夹
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public IList<td_Document> GetDirListByAuth(string userCode)
        {
            string paramUser = string.Format(";{0};", userCode);
            return _context.td_Document.Where(c => c.status == (int)StageMode.Normal && (c.WriteFlag == (int)AuthFlag.All || c.ReadFlag == (int)AuthFlag.All || (c.WriteFlag == (int)AuthFlag.User && c.WritePur.Contains(paramUser)) || (c.ReadFlag == (int)AuthFlag.User && c.ReadPur.Contains(paramUser)))).OrderByDescending(c => c.createDate).ToList();
        }
        /// <summary>
        /// 检查文件夹名是否重复
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="userCode"></param>
        /// <param name="name"></param>
        /// <param name="DirId"></param>
        /// <returns></returns>
        public int CheckDirName(DirFlag flag, string userCode, string name, int DirId, int pid)
        {
            var query = _context.td_Document.Where(c => c.DocFlag == (int)flag && c.title == name && c.id != DirId && c.pid == pid);
            if (flag == DirFlag.Private)
                return query.Where(c => c.eCode == userCode).Count();
            return query.Count();
        }
        /// <summary>
        /// 更新用户调休时间
        /// </summary>
        /// <param name="eCodes"></param>
        /// <param name="time"></param>
        /// <param name="Reason"></param>
        /// <returns></returns>
        public bool UpdateVacationTime(string[] eCodes, double time, string Reason)
        {
            if (eCodes == null || eCodes.Length == 0) return false;

            try
            {
                var list = _context.td_vacation.Where(c => eCodes.Contains(c.vac_eCode)).ToList();
                if (list == null || list.Count == 0) return false;

                var logList = new List<td_VacationLog>();
                foreach (var item in list)
                {
                    item.vac_TS = item.vac_TS.HasValue ? item.vac_TS + time : time;
                    logList.Add(new td_VacationLog()
                    {
                        ecode = item.vac_eCode,
                        ts = time,
                        reason = Reason
                    });
                }
                _context.td_VacationLog.AddRange(logList);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageLog.AddErrorLogDalSpecial("UpdateVacationTime(更新用户调休时间)", ex.Message);
                return false;
            }
        }

        public IList<v_TransactflowApply> GetTopTransactListByCondition(int top, string userCode, bool isAdmim)
        {
            return _context.v_TransactflowApply.Where(c => c.apply_UserId == userCode || (isAdmim && c.apply_Status == 1)).OrderBy(c => c.apply_Status).ThenByDescending(c => c.apply_CreateTime).Take(7).ToList();
        }
        /// <summary>
        /// 获取用户可以查看公共文件夹
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public IList<td_File> GetTopDirFileListByAuth(int top, string userCode)
        {
            string paramUser = string.Format(";{0};", userCode);
            int resourceDirId = Config.ResourceDirId;
            return _context.td_File.Where(f => _context.td_Document.Where(c => c.status == (int)StageMode.Normal && (c.WriteFlag == (int)AuthFlag.All || c.ReadFlag == (int)AuthFlag.All || (c.WriteFlag == (int)AuthFlag.User && c.WritePur.Contains(paramUser)) || (c.ReadFlag == (int)AuthFlag.User && c.ReadPur.Contains(paramUser))) && c.id != resourceDirId).Select(c => c.id).Contains(f.DirId)).OrderByDescending(f => f.CreateTime).Take(top).ToList();
        }
        public IList<v_File> GetTopShareFile(int top, string userCode)
        {
            string paramUser = string.Format(";{0};", userCode);
            return _context.v_File.Where(f => f.DirId == -1 && !string.IsNullOrEmpty(f.UserCodes) && f.UserCodes.Contains(paramUser)).OrderByDescending(f => f.CreateTime).Take(top).ToList();
        }

        /// <summary>
        /// 事务审核结果
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Result"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        public bool AuditTransactApply(int ID, AuditFlag Result, string Content)
        {
            try
            {
                var apply = _context.td_TransactApply.Where(c => c.apply_Id == ID).FirstOrDefault();
                if (apply == null) return false;
                apply.apply_Status = (int)Result;
                apply.apply_Content = Content;

                if (Result == AuditFlag.Pass && apply.apply_FlowID == 1)
                {
                    var model = new td_VacationLog();
                    model.ecode = apply.apply_UserId;
                    model.sdate = apply.apply_StartDate;
                    model.edate = apply.apply_EndDate;
                    model.reason = "[员工调休]:" + (string.IsNullOrEmpty(apply.apply_Remark) ? "(无)" : apply.apply_Remark);
                    model.ts = -apply.apply_Hours;
                    _context.td_VacationLog.Add(model);

                    var vac = _context.td_vacation.Where(c => c.vac_eCode == apply.apply_UserId).FirstOrDefault();
                    if (vac != null)
                        vac.vac_TS = vac.vac_TS.HasValue ? vac.vac_TS - apply.apply_Hours : -apply.apply_Hours;
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageLog.AddErrorLogDalSpecial("AuditTransactApply(事务审核结果)", ex.Message);
                return false;
            }
        }
        public bool CheckApplyTime(string userCode, DateTime start, DateTime end)
        {
            try
            {
                return _context.td_TransactApply.Where(c => c.apply_UserId == userCode && end >= c.apply_StartDate && start <= c.apply_EndDate).Count() > 0;
            }
            catch (Exception ex)
            {
                MessageLog.AddErrorLogDalSpecial("CheckApplyTime(申请时间检查)", ex.Message);
                return false;
            }
        }
        public bool AddWorkLogs(List<td_WorkTime_Temp> list)
        {
            try
            {
                if (list == null) return false;

                _context.td_WorkTime_Temp.RemoveRange(_context.td_WorkTime_Temp.ToList());
                _context.td_WorkTime.RemoveRange(_context.td_WorkTime.ToList());

                var workTimeList = (from a in list
                                    select new td_WorkTime
                                    {
                                        id = Convert.ToInt32(a.No),
                                        DateTime = Convert.ToDateTime(a.DateTime),
                                        EnNo = a.EnNo,
                                        Name = a.Name
                                    }).ToList();

                _context.td_WorkTime_Temp.AddRange(list);
                _context.td_WorkTime.AddRange(workTimeList);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageLog.AddErrorLogDalSpecial("AddWorkLogs(考勤导入)", ex.Message);
                return false;
            }
        }

        public bool AddEmployee(td_Employee entity)
        {
            if (entity == null) return false;
            _context.td_Employee.Add(entity);
            try
            {
                td_vacation model = new td_vacation();
                model.vac_eCode = entity.eCode;
                model.vac_Name = entity.eName;
                model.vac_FirstTs = 0;
                model.vac_TS = 0;
                model.vac_Date = DateTime.Now;
                _context.td_vacation.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageLog.AddErrorLogDalSpecial("AddEmployee(新增员工)", ex.Message);
                return false;
            }
        }

        public int CheckEmployeeRepeat(int flag, string name, int Id)
        {
            switch (flag)
            {
                case 1:
                    return _context.td_Employee.Where(c => c.eCode == name && c.eId != Id).Count();
                case 2:
                    return _context.td_Employee.Where(c => c.wCode == name && c.eId != Id).Count();
                case 3:
                    return _context.td_Employee.Where(c => c.loginName == name && c.eId != Id).Count();
                default:
                    return 1;
            }
        }

        public bool AddOrUpdateDoc(td_Document entity, string oldPath, string newPath)
        {
            try
            {
                if (entity.id < 1)
                {
                    _context.td_Document.Add(entity);
                }
                else
                {
                    if (string.IsNullOrEmpty(oldPath) || string.IsNullOrEmpty(newPath))
                    {
                        return false;
                    }
                    var doc = _context.td_Document.FirstOrDefault(c => c.id == entity.id);
                    if (doc == null) return false;

                    doc.DocFlag = entity.DocFlag;
                    doc.title = entity.title;
                    doc.context = entity.context;
                    doc.eCode = entity.eCode;
                    doc.Sort = entity.Sort;
                    doc.status = entity.status;
                    doc.ReadFlag = entity.ReadFlag;
                    doc.ReadPur = entity.ReadPur;
                    doc.WriteFlag = entity.WriteFlag;
                    doc.WritePur = entity.WritePur;

                    var files = _context.td_File.Where(c => c.DirId == entity.id).ToList();
                    if (files != null && files.Count > 0)
                    {
                        foreach (var item in files)
                            item.Path = item.Path.Replace(oldPath, newPath);
                    }
                    oldPath = CommonLib.FileUtils.GetLocalPath(oldPath);
                    newPath = CommonLib.FileUtils.GetLocalPath(newPath);
                    if (Directory.Exists(newPath))
                    {
                        return false;
                    }
                    if (Directory.Exists(oldPath))
                        Directory.Move(oldPath, newPath);
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageLog.AddErrorLogDalSpecial("AddOrUpdateDoc(更新文档设置)", ex.Message);
                return false;
            }
        }

        public bool AddShareFile(td_File newModel, td_File model, string userCodes)
        {
            try
            {
                if (string.IsNullOrEmpty(userCodes) || newModel == null || model == null) return false;

                _context.td_File.Add(newModel);

                var old = _context.td_File.FirstOrDefault(c => c.Id == model.Id);
                if (old == null) return false;

                old.DirId = model.DirId;
                old.Path = model.Path;
                old.CreateTime = model.CreateTime;

                string[] codes = userCodes.Split(';');

                foreach (var code in codes)
                {
                    if (string.IsNullOrEmpty(code)) continue;

                    var item = new td_File_tion();
                    item.eCode = code;
                    item.fId = model.Id;
                    item.stage = (int)StageMode.Normal;
                    item.crateDate = DateTime.Now;
                    _context.td_File_tion.Add(item);
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageLog.AddErrorLogDalSpecial("AddShareFile(分享文件)", ex.Message);
                return false;
            }
        }
        public List<DocFileInfo> GetAllDeleteDocList(int[] ids)
        {
            var list = _context.td_Document.Where(c => ids.Contains(c.id)).ToList();
            List<DocFileInfo> resultList = new List<DocFileInfo>();
            var files = _context.td_File.Where(c => ids.Contains(c.DirId)).ToList();
            resultList.AddRange(from a in list
                                orderby a.Sort
                                select new DocFileInfo()
                                {
                                    DocId = a.id,
                                    DocName = a.title,
                                    Sort = a.Sort,
                                    FileCount = files == null ? 0 : files.Where(c => c.DirId == a.id).Count()
                                });
            var childIds = _context.td_Document.Where(c => c.pid.HasValue && ids.Contains(c.pid.Value)).Select(c => c.id).ToArray();
            if (childIds != null && childIds.Length > 0)
            {
                resultList.AddRange(GetAllDeleteDocList(childIds));
            }
            return resultList;
        }*/
        #endregion

        #region 续上面的
        //public bool AddOrUpdateDoc(td_Document entity, string oldPath, string newPath)
        //{
        //    try
        //    {
        //        if (entity.id < 1)
        //        {
        //            _context.td_Document.Add(entity);
        //        }
        //        else
        //        {
        //            if (string.IsNullOrEmpty(oldPath) || string.IsNullOrEmpty(newPath))
        //            {
        //                return false;
        //            }
        //            var doc = _context.td_Document.FirstOrDefault(c => c.id == entity.id);
        //            if (doc == null) return false;

        //            doc.DocFlag = entity.DocFlag;
        //            doc.title = entity.title;
        //            doc.context = entity.context;
        //            doc.eCode = entity.eCode;
        //            doc.Sort = entity.Sort;
        //            doc.status = entity.status;
        //            doc.ReadFlag = entity.ReadFlag;
        //            doc.ReadPur = entity.ReadPur;
        //            doc.WriteFlag = entity.WriteFlag;
        //            doc.WritePur = entity.WritePur;

        //            var files = _context.td_File.Where(c => c.DirId == entity.id).ToList();
        //            if (files != null && files.Count > 0)
        //            {
        //                foreach (var item in files)
        //                    item.Path = item.Path.Replace(oldPath, newPath);
        //            }
        //            oldPath = FileUtils.GetLocalPath(oldPath);
        //            newPath = FileUtils.GetLocalPath(newPath);
        //            if (Directory.Exists(newPath))
        //            {
        //                return false;
        //            }
        //            if (Directory.Exists(oldPath))
        //                Directory.Move(oldPath, newPath);
        //        }
        //        _context.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageLog.AddErrorLogDalSpecial("AddOrUpdateDoc(更新文档设置)", ex.Message);
        //        return false;
        //    }
        //}

        /// <summary>
        /// 获取用户可以查看公共文件夹
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        //public IList<td_Document> GetDirListByAuth(string userCode)
        //{
        //    string paramUser = string.Format(";{0};", userCode);
        //    return _context.td_Document.Where(c => c.status == (int)StageMode.Normal && (c.WriteFlag == (int)AuthFlag.All || c.ReadFlag == (int)AuthFlag.All || (c.WriteFlag == (int)AuthFlag.User && c.WritePur.Contains(paramUser)) || (c.ReadFlag == (int)AuthFlag.User && c.ReadPur.Contains(paramUser)))).OrderByDescending(c => c.Sort).ToList();
        //}

        /// <summary>
        /// 检查文件夹名是否重复
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="userCode"></param>
        /// <param name="name"></param>
        /// <param name="DirId"></param>
        /// <returns></returns>
        //public int CheckDirName(DirFlag flag, string userCode, string name, int DirId, int pid)
        //{
        //    var query = _context.td_Document.Where(c => c.DocFlag == (int)flag && c.title == name && c.id != DirId && c.pid == pid);
        //    if (flag == DirFlag.Public)
        //        return query.Where(c => c.eCode == userCode).Count();
        //    return query.Count();
        //}

        //public List<DocFileInfo> GetAllDeleteDocList(int[] ids)
        //{
        //    var list = _context.td_Document.Where(c => ids.Contains(c.id)).ToList();
        //    List<DocFileInfo> resultList = new List<DocFileInfo>();
        //    var files = _context.td_File.Where(c => ids.Contains(c.DirId)).ToList();
        //    resultList.AddRange(from a in list
        //                        orderby a.Sort
        //                        select new DocFileInfo()
        //                        {
        //                            DocId = a.id,
        //                            DocName = a.title,
        //                            Sort = a.Sort,
        //                            FileCount = files == null ? 0 : files.Where(c => c.DirId == a.id).Count()
        //                        });
        //    var childIds = _context.td_Document.Where(c => c.pid.HasValue && ids.Contains(c.pid.Value)).Select(c => c.id).ToArray();
        //    if (childIds != null && childIds.Length > 0)
        //    {
        //        resultList.AddRange(GetAllDeleteDocList(childIds));
        //    }
        //    return resultList;
        //}

        //public bool AddEmployee(td_Employee entity)
        //{
        //    if (entity == null) return false;
        //    _context.td_Employee.Add(entity);
        //    try
        //    {
        //        td_vacation model = new td_vacation();
        //        model.vac_eCode = entity.eCode;
        //        model.vac_Name = entity.eName;
        //        model.vac_FirstTs = 0;
        //        model.vac_TS = 0;
        //        model.vac_Date = DateTime.Now;
        //        _context.td_vacation.Add(model);
        //        _context.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageLog.AddErrorLogDalSpecial("AddEmployee(新增员工)", ex.Message);
        //        return false;
        //    }
        //}
        #endregion

        //public IList<td_Document> GetManagerDocPageListByCondition(int iPageIndex, int iPageSize, ref int iTotalRecord, string manager, string searchKey)
        //{
        //    var query = _context.td_Document.Where(c => !string.IsNullOrEmpty(c.Manager));
        //    if (!string.IsNullOrEmpty(manager))
        //        query = query.Where(c => c.eCode == manager);
        //    if (!string.IsNullOrEmpty(searchKey))
        //        query = query.Where(c => c.title.Contains(searchKey));
        //    iTotalRecord = query.Count();
        //    return query.OrderBy(c => c.Sort).Skip(iPageIndex * iPageSize).Take(iPageSize).ToList();
        //}
    }
}
