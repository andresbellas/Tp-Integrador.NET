<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" Inherits="Proyecto_Integrador.Gerente" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2 class="mb-4">Gestión</h2>

        <!-- Fancy borde y sombra para la tabla -->
        <div class="p-3 border border-dark rounded shadow-sm bg-white">
            <!-- contenedor con scroll vertical -->
            <div style="max-height: 500px; overflow-y: auto;">
                <asp:GridView 
                    ID="gvMesas" 
                    runat="server" 
                    AutoGenerateColumns="false" 
                    CssClass="table table-striped table-bordered" 
                    DataKeyNames="Id_mesa" 
                    AllowPaging="false"
                    GridLines="Both"
                    BorderStyle="Solid">
                    <HeaderStyle BackColor="#007bff" ForeColor="Black" Font-Bold="True" />
                    <RowStyle BackColor="#f9f9f9" />
                    <Columns>
                        <asp:BoundField DataField="Id_mesa" HeaderText="Id Mesa" />
                        <asp:BoundField DataField="Nro_Mesa" HeaderText="Número Mesa" />
                        <asp:TemplateField HeaderText="Número Legajo">
         <ItemTemplate>
             <asp:Button 
            ID="btnLegajo" 
            runat="server" 
            Text='<%# Eval("Nro_Legajo").ToString() %>' 
            CssClass="btn btn-primary btn-sm"
            CommandName="legajoClick"
            CommandArgument='<%# Eval("Nro_Legajo") %>'
            CausesValidation="false" />
           </ItemTemplate>
      </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado">
               <ItemTemplate>
            <%# Eval("Id_Estado").ToString() == "1" ? "Libre" : "Ocupado" %>
              </ItemTemplate>
           </asp:TemplateField>
                        <asp:TemplateField HeaderText="Acción">
                            <ItemTemplate>
                                <asp:Button 
                                    ID="btnSeleccionar" 
                                    runat="server" 
                                    Text='<%# Eval("Id_Estado").ToString() == "1" ? "Abrir Pedido" : "Gestionar Pedido" %>' 
                                    CssClass='<%# Eval("Id_Estado").ToString() == "1" ? "btn btn-success btn-sm" : "btn btn-danger btn-sm" %>' 
                                    CommandName="accionMesa"
                                    CommandArgument='<%# Eval("Id_Estado") %>'
                                    CausesValidation="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <div class="bg-primary text-white p-3 rounded mt-3 d-flex flex-wrap gap-3 justify-content-start shadow-sm">
           

            <div class="dropdown">
                <button class="btn btn-light dropdown-toggle" type="button" data-bs-toggle="dropdown">
                    Empleados
                </button>
                <ul class="dropdown-menu">
                    <asp:LinkButton runat="server" ID="btnAltaEmpleado" CssClass="dropdown-item" CommandArgument="altaEmpl" OnClick="Accion_Click">Alta Empleado</asp:LinkButton>
                    <asp:LinkButton runat="server" ID="btnModEmpleado" CssClass="dropdown-item" CommandArgument="modificacionEmpl" OnClick="Accion_Click">Modificar Empleado</asp:LinkButton>
                    <asp:LinkButton runat="server" ID="btnBajaEmpleado" CssClass="dropdown-item" CommandArgument="bajaEmpl" OnClick="Accion_Click">Baja Empleado</asp:LinkButton>
                </ul>
            </div>

            <div class="dropdown">
                <button class="btn btn-light dropdown-toggle" type="button" data-bs-toggle="dropdown">
                    Insumos
                </button>
                <ul class="dropdown-menu">
                    <asp:LinkButton runat="server" CssClass="dropdown-item" CommandArgument="altaIns" OnClick="Accion_Click">Alta Insumo</asp:LinkButton>
                    <asp:LinkButton runat="server" CssClass="dropdown-item" CommandArgument="modificacionIns" OnClick="Accion_Click">Modificar Insumo</asp:LinkButton>
                    <asp:LinkButton runat="server" CssClass="dropdown-item" CommandArgument="listarInsumos" OnClick="Accion_Click">Ver Insumos</asp:LinkButton>
                </ul>
            </div>

            <div class="dropdown">
                <button class="btn btn-light dropdown-toggle" type="button" data-bs-toggle="dropdown">
                    Roles
                </button>
                <ul class="dropdown-menu">
                    <asp:LinkButton runat="server" CssClass="dropdown-item" CommandArgument="altaRol" OnClick="Accion_Click">Alta Rol</asp:LinkButton>
                    <asp:LinkButton runat="server" CssClass="dropdown-item" CommandArgument="modificacionRol" OnClick="Accion_Click">Modificar Rol</asp:LinkButton>
                    <asp:LinkButton runat="server" CssClass="dropdown-item" CommandArgument="eliminarRol" OnClick="Accion_Click">Eliminar Rol</asp:LinkButton>
                </ul>
            </div>

            <div class="dropdown">
                <button class="btn btn-light dropdown-toggle" type="button" data-bs-toggle="dropdown">
                    Medios de Pago
                </button>
                <ul class="dropdown-menu">
                    <asp:LinkButton runat="server" CssClass="dropdown-item" CommandArgument="altaMedPago" OnClick="Accion_Click">Alta Medio de Pago</asp:LinkButton>
                    <asp:LinkButton runat="server" CssClass="dropdown-item" CommandArgument="modificacionMedPago" OnClick="Accion_Click">Modificar Medio de Pago</asp:LinkButton>
                    <asp:LinkButton runat="server" CssClass="dropdown-item" CommandArgument="eliminarMedPago" OnClick="Accion_Click">Eliminar Medio de Pago</asp:LinkButton>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>