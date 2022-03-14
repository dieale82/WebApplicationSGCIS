<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditForm.ascx.cs" Inherits="EditForm" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <meta content="width=device-width, initial-scale=1.0" name="viewport">

    <title>Person manteinance</title>
    <meta content="" name="description">
    <meta content="" name="keywords">

    <!-- Favicons -->
    <link href="assets/img/favicon.png" rel="icon">
    <link href="assets/img/apple-touch-icon.png" rel="apple-touch-icon">

    <!-- Google Fonts -->
    <link href="https://fonts.gstatic.com" rel="preconnect">
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Nunito:300,300i,400,400i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i" rel="stylesheet">

    <!-- Vendor CSS Files -->
    <link href="assets/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">

    <!-- Template Main CSS File -->
    <link href="assets/css/style.css" rel="stylesheet">

    <script type="text/javascript">
        function OnlyNumbers(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
    </script>
</head>
<body>
    <main id="main" class="main">
        <section class="section">
            <div class="row">
                <div class="col-lg-6">

                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Person manteinance</h5>
                            <form>
                                <div class="row mb-3">
                                    <label for="inputText" class="col-sm-2 col-form-label">Name</label>
                                    <div class="col-sm-10">
                                        <asp:TextBox ID="txtId" CssClass="form-control" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
                                        </asp:TextBox>

                                        <asp:TextBox ID="txtName" CssClass="form-control" onkeyup="this.value=this.value.toUpperCase();" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>'>
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="row mb-3">
                                    <label for="inputText" class="col-sm-2 col-form-label">Age</label>
                                    <div class="col-sm-10">
                                        <asp:TextBox ID="txtAge" onkeypress="return OnlyNumbers(event)" CssClass="form-control" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Age") %>'>
                                        </asp:TextBox>                                        
                                    </div>
                                </div>
                                <div class="row mb-3">
                                    <label for="inputDropDownList" class="col-sm-2 col-form-label">Type</label>
                                    <div class="col-sm-10">
                                        <asp:DropDownList ID="cmdType" CssClass="form-select" runat="server" SelectedValue='<%# DataBinder.Eval(Container, "DataItem.PersonType") %>'>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row mb-3">
                                    <div class="col-sm-10">
                                        <asp:Button ID="btnUpdate" CssClass="btn btn-primary" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                            runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>
                                    </div>
                                </div>
                                <div class="row mb-3">
                                    <div class="col-sm-10">
                                        <asp:Button ID="Button2" Text="Cancel" runat="server" CssClass="btn btn-secondary" CausesValidation="False"
                                            CommandName="Cancel"></asp:Button>                                        
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </section>

    </main>

    <script src="assets/js/main.js"></script>


</body>
</html>
