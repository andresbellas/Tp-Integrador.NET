<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Cobrar.aspx.cs" Inherits="Proyecto_Integrador.Cobrar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5" style="max-width: 500px;">

        <h3 class="mb-4 text-center">
            Cobrar - Pedido N°: <asp:Label ID="lblNroPedido" runat="server" CssClass="fw-bold" />
        </h3>

        <div class="mb-3">
            <label for="ddlMedioPago" class="form-label">Método de Pago:</label>
            <asp:DropDownList ID="ddlMedioPago" runat="server" CssClass="form-control" />
        </div>

        <div class="mb-3">
            <label for="txtMontoTotal" class="form-label">Monto Total:</label>
            <asp:TextBox ID="txtMontoTotal" runat="server" CssClass="form-control" ReadOnly="true" />
        </div>

        <div class="mb-3">
            <label for="txtNombreCliente" class="form-label">Nombre Cliente:</label>
            <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="form-control" />
        </div>

        <div class="d-flex justify-content-between mt-4">
            <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn btn-secondary" OnClick="btnVolver_Click" />
            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-success" OnClick="btnAceptar_Click" />
        </div>

        <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger mt-3 fw-bold" Visible="false" />
    </div>

</asp:Content>