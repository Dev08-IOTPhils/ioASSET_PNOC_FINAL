using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using AdvanceBll;

/// <summary>
/// Summary description for PasswordPolicy
/// </summary>
public class PasswordPolicy
{

    private static int Minimum_Length = 12;
    private static int Upper_Case_length = 1;
    private static int Lower_Case_length = 1;
    private static int NonAlpha_length = 1;
    private static int Numeric_length = 1;

    public static string GetPasswordPolicy()
    {
        MiscBll mb = new MiscBll();
        return mb.GetUserAcccountSettings(Common.ConnString);

    }

    public static string IsValidBa(string Password)
    {
        string returnVal = "";
        string[] pp = GetPasswordPolicy().Split('|');
        Minimum_Length = Convert.ToInt32(pp[3]);
        if (pp[4] == "True")
        {
            Upper_Case_length = Convert.ToInt32(pp[5]);
        }
        else
        {
            Upper_Case_length = 0;
        }

        if (pp[6] == "True")
        {
            Lower_Case_length = Convert.ToInt32(pp[7]);
        }
        else
        {
            Lower_Case_length = 0;
        }

        if (pp[8] == "True")
        {
            Numeric_length = Convert.ToInt32(pp[9]);
        }
        else
        {
            Numeric_length = 0;
        }

        if (pp[10] == "True")
        {
            NonAlpha_length = Convert.ToInt32(pp[11]);
        }
        else
        {
            NonAlpha_length = 0;
        }
        if (Password.Length < Minimum_Length)
            returnVal += "\n-Minimum Length must be " + pp[3];
        if (UpperCaseCount(Password) < Upper_Case_length)
            returnVal += "\n-Upper Case Length must be " + pp[5];
        if (LowerCaseCount(Password) < Lower_Case_length)
            returnVal += "\n-Lower Case Length must be " + pp[7];
        if (NumericCount(Password) < Numeric_length)
            returnVal += "\n-Numeric Length must be " + pp[9];
        if (NonAlphaCount(Password) < NonAlpha_length)
            returnVal += "\n-Symbol Length must be " + pp[11];
        return returnVal;
    }

    public static bool IsValid(string Password)
    {

        if (Password.Length < Minimum_Length)
            return false;
        if (UpperCaseCount(Password) < Upper_Case_length)
            return false;
        if (LowerCaseCount(Password) < Lower_Case_length)
            return false;
        if (NumericCount(Password) < Numeric_length)
            return false;
        if (NonAlphaCount(Password) < NonAlpha_length)
            return false;
        return true;
    }

    private static int UpperCaseCount(string Password)
    {
        return Regex.Matches(Password, "[A-Z]").Count;
    }

    private static int LowerCaseCount(string Password)
    {
        return Regex.Matches(Password, "[a-z]").Count;
    }
    private static int NumericCount(string Password)
    {
        return Regex.Matches(Password, "[0-9]").Count;
    }
    private static int NonAlphaCount(string Password)
    {
        return Regex.Matches(Password, @"[^0-9a-zA-Z\._]").Count;
    }
}