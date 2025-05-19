<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Util" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        //      // Code that runs on application startup    
        RequestValidator.Current = new MyRequestValidator();
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown
        Common.LogActivity(Session["UserLogin"].ToString(), "Session Time out",Session["ConnString"].ToString(), Session["IPAddress"].ToString());

        Common.RemoveToActiveUsers();
        Common.IsUserAuthenticated = false;

        Common.TempUserID = null;

        System.Web.HttpContext.Current.Session.Clear();
        System.Web.HttpContext.Current.Session.RemoveAll();
        System.Web.HttpContext.Current.Session.Abandon();
    }
     protected void Application_EndRequest(Object sender, EventArgs e) {
        // Iterate through any cookies found in the Response object.
        foreach (string cookieName in Response.Cookies.AllKeys) {
            if(Response.Cookies[cookieName].Secure != true)
            {
                Response.Cookies[cookieName].Secure = true;
            }
        }
    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs
        //Exception ex = Server.GetLastError();

        //if (ex is HttpRequestValidationException)
        //{
        //    Response.Clear();
        //    Response.StatusCode = 200;
        //    Response.Write(@"[html]");
        //    Response.End();
        //}
       // HttpContext.Current.Response.Redirect("~/InternalServerError.aspx");
    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
        Common.LogActivity(Session["UserLogin"].ToString(), "Session Time out",Session["ConnString"].ToString(), Session["IPAddress"].ToString());

        Common.RemoveToActiveUsers();
        Common.IsUserAuthenticated = false;

        Common.TempUserID = null;

        System.Web.HttpContext.Current.Session.Clear();
        System.Web.HttpContext.Current.Session.RemoveAll();
        System.Web.HttpContext.Current.Session.Abandon();
    }

    public class MyRequestValidator : RequestValidator
    {
        protected override bool IsValidRequestString(HttpContext context, string value, RequestValidationSource requestValidationSource, string collectionKey, out int validationFailureIndex)
        {
            bool result = base.IsValidRequestString(context, value, requestValidationSource, collectionKey, out validationFailureIndex);

            if (!result)
            {
                // Write your validation here
                if (requestValidationSource == RequestValidationSource.Form ||
                    requestValidationSource == RequestValidationSource.QueryString)

                    return true; // Suppress error message
            }
            return result;
        }
    }

</script>
