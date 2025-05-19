namespace AdvanceEntity
{
    public class PropertyEntity : BaseEntity
    {

        //added by domdom
        public string Reason { get; set; }
        public int DetailsID { get; set;}
        public string CustodianIDstr { get; set; }
        public string Custodian { get; set; }
        public string Paricsword { get; set; }

        public string PropertyDisposalID { get; set; }


        //stock

        public decimal AcquiredCost { get; set; }
        public string AcquiredCostString { get; set; }
        public string Type { get; set; }

        public string Action { get; set; }
        public bool Active { get; set; }
        //----
        public string Approver { get; set; }
        public string NewPropertyNumber { get; set; }
        public int ApproverID { get; set; }
        public string Area { get; set; }
        public int AreaID { get; set; }
        public int AssetInventoryAccount { get; set; }
        //Goods Receive Property
        public string AssetType { get; set; }

        //public int AssignedPerson { get; set; }
        public string AssignedPersonName { get; set; }

        public string BarcodeNumber { get; set; }
        public string Brand { get; set; }
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public string Category { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int CertifierID { get; set; }
        public string Classification { get; set; }
        public int ClassificationID { get; set; }
        public string UserDepartment { get; set; }
        public string TypeCost { get; set; }


        //property filesdata
        public string ClassificationName { get; set; }

        public string Condition { get; set; }
        public string CreateDate { get; set; }
        public int CustodianID { get; set; }
        public string DateOfAcquisition { get; set; }
        public string DateOfCreation { get; set; }
        public string DateOfExtension { get; set; }
        //Property Maintenance
        public string DateOfMaintenance { get; set; }

        public string DateOfReturn { get; set; }
        public string DateOfTransaction { get; set; }
        public string DateOfTransfer { get; set; }
        public string DateToday { get; set; }
        public string DepreciatedCostString { get; set; }
        public string DepreciationMethod { get; set; }
        public string Description { get; set; }
        public string Destination { get; set; }
        public int DeviceID { get; set; }
        //Property Disposal
        public string DisposalNumber { get; set; }

        //added by nero
        public string DisposedDate { get; set; }

        public string DRNumber { get; set; }
        public string emailSend { get; set; }
        public int EmployeeID { get; set; }
        public int ExpenseAccount { get; set; }
        public string ExpiryDate { get; set; }
        public string FilterBy { get; set; }
        public int FromEmployeeID { get; set; }
        //Property Gate Pass
        public string GatePassNumber { get; set; }

        //goods receive header
        public int GoodsReceiveID { get; set; }

        public string GoodsReceiveNumber { get; set; }
        public int GRPropertyID { get; set; }
        public int IncomeAccount { get; set; }
        public int InspectorID { get; set; }
        public string InsuranceDate { get; set; }
        public string InsuranceEndDate { get; set; }
        public string InsuranceStartDate { get; set; }
        public string InventoryDate { get; set; }
        public string InventoryLocation { get; set; }
        public string InventoryName { get; set; }
        public string InventorySite { get; set; }
        public string InventoryStatus { get; set; }
        public string InvoiceNumber { get; set; }
        public string isActive { get; set; }
        public string isIn { get; set; }
        public string isInString { get; set; }
        public string isOut { get; set; }
        public string isOutString { get; set; }
        //Issuance
        public string IssuanceNumber { get; set; }

        //added by nero
        public string IssuedDate { get; set; }

        public string IssuedTo { get; set; }
        public string IsUse { get; set; }
        public string ItemCode { get; set; }
        public string LabelToPrint { get; set; }
        //added by nero
        public string LastmaintenanceDate { get; set; }

        public string Location { get; set; }
        public int LocationID { get; set; }
        public string Logic { get; set; }
        public string LongDescription { get; set; }
        public string MaintenanceLeadTime { get; set; }
        public string MaintenanceRemarks { get; set; }
        public string MaintenanceStatus { get; set; }
        public string MaintenanceStep { get; set; }
        public int MaintenanceThreshold { get; set; }
        public string MaintenanceType { get; set; }
        public string Model { get; set; }
        public int ModelID { get; set; }
        public string ModelName { get; set; }
        public string ModeOfDisposal { get; set; }
        public string NextStep { get; set; }
        public string NoOfDaysFromCreation { get; set; }
        public string NoOfItem { get; set; }
        public string NoOfItemReturn { get; set; }
        public string NoOfItemString { get; set; }
        public string Operator { get; set; }
        public string Origin { get; set; }
        public string PercentageOfCompletion { get; set; }
        //files data property
        public string Picture { get; set; }

        public int pmlid { get; set; }
        public string PONumber { get; set; }
        public string PRNumber { get; set; }
        public int PropertyID { get; set; }
        public string PropertyImage { get; set; }
        public string TagType { get; set; }

        //Property Log/Movement
        public string PropertyLog { get; set; }

        public string PropertyLogDate { get; set; }
        public string PropertyNumber { get; set; }
        //Gate Pass
        public string Purpose { get; set; }

        public int Quantity { get; set; }
        public string QuantityString { get; set; }
        public int ReceivedBy { get; set; }
        public string Receiver { get; set; }
        //recID
        public int RecID { get; set; }

        public int RecordID { get; set; }
        public string RemainingUsefulLifeString { get; set; }
        public string Remarks { get; set; }
        public string RemarksTransfer { get; set; }
        public string ReportName { get; set; }
        public string RequestedBy { get; set; }
        public int RequestedById { get; set; }
        //Request
        public string RequestNumber { get; set; }

        public string ReturnNumber { get; set; }
        //Property Return Request
        public string ReturnRequestNumber { get; set; }

        public string RFIDNumber { get; set; }
        public decimal SalePrice { get; set; }
        public string Selector { get; set; }
        //added by nero
        public string SerialNo { get; set; }

        public string ServiceProvider { get; set; }
        public int ServiceProviderID { get; set; }
        //Property Location
        public string Site { get; set; }

        public int SiteID { get; set; }
        public string Status { get; set; }
        public int StockID { get; set; }
        public string StringNoOfItem { get; set; }
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string SupportingDocument { get; set; }
        public int TagID { get; set; }
        public int TagTypeID { get; set; }
        public int TaskID { get; set; }
        public int ToAreaID { get; set; }
        public int ToEmployeeID { get; set; }
        public int ToLocationID { get; set; }
        public int ToSiteID { get; set; }
        public string TransactionNumber { get; set; }
        public string TransactionType { get; set; }
        public string TransactorName { get; set; }
        public string TransferNumber { get; set; }
        //Transfer
        public int TransferType { get; set; }

        //tagging / printing
        public string UnitCostString { get; set; }

        public string UnitOfMeasurement { get; set; }
        public int UnitOfMeasurementID { get; set; }
        public int UsefulLife { get; set; }
        // added by nero
        public string UsefulLifeString { get; set; }

        //filter
        public string Values { get; set; }

        public string WarrantyDate { get; set; }
        public string WarrantyEndDate { get; set; }
        public string WarrantyStartDate { get; set; }
        //Property ReTagging
    }
}