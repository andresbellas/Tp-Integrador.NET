<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ABM_Roles.aspx.cs" Inherits="Proyecto_Integrador.ABM_Roles" %>
<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <h2>Gestión de Roles</h2>

        <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger fw-bold d-block mb-3" />

        <asp:Panel ID="PanelFormulario" runat="server" CssClass="border p-4 rounded bg-light shadow-sm" Visible="false">

            <asp:TextBox ID="txtIdRol" runat="server" CssClass="form-control" Visible="false" />

            <div class="mb-3">
                <label for="txtNombreRol" class="form-label">Nombre del Rol</label>
                <asp:TextBox ID="txtNombreRol" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label for="txtDescripcion" class="form-label">Descripción</label>
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
            </div>

            <asp:CheckBox ID="chkBajaRol" runat="server" CssClass="form-check-input" Text=" ¿Dar de baja?" />

            <div class="mt-3">
                <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-success" Text="Guardar" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnVolver" runat="server" CssClass="btn btn-secondary ms-2" Text="Volver" OnClick="btnVolver_Click" />
            </div>
        </asp:Panel>

        <asp:GridView ID="gvRoles" runat="server" CssClass="table table-bordered mt-4"
                      AutoGenerateColumns="False"
                      DataKeyNames="id_rol"
                      OnRowCommand="gvRoles_RowCommand"
                      Visible="false">
            <Columns>
                <asp:BoundField DataField="Nombre_rol" HeaderText="Nombre" />
                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                <asp:ButtonField ButtonType="Button" Text="Seleccionar" CommandName="Seleccionar" />
            </Columns>
        </asp:GridView>

    <asp:Button ID="btnVolverGrilla" runat="server" CssClass="btn btn-secondary mt-3" Text="Volver" OnClick="btnVolverGrilla_Click" />

</asp:Content>
