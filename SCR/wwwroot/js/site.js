function formatDate(dateStr) {
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

function refreshChartContainer() {
    var threadType = $("#thread-type").val();

    $.ajax({
        type: "GET",
        url: "/Home/Threads?type=" + threadType,
        success: function (response) {
            if (response.length === 0) {
                return;
            }

            $("#chart-container ul").empty();

            $.each(response, function (index, value) {
                var status = '';
                if (value.threadStatus == 1) {
                    status = 'Dodany';
                }
                else if (value.threadStatus == 2) {
                    status = 'Wykonywany';
                }
                else if (value.threadStatus == 3) {
                    status = 'Zakończony';
                }

                var threadSpecialField;
                if (threadType == 0) { // priority
                    threadSpecialField = '<br />Priorytet: ' + value.priority;
                }
                else { //edf or dms
                    threadSpecialField = '<br />Ważny do: ' + formatDate(value.deadline);
                }

                $("#chart-container ul").append(
                    '<li class="list-group-item">' +
                        'Id: ' + value.id +
                        '<br />Dodany: ' + formatDate(value.inserted) +
                        '<br />Wykonany: ' + formatDate(value.finished) +
                        threadSpecialField +
                        '<br />Koszt: ' + value.cost +
                        '<br />Status: ' + status +
                    '</li>');
            });
        }
    });
}