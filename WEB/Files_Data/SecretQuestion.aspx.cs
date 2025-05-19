using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using AdvanceEntity;
using AdvanceBll;
using System.Web.Script.Serialization;

public partial class Files_Data_SecretQuestion : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckAccess();
        //BrandList.InnerHtml = GetBrand("",1);
    }
    public static string ViewAccess = "";
    public static string AddAccess = "";
    public static string EditAccess = "";
    public static string DeleteAccess = "";
    public void CheckAccess()
    {
        RoleBll rb = new RoleBll();

        string AccessButon = rb.CheckR("Files_Data/SecretQuestion.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
        string[] ButtonAccess = AccessButon.Split('|');
        ViewAccess = ButtonAccess[0];
        AddAccess = ButtonAccess[1];
        EditAccess = ButtonAccess[2];
        DeleteAccess = ButtonAccess[3];
    }

    [WebMethod]
    public static string SaveSecretQuestion(int id, string question, bool active)
    {
        SecretQuestion SecretQuestionEntity = new SecretQuestion();
        SecretQuestionBll secretQuestionBll = new SecretQuestionBll();

        SecretQuestionEntity.QuestionID = id;
        SecretQuestionEntity.Active = active;
        SecretQuestionEntity.Question = question;
        SecretQuestionEntity.CreatedBy = Common.UserName;
        SecretQuestionEntity.ConnectionString = Common.ConnString;

        return secretQuestionBll.SaveSecretQuestion(SecretQuestionEntity).ToString();
        
    }

    [WebMethod]
    public static string SecretQuestionByID(int id)
    {
        SecretQuestion secretQuestion = new SecretQuestion();
        SecretQuestionBll secretQuestionBll = new SecretQuestionBll();

        secretQuestion.QuestionID = id;
        secretQuestion.ConnectionString = Common.ConnString;

        return secretQuestionBll.SecretQuestionByID(secretQuestion);
    }


    [WebMethod]
    public static string GetSecretQuestion()
    {
        SecretQuestionBll SecretQbll = new SecretQuestionBll();
        string html = string.Empty;
        List<SecretQuestion> JsonItemList = new List<SecretQuestion>();
        try
        {
            DataTable dt = SecretQbll.GetSecretQuestion(Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SecretQuestion x = new SecretQuestion();
                    x.Question = dt.Rows[i]["Question"].ToString();
                   
                    x.IsActive = "<img "+ ((bool.Parse(dt.Rows[i]["Active"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) +"/>";
                    if (DeleteAccess == "0")
                    {
                        x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Security Question' onclick=\"GetSecreQuestionID('" + dt.Rows[i]["QuestionID"] + "')\"><span class='fa fa-edit'></span></button>  </center>";

                    }
                    else
                    {
                        x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Security Question' onclick=\"GetSecreQuestionID('" + dt.Rows[i]["QuestionID"] + "')\"><span class='fa fa-edit'></span></button> "
                          + " <button type='button' class='btn btn-xs btn-danger btn_Delete btn_DeleteSecurityQuestion' data-toggle='tooltip' title='Delete Security Question' onclick=\"DeleteSecretQuestion('" + dt.Rows[i]["QuestionID"] + "')\"><span class='fa fa-trash'></span></button> </center>";

                    }

                    JsonItemList.Add(x);

                }

                JavaScriptSerializer json = new JavaScriptSerializer();
                json.MaxJsonLength = Int32.MaxValue;
                html = json.Serialize(JsonItemList);


                return html;
            }

            else { return ""; }

        }
        catch (Exception ex)
        {
            return "";
        }
        
    }

    [WebMethod]
    public static string DeleteSecretQuestion(int id)
    {
        SecretQuestion SecretQuestionEntity = new SecretQuestion();
        SecretQuestionBll SecretQuestionBll = new SecretQuestionBll();

        SecretQuestionEntity.QuestionID = id;
        SecretQuestionEntity.CreatedBy = Common.UserName;
        SecretQuestionEntity.ConnectionString = Common.ConnString;

        return SecretQuestionBll.DeleteSecretQuestion(SecretQuestionEntity).ToString();

        
    }
}