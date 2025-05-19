using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

/// <summary>
/// Summary description for Converter
/// </summary>
public class Converter
{
    public static string ConvertDataTabletoJSON(DataTable dt)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }

    public static double ConvertStringToDouble(string sVal)
    {
        double myout = 0;
        double.TryParse(sVal, out myout);
        return myout;
    }

    public static DataTable ToDataTable<T>(List<T> items)
    {
        DataTable dataTable = new DataTable(typeof(T).Name);
        PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (PropertyInfo prop in Props)
        {
            dataTable.Columns.Add(prop.Name);
        }
        foreach (T item in items)
        {
            var values = new object[Props.Length];
            for (int i = 0; i < Props.Length; i++)
            {
                values[i] = Props[i].GetValue(item, null);
            }
            dataTable.Rows.Add(values);
        }
        return dataTable;
    }

    public static string AsciiToHex(string asciiString)
    {
        StringBuilder builder = new StringBuilder();
        foreach (char c in asciiString)
        {
            builder.Append(Convert.ToInt32(c).ToString("X"));
        }
        return builder.ToString();
    }
    public static string Base64FromByteArray(byte[] image)
    {
        return "" + Convert.ToBase64String(image);
    }

    public static byte[] ExtractResource(Image image)
    {
        MemoryStream ms = new MemoryStream();

        image.Save(ms, ImageFormat.Png);
        if (ms == null)
            return null;
        byte[] imageByteArray = ms.ToArray();
        return imageByteArray;
    }
}