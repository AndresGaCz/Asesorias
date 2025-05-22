<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/mpPrincipal.Master" AutoEventWireup="true" CodeBehind="wpHistorialAsesorias.aspx.cs" Inherits="Asesorias.wpHistorialAsesorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .table-container {
            margin-top: 20px;
        }
        .table th {
            background-color: #2c3e50;
            color: white;
        }
        .estado-pendiente {
            color: #ffc107;
            font-weight: bold;
        }
        .estado-aceptada {
            color: #28a745;
            font-weight: bold;
        }
        .estado-rechazada {
            color: #dc3545;
            font-weight: bold;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h2>Historial de Asesorías</h2>
        
        <div class="row mb-3">
            <div class="col-md-4">
                <div class="input-group">
                    <asp:TextBox ID="txtBusqueda" runat="server" CssClass="form-control" placeholder="Buscar..."></asp:TextBox>
                    <div class="input-group-append">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-outline-secondary" OnClick="btnBuscar_Click" />
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged">
                    <asp:ListItem Value="">Todos los estados</asp:ListItem>
                    <asp:ListItem Value="Pendiente">Pendientes</asp:ListItem>
                    <asp:ListItem Value="Aceptada">Aceptadas</asp:ListItem>
                    <asp:ListItem Value="Rechazada">Rechazadas</asp:ListItem>
                </asp:DropDownList>
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
                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <asp:Label ID="lblEstado" runat="server" 
                                CssClass='<%# "estado-" + Eval("estado").ToString().ToLower() %>'
                                Text='<%# Eval("estado") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pagination" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>