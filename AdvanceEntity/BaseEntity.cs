using System;

namespace AdvanceEntity
{
    public class BaseEntity
    {
        public string Action { get; set; }
        public int AreaID { get; set; }
        public int AssignedPerson { get; set; }
        public string ConnectionString { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ExpenseType { get; set; }
        public string IsActive { get; set; }
        public string IsReturnable { get; set; }
        public int LocationID { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int SiteID { get; set; }
        #region Password Policy

        public string DefaultOrGenerated { get; set; }
        public string DefaultPassword { get; set; }
        public int LoginAttempt { get; set; }
        public int LowerCaseSize { get; set; }
        public int NumberSize { get; set; }
        public int PasswordSize { get; set; }
        public int SymbolSize { get; set; }
        public int TaggingType { get; set; }
        public int UpperCaseSize { get; set; }
        public string UsernameOrEmail { get; set; }
        public bool WithLoginAttempt { get; set; }
        public bool WithLowerCase { get; set; }
        public bool WithNumber { get; set; }
        public bool WithSymbol { get; set; }
        public bool WithUpperCase { get; set; }
        #endregion Password Policy
    }
}