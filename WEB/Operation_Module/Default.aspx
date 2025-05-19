<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Operation_Module_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <style>
                .page-header, .page-header-space {
                    height: 253px;
                }

                .page-footer, .page-footer-space {
                    height: 450px;
                }

                .page-footer {
                    position: fixed; bottom: 0; width: 100%;
                }

                .page-header {
                    position: fixed; top: 0mm; width: 100%;
                }

                .page-body {
                    max-height: 100px;
                }

                @page {
                    size: A4; margin: 10mm;
                }

                @media print {
                    table {
                        padding: 0px 10px 0px 0px;page-break-inside: auto;
                    }

                    thead {
                        display: table-header-group;
                    }

                    tfoot {
                        display: table-footer-group;
                    }

                    body {
                        margin: 0;
                    }
                }
            </style>
            <div class="page-header">
                <table style="font-size: 14px font-family:Arial" width="101%">
                    <tr>
                        <td colspan="2" align="left" width="100%">
                            <img src="../Image/AYALA Corp/AYALA Corp.png" width="20%" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center;">
                            <h2>ASSET RETURN FORM</h2>
                        </td>

                    </tr>
                    <tr>
                        <td align="left" width="50%"><b>ASSET RETURN REF. NO: </b>1</td>
                        <td align="left"><b>DATE:</b>1

                        </td>

                    </tr>
                    <tr>
                        <td align="left" width="50%">
                            <br />
                            <b>ASSIGNEE NAME: </b>1</td>
                        <td colspan="1" align="left"><b>USER DEPARTMENT: </b></td>

                    </tr>
                    <tr>
                        <td colspan="2" align="left">
                            <br />
                            <b>REASON OF RETURN: </b>1</td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left">
                            <br />
                            <br />
                            This is to acknowledge return of the following Company-issued Equipment or Property:</td>
                    </tr>
                </table>
            </div>
            <div class="page-footer">--------------------------------------<br />
                <br />
                <br />
                <br />
                <table  style="width: 100%;font-size:13px;" cellspacing="0">
                    <tr>
                        <td colspan="2" style="text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">&nbsp&nbsp<b>ASSESSMENT FINDINGS:</b></td>
                    </tr>
                    <tr>
                        <td width="50%" style="text-align: left; border-left:1px solid;border-bottom:1px solid;"></td>
                        <td style="text-align: left; border-bottom:1px solid;border-right:1px solid;"><b>ASSESSED/CHECKED BY:</b></td>
                    </tr>
                </table>
                <br />
                <table border: thick solid style="width: 101%;font-size:13px" cellspacing="0" style="width: 100%">
                    <tr>
                        <td colspan="2" style="text-align: left; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">&nbsp&nbsp<b>RECOMMENDATION:</b></td>
                    </tr>
                    <tr>
                        <td width="50%" style="text-align: left; border-left:1px solid;border-bottom:1px solid;"></td>
                        <td style="text-align: left; border-bottom:1px solid;border-right:1px solid;"></td>
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td>
                            <p align="left" style="font-size: 9.5px; font-family: Arial">
                                <br />
                                <b>RECEIVED FROM (NAME/SIGNATURE):</b></p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p align="left" style="font-size: 9.5px; font-family: Arial">
                                <br />
                                <b>DATE:</b></p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p align="left" style="font-size: 9.5px; font-family: Arial">
                                <br />
                                <b>RETURNED TO (NAME/SIGNATURE):</b></p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p align="left" style="font-size: 9.5px; font-family: Arial">
                                <br />
                                <b>DATE:</b></p>
                        </td>
                    </tr>
                </table>
            </div>
            <table style="width: 100%">
                <thead>
                    <tr>
                        <td>
                            <div class="page-header-space"></div>
                        </td>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <div class="page-body" style="line-height: 1">
                                <tr>
                                    <td style="text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;"><b>ASSET NUMBER</b></td>
                                    <td style="text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;"><b>DESCRIPTION</b></td>
                                    <td style="text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;"><b>SERIAL / IMEI</b></td>
                                    <td style="text-align: center; border-top:1px solid;border-bottom:1px solid;border-left:1px solid;border-right:1px solid;"><b>CONDITION</b></td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;">1</td>
                                    <td style="text-align: center; border-bottom:1px solid;border-left:1px solid;border-right:1px solid;">1</td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center;"><i><b>***Nothing Follows***</b></i></td>
                                </tr>
                            </div>
                        </td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr>
                        <td>
                            <div class="page footer page-footer-space"></div>
                        </td>
                    </tr>
                </tfoot>
            </table>
        </div>
    </form>
</body>
</html>
