<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <%: Html.Kendo().Grid<Kendo.Mvc.Examples.Models.ProductViewModel>()
            .Name("Grid")
            .Columns(columns =>
            {
                columns.Bound(p => p.ProductName);
                columns.Bound(p => p.UnitPrice).Width(120);
                columns.Bound(p => p.UnitsInStock).Width(120);
                columns.Bound(p => p.Discontinued).Width(120);
                columns.Command(command => command.Destroy()).Width(150);
            })
            .ToolBar(toolbar =>
            {
                toolbar.Create();
                toolbar.Save();
            })
            .HtmlAttributes(new { style = "height: 550px;" })
            .Editable(editable => editable.Mode(GridEditMode.InCell))
            .Pageable()
            .Navigatable()
            .Sortable()
            .Scrollable()
            .DataSource(dataSource => dataSource
                .Ajax()
                .Batch(true)
                .PageSize(20)                
                .ServerOperation(false)
                .Events(events => events.Error("error_handler"))
                .Model(model => model.Id(p => p.ProductID))
                .Create("Editing_Create", "Grid")
                .Read("Editing_Read", "Grid")
                .Update("Editing_Update", "Grid")
                .Destroy("Editing_Destroy", "Grid")
            )
    %>
    <script type="text/javascript">
        function error_handler(e) {
            if (e.errors) {
                var message = "Errors:\n";
                $.each(e.errors, function (key, value) {
                    if ('errors' in value) {
                        $.each(value.errors, function () {
                            message += this + "\n";
                        });
                    }
                });
                alert(message);
            }
        }
    </script>
</asp:Content>
