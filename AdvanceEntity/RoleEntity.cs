using System.Collections.Generic;

namespace AdvanceEntity
{
    public class RoleAccess
    {
        public List<RoleAccess> getSetModuleID { get; set; }
        public int ModuleID { get; set; }
    }

    public class RoleEntity : BaseEntity
    {
        private List<RoleAccess> ListRoleAccess = new List<RoleAccess>();
        private List<RoleMember> ListRoleMember = new List<RoleMember>();
        public bool Active { get; set; }
        public string Description { get; set; }
        public List<RoleAccess> GetSetRoleAccess
        {
            set { ListRoleAccess = value; }
            get { return ListRoleAccess; }
        }

        public List<RoleMember> GetSetRoleMember
        {
            set { ListRoleMember = value; }
            get { return ListRoleMember; }
        }

        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string User { get; set; }
    }

    public class RoleMember
    {
        public List<RoleMember> getSetUserID { get; set; }
        public string UserID { get; set; }
    }
}