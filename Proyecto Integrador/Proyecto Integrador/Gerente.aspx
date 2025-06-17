<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" Inherits="Proyecto_Integrador.Gerente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2>Gestión de Mesas</h2>
        
        <asp:GridView ID="gvMesas" runat="server" AutoGenerateColumns="false" CssClass="table table-striped" DataKeyNames="Id_mesa" AllowPaging="true" PageSize="10">
            <Columns>
                  <asp:BoundField DataField="Id_mesa" HeaderText="Id Mesa" />
                  <asp:BoundField DataField="Nro_Mesa" HeaderText="Número Mesa" />
                   <asp:BoundField DataField="Nro_Legajo" HeaderText="Número Legajo" />
                   <asp:BoundField DataField="Id_Estado" HeaderText="Estado" />
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