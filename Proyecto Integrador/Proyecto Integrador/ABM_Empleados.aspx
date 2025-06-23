<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ABM_Empleados.aspx.cs" Inherits="Proyecto_Integrador.ABM_Empleados" MasterPageFile="~/Site1.Master" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2 class="mt-4 mb-3">Gestión de Empleados</h2>


<asp:GridView ID="gvEmpleados" runat="server" 
              AutoGenerateColumns="False"
              DataKeyNames="id_empleado"
              CssClass="table table-bordered"
              OnRowCommand="gvEmpleados_RowCommand">
    <Columns>
        <asp:TemplateField HeaderText="Nombre">
            <HeaderTemplate>
                <asp:TextBox ID="txtFiltroNombre" runat="server" CssClass="form-control"
                             AutoPostBack="true" OnTextChanged="Filtro_TextChanged" placeholder="Filtrar Nombre" />
            </HeaderTemplate>
            <ItemTemplate>
                <%# Eval("Nombre") %>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Apellido">
            <HeaderTemplate>
                <asp:TextBox ID="txtFiltroApellido" runat="server" CssClass="form-control"
                             AutoPostBack="true" OnTextChanged="Filtro_TextChanged" placeholder="Filtrar Apellido" />
            </HeaderTemplate>
            <ItemTemplate>
                <%# Eval("Apellido") %>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:BoundField DataField="Nro_Legajo" HeaderText="Legajo" />
        <asp:ButtonField Text="Seleccionar" CommandName="Seleccionar" ButtonType="Button" />
    </Columns>
</asp:GridView>

    <asp:Button ID="btnVolverGrilla" runat="server" CssClass="btn btn-secondary mt-3" Text="Volver" OnClick="btnVolverGrilla_Click" />

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

        <div class="form-check mb-3" id="divBaja" runat="server">
            <asp:CheckBox runat="server" ID="chkBaja" CssClass="form-check-input" />
            <label class="form-check-label" for="chkBaja">Empleado dado de baja</label>
        </div>

        <asp:Button runat="server" ID="btnGuardar" Text="Guardar" CssClass="btn btn-success mt-3" OnClick="btnGuardar_Click" />
        <asp:Button runat="server" ID="btnVolver" Text="Volver" CssClass="btn btn-secondary mt-3 ms-2" OnClick="btnVolver_Click" />
        <asp:Button runat="server" ID="btnAceptarConfirmacion" Text="Aceptar" CssClass="btn btn-primary mt-3 ms-2" OnClick="btnAceptarConfirmacion_Click" Visible="false" />

    </asp:Panel>
</asp:Content>
