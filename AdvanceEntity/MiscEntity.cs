namespace AdvanceEntity
{
    public class MiscEntity : BaseEntity
    {
        //Accounting Setting
        public int AccountingPeriod { get; set; }

        public string AcknowledgementMode { get; set; }

        public string Address { get; set; }

        //Threshold
        public int AgingThreshold { get; set; }

        public string BakFileName { get; set; }

        public string BakFilePath { get; set; }

        public string BarcodeNumberGeneration { get; set; }

        public decimal CapexOpex { get; set; }

        public string CompanyLogo { get; set; }

        //COmpany Information Setting
        public string CompanyName { get; set; }

        public string ContactNumber { get; set; }

        public string ContactPerson { get; set; }

        public int ConversionMonth { get; set; }

        public bool CreationApproval { get; set; }

        public bool CreationScanner { get; set; }

        public bool CSVUploadApproval { get; set; }

        public bool SupplyAllocationApproval { get; set; }

        //DataUpload
        public string DataUploadFileNameProperty { get; set; }

        public string DataUploadFileNameStock { get; set; }

        public string DataUploadFileNameUser { get; set; }

        public int DaysPerYear { get; set; }

        //DB Directory
        public string DBDirectory { get; set; }

        public bool DBRestoreApproval { get; set; }

        //Depreciation Setting
        public string DepreciationFormula { get; set; }

        public string Email { get; set; }

        public string EmailAddress { get; set; }

        public string EmailSubject { get; set; }

        public int EndMonth { get; set; }

        public string ExpenseType { get; set; }

        public string FormLogo { get; set; }

        public bool GatePassApproval { get; set; }

        public int HandHeldThreshold { get; set; }

        //Home Page Image
        public string HomePageImage { get; set; }

        public decimal ICS { get; set; }

        public bool IssuanceAcknowledgement { get; set; }

        public string LogoPath { get; set; }

        public int MaintenanceThreshold { get; set; }

        public decimal PAR { get; set; }

        public string Password { get; set; }

        public bool PropertyDisposalApproval { get; set; }

        public bool PropertyDisposalScanner { get; set; }

        public bool PropertyIssuanceApproval { get; set; }

        public bool PropertyIssuanceScanner { get; set; }

        public string PropertyNoGeneration { get; set; }

        public bool PropertyRequestApproval { get; set; }

        public bool PropertyRequestScanner { get; set; }

        public bool PropertyReturnRequestScanner { get; set; }

        public bool PropertyTransferApproval { get; set; }

        public bool PropertyTransferScanner { get; set; }

        public decimal RateOfDepreciation { get; set; }

        public string RequestedDate { get; set; }

        public bool RequireMaintenance { get; set; }

        public decimal ResidualValue { get; set; }

        //Database Restore
        public int RestoreID { get; set; }

        public string RestoreName { get; set; }

        public string RestoreRequestor { get; set; }

        public bool RetaggingApproval { get; set; }

        //RFID and Barcode Setting
        public string RFIDNumberGeneration { get; set; }

        public bool RFIDprinting { get; set; }

        public decimal SalvageValue { get; set; }

        public string SMTPhost { get; set; }

        public int SMTPport { get; set; }

        public bool StockDisposalApproval { get; set; }

        public bool StockIssuanceApproval { get; set; }

        public bool StockMergeApproval { get; set; }

        public bool StockRequestRemainingQuantity { get; set; }

        public bool StockTaking { get; set; }

        //Mail Settings
        public string TestEmail { get; set; }
        public string TradingName { get; set; }

        public int TransactionMonth { get; set; }

        //added by nero
        public int TransactionYear { get; set; }

        public bool TurnOverStockApproval { get; set; }

        public string UploadBy { get; set; }

        public string UploadDate { get; set; }

        //uploaded Fils Directory
        public string UploadedFilesDirectory { get; set; }

        public int UsefulLife { get; set; }

        //User Default Password
        public string UserDefaultPassword { get; set; }
        public string VerificationMode { get; set; }
        //System Settings
        //ADDEDBYGHE
        public string VersionMode { get; set; }

        public string WebIcon { get; set; }
        public string WebTitleCollpased { get; set; }
        public string WebTitleExpanded { get; set; }
    }
}