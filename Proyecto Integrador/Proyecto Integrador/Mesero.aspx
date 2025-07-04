<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Mesero.aspx.cs" Inherits="Proyecto_Integrador.Mesero" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-4">
        <h2 class="mb-4">Gestión Mesas - 
    <asp:Label ID="lblNombreMesero" runat="server" CssClass="fw-bold text-primary"></asp:Label>
        </h2>

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
                    BorderStyle="Solid"
                    OnRowCommand="gvMesas_RowCommand">
                    <HeaderStyle BackColor="#007bff" ForeColor="Black" Font-Bold="True" />
                    <RowStyle BackColor="#f9f9f9" />
                    <Columns>
                        <asp:BoundField DataField="Id_mesa" HeaderText="Id Mesa" />
                        <asp:BoundField DataField="Nro_Mesa" HeaderText="Número Mesa" />
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
                                    CommandArgument='<%# Eval("Id_mesa") %>'
                                    CausesValidation="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div class="bg-primary text-white p-3 rounded mt-3 d-flex flex-wrap gap-3 justify-content-start shadow-sm">



                <div class="dropdown">
                    <button class="btn btn-light dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                        Insumos
                    </button>
                    <ul class="dropdown-menu">
                        <asp:LinkButton runat="server" CssClass="dropdown-item" CommandArgument="listarInsumos" OnClick="Accion_Click">Listar Insumos</asp:LinkButton>
                    </ul>
                </div>

            </div>
        </div>
</asp:Content>
