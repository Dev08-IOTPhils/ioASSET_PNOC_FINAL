using AdvanceDal;

namespace AdvanceBll
{
    public class ConnectionStringBll
    {
        private ConnectionStringDal csdal = new ConnectionStringDal();

        public string SetConnectionStringInUsed(string connectionstringinused)
        {
            return connectionstringinused;
        }
    }
}