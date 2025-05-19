using IOTPIEncrypto;
using Microsoft.Win32;
using System;
using System.Globalization;
using System.IO;
using System.Management;
using System.Web;

namespace AdvanceBll
{
    public class LicenseChecker
    {
        public static string ClientCode = "";
        public static DateTime InstallationDate;
        public static int isFreeorPaid = 0;

        //0 - Free 1 - Paid
        public static int myLicenseType = 0;

        // 0 - No License 1 - Temp License 2 - Ext. License, 3 - Permanent License
        public static SystemInfLicense sil;

        public static DateTime SSADate;
        public static int SSANotif = 0;

        //0 - no notif 1 - show notif
        public static int TimeBomb = 0;

        public static DateTime TimeBombDate;
        private static LicenseEncrpyto licenseEncrypto = new LicenseEncrpyto();
        private static string productcode = "AM001";
        //0 - dont do anything  1 - close app
        public static string CheckLicense()
        {
            int numberofdays = 0;
            try
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\IOTPIEncryto\0787-6335-7190-1115-2014", true);

                if (registryKey != null)
                {
                    object value = registryKey.GetValue(productcode);

                    if (value != null)
                    {
                        string rgstryKey = value.ToString();
                        string _LicenseKey = Decrypt(Decrypt(Decrypt(Decrypt(Decrypt(rgstryKey)))));
                        string serialNumber = string.Empty;
                        string processId = string.Empty;
                        string uuid = string.Empty;
                        string ComputerInfo = string.Empty;
                        string query = "SELECT * FROM Win32_BaseBoard";
                        ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
                        foreach (ManagementObject info in searcher.Get())
                        {
                            try { ComputerInfo += (info.GetPropertyValue("SerialNumber").ToString()); break; } catch (Exception) { }
                        }
                        ComputerInfo += "|";
                        query = "Select * FROM Win32_Processor";
                        searcher = new ManagementObjectSearcher(query);
                        foreach (ManagementObject info in searcher.Get())
                        {
                            try { ComputerInfo += (info.GetPropertyValue("ProcessorId").ToString()); break; } catch (Exception) { }
                        }
                        ComputerInfo += "|";
                        query = "Select * FROM Win32_ComputerSystemProduct";
                        searcher = new ManagementObjectSearcher(query);
                        foreach (ManagementObject info in searcher.Get())
                        {
                            try { ComputerInfo += (info.GetPropertyValue("UUID").ToString()); break; } catch (Exception) { }
                        }
                        ComputerInfo += "|";

                        //Split the License Key Part
                        string[] _SplitLicenseKey = _LicenseKey.Split('|');
                        RegistryGetData(_SplitLicenseKey);
                        TempLicenseGetData(_SplitLicenseKey);
                        //Get Computer Details
                        string _LisenceComputerDetails = _SplitLicenseKey[0] + "|" + _SplitLicenseKey[1] + "|" + _SplitLicenseKey[2] + "|" + _SplitLicenseKey[3] + "|";
                        string _ApplicationComputerDetails = _SplitLicenseKey[0] + "|" + ComputerInfo;
                        if (_LisenceComputerDetails == _ApplicationComputerDetails)
                        {
                            //Check License Status
                            if (_SplitLicenseKey[11] == "1" || _SplitLicenseKey[11] == "2")
                            {
                                //Check Expiry Date
                                if (Converter.myParseDate(_SplitLicenseKey[10]) < DateTime.Now) return "invalid";
                            }
                        }
                        else return "invalid";
                        return "alright";
                    }
                    else
                    {
                        //string secretFileValue = File.ReadAllText("C:\\Windows\\INF\\41336682-3658-1422-0000-9042766-I" + productcode + ".dll");
                        //string decryptedValue = Decrypt(Decrypt(Decrypt(Decrypt(Decrypt(secretFileValue)))));
                        //string[] decryptedValueArray = decryptedValue.Split('|');
                        string secretFileValuex = File.ReadAllText(@"C:\Windows\INF\41336682-3658-1422-0000-9042766-I" + productcode + ".dll");
                        string decryptedValuex = Decrypt(Decrypt(Decrypt(Decrypt(Decrypt(secretFileValuex)))));
                        string[] decryptedValueArrayx = decryptedValuex.Split('|');

                        TempLicenseGetData(decryptedValueArrayx);
                        DateTime dateofexpiryx = DateTime.ParseExact(decryptedValueArrayx[10], "MM/dd/yyyy", null);
                        if (Converter.myParseDate(decryptedValueArrayx[10]) >= DateTime.Now && decryptedValueArrayx[11] != "0")
                        {
                            numberofdays = (Converter.myParseDate(decryptedValueArrayx[10]) - DateTime.Now).Days + 1;
                        }
                        else
                        {
                            numberofdays = 404; //not found or activation failed
                        }

                        return numberofdays.ToString();
                    }
                }
                else
                {
                    string secretFileValue = File.ReadAllText(@"C:\Windows\INF\41336682-3658-1422-0000-9042766-I" + productcode + ".dll");
                    string decryptedValue = Decrypt(Decrypt(Decrypt(Decrypt(Decrypt(secretFileValue)))));
                    string[] decryptedValueArray = decryptedValue.Split('|');
                    TempLicenseGetData(decryptedValueArray);
                    DateTime dateofexpiry = DateTime.ParseExact(decryptedValueArray[10], "MM/dd/yyyy", null);
                    if (Converter.myParseDate(decryptedValueArray[10]) >= DateTime.Now && decryptedValueArray[11] != "0")
                    {
                        numberofdays = (Converter.myParseDate(decryptedValueArray[10]) - DateTime.Now).Days + 1;
                    }
                    else
                    {
                        numberofdays = 404; //not found or activation failed
                    }
                    if (numberofdays != 404)
                    {
                        return "You have " + numberofdays.ToString() + " day(s) left to activate the system. Please contact your solution provider.";
                    }
                    else
                    {
                        return numberofdays.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                string xxx = ex.Message;
                return "No License Found.";
            }
        }

        public static string CheckLicenseUAC()
        {
            try
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\IOTPIEncryto\0787-6335-7190-1115-2014", true);

                if (registryKey != null)
                {
                    object value = registryKey.GetValue(productcode);
                    if (value != null)
                    {
                        string rgstryKey = value.ToString();
                        string _LicenseKey = Decrypt(Decrypt(Decrypt(Decrypt(Decrypt(rgstryKey)))));

                        //Split the License Key Part
                        string[] _SplitLicenseKey = _LicenseKey.Split('|');
                        return _SplitLicenseKey[23] + "|" + _SplitLicenseKey[24];
                    }
                    else
                    {
                        string secretFileValuex = File.ReadAllText(@"C:\Windows\INF\41336682-3658-1422-0000-9042766-I" + productcode + ".dll");
                        string decryptedValuex = Decrypt(Decrypt(Decrypt(Decrypt(Decrypt(secretFileValuex)))));
                        string[] decryptedValueArrayx = decryptedValuex.Split('|');

                        return decryptedValueArrayx[23] + "|" + decryptedValueArrayx[24];
                    }
                }
                else
                {
                    string secretFileValuex = File.ReadAllText(@"C:\Windows\INF\41336682-3658-1422-0000-9042766-I" + productcode + ".dll");
                    string decryptedValuex = Decrypt(Decrypt(Decrypt(Decrypt(Decrypt(secretFileValuex)))));
                    string[] decryptedValueArrayx = decryptedValuex.Split('|');

                    return decryptedValueArrayx[23] + "|" + decryptedValueArrayx[24];
                }
            }
            catch (Exception ex)
            {
                string xxx = ex.Message;
                return "No License Found.";
            }
        }

        public static string Decrypt(string cipherText)
        {
            try
            { return licenseEncrypto.Decrypt("Succe55", cipherText); }
            catch (Exception)
            { return "Unable to decrypt the data"; }
        }

        public static string Encrypt(string defaultText)
        {
            try
            { return licenseEncrypto.Encrypt("Succe55", defaultText); }
            catch (Exception)
            { return "Unable to encrypt the data"; }
        }

        public static string generateFirstString()
        {
            string registrationDetails = string.Empty;
            registrationDetails += productcode + "|";
            string query = "SELECT * FROM Win32_BaseBoard";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            foreach (ManagementObject info in searcher.Get())
            {
                try { registrationDetails += (info.GetPropertyValue("SerialNumber").ToString()); break; } catch (Exception) { }
            }
            registrationDetails += "|";
            query = "Select * FROM Win32_Processor";
            searcher = new ManagementObjectSearcher(query);
            foreach (ManagementObject info in searcher.Get())
            {
                try { registrationDetails += (info.GetPropertyValue("ProcessorId").ToString()); break; } catch (Exception) { }
            }
            registrationDetails += "|";
            query = "Select * FROM Win32_ComputerSystemProduct";
            searcher = new ManagementObjectSearcher(query);
            foreach (ManagementObject info in searcher.Get())
            {
                try { registrationDetails += (info.GetPropertyValue("UUID").ToString()); break; } catch (Exception) { }
            }

            return registrationDetails;
        }

        public static string generateregistryString(bool isRequest)
        {
            string registrationDetails = string.Empty;
            if (!isRequest) registrationDetails += "0011|";
            else registrationDetails += productcode + "|";

            string query = "SELECT * FROM Win32_BaseBoard";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            foreach (ManagementObject info in searcher.Get())
            {
                try { registrationDetails += (info.GetPropertyValue("SerialNumber").ToString()); break; } catch (Exception) { }
            }
            registrationDetails += "|";
            query = "Select * FROM Win32_Processor";
            searcher = new ManagementObjectSearcher(query);
            foreach (ManagementObject info in searcher.Get())
            {
                try { registrationDetails += (info.GetPropertyValue("ProcessorId").ToString()); break; } catch (Exception) { }
            }
            registrationDetails += "|";
            query = "Select * FROM Win32_ComputerSystemProduct";
            searcher = new ManagementObjectSearcher(query);
            foreach (ManagementObject info in searcher.Get())
            {
                try { registrationDetails += (info.GetPropertyValue("UUID").ToString()); break; } catch (Exception) { }
            }

            return registrationDetails;
        }

        public static string GetExpirationDate()
        {
            string myout = "Expired";
            try
            {
                if (sil != null)
                {
                    if (sil.LicenseType == 0) myout = "---";
                    else if (sil.LicenseType == 1) myout = sil.Expiration.ToString("MMM dd, yyyy");
                    else if (sil.LicenseType == 2) myout = sil.Expiration.ToString("MMM dd, yyyy");
                    else if (sil.LicenseType == 3) myout = "No expiration";
                }
                else
                {
                    if (myLicenseType == 0) myout = "---";
                    else if (myLicenseType == 1) myout = sil.Expiration.ToString("MMM dd, yyyy");
                    else if (myLicenseType == 2) myout = sil.Expiration.ToString("MMM dd, yyyy");
                    else if (myLicenseType == 3) myout = "No expiration";
                }
            }
            catch { }
            return myout;
        }

        public static string GetLicenseType()
        {
            string myout = "No License.";
            if (sil != null)
            {
                if (sil.LicenseType == 0) myout = "No License";
                else if (sil.LicenseType == 1) myout = "Temp. License";
                else if (sil.LicenseType == 2) myout = "Extnd. Temp. License";
                else if (sil.LicenseType == 3) myout = "Perm. License";
            }
            else
            {
                if (myLicenseType == 0) myout = "No License";
                else if (myLicenseType == 1) myout = "Temp. License";
                else if (myLicenseType == 2) myout = "Extnd. Temp. License";
                else if (myLicenseType == 3) myout = "Perm. License";
            }
            return myout;
        }

        public static string LicenseGen(bool isRequest)
        {
            string secretFileValue = File.ReadAllText("C:\\Windows\\INF\\41336682-3658-1422-0000-9042766-I" + productcode + ".dll");
            string decryptedValue = Decrypt(Decrypt(Decrypt(Decrypt(Decrypt(secretFileValue)))));

            return decryptedValue;
        }

        public static void RegistryGetData(string[] split)
        {
            try
            {
                InstallationDate = Converter.myParseDate(split[16]);
                SSADate = Converter.myParseDate(split[17]);
                SSANotif = Converter.convertStringToInt(split[18]);
                TimeBombDate = Converter.myParseDate(split[19]);
                TimeBomb = Converter.convertStringToInt(split[20]);
                isFreeorPaid = Converter.convertStringToInt(split[21]);
                ClientCode = split[22];
                myLicenseType = 3;
            }
            catch { }
        }

        public static void TempLicenseGetData(string[] split)
        {
            try
            {
                sil = new SystemInfLicense();

                sil.ProductCode = split[0];
                sil.ComputerSerial = split[1];
                sil.ProcessorId = split[2];
                sil.UUID = split[3];
                sil.SalesInvoice = split[4];
                sil.CompanyName = split[5];
                sil.Address = split[6];
                sil.ContactPerson = split[7];
                sil.ContactNumber = split[8];
                sil.EmailAddress = split[9];
                sil.Expiration = Converter.myParseDate(split[10]);
                sil.LicenseType = Converter.convertStringToInt(split[11]);
                sil.PurchaseOrder = split[12];
                sil.DeliveryReceipt = split[13];
                sil.SerialNumber = split[14];
                sil.ProductKey = split[15];
                sil.InstallationDate = Converter.myParseDate(split[16]);
                sil.SSADate = Converter.myParseDate(split[17]);
                sil.SSAToggle = Converter.convertStringToInt(split[18]);
                sil.TimeBombDate = Converter.myParseDate(split[19]);
                sil.TimeBombToggle = Converter.convertStringToInt(split[20]);
                sil.FreeorPaid = Converter.convertStringToInt(split[21]);
                sil.ClientCode = split[22];
                sil.minda = split[23];
                sil.tsuc = split[24];
                sil.resu = split[25];
            }
            catch { }
        }

        public static string uploadactivation(string path, string filename)
        {
            string returnValue = "";
            try
            {
                string encrytedValue = "";
                string secretFileValue = "";
                try
                {
                    encrytedValue = File.ReadAllText(path + filename);
                    secretFileValue = File.ReadAllText("C:\\Windows\\INF\\41336682-3658-1422-0000-9042766-I" + productcode + ".dll");
                }
                catch { }
                string decryptedValue = Decrypt(Decrypt(Decrypt(Decrypt(Decrypt(encrytedValue)))));
                string[] decryptedValueArray = decryptedValue.Split('|');

                string decryptedSecretValue = Decrypt(Decrypt(Decrypt(Decrypt(Decrypt(secretFileValue)))));
                string[] decryptedSecretValueArray = decryptedValue.Split('|');

                string serialNumber = string.Empty;
                string processId = string.Empty;
                string uuid = string.Empty;

                ProcessInfo p = new ProcessInfo();

                string query = "SELECT * FROM Win32_BaseBoard";

                ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
                foreach (ManagementObject info in searcher.Get())
                {
                    try { serialNumber = (info.GetPropertyValue("SerialNumber").ToString()); break; } catch (Exception) { }
                }

                query = "Select * FROM Win32_Processor";
                searcher = new ManagementObjectSearcher(query);
                foreach (ManagementObject info in searcher.Get())
                {
                    try { processId = (info.GetPropertyValue("ProcessorId").ToString()); break; } catch (Exception) { }
                }

                query = "Select * FROM Win32_ComputerSystemProduct";
                searcher = new ManagementObjectSearcher(query);
                foreach (ManagementObject info in searcher.Get())
                {
                    try { uuid = (info.GetPropertyValue("UUID").ToString()); break; } catch (Exception) { }
                }

                if (decryptedValueArray[0] == productcode)
                {
                    if (decryptedValueArray[11] == "1")
                    {
                        //update the script in system/inf/n
                        string firstcode = generateFirstString() + "|" + decryptedValueArray[4] + "|" + decryptedValueArray[5] + "|" + decryptedValueArray[6] + "|" + decryptedValueArray[7] + "|" + decryptedValueArray[8] + "|" + decryptedValueArray[9] + "|" + decryptedValueArray[10] + "|" + decryptedValueArray[11] + "|" + decryptedValueArray[12] + "|" + decryptedValueArray[13] + "|" + decryptedValueArray[14] + "|" + decryptedValueArray[15] + "|" + decryptedValueArray[16] + "|" + decryptedValueArray[17] + "|" + decryptedValueArray[18] + "|" + decryptedValueArray[19] + "|" + decryptedValueArray[20] + "|" + decryptedValueArray[21] + "|" + decryptedValueArray[22] + "|" + decryptedValueArray[23] + "|" + decryptedValueArray[24] + "|";
                        if (WriteToSystemINF(firstcode))
                        {
                            returnValue = "temp";
                        }
                    }
                    else if (decryptedValueArray[11] == "2")
                    {
                        //update the script in system/inf/n
                        string firstcode = generateFirstString() + "|" + decryptedValueArray[4] + "|" + decryptedValueArray[5] + "|" + decryptedValueArray[6] + "|" + decryptedValueArray[7] + "|" + decryptedValueArray[8] + "|" + decryptedValueArray[9] + "|" + decryptedValueArray[10] + "|" + decryptedValueArray[11] + "|" + decryptedValueArray[12] + "|" + decryptedValueArray[13] + "|" + decryptedValueArray[14] + "|" + decryptedValueArray[15] + "|" + decryptedValueArray[16] + "|" + decryptedValueArray[17] + "|" + decryptedValueArray[18] + "|" + decryptedValueArray[19] + "|" + decryptedValueArray[20] + "|" + decryptedValueArray[21] + "|" + decryptedValueArray[22] + "|" + decryptedValueArray[23] + "|" + decryptedValueArray[24] + "|";
                        if (WriteToSystemINF(firstcode))
                        {
                            returnValue = "extended";
                        }
                    }
                    else if (decryptedValueArray[11] == "3")
                    {
                        //update the registry
                        //string firstcode = "" + generateregistryString(false) + "|" + decryptedValueArray[10] + "|" + decryptedValueArray[11] + "|" + productcode + "|" + decryptedValueArray[16] + "|" + decryptedValueArray[17] + "|" + decryptedValueArray[18] + "|" + decryptedValueArray[19] + "|" + decryptedValueArray[20] + "|" + decryptedValueArray[21] + "|" + decryptedValueArray[22] + "|";
                        string firstcode = generateFirstString() + "|" + decryptedValueArray[4] + "|" + decryptedValueArray[5] + "|" + decryptedValueArray[6] + "|" + decryptedValueArray[7] + "|" + decryptedValueArray[8] + "|" + decryptedValueArray[9] + "|" + decryptedValueArray[10] + "|" + decryptedValueArray[11] + "|" + decryptedValueArray[12] + "|" + decryptedValueArray[13] + "|" + decryptedValueArray[14] + "|" + decryptedValueArray[15] + "|" + decryptedValueArray[16] + "|" + decryptedValueArray[17] + "|" + decryptedValueArray[18] + "|" + decryptedValueArray[19] + "|" + decryptedValueArray[20] + "|" + decryptedValueArray[21] + "|" + decryptedValueArray[22] + "|" + decryptedValueArray[23] + "|" + decryptedValueArray[24] + "|";
                        RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE", true);
                        key.CreateSubKey("IOTPIEncryto");
                        key = key.OpenSubKey("IOTPIEncryto", true);
                        key.CreateSubKey("0787-6335-7190-1115-2014");
                        key = key.OpenSubKey("0787-6335-7190-1115-2014", true);
                        string aplicationCode = productcode;
                        string encryptedValue = Encrypt(Encrypt(Encrypt(Encrypt(Encrypt(firstcode)))));
                        key.SetValue(aplicationCode, encryptedValue);
                        key.Close();
                        returnValue = "activated";
                    }
                }
                return returnValue;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "error";
            }
        }
        public static bool WriteToSystemINF(string sval)
        {
            bool myout = false;
            try
            {
                string encryptedValue = Encrypt(Encrypt(Encrypt(Encrypt(Encrypt(sval)))));
                using (var streamWriter = new StreamWriter("C:\\Windows\\INF\\41336682-3658-1422-0000-9042766-I" + productcode + ".dll", false))
                {
                    streamWriter.WriteLine(encryptedValue);
                }
                myout = true;
            }
            catch { }
            return myout;
        }

        public class Converter
        {
            public static int convertDoubleToInt(double sVal)
            {
                int myout = 0;
                try
                {
                    myout = Convert.ToInt32(sVal);
                }
                catch { }
                return myout;
            }

            public static DateTime convertStringToDateTime(string sVal)
            {
                DateTime dt = DateTime.Now;
                DateTime.TryParse(sVal, out dt);
                return dt;
            }

            public static int convertStringToInt(string sVal)
            {
                int myout = 0;
                int.TryParse(sVal, out myout);
                return myout;
            }

            public static DateTime myParseDate(string sVal)
            {
                DateTime dt = DateTime.Now;
                DateTime.TryParseExact(sVal, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                return dt;
            }
        }

        public class SystemInfLicense
        {
            public string Address { get; set; }
            public string ClientCode { get; set; }
            public string CompanyName { get; set; }
            public string ComputerSerial { get; set; }
            public string ContactNumber { get; set; }
            public string ContactPerson { get; set; }
            public string DeliveryReceipt { get; set; }
            public string EmailAddress { get; set; }
            public DateTime Expiration { get; set; }
            public int FreeorPaid { get; set; }
            public DateTime InstallationDate { get; set; }
            public int LicenseType { get; set; }
            public string minda { get; set; }
            public string ProcessorId { get; set; }
            public string ProductCode { get; set; }
            public string ProductKey { get; set; }
            public string PurchaseOrder { get; set; }
            public string resu { get; set; }
            public string SalesInvoice { get; set; }
            public string SerialNumber { get; set; }
            public DateTime SSADate { get; set; }
            public int SSAToggle { get; set; }
            public DateTime TimeBombDate { get; set; }
            public int TimeBombToggle { get; set; }
            public string tsuc { get; set; }
            public string UUID { get; set; }
        }
    }
}