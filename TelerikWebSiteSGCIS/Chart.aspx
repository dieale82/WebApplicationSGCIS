<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Chart.aspx.cs" Inherits="Chart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <meta content="width=device-width, initial-scale=1.0" name="viewport">

    <title>Chart window</title>
    <meta content="" name="description">
    <meta content="" name="keywords">

    <!-- Favicons -->
    <link href="assets/img/favicon.png" rel="icon">
    <link href="assets/img/apple-touch-icon.png" rel="apple-touch-icon">

    <!-- Google Fonts -->
    <link href="https://fonts.gstatic.com" rel="preconnect">
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Nunito:300,300i,400,400i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i" rel="stylesheet">

    <!-- Vendor CSS Files -->
    <link href="assets/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">

    <!-- Template Main CSS File -->
    <link href="assets/css/style.css" rel="stylesheet">
    <script src="scripts/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="https://canvasjs.com/assets/script/jquery.canvasjs.min.js"></script> 

    <script>
        $(function () {
            $.ajax({
                type: "POST",
                url: "Chart.aspx/GetCharData",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (rows) {

                    var jsonData = JSON.parse(rows.d);
                    var dataArray = [];                   

                    for (row in jsonData) {
                        dataArray.push(jsonData[row]);                        
                    }

                    LoadChartE(dataArray);                    
                }
            });

            function LoadChartE(jsonData) {
                $(".chartContainer").CanvasJSChart({
                    title: {
                        text: "Random Data"
                    },
                    axisY: {                                              
                        includeZero: false
                    },
                    data: [
                        {
                            type: "column",
                            toolTipContent: "{label}: {y}",
                            dataPoints: jsonData
                        }
                    ]
                });
            }
        });
    </script>    
</head>
<body>
    <main>
        <div class="container-fluid">
            <div class="row">
                <div class="col-4 bg-light">
                    <p runat="server" id="lblPersonName"></p>
                </div>
                <div class="col-4 bg-light">
                    <p runat="server" id="lblAge"></p>
                </div>
                <div class="col-4 bg-light">
                    <p runat="server" id="lblType"></p>
                </div>
            </div>
        </div>
        <div class="container-fluid">
            <div class="row">
                <div class="col">
                    <div class="chartContainer" style="height: 300px; width: 100%"></div> 
                </div>
            </div>
        </div>      

    </main>    
    <script src="assets/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>    
</body>
</html>
