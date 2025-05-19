<%@ Page Language="C#" AutoEventWireup="true" CodeFile="500.aspx.cs" Inherits="_500" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>500 Internal Error
            </h1>
            <ol class="breadcrumb">
                <li class="active">500 error</li>
            </ol>
        </section>

        <!-- Main content -->
        <section class="content">
            <div class="error-page">
                <h2 class="headline text-yellow">500</h2>

                <div class="error-content">
                    <h3><i class="fa fa-warning text-yellow"></i>Internal Server Error.</h3>

                    <p>
                        Internal Server Error. please contact system administrator.
            Meanwhile, you may <a href="../Home/Dashboard.aspx">return to dashboard</a>.
                    </p>

                    <!--<form class="search-form">
            <div class="input-group">
              <input type="text" name="search" class="form-control" placeholder="Search">

              <div class="input-group-btn">
                <button type="submit" name="submit" class="btn btn-warning btn-flat"><i class="fa fa-search"></i>
                </button>
              </div>
            </div>-->
                    <!-- /.input-group -->
                </div>
                <!-- /.error-content -->
            </div>
            <!-- /.error-page -->
        </section>
        <!-- /.content -->
    </div>
    <!-- /.content-wrapper -->
</body>
</html>
