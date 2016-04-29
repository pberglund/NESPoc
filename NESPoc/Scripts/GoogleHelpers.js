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

function ConfigureCharts(opts) {

    if (opts == null) {
        return;
    }

    var currentChartObjs = [];

    for (var i = 0; i < opts.length; i++) {

        var options = opts[i];

        var baseUrl = options["BaseUrl"] || '/Google/';

        var action = options["method"] || 'GraphData';

        var t = '?type=' + (options["type"] || 0);

        var url = baseUrl + action + t;

        $.post(url).done(function (d) {
            var preProcessing = options["preProcessing"] || function(raw) { return { data: raw };};
            var pre = preProcessing(d);
            var data = options["mainManipulation"](pre);
            var chartObjs = options["postProcessing"](data);

            for (var j = 0; j < chartObjs.length; j++) {
                var chartType = chartObjs[j].chartType;
                chartObjs[j].chart = new google.visualization[chartType](document.getElementById(chartObjs[j].elementId));
                currentChartObjs.push(chartObjs[j]);
            }

            if (i == opts.length) {

                $(window).resize(function () {
                    for (var k = 0; k < currentChartObjs.length; k++) {
                        drawChartHelper(currentChartObjs[k]);
                    }
                });

                $(window).resize();
            }
        });
    }
}

function createDataTable(dataVals) {
    return google.visualization.arrayToDataTable(dataVals);
}
function formatCurrencyValue(value, pattern) {
    return format(0, null, null, value, pattern);
}

function formatCurrency(data, col, pattern) {
    return format(0, data, col, null, pattern);
}

function formatPercentageValue(value, pattern) {
    return format(1, null, null, value, pattern);
}
function formatPercentage(data, col) {
    return format(1, data, col, null);
}

function formatDateValue(value, pattern) {
    return format(2, null, null, value, pattern);
}

function formatDate(data, col, pattern) {
    return format(2, data, col, null, pattern);
}

function format(type, data, col, value, pattern) {

    var formatter;

    switch(type) {
        case 0:
        default:

            var currencyFormat = pattern || '$###,###';
            var currencyFormatter = new google.visualization.NumberFormat({
                negativeColor: 'red',
                negativeParens: true,
                pattern: currencyFormat
            });
            formatter = currencyFormatter;
            break;
        case 1:
            var percentFormat = pattern || '##.##%';
            var percentFormatter = new google.visualization.NumberFormat(
                       {
                           pattern: percentFormat
                       });
            formatter = percentFormatter;
            break;
        case 2:
            var dateFormat = pattern || "MMM yy";
            var dateFormatter = new google.visualization.DateFormat({
                pattern: dateFormat
            });
            formatter = dateFormatter;
            break;

    }

    if (value == null && data != null && col != null) {
        formatter.format(data, col);
        return data;
    } else {
        var formattedVal = formatter.formatValue(value);
        return formattedVal;
    }

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

function goclone(source) {
    if (Object.prototype.toString.call(source) === '[object Array]') {
        var clone = [];
        for (var i = 0; i < source.length; i++) {
            clone[i] = goclone(source[i]);
        }
        return clone;
    } else if (typeof (source) == "object") {
        var clone = {};
        for (var prop in source) {
            if (source.hasOwnProperty(prop)) {
                clone[prop] = goclone(source[prop]);
            }
        }
        return clone;
    } else {
        return source;
    }
}

function roundedToFixed(_float, _digits) {
    var rounder = Math.pow(10, _digits);
    return (Math.round(_float * rounder) / rounder).toFixed(_digits);
}

function TrendLineSlope(xValues, yValues) {

    if (yValues == null) {
        return 0;
    }

    if (xValues != null && (xValues.length != yValues.length)) {
        return 0;
    }

    if (xValues == null) {
        xValues = [];
        for (var j = 0; j < yValues.length; j++) {
            xValues.push(j);
        }
    }

    var xAvg = 0;
    var yAvg = 0;

    for (var i = 0; i < xValues.length; i++) {
        xAvg += i;
        yAvg += yValues[i];
    }

    xAvg = xAvg / xValues.length;
    yAvg = yAvg / yValues.length;

    var v1 = 0;
    var v2 = 0;

    for (var j = 0; j < yValues.length; j++) {
        v1 += (xValues[j] - xAvg) * (yValues[j] - yAvg);
        v2 += Math.pow(xValues[j] - xAvg, 2);
    }

    var a = v1 / v2;

    return a;
    //var b = yAvg - a * xAvg; //The y intercept
}