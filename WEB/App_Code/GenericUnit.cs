using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GenericUnit
/// </summary>
public abstract class GenericUnit : CommonRecords
{
    public GenericUnit.DBExecutionType DBExecution { get; set; }

    public enum DBExecutionType
    {
        Insert = 0,
        Update = 1,
        Delete = 2,
    }

}