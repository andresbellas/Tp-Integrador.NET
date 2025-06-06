<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" Inherits="Proyecto_Integrador.Gerente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2>Gestión de Mesas</h2>
        
        <asp:GridView ID="gvMesas" runat="server" AutoGenerateColumns="false" CssClass="table table-striped" DataKeyNames="MesaId" AllowPaging="true" PageSize="10">
            <Columns>
                <asp:BoundField DataField="MesaId" HeaderText="ID Mesa" />
                <asp:BoundField DataField="NumeroMesa" HeaderText="Número" />
                <asp:BoundField DataField="Estado" HeaderText="Estado" />
                <asp:BoundField DataField="NumeroLegajo" HeaderText="Legajo Mesero" />
                <asp:CommandField ShowSelectButton="True" SelectText="Seleccionar" />
            </Columns>
        </asp:GridView>

        <div class="mt-3">
            <asp:Button ID="btnAsignarMesero" runat="server" Text="Asignar Mesero" CssClass="btn btn-primary me-2" Enabled="false" />
            <asp:Button ID="btnDesasignarMesero" runat="server" Text="Desasignar Mesero" CssClass="btn btn-secondary me-2" Enabled="false" />
            <asp:Button ID="btnAbrirPedido" runat="server" Text="Abrir Pedido" CssClass="btn btn-success me-2" Enabled="false" />
            <asp:Button ID="btnCancelarPedido" runat="server" Text="Cancelar Pedido" CssClass="btn btn-danger me-2" Enabled="false" />
            <asp:Button ID="btnCerrarPedido" runat="server" Text="Cerrar Pedido" CssClass="btn btn-warning" Enabled="false" />
            <asp:Button ID="btnGestionMeseros" runat="server" Text="Gestionar Meseros" CssClass="btn btn-warning" Enabled="false" />
        </div>
    </div>
</asp:Content>