!function (map) {
    "use strict";


    $("#demo-sparkline-area").sparkline([57, 69, 70, 62, 73, 79, 76, 77, 73, 52, 57, 50, 60, 55, 70, 68], {
        type: 'line',
        width: '100%',
        height: '40',
        spotRadius: 5,
        lineWidth: 1.5,
        lineColor: 'rgba(255,255,255,.85)',
        fillColor: 'rgba(0,0,0,0.03)',
        spotColor: 'rgba(255,255,255,.5)',
        minSpotColor: 'rgba(255,255,255,.5)',
        maxSpotColor: 'rgba(255,255,255,.5)',
        highlightLineColor: '#ffffff',
        highlightSpotColor: '#ffffff',
        tooltipChartTitle: 'Usage',
        tooltipSuffix: ' %'

    });


    $("#sparkline8").sparkline([5, 6, 7, 2, 0, 4, 2, 4, 5, 7, 2, 4, 12, 14, 4, 2, 14, 12, 7], {
        type: 'bar',
        barWidth: 15,
        height: '150px',
        barColor: '#149957',
        negBarColor: '#40b87b'});
    $(".sparkline8").sparkline([4, 2], {
        type: 'pie',
        sliceColors: ['#01a8fe', '#ddd']
    });
    $(".sparkline9").sparkline([3, 2], {
        type: 'pie',
        sliceColors: ['#01a8fe', '#ddd']
    });
    $(".sparkline10").sparkline([4, 1], {
        type: 'pie',
        sliceColors: ['#01a8fe', '#ddd']
    });
    $(".sparkline11").sparkline([1, 3], {
        type: 'pie',
        sliceColors: ['#01a8fe', '#ddd']
    });
    $(".sparkline12").sparkline([3, 5], {
        type: 'pie',
        sliceColors: ['#01a8fe', '#ddd']
    });
    $(".sparkline13").sparkline([6, 2], {
        type: 'pie',
        sliceColors: ['#01a8fe', '#ddd']
    });

//moris chart
    $(function () {


        if ($('#c-weather')[0]) {
            $.simpleWeather({
                location: 'Austin, TX',
                woeid: '',
                unit: 'f',
                success: function (weather) {
                    var html = '<div class="cw-current media">' +
                            '<div class="pull-left cwc-icon cwci-' + weather.code + '"></div>' +
                            '<div class="cwc-info media-body">' +
                            '<div class="cwci-temp">' + weather.temp + '&deg;' + weather.units.temp + '</div>' +
                            '<ul class="cwci-info">' +
                            '<li>' + weather.city + ', ' + weather.region + '</li>' +
                            '<li>' + weather.currently + '</li>' +
                            '</ul>' +
                            '</div>' +
                            '</div>' +
                            '<div class="cw-upcoming"></div>';

                    $("#c-weather").html(html);

                    setTimeout(function () {


                        for (i = 0; i < 5; i++) {
                            var l = '<ul class="clearfix">' +
                                    '<li class="m-r-15">' +
                                    '<i class="cwc-icon cwci-sm cwci-' + weather.forecast[i].code + '"></i>' +
                                    '</li>' +
                                    '<li class="cwu-forecast">' + weather.forecast[i].high + '/' + weather.forecast[i].low + '</li>' +
                                    '<li>' + weather.forecast[i].text + '</li>' +
                                    '</ul>';

                            $('.cw-upcoming').append(l);
                        }
                    });
                },
                error: function (error) {
                    $("#c-weather").html('<p>' + error + '</p>');
                }
            });
        }



        var lineData = {
            labels: ["January", "February", "March", "April", "May", "June", "July"],
            datasets: [
                {
                    label: "Example dataset",
                    fillColor: "transparent",
                    strokeColor: "#d447d3",
                    pointColor: "#d447d3",
                    pointStrokeColor: "#fff",
                    pointHighlightFill: "#fff",
                    pointHighlightStroke: "#FFCA28",
                    data: [65, 59, 80, 81, 56, 55, 40]
                },
				{
                    label: "Example dataset",
                    fillColor: "transparent",
                    strokeColor: "#FFCA28",
                    pointColor: "#009688",
                    pointStrokeColor: "#fff",
                    pointHighlightFill: "#fff",
                    pointHighlightStroke: "#FFCA28",
                    data: [45, 39, 60, 51, 86, 35, 60]
                },
				{
                    label: "Example dataset",
                    fillColor: "transparent",
                    strokeColor: "#fd982c",
                    pointColor: "#fd982c",
                    pointStrokeColor: "#fff",
                    pointHighlightFill: "#fff",
                    pointHighlightStroke: "#FFCA28",
                    data: [35, 79, 60, 81, 46, 75, 20]
                },
                {
                    label: "Example dataset",
                    fillColor: "rgba(127, 216, 248, 0.5)",
                    strokeColor: "#7fd8f8",
                    pointColor: "#F44336",
                    pointStrokeColor: "#fff",
                    pointHighlightFill: "#fff",
                    pointHighlightStroke: "#009688",
                    data: [28, 48, 40, 19, 86, 27, 90]
                }
            ]
        };
        var lineOptions = {
            scaleShowGridLines: true,
            scaleGridLineColor: "#ddd",
            scaleGridLineWidth: 1,
            bezierCurve: true,
            bezierCurveTension: 0.4,
            pointDot: true,
            pointDotRadius: 4,
            pointDotStrokeWidth: 1,
            pointHitDetectionRadius: 20,
            datasetStroke: true,
            datasetStrokeWidth: 2,
            datasetFill: true,
            responsive: true
        };


        var ctx = document.getElementById("lineChart").getContext("2d");
        var myNewChart = new Chart(ctx).Line(lineData, lineOptions);



        var data = [{
                label: "Sales 1",
                data: 21,
                color: "#d3d3d3"
            }, {
                label: "Sales 2",
                data: 3,
                color: "#bababa"
            }, {
                label: "Sales 3",
                data: 15,
                color: "#79d2c0"
            }, {
                label: "Sales 4",
                data: 52,
                color: "#01a8fe"
            }];

        var doughnutData = [
            {
                value: 300,
                color: "#6cc5f3",
                highlight: "#01a8fe",
                label: "Chorme"
            },
            {
                value: 150,
                color: "#dedede",
                highlight: "#01a8fe",
                label: "Mozilla"
            },
            {
                value: 130,
                color: "#43b9f5",
                highlight: "#01a8fe",
                label: "Safari"
            }
        ];

        var doughnutOptions = {
            segmentShowStroke: true,
            segmentStrokeColor: "#fff",
            segmentStrokeWidth: 4,
            percentageInnerCutout: 45, // This is 0 for Pie charts
            animationSteps: 100,
            animationEasing: "easeOutBounce",
            animateRotate: true,
            animateScale: false,
            responsive: true
        };


       


    });




}(window.jQuery);