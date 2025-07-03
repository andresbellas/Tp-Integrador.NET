<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="InformacionMesero.aspx.cs" Inherits="Proyecto_Integrador.InfomormacionMesero" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5 p-4 border rounded shadow-sm bg-light" style="max-width: 600px;">
        <h2 class="mb-4 text-center text-primary fw-bold" style="font-size: 2.5rem;">Datos del Mesero</h2>

        <asp:Panel ID="pnlDatosMesero" runat="server">
            <div class="row mb-4 align-items-center">
                <label class="col-sm-4 col-form-label fw-semibold text-secondary" style="font-size: 1.2rem;">Legajo:</label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtLegajo" runat="server" CssClass="form-control fs-5 text-dark" ReadOnly="true" />
                </div>
            </div>

            <div class="row mb-4 align-items-center">
                <label class="col-sm-4 col-form-label fw-semibold text-secondary" style="font-size: 1.2rem;">Nombre:</label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control fs-5 text-dark" ReadOnly="true" />
                </div>
            </div>

            <div class="row mb-4 align-items-center">
                <label class="col-sm-4 col-form-label fw-semibold text-secondary" style="font-size: 1.2rem;">Apellido:</label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control fs-5 text-dark" ReadOnly="true" />
                </div>
            </div>

            <div class="row mb-4 align-items-center">
                <label class="col-sm-4 col-form-label fw-semibold text-secondary" style="font-size: 1.2rem;">Usuario:</label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control fs-5 text-dark" ReadOnly="true" />
                </div>
            </div>

            <div class="row mb-4 align-items-center">
                <label class="col-sm-4 col-form-label fw-semibold text-secondary" style="font-size: 1.2rem;">Rol:</label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtRol" runat="server" CssClass="form-control fs-5 text-dark" ReadOnly="true" />
                </div>
            </div>

            <div class="row mt-4">
                <div class="col-sm-6 d-grid">
                    <asp:Button ID="btnDesasignar" runat="server" Text="Desasignar Mesero" OnClick="btnDesasignar_Click" OnClientClick="return confirm('¿Seguro que quieres desasignar?');" CssClass="btn btn-danger btn-lg" />
                </div>
            </div>
        </asp:Panel>

        <%-- New Panel for Assign Employee --%>
        <asp:Panel ID="pnlAsignarMesero" runat="server" Visible="false">
            <h3 class="mb-4 text-center text-info fw-bold" style="font-size: 2rem;">Asignar Mesero</h3>
            <div class="row mb-4 align-items-center">
                <label class="col-sm-4 col-form-label fw-semibold text-secondary" style="font-size: 1.2rem;">Legajo a Asignar:</label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtLegajoAsignar" runat="server" CssClass="form-control fs-5 text-dark" placeholder="Ingrese el número de legajo" TextMode="Number" />
                </div>
            </div>
            <div class="row mt-4">
                <div class="col-sm-12 d-grid">
                    <asp:Button ID="btnAsignar" runat="server" Text="Asignar Mesero" OnClientClick="return confirm('¿Seguro que quieres asignar?');"    OnClick="btnAsignar_Click" CssClass="btn btn-success btn-lg" />
                </div>
            </div>
        </asp:Panel>

        <div class="row mb-3">
            <div class="col-sm-12 text-center">
                <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger fw-bold fs-5"></asp:Label>
            </div>
        </div>

        <div class="row mt-4">
            <div class="col-sm-12 d-grid">
                <asp:Button ID="btnVolver" runat="server" Text="Volver Atrás" CssClass="btn btn-secondary btn-lg" OnClick="btnVolver_Click" />
            </div>
        </div>
    </div>
</asp:Content>