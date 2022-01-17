
($.ajax({
    url: 'https://localhost:44321/api/employee/gender'
}).done((data) => {
    $.each(data, function (key, val) {
    });
    console.log(data);
}).fail((error) => {
    console.log(error)
}))

var barOptions = {
    chart: {
        type: 'bar'
    },
    series: [{
        name: 'Count',
        data: []
    }],
    xaxis: {
        categories: []
    }
}

var pieOptions = {
    chart: {
        type: 'donut'
    },
    labels : ["Male", "Female"], 
    series : [1,2]
}

var barChart = new ApexCharts(document.querySelector("#barChart"), barOptions);
var pieChart = new ApexCharts(document.querySelector("#pieChart"), pieOptions);

barChart.render();
pieChart.render();

var url = 'https://my-json-server.typicode.com/apexcharts/apexcharts.js/yearly';
var url1 = 'https://localhost:44321/api/employee/gender';

$.getJSON(url1, function (response) {
    barChart.updateSeries([{
        data: response
    }])
    console.log(response);
});