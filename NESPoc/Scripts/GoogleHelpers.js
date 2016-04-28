

function InitializePage(method, callBackFunc, type, random) {

    if (method == null || method == '') {
        method = 'GraphData';
    }

    if (type == null) {
        type = 0;
    }

    var url = '/Google/' + method + '?type=' + type;
    if (random != null) {
        url += "&random=" + random;
    }

    $.post(url).done(function (d) {
        callBackFunc(d);
    });
}

function toggleTrendLines(chartObj) {

    if (chartObj.options.trendlines != null) {
        chartObj.options.trendlines = null;
    } else {
        chartObj.options.trendlines = {
            0: {
                color: 'black',
                tooltip: false,
            }
        };
    }
    console.log('trend draw');
    drawChartHelper(chartObj);
}

function configureChartWithTrendLines(chartObj, contextId, chartFunc, updateId, trendLineId, randomized) {
    chartObj = configureChart(chartObj, contextId, chartFunc, updateId, randomized);
    
    if (trendLineId != null) {

        $('#' + trendLineId).unbind().click(function (e) {
            e.preventDefault();
            toggleTrendLines(chartObj);
                
            });
        
    }
    return chartObj;
}

function configureChart(chartObj, contextId, chartFunc, updateId, randomized) {

    chartObj.chart = new google.visualization[chartFunc](document.getElementById(contextId));
    console.log('configure draw');
    drawChartHelper(chartObj);

    if (updateId != null) {

        $('#' + updateId).unbind().click(function(e) {
            e.preventDefault();
            randomized(chartObj);
        });
    }

    $(window).resize(function () {
        console.log('resize draw');
        drawChartHelper(chartObj);
    });
    

    return chartObj;
}


function massageData(d) {
    var data = [];
    for (var i = 0; i < d.length; i++) {
        data.push([{ v: i, f: d[i][0].toString() }, d[i][1]]);
    }
    return data;
}
function massageTooltipData(d) {
    var data = [];

    data.push([d[0][0], d[0][1], { type: 'string', role: 'tooltip' }]);

    for (var i = 1; i < d.length; i++) {
        var subData = [];
        for (var j = 0; j < d[i].length; j++) {
            subData.push(d[i][j]);
        }
        data.push(subData);
    }
    return data;
}

function massageYearData(d) {
    var data = [];
    for (var i = 0; i < d.length; i++) {
        var subData = [];
        subData.push(d[i][0].toString());
        for (var j = 1; j < d[i].length; j++) {
            subData.push(d[i][j]);
        }
        data.push(subData);
    }
    return data;
}


function randomizeData(chartObj, type, random, massageFunc) {

    if (chartObj == null) {
        return;
    }

    if (type == null) {
        type = 0;
    }
    if (random == null) {
        random = false;
    }

    $.post('/Google/GraphData?type=' + type + "&random=" + random).done(function (d) {

        var massagedData = massageFunc(d);
        var newData = google.visualization.arrayToDataTable(massagedData);
        chartObj.data = newData;
        drawChartHelper(chartObj);
    });
}

function randomData(max) {
    if (max == null || max < 0) {
        max = 100;
    }

    var val = Math.floor((Math.random() * max) + 1);
    return val;
}

function randomizePointOnChart(chartObj, max) {
    if (chartObj.chart == null) {
        return;
    }
    if (chartObj.data == null) {
        return;
    }
    if (chartObj.options == null) {
        return;
    }
    if (max == null || max < 0) {
        max = 100;
    }

    var value = Math.floor((Math.random() * max) + 1);

    var dataSet = Math.floor((Math.random() * chartObj.data.getNumberOfRows()));

    var row = [];

    var dataSection = chartObj.data.Lf[dataSet].c;
    

    if (dataSection == null) {
        return;
    }

    for (var i = 0; i < dataSection.length; i++) {
        if (dataSection[i].f != null) {
            row.push(dataSection[i]);
            continue;
        }
        row.push(dataSection[i].v);
    }

    var valueToChange = null;

    do {

        valueToChange = Math.floor((Math.random() * row.length)) + 1;
        if (valueToChange >= row.length) {
            valueToChange = row.length - 1;
        }

    } while (!isNumeric(row[valueToChange]))
    
    row[valueToChange] = value;

    for (var j = 0; j < row.length; j++) {
        var temp = row[j].toString();

        if (temp.indexOf(':') === -1) {
            continue;
        }
        var entries = temp.split(':');
        var newVal = '';
        for (var k = 0; k < entries.length - 1; k++) {
            newVal += entries[k] + ": ";
        }
        newVal += value;

        row[j] = newVal;

    }

    
    chartObj.data.removeRow(dataSet);
    chartObj.data.insertRows(dataSet, [row]);
    
    console.log('random draw');
    drawChartHelper(chartObj);
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