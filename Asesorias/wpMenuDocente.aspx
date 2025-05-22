<%@ Page Title="" Language="C#" MasterPageFile="~/mpPrincipal.Master" AutoEventWireup="true" CodeBehind="wpMenuDocente.aspx.cs" Inherits="Asesorias.wpMenuDocente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMenu" runat="server">
    <li class="nav-item">
        <asp:HyperLink ID="hlAsesoriasPendientes" runat="server" CssClass="nav-link" NavigateUrl="~/wpAsesoriasPendientes.aspx">Asesorías Pendientes</asp:HyperLink>
    </li>
    <li class="nav-item">
        <asp:HyperLink ID="hlHistorialAsesorias" runat="server" CssClass="nav-link" NavigateUrl="~/wpHistorialAsesorias.aspx">Historial de Asesorías</asp:HyperLink>
    </li>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="jumbotron">
        <h1 class="display-4">Bienvenido Docente</h1>
        <p class="lead">Sistema de gestión de asesorías académicas</p>
        <hr class="my-4">
        <p>Desde aquí puedes gestionar las solicitudes de asesorías de tus estudiantes.</p>
    </div>
</asp:Content>