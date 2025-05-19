namespace AdvanceEntity
{
    public class UserEntity : BaseEntity
    {
        public bool Active { get; set; }
        public string Administrative { get; set; }
        public string Approver { get; set; }
        public int ApproverID { get; set; }
        public string Certifier { get; set; }
        public int CertifierID { get; set; }
        public string ConfirmPassword { get; set; }
        public string Custodians { get; set; }
        public int DepartmentID { get; set; }
        public string EmailAddress { get; set; }
        public string EmployeeNo { get; set; }
        public string FirstName { get; set; }
        public string ImageExtension { get; set; }
        public string Inspector { get; set; }
        public int IntegratorID { get; set; }
        public bool IsAdministrative { get; set; }
        public bool IsApprover { get; set; }
        public bool IsCertifier { get; set; }
        public bool IsCustodian { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsInspector { get; set; }
        public string LastName { get; set; }
        //datatable
        public string Name { get; set; }

        public string NewPassword { get; set; }
        public string Password { get; set; }
        public int PositionID { get; set; }
        public string SignatureExtension { get; set; }
        public int SiteID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        //added by nero

        //Added by: Domdom
        public string Department { get; set; }
        public string Position { get; set; }
        public string Site { get; set; }
        public string Remarks { get; set; }
    }
}