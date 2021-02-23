window.onload = function () {
    var dataPoints = [];
    var stockChart = new CanvasJS.StockChart("stockChartContainer", {
        exportEnabled: true,
        title: {
            text: "Sales"
        },
        subtitles: [{
            text: "Daily sales trend"
        }],
        charts: [{
            axisX: {
                crosshair: {
                    enabled: true,
                    snapToDataPoint: true,
                    valueFormatString: "MM DD YYYY"
                }
            },
            axisY: {
                title: "",
                prefix: "RON ",
                suffix: "",
                crosshair: {
                    enabled: true,
                    snapToDataPoint: true,
                    valueFormatString: "$#,###.00 RON",
                }
            },
            data: [{
                type: "line",
                xValueFormatString: "MM DD YYYY",
                yValueFormatString: "$#,###.## RON",
                dataPoints: dataPoints
            }]
        }],
        navigator: {
            slider: {
                minimum: new Date(2010, 00, 01),
                maximum: new Date(2022, 00, 01)
            }
        }
    });
    $.ajax({
        url:"/Administrator/GetSales",
        success: function (data) {
            console.log(data);
            var d = data.result;
            for (var i = 0; i < d.length; i++) {
                dataPoints.push({ x: new Date(d[i].date), y: Number(d[i].sale) });
            }
            stockChart.render();
        },
        error: function () {
            console.log("Error");
        }
    });
}