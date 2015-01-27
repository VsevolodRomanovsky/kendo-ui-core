﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 <style>
    #export
    {
        padding: 0 0 10px 1px;
    }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <button id="export" class="k-button k-button-icontext"><span class="k-icon k-i-pdf"></span>Export to PDF</button>

    <%= Html.Kendo().PivotGrid()
        .Name("pivotgrid")
        .Pdf(pdf => pdf
            .FileName("Kendo UI PivotGrid Export.pdf")
            .ProxyURL(Url.Action("Pdf_Export_Save", "PivotGrid"))
        )
        .ColumnWidth(200)
        .Height(570)
        .DataSource(dataSource => dataSource.
            Xmla()
            .Columns(columns => {
                columns.Add("[Date].[Calendar]").Expand(true);
                columns.Add("[Product].[Category]");
            })
            .Rows(rows => rows.Add("[Geography].[City]"))
            .Measures(measures => measures.Values(new string[]{"[Measures].[Reseller Freight Cost]"}))
            .Transport(transport => transport
                .Connection(connection => connection
                    .Catalog("Adventure Works DW 2008R2")
                    .Cube("Adventure Works"))
                .Read(read => read
                    .Url("http://demos.telerik.com/olap/msmdpump.dll")
                    .DataType("text")
                    .ContentType("text/xml")
                    .Type(HttpVerbs.Post) 
                )
            )
            .Events(e => e.Error("onError"))
        )
    %>

<script>
    $(function () {
        $("#export").click(function () {
            $("#pivotgrid").getKendoPivotGrid().saveAsPDF();
        });
    });

    function onError(e) {
        alert("error: " + kendo.stringify(e.errors[0]));
    }
</script>
</asp:Content>
