<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ABM_Insumos.aspx.cs" Inherits="Proyecto_Integrador.ABM_Insumos" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Gestión de Insumos</h2>

    <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger fw-bold d-block mb-3" />

    <asp:Panel ID="PanelFormulario" runat="server" CssClass="border p-4 rounded bg-light shadow-sm" Visible="false">
        <asp:TextBox ID="txtIdInsumo" runat="server" Visible="false" />

        <div class="mb-3">
            <label for="txtSku" class="form-label">SKU</label>
            <asp:TextBox ID="txtSku" runat="server" CssClass="form-control" />
        </div>

        <div class="mb-3">
            <label for="txtNombre" class="form-label">Nombre</label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
        </div>

        <div class="mb-3">
            <label for="txtPrecio" class="form-label">Precio</label>
            <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" />
        </div>

        <div class="mb-3">
            <label for="txtCantidad" class="form-label">Cantidad</label>
            <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" />
        </div>

        <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-success" Text="Guardar" OnClick="btnGuardar_Click" />
        <asp:Button ID="btnVolver" runat="server" CssClass="btn btn-secondary ms-2" Text="Volver" OnClick="btnVolver_Click" />
    </asp:Panel>

    <asp:GridView ID="gvInsumos" runat="server"
              AutoGenerateColumns="False"
              DataKeyNames="Id_insumo"
              CssClass="table table-bordered"
              OnRowCommand="gvInsumos_RowCommand">
    <Columns>
        <asp:BoundField DataField="Sku" HeaderText="SKU" />
        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
        <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" />
        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
        
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Button ID="btnVerDetalle" runat="server"
                            Text='<%# ModoABM == "listarInsumos" ? "Ver Detalle" : "Seleccionar" %>'
                            CommandName="Seleccionar"
                            CommandArgument='<%# Container.DataItemIndex %>'
                            CssClass="btn btn-info btn-sm" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
    <asp:Button ID="btnVolverGrilla" runat="server" CssClass="btn btn-secondary mt-3" Text="Volver" OnClick="btnVolverGrilla_Click" />
</asp:Content>
