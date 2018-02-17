<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="ProjectTasks.Pages.MainPage" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link rel="stylesheet" href="~/Content/bootstrap.css" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style2 {
            width: 86px;
        }
        .auto-style4 {
            width: 98px;
        }
        .auto-style5 {
            width: 121px;
        }
        .auto-style6 {
            width: 109px;
        }
        .auto-style7 {
            width: 110px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
      <div style="caption-side"><b>Table of Tasks</b></div>
            <div>
            <table id='table'>
            <tr>
                        <th class="auto-style2">Name</th>
                        <th class="auto-style5">Workflow</th>
                        <th class="auto-style6">StartDate</th>
                        <th class="auto-style4">EndDate</th>
                        <th class="auto-style7">Executor</th>
                        <th>Status</th>
            </tr>
            </table>
                </div>
        <%
            
            foreach (ProjectTasks.Task task in GetTasks())
            {
                Response.Write(String.Format(@"
            <div>
            <tr>
                <td class='auto-style2'> {0}</td>
                <td class='auto-style1'> {1}</td>
                <td class='auto-style3'>{2}</td>
                <td class='auto-style4'> {3} </td>
                <td class='auto-style5'> {4} </td>
                <td>  {5}</td>
            <button name='delete' type='submit' value='{6}'>
                               Delete
                            </button>
            <button name='edit' type='submit' value='{6}'>
                              Edit
                            </button>
            </tr>
            </div>",
                    task.Name, task.Workload, ((DateTime)task.StartDate).ToString("dd.MM.yyyy"), ((DateTime)task.EndDate).ToString("dd.MM.yyyy"), task.Executor.Name, task.Status, task.Id));
            }
            %>
        
            <a runat="server" href="~/Pages/Insert.aspx">Insert</a>
    </form>
</body>
</html>
