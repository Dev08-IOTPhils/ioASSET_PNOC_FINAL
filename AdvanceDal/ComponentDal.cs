using AdvanceEntity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceDal
{
    public class ComponentDal
    {
        public DataTable SelectProperty(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_AvailablePropertyForComponentEntry]")
                    , new SqlParameter("orderBy", "RecID ASC")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch
            {
                throw;
            }
        }
        
        public DataTable GetCompEntry(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";


                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_ComponentEntryHeader]")
                    , new SqlParameter("orderBy", "ComponentEntryHeaderID Desc")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetCompEntryA(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = " Status = 'Pending Approval' ";


                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_ComponentEntryHeader]")
                    , new SqlParameter("orderBy", "ComponentEntryHeaderID Desc")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetCompAdded(string ConnString,string id)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = " ComponentEntryNo = '" + id+"'";
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_ComponentEntryDetails]")
                    , new SqlParameter("orderBy", "ComponentEntryDetailsID Desc")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch
            {
                throw;
            }
        }
        
        public int CreateComponent(int TransId, int pmlid, string ConnString, string User)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_CreateComponentEntry"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ComponentEntryHeaderID", TransId)
                    , new SqlParameter("PMLID", pmlid)
                    , new SqlParameter("User", User)
                    );
            }
            catch
            {
                throw;
            }
        }
        public int LockTransaction(int TransId, string ConnString, string User)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_ConfirmComponentEntry"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ComponentEntryHeaderID", TransId)
                    , new SqlParameter("User", User)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int UnlockTransaction(int TransId, string ConnString, string User)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_UnlockComponentEntry"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ComponentEntryHeaderID", TransId)
                    , new SqlParameter("User", User)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int AddComponents(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_AddComponentToEntry"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("TransactionNo", x.TransactionNumber)
                    , new SqlParameter("ComponentNo", x.PropertyNumber)
                    , new SqlParameter("Description", x.Description)
                    //, new SqlParameter("LongDescription", x.LongDescription)
                    , new SqlParameter("SerialNo", x.SerialNo)
                    //, new SqlParameter("Classification", x.Classification)
                    //, new SqlParameter("Brand", x.Brand)
                    //, new SqlParameter("Model", x.Model)
                    //, new SqlParameter("TagID", x.TagID)
                    , new SqlParameter("AcquiredDate", x.DateOfAcquisition)
                    , new SqlParameter("UnitOfMeasurement", x.UnitOfMeasurement)
                    , new SqlParameter("AcquiredCost", x.AcquiredCost)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int UpdateComponents(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_UpdateComponentToEntry"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ComponentEntryDetailsID", x.PropertyID)
                    , new SqlParameter("TransactionNo", x.TransactionNumber)
                    , new SqlParameter("ComponentNo", x.PropertyNumber)
                    , new SqlParameter("Description", x.LongDescription)
                    , new SqlParameter("SerialNo", x.SerialNo)
                    , new SqlParameter("UnitOfMeasurement", x.UnitOfMeasurement)
                    , new SqlParameter("AcquiredCost", x.AcquiredCost)
                    , new SqlParameter("AcquiredDate", x.DateOfAcquisition)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int DeleteComponent(int idForUpdate, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeleteComponentToEntry"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ComponentEntryDetailsID", idForUpdate)
                    );
            }
            catch
            {
                throw;
            }
        }
        public int CancelComponent(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_CancelComponentEntry"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("TransactionNo", x.PropertyNumber)
                    , new SqlParameter("Remarks", x.Remarks)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int ApproveComponent(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_ApproveComponentEntry"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ComponentEntryHeaderID", x.PropertyID)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }
        public int DeclineComponent(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeclineComponentEntry"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ComponentEntryHeaderID", x.PropertyID)
                    , new SqlParameter("Remarks", x.Remarks)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetComponentMasterlist(string ConnString, string search, string Filter)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_ICML]")
                    , new SqlParameter("orderBy", "RecID Asc")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public DataTable GetComponentInformation(string ConnString, string search)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_IntegratedComponentInformation]")
                    , new SqlParameter("orderBy", "RecID Asc")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetComponentInformation(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveComponentInformation"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("PropertyNumber", x.PropertyNumber)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int UpdateComponentInformation(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_UpdateComponentInformation"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("PropertyNumber", x.PropertyNumber)
                    , new SqlParameter("SerialNo", x.SerialNo)
                    , new SqlParameter("Description", "")
                    , new SqlParameter("LongDescription", x.LongDescription)
                    , new SqlParameter("Classification", x.Classification)
                    , new SqlParameter("Brand", x.Brand)
                    , new SqlParameter("Model", x.Model)
                    , new SqlParameter("UnitOfMeasurement", x.UnitOfMeasurement)
                    , new SqlParameter("AcquiredCost", x.AcquiredCost)
                    , new SqlParameter("TagID", x.TagID)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
