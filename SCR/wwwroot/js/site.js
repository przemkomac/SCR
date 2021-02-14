function formatDate(dateStr) {
    if (dateStr) {
        var date = new Date(dateStr);

        var dateStr =
            ("00" + date.getDate()).slice(-2) + "/" +
            ("00" + (date.getMonth() + 1)).slice(-2) + "/" +
            date.getFullYear() + " " +
            ("00" + date.getHours()).slice(-2) + ":" +
            ("00" + date.getMinutes()).slice(-2) + ":" +
            ("00" + date.getSeconds()).slice(-2);

        return dateStr;
    }

    return "null";
}

function refreshLogsContainer() {
    $.ajax({
        type: "GET",
        url: "/Home/Logs",
        success: function (response) {
            if (response.length === 0) {
                return;
            }

            $("#logs-container ul").empty();

            $.each(response, function (index, value) {
                $("#logs-container ul").append('<li class="list-group-item">' + formatDate(value.key) + ': ' + value.value + '</li>');
            });
        }
    });
}

function initGraph(threads) {
    var allowedColors = ["red", "green", "blue", "yellow", "black", "pink"];
    var whiteColor = "white";

    var options = {
        animationEnabled: false,
        title: {
            text: "Czas pracy procesora"
        },
        axisY: {
            suffix: " s"
        },
        toolTip: {
            shared: true,
            reversed: true
        },
        legend: {
            reversed: true,
            verticalAlign: "center",
            horizontalAlign: "right"
        },
        data: []
    };

    $.each(threads, function (index, value) {
        if (value.ThreadNo === -1) {
            options.data.push({
                type: "stackedColumn",
                name: 'IDLE',
                color: whiteColor,
                showInLegend: true,
                yValueFormatString: "#,##0\" s\"",
                dataPoints: [
                    { label: "Czas", y: value.Capacity }
                ]
            });
        } else {
            options.data.push({
                type: "stackedColumn",
                name: 'Thread no. ' + (value.ThreadNo + 1),
                color: allowedColors[value.ThreadNo],
                showInLegend: true,
                yValueFormatString: "#,##0\" s\"",
                dataPoints: [
                    { label: "Czas", y: value.Capacity }
                ]
            });
        }
    });

    $("#chartContainer").CanvasJSChart(options);
}