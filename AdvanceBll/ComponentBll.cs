using AdvanceDal;
using AdvanceEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceBll
{
    public class ComponentBll
    {
        private ComponentDal componentDal = new ComponentDal();
        public DataTable SelectProperty(string ConnString)
        {
            try
            {
                return componentDal.SelectProperty(ConnString);
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetCompEntry(string ConnString)
        {
            try
            {
                return componentDal.GetCompEntry(ConnString);
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetCompEntryA(string ConnString)
        {
            try
            {
                return componentDal.GetCompEntryA(ConnString);
            }
            catch
            {
                throw;
            }
        }
        public int CreateComponent(int TransId, int pmlid, string ConnString, string User)
        {
            int id = componentDal.CreateComponent(TransId, pmlid, ConnString, User);
            return id;
        }
        public int LockTransaction(int TransId, string ConnString, string User)
        {
            int id = componentDal.LockTransaction(TransId, ConnString, User);
            return id;
        }

        public int UnlockTransaction(int TransId, string ConnString, string User)
        {
            int id = componentDal.UnlockTransaction(TransId, ConnString, User);
            return id;
        }
        public int AddComponents(PropertyEntity x)
        {
            int id = componentDal.AddComponents(x);
            return id;
        }

        public int UpdateComponents(PropertyEntity x)
        {
            int id = componentDal.UpdateComponents(x);
            return id;
        }

        public int DeleteComponent(int idForUpdate, string ConnString)
        {
            int id = componentDal.DeleteComponent(idForUpdate, ConnString);
            return id;
        }
        public int CancelComponent(PropertyEntity x)
        {
            int id = componentDal.CancelComponent(x);
            return id;
        }
        
        public DataTable GetCompAdded(string ConnString,string id)
        {
            try
            {
                return componentDal.GetCompAdded(ConnString,id);
            }
            catch
            {
                throw;
            }
        }
        public int ApproveComponent(PropertyEntity x)
        {
            int id = componentDal.ApproveComponent(x);
            return id;
        }
        public int DeclineComponent(PropertyEntity x)
        {
            int id = componentDal.DeclineComponent(x);
            return id;
        }
        public DataTable GetComponentMasterlist(string ConnString, string search, string Filter)
        {
            try
            {
                return componentDal.GetComponentMasterlist(ConnString, search, Filter);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetComponentInformation(string ConnString, string search)
        {
            try
            {
                return componentDal.GetComponentInformation(ConnString, search);
            }
            catch
            {
                throw;
            }
        }

        public string GetComponentInformation(PropertyEntity x)
        {
            try
            {
                string result = "";

                DataTable dt = componentDal.GetComponentInformation(x);

                result = dt.Rows[0]["ParentPropertyNumber"].ToString() + "|" +
                         dt.Rows[0]["PropertyNumber"].ToString() + "|" +
                         dt.Rows[0]["LongDescription"].ToString() + "|" +
                         dt.Rows[0]["SerialNo"].ToString() + "|" +
                         dt.Rows[0]["Classification"].ToString() + "|" +
                         dt.Rows[0]["Brand"].ToString() + "|" +
                         dt.Rows[0]["Model"].ToString() + "|" +
                         dt.Rows[0]["TagID"].ToString() + "|" +
                         dt.Rows[0]["UnitOfMeasurement"].ToString() + "|" +
                         dt.Rows[0]["AcquiredCost"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public int UpdateComponentInformation(PropertyEntity x)
        {
            int id = componentDal.UpdateComponentInformation(x);
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
