<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/mpPrincipal.Master" AutoEventWireup="true" CodeBehind="wpAsesoriasPendientes.aspx.cs" Inherits="Asesorias.wpAsesoriasPendientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .table-container {
            margin-top: 20px;
        }
        .table th {
            background-color: #2c3e50;
            color: white;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h2>Asesorías Pendientes</h2>
        
        <div class="row mb-3">
            <div class="col-md-4">
                <div class="input-group">
                    <asp:TextBox ID="txtBusqueda" runat="server" CssClass="form-control" placeholder="Buscar..."></asp:TextBox>
                    <div class="input-group-append">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-outline-secondary" OnClick="btnBuscar_Click" />
                    </div>
                </div>
            </div>
        </div>
        
        <div class="table-container">
            <asp:GridView ID="gvAsesorias" runat="server" CssClass="table table-striped table-bordered" 
                AutoGenerateColumns="False" AllowPaging="True" PageSize="5" OnPageIndexChanging="gvAsesorias_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="ID" />
                    <asp:BoundField DataField="estudiante_nombre" HeaderText="Estudiante" />
                    <asp:BoundField DataField="materia_nombre" HeaderText="Materia" />
                    <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="hora_inicio" HeaderText="Hora Inicio" />
                    <asp:BoundField DataField="hora_fin" HeaderText="Hora Fin" />
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-success btn-sm" 
                                CommandArgument='<%# Eval("id") %>' OnClick="btnAceptar_Click" />
                            <asp:Button ID="btnRechazar" runat="server" Text="Rechazar" CssClass="btn btn-danger btn-sm ml-1" 
                                CommandArgument='<%# Eval("id") %>' OnClick="btnRechazar_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pagination" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>