<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" Inherits="Proyecto_Integrador.Gerente" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- NAVBAR -->
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark rounded-0 shadow-sm">
        <div class="container-fluid">
            <a class="navbar-brand" href="#" style="font-size: 1.5rem;">Menú Administración</a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav"
                aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav">
                    <li class="nav-item">
                        <asp:LinkButton
                            runat="server"
                            ID="btnGestionGeneral"
                            CssClass="nav-link active"
                            CausesValidation="false"
                            OnClick="btnGestionGeneral_Click"
                            style="font-size: 1.2rem;">
                            Gestión General</asp:LinkButton>
                    </li>
                    <li class="nav-item">
                        <asp:LinkButton
                            runat="server"
                            ID="btnHistorial"
                            CssClass="nav-link"
                            CausesValidation="false"
                            OnClick="btnHistorial_Click"
                            style="font-size: 1.2rem;">
                            Historial de Pagos</asp:LinkButton>
                    </li>
                    <li class="nav-item">
                        <asp:LinkButton
                            runat="server"
                            ID="btnHistorialPedidos"
                            CssClass="nav-link"
                            CausesValidation="false"
                            OnClick="btnHistorialPedidos_Click"
                            style="font-size: 1.2rem;">
                            Historial de Pedidos</asp:LinkButton>
                    </li>
                </ul>
            </div>
        </div>
    </nav>

    <div class="container mt-4">

        <h2 id="tituloGestion" class="mb-4" runat="server">Gestión General Administración</h2>

        <!-- MESAS -->
        <div class="p-3 border border-dark rounded shadow-sm bg-white" id="divMesas" runat="server" visible="true">
            <div style="max-height: 500px; overflow-y: auto;">
                <asp:GridView
                    ID="gvMesas"
                    runat="server"
                    AutoGenerateColumns="false"
                    CssClass="table table-striped table-bordered"
                    DataKeyNames="Id_mesa"
                    AllowPaging="false"
                     OnRowCommand="gvMesas_RowCommand">
                    <columns>
                        <asp:BoundField DataField="Id_mesa" HeaderText="Id Mesa" />
                        <asp:BoundField DataField="Nro_Mesa" HeaderText="Número Mesa" />
                        <asp:TemplateField HeaderText="Número Legajo">
                            <itemtemplate>
                                <asp:Button
                                    ID="btnLegajo"
                                    runat="server"
                                    Text='<%# (Eval("Nro_Legajo") == DBNull.Value || Convert.ToInt32(Eval("Nro_Legajo")) == 0) ? "No asignado" : Eval("Nro_Legajo").ToString() %>'
                                    CssClass="btn btn-primary btn-sm"
                                    CommandName="legajoClick"
                                    CommandArgument='<%# Eval("Nro_Legajo") %>'
                                    CausesValidation="false"
                                    OnClick="Informacion_Click" />
                            </itemtemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado">
                            <itemtemplate>
                                <%# Eval("NombreEstado") %>
                            </itemtemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Acción">
                            <itemtemplate>
                                <asp:Button
                                    ID="btnSeleccionar"
                                    runat="server"
                                    Text='<%# Eval("Id_Estado").ToString() == "1" ? "Abrir Pedido" : "Gestionar Pedido" %>'
                                    CssClass='<%# Eval("Id_Estado").ToString() == "1" ? "btn btn-success btn-sm" : "btn btn-danger btn-sm" %>'
                                    CommandName="accionMesa"
                                    CommandArgument='<%# Eval("Id_mesa") %>'
                                    CausesValidation="false" />
                            </itemtemplate>
                        </asp:TemplateField>
                    </columns>
                </asp:GridView>
            </div>
        </div>


        <!-- HISTORIAL DE PAGOS -->
        <div id="divHistorial" runat="server" visible="false">
            <h2 class="mb-4">Historial de Cobranzas</h2>
            <div class="p-3 border border-dark rounded shadow-sm bg-white">
                <div style="max-height: 500px; overflow-y: auto;">
                    <asp:GridView
                        ID="gvCobranzas"
                        runat="server"
                        AutoGenerateColumns="false"
                        CssClass="table table-striped table-bordered">
                        <columns>
                            <asp:BoundField DataField="id_cobranza" HeaderText="ID Cobranza" />
                            <asp:BoundField DataField="Id_Pedido" HeaderText="ID Pedido" />
                            <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C}" />
                            <asp:BoundField DataField="MedioDePago.Nombre_Pago" HeaderText="Medio de Pago" />
                        </columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="mt-3">
                <asp:Label ID="lblFiltro" runat="server" Text="Filtrar por Medio de Pago:" />
                <asp:TextBox ID="txtFiltroMedioPago" runat="server" CssClass="form-control" style="max-width: 300px; display: inline-block; margin-left: 10px;" />
            </div>
        </div>

        <!-- HISTORIAL DE PEDIDOS -->
        <div id="divHistorialPedidos" runat="server" visible="false">
            <h2 class="mb-4">Historial de Pedidos</h2>
            <div class="p-3 border border-dark rounded shadow-sm bg-white">
                <div style="max-height: 500px; overflow-y: auto;">
               <asp:GridView 
    ID="gvPedidos" 
    runat="server" 
    AutoGenerateColumns="False" 
    CssClass="table table-striped table-bordered"
    DataKeyNames="Id_Pedido"
    AllowPaging="false"
    >
    <Columns>
        <asp:BoundField DataField="Id_Pedido" HeaderText="ID Pedido" />
       <asp:BoundField DataField="Fecha_Pedido" HeaderText="Fecha Pedido" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
        <asp:BoundField DataField="Nro_Pedido" HeaderText="Pedido Número" />
        <asp:TemplateField HeaderText="Estado">
            <ItemTemplate>
                <%# Convert.ToInt32(Eval("Id_Estado")) == 3 ? "Activo" : (Convert.ToInt32(Eval("Id_Estado")) == 4 ? "Cerrado" : "Desconocido") %>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
                </div>
            </div>
        </div>

        <!-- BOTONES -->
        <div id="divBotones" runat="server" class="bg-primary text-white p-3 rounded mt-3 d-flex flex-wrap gap-3 justify-content-start shadow-sm">
            <div class="dropdown">
                <button class="btn btn-light dropdown-toggle" type="button" data-bs-toggle="dropdown">Empleados</button>
                <ul class="dropdown-menu">
                    <asp:LinkButton runat="server" ID="btnAltaEmpleado" CssClass="dropdown-item" CommandArgument="altaEmpl" OnClick="Accion_Click">Alta Empleado</asp:LinkButton>
                    <asp:LinkButton runat="server" ID="btnModEmpleado" CssClass="dropdown-item" CommandArgument="modificacionEmpl" OnClick="Accion_Click">Modificar Empleado</asp:LinkButton>
                    <asp:LinkButton runat="server" ID="btnBajaEmpleado" CssClass="dropdown-item" CommandArgument="bajaEmpl" OnClick="Accion_Click">Baja Empleado</asp:LinkButton>
                </ul>
            </div>
            <div class="dropdown">
                <button class="btn btn-light dropdown-toggle" type="button" data-bs-toggle="dropdown">Insumos</button>
                <ul class="dropdown-menu">
                    <asp:LinkButton runat="server" CssClass="dropdown-item" CommandArgument="altaIns" OnClick="Accion_Click">Alta Insumo</asp:LinkButton>
                    <asp:LinkButton runat="server" CssClass="dropdown-item" CommandArgument="modificacionIns" OnClick="Accion_Click">Modificar Insumo</asp:LinkButton>
                    <asp:LinkButton runat="server" CssClass="dropdown-item" CommandArgument="listarInsumos" OnClick="Accion_Click">Ver Insumos</asp:LinkButton>
                </ul>
            </div>
            <div class="dropdown">
                <button class="btn btn-light dropdown-toggle" type="button" data-bs-toggle="dropdown">Roles</button>
                <ul class="dropdown-menu">
                    <asp:LinkButton runat="server" CssClass="dropdown-item" CommandArgument="altaRol" OnClick="Accion_Click">Alta Rol</asp:LinkButton>
                    <asp:LinkButton runat="server" CssClass="dropdown-item" CommandArgument="modificacionRol" OnClick="Accion_Click">Modificar Rol</asp:LinkButton>
                    <asp:LinkButton runat="server" CssClass="dropdown-item" CommandArgument="eliminarRol" OnClick="Accion_Click">Baja Rol</asp:LinkButton>
                </ul>
            </div>
            <div class="dropdown">
                <button class="btn btn-light dropdown-toggle" type="button" data-bs-toggle="dropdown">Medios de Pago</button>
                <ul class="dropdown-menu">
                    <asp:LinkButton runat="server" CssClass="dropdown-item" CommandArgument="altaMedPago" OnClick="Accion_Click">Alta Medio de Pago</asp:LinkButton>
                    <asp:LinkButton runat="server" CssClass="dropdown-item" CommandArgument="modificacionMedPago" OnClick="Accion_Click">Modificar Medio de Pago</asp:LinkButton>
                    <asp:LinkButton runat="server" CssClass="dropdown-item" CommandArgument="eliminarMedPago" OnClick="Accion_Click">Baja Medio de Pago</asp:LinkButton>
                </ul>
            </div>
        </div>

    </div>
</asp:Content>
