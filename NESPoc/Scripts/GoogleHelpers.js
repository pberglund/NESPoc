
function configureChart(chartVar, contextId, chartFunc, data, options, updateId, randomized) {

    chartVar = new google.visualization[chartFunc](document.getElementById(contextId));
    chartVar.draw(data, options);

    $('#' + updateId).click(function (e) {
        e.preventDefault();
        randomized(chartVar);
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

function randomizePointOnChart(chart, data, options, max) {
    if (chart == null) {
        return;
    }
    if (data == null) {
        return;
    }
    if (options == null) {
        return;
    }
    if (max == null || max < 0) {
        max = 100;
    }

    var value = Math.floor((Math.random() * max) + 1);

    var dataSet = Math.floor((Math.random() * data.getNumberOfRows()));

    var row = [];

    for (var i = 0; i < data.Lf[dataSet].c.length; i++) {
        row.push(data.Lf[dataSet].c[i].v);
    }

    var valueToChange = Math.floor((Math.random() * row.length)) + 1;

    if (valueToChange >= row.length) {
        valueToChange = row.length - 1;
    }


    row[valueToChange] = value;

    data.removeRow(dataSet);
    data.insertRows(dataSet, [row]);

    chart.draw(data, options);
}
function merge_options(obj1, obj2) {
    var obj3 = {};
    for (var obj1AttrName in obj1) { obj3[obj1AttrName] = obj1[obj1AttrName]; }
    for (var obj2AttrName in obj2) { obj3[obj2AttrName] = obj2[obj2AttrName]; }
    return obj3;
}