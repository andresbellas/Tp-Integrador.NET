<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ABM_Empleados.aspx.cs" Inherits="Proyecto_Integrador.ABM_Empleados" MasterPageFile="~/Site1.Master" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2 class="mt-4 mb-3">Gestión de Empleados</h2>

    <asp:Panel ID="PanelBuscarEmpleado" runat="server" CssClass="mb-3" Visible="false">
        <div class="input-group">
            <asp:TextBox runat="server" ID="txtBuscarNombre" CssClass="form-control" placeholder="Nombre del empleado a buscar"></asp:TextBox>
            <asp:Button runat="server" ID="btnBuscarEmpleado" Text="Buscar" CssClass="btn btn-secondary" OnClick="btnBuscarEmpleado_Click" />
        </div>
    </asp:Panel>


    <asp:Label runat="server" ID="lblMensaje" CssClass="text-danger fw-bold d-block mb-3"></asp:Label>


    <asp:Panel ID="PanelFormulario" runat="server" CssClass="border p-4 rounded shadow-sm bg-light" Visible="false">

        <h4 class="mb-4">
            <asp:Label runat="server" ID="lblTituloFormulario" Text=""></asp:Label>
        </h4>

        <asp:TextBox runat="server" ID="txtId" CssClass="form-control" Visible="false" />

        <div class="mb-3" id="divNombre" runat="server">
            <label for="txtNombre" class="form-label">Nombre</label>
            <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" />
        </div>

        <div class="mb-3" id="divApellido" runat="server">
            <label for="txtApellido" class="form-label">Apellido</label>
            <asp:TextBox runat="server" ID="txtApellido" CssClass="form-control" />
        </div>

        <div class="mb-3" id="divLegajo" runat="server">
            <label for="txtLegajo" class="form-label">Nro. Legajo</label>
            <asp:TextBox runat="server" ID="txtLegajo" CssClass="form-control" TextMode="Number" />
        </div>

        <div class="mb-3" id="divRol" runat="server">
            <label for="ddlRoles" class="form-label">Rol</label>
            <asp:DropDownList runat="server" ID="ddlRoles" CssClass="form-select" />
        </div>

        <div class="mb-3" id="divUsuario" runat="server">
            <label for="txtUsuario" class="form-label">Usuario</label>
            <asp:TextBox runat="server" ID="txtUsuario" CssClass="form-control" />
        </div>



        <div class="mb-3" id="divContraseña" runat="server">
            <label for="txtContraseña" class="form-label">Contraseña</label>
            <asp:TextBox runat="server" ID="txtContraseña" CssClass="form-control" TextMode="Password" />
        </div>

        <asp:Button runat="server" ID="btnGuardar" Text="Guardar" CssClass="btn btn-success mt-3" OnClick="btnGuardar_Click" />
        <asp:Button runat="server" ID="btnVolver" Text="Volver" CssClass="btn btn-secondary mt-3 ms-2" OnClick="btnVolver_Click" />
        <asp:Button runat="server" ID="btnAceptarConfirmacion" Text="Aceptar" CssClass="btn btn-primary mt-3 ms-2" OnClick="btnAceptarConfirmacion_Click" Visible="false" />

    </asp:Panel>
</asp:Content>
