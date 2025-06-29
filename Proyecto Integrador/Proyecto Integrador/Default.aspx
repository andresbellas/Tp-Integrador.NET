<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Proyecto_Integrador.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <script type="text/javascript">
        window.addEventListener('pageshow', function (event) {
            
            if (event.persisted) {
                __doPostBack('<%= Page.UniqueID %>', 'FromCache');
            }
        });
     </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container d-flex justify-content-center align-items-center" style="min-height: 60vh;">
        
        <div class="w-100" style="max-width: 400px;">
            <h2 class="text-center mb-4">Login</h2>
            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>

            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control mb-3" Placeholder="Usuario" />
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control mb-3" Placeholder="Contraseña" />

            <asp:Button ID="btnLogin" runat="server" Text="Ingresar" CssClass="btn btn-primary w-100" OnClick="btnLogin_Click" />
            <asp:Label ID="lblError" runat="server" CssClass="text-danger" />
        </div>
    </div>
</asp:Content>