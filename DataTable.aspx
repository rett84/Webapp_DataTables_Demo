<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataTable.aspx.cs" Inherits="Webapp_Demo.DataTable" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link href="../DataTables/datatables.css" rel="stylesheet" />
        <link href="../DataTables/buttons.dataTables.css" rel="stylesheet" />
        <link href="../DataTables/select.dataTables.css" rel="stylesheet" />
        <link href="../DataTables/dataTables.dateTime.min.css" rel="stylesheet" />
        <link href="../DataTables/fixedHeader.dataTables.min.css" rel="stylesheet"/>
        <link href="../DataTables/stateRestore.dataTables.min.css" rel="stylesheet"/>

        <script src="../DataTables/jquery-3.7.1.min.js"></script>
        <script src="../DataTables/datatables.js"></script>
        <script src="../DataTables/dataTables.buttons.js"></script>
        <script src="../DataTables/buttons.dataTables.js"></script>
        <script src="../DataTables/dataTables.select.js"></script>
        <script src="../DataTables/select.dataTables.js"></script>
        <script src="../DataTables/dataTables.dateTime.min.js"></script>
        <script src="../DataTables/moment.min.js"></script>
        <script src="../DataTables/datetime-moment.js"></script>
        <script src="../DataTables/dataTables.fixedHeader.min.js"></script>
        <script src="../DataTables/dataTables.stateRestore.min.js"></script>
        <script src="../DataTables/sum().js"></script>

         <style type="text/css" class="init">
            table {
                table-layout: fixed; <%--this property lets the width parameter to be set in the
                                    columns definitions --%>
            }

            .container {
                margin-left:20px;
            }
	    </style>

       <script type="text/javascript">

           $(document).ready(function () {
               //DataTables declaration
               var table = new DataTable('#mainGrid', {

                   "fixedHeader": {
                       footer: true
                   },

                   "statesave": true,

                   "lengthMenu": [10, 25, 50, 100, { label: 'All', value: -1 }],

                   initComplete: function () {

                   },//Init complete

                   //Call server side method to populate Datatables
                   //Server returns JSON
                   "ajax": {
                       url: 'DataTable.aspx/BindDataSP',
                       dataSrc: function (response) {
                           var tr = response;
                           console.log(typeof tr);
                           return tr;
                       },
                       type: "POST",
                       contentType: 'application/json; charset=utf-8'
                   },

                   //Order must match thead declaration
                   "columns": [
                       { "data": 'First Name'},
                       { "data": 'Last Name'},
                       { "data": 'Job Title', "width": "130px" },
                       { "data": 'Business Phone', "width": "50px" },
                       {
                           "data": 'Salary',
                            render: DataTable.render.number(null, null, 2, '$')},
                       { "data": 'Address'},
                       { "data": 'City', "width": "100px" },
                       { "data": 'State/Province', "width": "100px" },
                       { "data": 'Country/Region', "width": "100px" }
                   ]

               }); //DataTables declaration closing
           }); //Document ready closing

       </script>

</head>
<body>


    <form id="form1" runat="server">

       <div class="jumbotron">
        <h1>DataTables Demo</h1>
        <p class="lead">This is my DataTables demo for my portfolio. Here I have C#, SQL, JS, jquery
            and Ajax technologies, plus the powerful DataTables library, being demonstrated.</p>
    </div>
    <div>
    <%--Grid declaration--%>
     <table id="mainGrid" class="display"  style="width:100%">
        <thead>
               <tr>
                <th>First Name</th>
                <th>Last Name</th>
                <th>Job Title</th>
                <th>Business Phone#</th>
                <th>Salary</th>
                <th>Address</th>
                <th>City</th>
                <th>Province</th>
                <th>Country</th>
            </tr>
        </thead>
        <tfoot>
            <tr>
                <th>First Name</th>
                <th>Last Name</th>
                <th>Job Title</th>
                <th>Business Phone#</th>
                <th>Salary</th>
                <th>Address</th>
                <th>City</th>
                <th>Province</th>
                <th>Country</th>
            </tr>
        </tfoot>
    </table>
        </div>

    <footer>
        <p>&copy; <%: DateTime.Now.Year %> - My DataTables Demo - L. Rettore</p>
    </footer>
    </form>
</body>
</html>
