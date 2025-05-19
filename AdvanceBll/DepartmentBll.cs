using AdvanceDal;
using AdvanceEntity;
using System;
using System.Data;

namespace AdvanceBll
{
    public class DepartmentBll
    {
        private ActivityLogBll activityLogBLL = new ActivityLogBll();
        private DepartmentDal departmentDAL = new DepartmentDal();
        public int DeleteDepartment(DepartmentEntity departmentEntity)
        {
            int returnVal = departmentDAL.DeleteDepartment(departmentEntity);
            try
            {
                return returnVal;
            }
            catch (Exception ex)
            {
                return returnVal;
            }
        }

        public DataTable GetDepartment(string ConnString)
        {
            try
            {
                return departmentDAL.GetDepartment(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public string GetDepartmentByName(DepartmentEntity departmentEntity)
        {
            try
            {
                string result = "";

                DataTable dtDepartment = departmentDAL.GetDepartmentByName(departmentEntity);

                result = dtDepartment.Rows[0]["DepartmentID"].ToString() + "|" +
                        dtDepartment.Rows[0]["DepartmentName"].ToString() + "|" +
                        dtDepartment.Rows[0]["Description"].ToString() + "|" +
                        dtDepartment.Rows[0]["Active"].ToString();

                return result;
            }
            catch
            {
                throw;
            }
        }

        public string GetDepartmentID(DepartmentEntity departmentEntity)
        {
            try
            {
                string result = "";

                DataTable dt = departmentDAL.GetDepartmentID(departmentEntity);

                result = dt.Rows[0]["DepartmentID"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetDepartmentList(DepartmentEntity departmentEntity)
        {
            try
            {
                return departmentDAL.GetDepartmentList(departmentEntity);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetDepartmentList1(DepartmentEntity departmentEntity)
        {
            try
            {
                return departmentDAL.GetDepartmentList1(departmentEntity);
            }
            catch
            {
                throw;
            }
        }

        public int SaveDepartment(DepartmentEntity departmentEntity)
        {
            int id = departmentDAL.SaveDepartment(departmentEntity);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }
    }
}