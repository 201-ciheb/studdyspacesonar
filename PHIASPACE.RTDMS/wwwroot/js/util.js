function getColumNames(columnsIn) {
   
    var columNames = [];
    var i = 0;
    var j = 0;
    for (var key in columnsIn) {
        if (i > 0) {
            columNames[j] = key;
            j += 1;
        }
        i += 1;
    }

    return columNames;
}

function getColumValues(columnsIn) {

    var columValues = [];
    var i = 0;
    //for (var value in columnsIn) {
    //    columValues[i] = value;
    //    i += 1;
    //}
    var k = 0;

    for (var j in columnsIn) {
        var sub_key = j;
        var sub_val = columnsIn[j];
        if (i > 0) {
            columValues[k] = sub_val;
            k += 1;
        }
       // pie.push({ name: sub_key, y: sub_val, color: Highcharts.getOptions().colors[count] });
        //count += 1;
        i += 1;
    }

    //for (var i in data) {
    //    var key = i;
    //    var val = data[i];
    //    for (var j in val) {
    //        var sub_key = j;
    //        var sub_val = val[j];
    //        pie.push({ name: sub_key, y: sub_val, color: Highcharts.getOptions().colors[count] });
    //        count += 1;
    //    }
    //}

    return columValues;
}

function plotPieChart(container, xtitle, categories, series){
    Highcharts.chart(container, {
        title: {
            text: xtitle
        },
        xAxis: {
            categories: categories
        },
        labels: {
            items: [{
                //html: 'Total fruit consumption',
                style: {
                    color: (Highcharts.theme && Highcharts.theme.textColor) || 'black'
                }
            }]
        },
        series: series,
        credits: false,
    });
}

function plotBarChart(container, xtitle, categories, series){
    Highcharts.chart(container, {
        title: {
            text: xtitle
        },
        xAxis: {
            categories: categories
        },
        labels: {
            items: [{
                // html: 'Result of household interview'
            }]
        },
        series: series,
        credits: false,
    });

}
