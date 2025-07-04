<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Pedido.aspx.cs" Inherits="Proyecto_Integrador.Pedido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="bg-light border rounded p-3 mb-3 shadow-sm">
        <h4>Mesa N°:
            <asp:Label ID="lblNroMesa" runat="server" CssClass="fw-bold" />
            &nbsp;|&nbsp;
            Mesero:
            <asp:Label ID="lblNombreMesero" runat="server" CssClass="fw-bold" />
            &nbsp;|&nbsp;
            Fecha:
            <asp:Label ID="lblFecha" runat="server" CssClass="fw-bold" />
        </h4>
    </div>

    <asp:GridView ID="gvItemsPedido" runat="server" AutoGenerateColumns="false"
        OnRowEditing="gvItemsPedido_RowEdit"
        OnRowUpdating="gvItemsPedido_RowUpdate"
        OnRowCancelingEdit="gvItemsPedido_RowCancel"
        OnRowDeleting="gvItemsPedido_RowDelete"
        CssClass="table table-striped"
        DataKeyNames="Id_item">
        <Columns>
            <asp:BoundField DataField="Sku" HeaderText="SKU" ReadOnly="true" />
            <asp:BoundField DataField="NombreInsumo" HeaderText="Nombre del Insumo" ReadOnly="true" />
            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
            <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C}" ReadOnly="true" />
            <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />
        </Columns>
    </asp:GridView>

    <div class="d-flex align-items-center gap-2 mb-4">
        <asp:DropDownList
            ID="ddlInsumos"
            runat="server"
            CssClass="form-control"
            Width="300px" />

        <asp:TextBox
            ID="txtCantidad"
            runat="server"
            CssClass="form-control"
            Width="100px"
            placeholder="Cantidad" />

        <asp:Button
            ID="btnAgregarInsumo"
            runat="server"
            Text="Agregar al Pedido"
            CssClass="btn btn-success"
            OnClick="btnAgregarInsumo_Click" />
    </div>

    <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger fw-bold" Visible="false" />

   <div class="mb-3 d-flex gap-2">
    <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn btn-secondary" OnClick="btnVolver_Click" />
    <asp:Button ID="btnCancelarPedido" runat="server" Text="Cancelar Pedido" CssClass="btn btn-danger" OnClick="btnCancelarPedido_Click" Visible="false" />
    <asp:Button ID="btnFinalizarPedido" runat="server" Text="Finalizar Pedido" CssClass="btn btn-success" OnClick="btnFinalizarPedido_Click" Visible="false" />
</div>

</asp:Content>