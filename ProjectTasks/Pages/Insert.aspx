<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Insert.aspx.cs" Inherits="ProjectTasks.Pages.Insert" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
<link rel="stylesheet" href="//code.jquery.com/ui/1.11.0/themes/smoothness/jquery-ui.css" />
<link rel="stylesheet" href="~/Content/bootstrap.css" />
<script src="//code.jquery.com/jquery-1.10.2.js"></script>
<script src="//code.jquery.com/ui/1.11.0/jquery-ui.js"></script>
<script>
           $(function () {
               $("#StartDate").datepicker({ dateFormat: 'dd.mm.yy' });
           });
</script>
    <script>
        $(function () {
            $("#EndDate").datepicker({ dateFormat: 'dd.mm.yy' });
        });
</script>
</head>
<body>
    <div style="caption-side"><b>Inserting task</b></div>
    <form id="post" runat="server">
        <div>
            <label>Enter name:</label><asp:TextBox id="name" runat="server"/>
           <asp:RequiredFieldValidator ControlToValidate="name"  style="color:red"  ErrorMessage="*Name is required." ID="rfvName"
   RunAt="Server" />
            </div>
        <div>
            <label>Enter Workload:</label><asp:TextBox id="workload" runat="server" />
            <asp:RequiredFieldValidator ControlToValidate="workload"  style="color:red"  ErrorMessage="*Workload is required." ID="RequiredFieldValidator1"
   RunAt="Server" />
        </div>
        <div>
            <label>StartDate:</label><asp:TextBox ID="StartDate" runat="server" ></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="StartDate"  style="color:red"  ErrorMessage="*StartDate is required." ID="RequiredFieldValidator2"
   RunAt="Server" />
        </div>
        <div>
            <label>EndDate:</label><asp:TextBox ID="EndDate" runat="server" ></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="EndDate"  style="color:red"  ErrorMessage="*EndDate is required." ID="RequiredFieldValidator3"
   RunAt="Server" />
        </div>
        <div>
           <%-- 
            <select id="Status">
                <option value="">Choose</option>
                <option value="Done">Done</option>
                <option value="In process">In process</option>
                <option value="Doesn't started">Doesn't started</option>
                <option value="Kept">Kept</option>
            </select>--%>
            <label>Status</label>
            <asp:DropDownList runat="server" ID="Status" AppendDataBoundItems="true">
                <asp:ListItem Text="Please select an option" Value="" />
                <asp:ListItem Text="Done" Value="Done" />
                <asp:ListItem Text="In process" Value="In process" />
                <asp:ListItem Text="Doesn't started" Value="Doesn't started" />
                <asp:ListItem Text="Kept" Value="Kept" />
            </asp:DropDownList>
            <asp:RequiredFieldValidator ControlToValidate="Status"  style="color:red"  ErrorMessage="*Status is required." ID="RequiredFieldValidator4" RunAt="Server" />
            </div>
         <div>
            <label>Executor</label>
            <select name="ExecutorID">
                <option value="">Choose</option>
                 <%
                     foreach (ProjectTasks.Executor ex in GetExecutors())
                     {
                         Response.Write(String.Format(@"
                            <option value='{1}'>{0}</option>"
                         , ex.Surname, ex.Id));
                     }
            %>
            </select>
             <%
                    Response.Write(String.Format(@" <p style='color:red'>{0}</p>"
                         , error));
            %>
           <%--  <asp:DropDownList runat="server" ID="DropDownList1" DataSource="Get" AppendDataBoundItems="true">
                <asp:ListItem Text="Please select an option" Value="" />
            </asp:DropDownList>--%>
        </div>
        <div>
          <%--  <button name='insert' type="submit">Insert</button>--%>
              <asp:Button ID="insert" runat="server" Text="Insert"  
               OnClick="insertBtn_Click" Width="95px" /> 
            &nbsp;<a runat="server" href="~/Pages/MainPage.aspx">Go Back</a>
        </div>
    </form>
</body>
</html>
