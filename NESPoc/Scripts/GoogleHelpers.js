
function toggleTrendLines(chart, dataVal, optionsVal) {

    if (optionsVal.trendlines != null) {
        optionsVal.trendlines = null;
    } else {
        optionsVal.trendlines = {
            0: {
                color: 'black',
            }
        };
    }
    console.log('trend draw');
    drawChartHelper(chart, dataVal, optionsVal);
}

function configureChartWithTrendLines(chartVar, contextId, chartFunc, data, options, updateId, trendLineId, randomized) {
    chartVar = configureChart(chartVar, contextId, chartFunc, data, options, updateId, randomized);
    
    if (trendLineId != null) {

        $('#' + trendLineId).unbind().click(function (e) {
            e.preventDefault();
            toggleTrendLines(chartVar, data, options);
                
            });
        
    }
    return chartVar;
}

function configureChart(chartVar, contextId, chartFunc, dataVar, optionsVar, updateId, randomized) {

    chartVar = new google.visualization[chartFunc](document.getElementById(contextId));
    console.log('configure draw');
    drawChartHelper(chartVar, dataVar, optionsVar);

    if (updateId != null) {

        $('#' + updateId).unbind().click(function(e) {
            e.preventDefault();
            randomized(chartVar);
        });
    }

    $(window).resize(function () {
        console.log('resize draw');
        drawChartHelper(chartVar, dataVar, optionsVar);
    });
    

    return chartVar;
}

function randomData(max) {
    if (max == null || max < 0) {
        max = 100;
    }

    var val = Math.floor((Math.random() * max) + 1);
    return val;
}

function randomizePointOnChart(chart, dataVar, optionsVar, max) {
    if (chart == null) {
        return;
    }
    if (dataVar == null) {
        return;
    }
    if (optionsVar == null) {
        return;
    }
    if (max == null || max < 0) {
        max = 100;
    }

    var value = Math.floor((Math.random() * max) + 1);

    var dataSet = Math.floor((Math.random() * dataVar.getNumberOfRows()));

    var row = [];

    var dataSection = dataVar.Lf[dataSet].c;
    

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
        if (row[j].toString().indexOf(':') === -1) {
            continue;
        }

        var temp = row[j];
        var entries = temp.split(':');
        var newVal = '';
        for (var k = 0; k < entries.length - 1; k++) {
            newVal += entries[k] + ": ";
        }
        newVal += value;

        row[j] = newVal;

    }

    
        dataVar.removeRow(dataSet);
        dataVar.insertRows(dataSet, [row]);
    
    console.log('random draw');
    drawChartHelper(chart, dataVar, optionsVar);
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
function drawChartHelper(chartVar, dataVal, optionsVal) {

    if (drawing) {
        return;
    }
    google.visualization.events.addListener(chartVar, 'ready',
        function () {
            drawing = false;
        });
    chartVar.draw(dataVal, optionsVal);
}