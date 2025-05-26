<%@ Page Title="Timesheet Viewer" Language="C#" MasterPageFile="~/Site1.master" AutoEventWireup="true" CodeBehind="JibbleTSheets.aspx.cs" Inherits="Webapp_Demo.JibbleTSheets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../DataTables/datatables.css" rel="stylesheet" />
    <link href="../DataTables/buttons.dataTables.css" rel="stylesheet" />
    <link href="../DataTables/select.dataTables.css" rel="stylesheet" />

    <style type="text/css" class="init">
        table {
            table-layout: fixed;
        }
        .container {
            margin-left: 10px;
        }
    </style>

    <script src="../DataTables/jquery-3.7.1.min.js"></script>
    <script src="../DataTables/datatables.js"></script>
    <script src="../DataTables/dataTables.buttons.js"></script>
    <script src="../DataTables/buttons.dataTables.js"></script>
    <script src="../DataTables/dataTables.select.js"></script>
    <script src="../DataTables/select.dataTables.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            var table = new DataTable('#mainGrid', {
                "lengthMenu": [10, 25, 50, 100, { label: 'All', value: -1 }],
                "pageLength": 10,
                "fixedHeader": {
                    footer: true
                },
                initComplete: function () { },
                "ajax": {
                    url: 'JibbleTSheets.aspx/GetTEntriesRaw',
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    data: function (d) {
                        return JSON.stringify({
                            dateStart: $('#txtdatestart').val() || "1900-01-01",
                            dateEnd: $('#txtdateend').val() || new Date().toJSON().slice(0, 10)
                        });
                    },
                    dataSrc: function (response) {
                        return response;
                    }
                },
                "columns": [
                    { "data": 'fullname' },
                    { "data": 'jobcode_id' },
                    { "data": 'date' },
                    { "data": 'duration' }
                ],
            });

            $('#getData').on('click', function () {
                var table = $('#mainGrid').dataTable();
                table.api().ajax.reload();
            });
        });
    </script>

    <div style="margin-top: 40px;">
        <div style="margin-bottom: 20px; display: flex; gap: 20px; align-items: flex-end;">
            <div>
                <button type="button" id="getData" style="margin-right: 10px;">Get Timesheets</button>
            </div>
            <div>
                <label for="txtdatestart">Start Date:</label><br />
                <input type="date" id="txtdatestart" />
            </div>
            <div>
                <label for="txtdateend">End Date:</label><br />
                <input type="date" id="txtdateend" />
            </div>
        </div>

        <table id="mainGrid" class="display" style="width:50%">
            <thead>
                <tr>
                    <th>Full Name</th>
                    <th>Job Code ID</th>
                    <th>Date</th>
                    <th>Duration(h)</th>
                </tr>
            </thead>
            <tfoot>
                <tr>
                    <th>Full Name</th>
                    <th>Job Code ID</th>
                    <th>Date</th>
                    <th>Duration(h)</th>
                </tr>
            </tfoot>
        </table>
    </div>
</asp:Content>
