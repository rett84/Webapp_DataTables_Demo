<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="QbTSheet.aspx.cs" Inherits="Webapp_Demo.QbTSheet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../DataTables/datatables.css" rel="stylesheet" />
    <link href="../DataTables/buttons.dataTables.css" rel="stylesheet" />
    <link href="../DataTables/select.dataTables.css" rel="stylesheet" />



    <style type="text/css" class="init">
        table {
            table-layout: fixed; <%--this property lets the width parameter to be set in the
                                columns definitions --%>
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

            
            //DataTables declaration
           var table = new DataTable('#mainGrid', {

                "lengthMenu": [10, 25, 50, 100, { label: 'All', value: -1 }],

                "pageLength": 10,

                "fixedHeader": {
                    footer: true
               },

               initComplete: function () {
                  
               },

                // //Call server side method to populate Datatables
                ////Server returns JSON
               "ajax": {
                   url: 'QbTSheet.aspx/TSDatatoJson',
                   type: "POST",
                   contentType: 'application/json; charset=utf-8',
                   dataType: "json",
                   data: function (d) {
                       return JSON.stringify({
                           dateStart: $('#txtdatestart').val() || "2020-01-01",
                           dateEnd: $('#txtdateend').val() || new Date().toJSON().slice(0, 10)});
                   },
                   dataSrc: function (response) {
                       var tr = response;
                       console.log(typeof tr);
                       return tr;
                   }
                },


                 //Set column specific initialisation properties
                 //Data comes from JSON server side
                 //Order must match thead declaration
                "columns": [
                    { "data": 'fullname'},
                    { "data": 'jobcode_id' },
                    { "data": 'date'},
                    { "data": 'duration' },
                ],

           }); //DataTables declaration closing

          

            $('#getData').on('click', function refreshData() {
               
                var table = $('#mainGrid').dataTable();
                table.api().ajax.reload();
                console.log('test');
            });

        }); //Document ready closing

        

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
        <%--Grid declaration--%>
        <table id="mainGrid" class="display"  style="width:50%">
            <thead>
                   <tr>
                    <th>Full Name</th>
                    <th>Job Code ID</th>
                    <th>Date</th>
                    <th>Duration</th>
                </tr>
            </thead>
            <tfoot>
                <tr>
                    <th>Full Name</th>
                    <th>Job Code ID</th>
                    <th>Date</th>
                    <th>Duration</th>
                </tr>
            </tfoot>
        </table>
    </div>
</asp:Content>
