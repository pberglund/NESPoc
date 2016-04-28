

function InitializePage(method, callBackFunc, options) {

    var opts = options || {};

    var baseUrl = opts["BaseUrl"] || '/Google/';

    var action = method || 'GraphData';

    var t = '?type=' + (opts["type"] || 0);

    var url = baseUrl + action +  t;

    $.post(url).done(function (d) {
        callBackFunc(d);
    });
}

function merge_options(obj1, obj2) {
    var obj3 = {};
    for (var obj1AttrName in obj1) { obj3[obj1AttrName] = obj1[obj1AttrName]; }
    for (var obj2AttrName in obj2) { obj3[obj2AttrName] = obj2[obj2AttrName]; }
    return obj3;
}

function isNumeric(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}

var drawing = false;
function drawChartHelper(chartObj) {

    if (drawing) {
        return;
    }
    google.visualization.events.addListener(chartObj.chart, 'ready',
        function () {
            drawing = false;
        });
    chartObj.chart.draw(chartObj.data, chartObj.options);
}

function GetMagnitude(val) {
    for (var n = 0, formattedVal = val; formattedVal >= 10; n++) {
        formattedVal /= 10;
    }
    return {
        Magnitude: n,
        Remainder: formattedVal,
    };
}