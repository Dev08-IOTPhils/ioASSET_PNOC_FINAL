namespace AdvanceEntity
{
    public class StockEntity : BaseEntity
    {
        public decimal AcquiredCost { get; set; }
        public bool Active { get; set; }
        public string Approver { get; set; }
        public int ApproverID { get; set; }
        public int AssetInventoryAccount { get; set; }
        public string BarcodeNumber { get; set; }
        public string Brand { get; set; }
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public string Category { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Classification { get; set; }
        public int ClassificationID { get; set; }
        public string ClassificationName { get; set; }
        public int CriticalLevel { get; set; }
        public string CriticalLevelString { get; set; }
        public string Custodian { get; set; }
        public int CustodianID { get; set; }
        public string DateOfAcquisition { get; set; }
        public string DateOfTransaction { get; set; }
        public string Description { get; set; }
        public string DisposalNumber { get; set; }
        public string DRNumber { get; set; }
        public int EmployeeID { get; set; }
        public int ExpenseAccount { get; set; }
        public string FromStockIC { get; set; }
        //Goods Receive
        public int GRStockID { get; set; }

        public int IncomeAccount { get; set; }
        public string InvoiceNumber { get; set; }
        //Stock Issuance
        public string IssuanceNo { get; set; }

        public string IssuedTo { get; set; }
        public string ItemCode { get; set; }
        public string Location { get; set; }
        public string Model { get; set; }
        public int ModelID { get; set; }
        public string ModelName { get; set; }
        public string ModeOfDisposal { get; set; }
        public string NoOfItem { get; set; }
        public string PONumber { get; set; }
        public string PRNumber { get; set; }
        public int Quantity { get; set; }
        public string QuantityString { get; set; }
        public string QuantityStringIssue { get; set; }
        public int ReceivedBy { get; set; }
        public int RecID { get; set; }
        public string RecIDstring { get; set; }
        //
        public string Region { get; set; }

        public string Remarks { get; set; }
        public string OtherRemarks { get; set; }
        //Stock Request
        public string RequestNo { get; set; }

        public bool Returnable { get; set; }
        //Stock Return
        public string ReturnNo { get; set; }

        public string RFIDNumber { get; set; }
        public string Status { get; set; }
        public int StockID { get; set; }
        public string StockImage { get; set; }
        public string StockMergeNo { get; set; }
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string SupportingDocument { get; set; }
        public string TagType { get; set; }
        public int TagTypeID { get; set; }
        //Stock Merge
        public string ToStockIC { get; set; }

        public string TransactionDate { get; set; }
        public string TransactorName { get; set; }
        public string UnitOfMeasurement { get; set; }
        public int UnitOfMeasurementID { get; set; }

        //Stock Allocation

        public int FromSite { get; set; }
        public int ToSite { get; set; }
        public string FromSiteStr { get; set; }

        public string ToSiteStr { get; set; }
        public string StockAllocationNo { get; set; }
        public string CancelRemarks { get; set; }

        
    }
}