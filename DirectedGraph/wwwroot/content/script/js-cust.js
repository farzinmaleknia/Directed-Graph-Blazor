
function getElementDimension(id) {
    const dimention = document.getElementById(`${id}`);
    if (dimention) {
        var offsets = dimention.getBoundingClientRect();

        return {
            height: `${document.documentElement.clientHeight}`,
            width: `${dimention.clientWidth}`,
            top: `${offsets.top}`,
            left: `${offsets.left}`,
            right: `${offsets.right}`,
        }

    } else {
        return (null);
    }

}

function makeDivZoomable(id, scale) {
    var isDown = false;
    var div = document.getElementById(`${id}`); //.getElementsByTagName('p')
    zX = 1;
    newTOx = 0;
    newTOy = 0;
    Tx = 0;
    Ty = 0;

    var diff = 1 - scale;
    var times = Math.abs(diff * 10);
    var dir2 = - (diff / times);

    for (var i = 0; i < times; i++) {
        zX += dir2;
        newTOx = div.clientWidth / 2;
        newTOy = div.clientHeight / 2;

        div.style.transformOrigin = `${newTOx}px ${newTOy}px`;
        div.style.transform = `scale(${zX}) translate(${Tx}px, ${Ty}px)`;

        console.log(`diff - ${diff}, times - ${times}, dir2 - ${dir2}, zX - ${zX}`)

    }



    //window.addEventListener('wheel', function (e) {
    //    var dir;
    //    dir = (e.deltaY > 0) ? -0.1 : 0.1;

    //    var matrix = window.getComputedStyle(div).transform;
    //    var matrixArray = matrix.replace("matrix(", "").split(",");
    //    var preScale = parseFloat(matrixArray[0]);

    //    var newScale = e.deltaY > 0 ? preScale - 0.1 : preScale + 0.1;

    //    var lastTOx = newTOx;
    //    var lastTOy = newTOy;

    //    mousePosition = {
    //        x: e.clientX - div.offsetLeft,
    //        y: e.clientY - div.offsetTop
    //    };

    //    var newTx = ((mousePosition.x - lastTOx) / (((Math.abs(preScale))) * 10)) * (Math.abs(preScale - 1) * 10);
    //    var newTy = ((mousePosition.y - lastTOy) / (((Math.abs(preScale))) * 10)) * (Math.abs(preScale - 1) * 10);

    //    if (preScale < 1) {
    //        newTx = -newTx;
    //        newTy = -newTy;
    //    }

    //    console.log(zX)

    //    newTOx = mousePosition.x;
    //    newTOy = mousePosition.y;

    //    div.style.transformOrigin = `${newTOx}px ${newTOy}px`;

    //    if (newScale > 0.09 || preScale != 0.1) {
    //        zX += dir;

    //        if (!isNaN(newTx)) {
    //            Tx += newTx;
    //            Ty += newTy;

    //            div.style.transform = `scale(${zX}) translate(${Tx}px, ${Ty}px)`;

    //        } else {
    //            div.style.transform = `scale(${zX})`;

    //        }
    //    }



    //    e.preventDefault();
    //    return;
    //}, { passive: false });


    div.addEventListener('mousedown', function (e) {
        if (e.which == 1) {
            isDown = true;
            offset = [
                div.offsetLeft - e.clientX,
                div.offsetTop - e.clientY
            ];
            div.style.cursor = "grabbing";
        }

    }, true);

    document.addEventListener('mouseup', function () {
        isDown = false;
        div.style.cursor = "pointer";
    }, true);

    document.addEventListener('mousemove', function (event) {
        event.preventDefault();
        if (isDown) {
            mousePosition = {

                x: event.clientX,
                y: event.clientY

            };
            div.style.left = (mousePosition.x + offset[0]) + 'px';
            div.style.top = (mousePosition.y + offset[1]) + 'px';
        }
    }, true);
}

function refreshScale(id, top, left, scale) {

    var div = document.getElementById(`${id}`);
    div.style.transform = 'scale(1)';
    div.style.transformOrigin = `${div.offsetLeft + (div.width / 2)}px ${div.offsetTop + (div.height / 2)}px`;
    div.style.top = top + 'px';
    div.style.left = left + 'px';
    zX = 1;
    Tx = 0;
    Ty = 0;

    var diff = 1 - scale;
    var times = Math.abs(diff * 10);
    var dir2 = - (diff / times);

    for (var i = 0; i < times; i++) {
        zX += dir2;
        newTOx = div.clientWidth / 2;
        newTOy = div.clientHeight / 2;

        div.style.transformOrigin = `${newTOx}px ${newTOy}px`;
        div.style.transform = `scale(${zX}) translate(${Tx}px, ${Ty}px)`;

        console.log(`diff - ${diff}, times - ${times}, dir2 - ${dir2}, zX - ${zX}`)

    }

}





function highchartsDG(id, transitions, myNodesJson) {
    console.log("jskhfdkdsh");
    const el = document.getElementById(`${id}`)


    var myNodes = JSON.parse(myNodesJson);

    el.addEventListener('click', function () {
        
        var t0 = performance.now();
        
        Highcharts.chart('container2', {
            chart: {
                type: 'networkgraph',
            },
            title: {
                text: 'Network Graph',
                align: 'left'
            },
            subtitle: {
                text: 'Nazmaran Co.',
                align: 'left'
            },

            plotOptions: {

                networkgraph: {
                    keys: ['from', 'to'],
                    draggable: true,
                    layoutAlgorithm: {
                        enableSimulation: false,
                        maxIterations: 180,
                        integration: 'euler',
                        initialPositions: "circle",
                        gravitationalConstant: 0.09,
                        //linkLength: 70,
                        //friction: -0.981,
                        
                    },
                    point: {
                        events: {
                            click(e) {
                                handlePointClick(e.point);
                            }
                        }
                    }
                },
                series: {
                    dataLabels: {
                        enabled: true,
                        linkFormat: '',
                        style: {
                            fontSize: '1em',
                            fontWeight: 'normal'
                        }
                    },
                }
            },
            series: [{
                tooltip: {
                    nodeFormatter: function () {
                        //return `<table><tr><th colspan="2"><h4>${this.id}</h4><hr/></th></tr><tr><td style="color: ${this.marker.fillColor}">${this.id} </td><td style="text-align: right"><b>${this.id} EUR</b></td></tr></table>`;
                        return `<p style=""><b>${this.title}</b><br></br><b style="color: ${this.marker.fillColor}">${this.marker.radius} Link's</b><br>
                        ${this.description}</p>`;
                    },
                },
                id: 'NazmDb-Graph',
                nodes: myNodes,
                data: transitions,
            }],
        });


        var t1 = performance.now();

        console.log("Call to doSomething took " + (t1 - t0) / 1000 + " milliseconds.");
    });
}



async function handlePointClick(e) {
    var res = await DotNet.invokeMethodAsync('DirectedGraph', 'JsCallback', e.title)
    let myModal = new bootstrap.Modal(document.getElementById('HighchartPointModal'), {});


    if (res != null) {
        myModal.show();
    }

}


//function highchartsDGTwo(id, integration, linkLength, transitions, myNodesJson) {
//    const el = document.getElementById(`${id}`);

//    var myNodes = JSON.parse(myNodesJson);

//    el.addEventListener('click', function () {
//        Highcharts.chart("container2", {
//            chart: {
//                type: 'networkgraph',
//                plotBorderWidth: 1
//            },
//            title: {
//                text: 'Phrasal verbs'
//            },
//            subtitle: {
//                text: 'Integration: ' + integration
//            },
//            plotOptions: {
//                networkgraph: {
//                    layoutAlgorithm: {
//                        enableSimulation: false,
//                        initialPositions: "circle",
//                        //linkLength: 60,
//                        integration: integration,
//                        //linkLength: linkLength
//                    },
//                    keys: ['from', 'to'],
//                    marker: {
//                        radius: 5,
//                        lineWidth: 1
//                    }
//                },
//                series: {
//                    dataLabels: {
//                        enabled: true,
//                        linkFormat: '',
//                        style: {
//                            fontSize: '0.8em',
//                            fontWeight: 'normal'
//                        }
//                    },
//                }
//            },
//            series: [{


//                tooltip: {
//                    nodeFormatter: function () {
//                        //return `<table><tr><th colspan="2"><h4>${this.id}</h4><hr/></th></tr><tr><td style="color: ${this.marker.fillColor}">${this.id} </td><td style="text-align: right"><b>${this.id} EUR</b></td></tr></table>`;
//                        return `<p style=""><b>${this.title}</b><br></br><b style="color: ${this.marker.fillColor}">${this.marker.radius} Link's</b><br>
//                        ${this.description}</p>`;
//                    },
//                },
//                nodes: myNodes,
//                data: transitions,

//                //nodes: [{
//                //    id: 'for',
//                //    dataLabels: {
//                //        enabled: true
//                //    },
//                //    marker: {
//                //        radius: 2,
//                //        fillColor: 'red'
//                //    }
//                //}, {
//                //    id: 'up',
//                //    dataLabels: {
//                //        enabled: true
//                //    },
//                //    marker: {
//                //        radius: 15,
//                //        fillColor: 'red'
//                //    }
//                //}, {
//                //    id: 'back',
//                //    dataLabels: {
//                //        enabled: true
//                //    },
//                //    marker: {
//                //        radius: 2.5,
//                //        fillColor: 'red'
//                //    }
//                //}, {
//                //    id: 'away',
//                //    dataLabels: {
//                //        enabled: true
//                //    },
//                //    marker: {
//                //        radius: 2.5,
//                //        fillColor: 'red'
//                //    }
//                //}, {
//                //    id: 'down',
//                //    dataLabels: {
//                //        enabled: true
//                //    },
//                //    marker: {
//                //        radius: 5.5,
//                //        fillColor: 'red'
//                //    }
//                //}, {
//                //    id: 'on',
//                //    dataLabels: {
//                //        enabled: true
//                //    },
//                //    marker: {
//                //        radius: 7.5,
//                //        fillColor: 'red'
//                //    }
//                //}, {
//                //    id: 'out',
//                //    dataLabels: {
//                //        enabled: true
//                //    },
//                //    marker: {
//                //        radius: 13.5,
//                //        fillColor: 'red'
//                //    }
//                //}, {
//                //    id: 'off',
//                //    dataLabels: {
//                //        enabled: true
//                //    },
//                //    marker: {
//                //        radius: 6.5,
//                //        fillColor: 'red'
//                //    }
//                //}, {
//                //    id: 'break',
//                //    dataLabels: {
//                //        enabled: true
//                //    },
//                //    marker: {
//                //        radius: 3
//                //    }
//                //}, {
//                //    id: 'into',
//                //    dataLabels: {
//                //        enabled: true
//                //    },
//                //    marker: {
//                //        radius: 2,
//                //        fillColor: 'red'
//                //    }
//                //}, {
//                //    id: 'in',
//                //    dataLabels: {
//                //        enabled: true
//                //    },
//                //    marker: {
//                //        radius: 4,
//                //        fillColor: 'red'
//                //    }
//                //}, {
//                //    id: 'get',
//                //    dataLabels: {
//                //        enabled: true
//                //    },
//                //    marker: {
//                //        radius: 8.5
//                //    }
//                //}, {
//                //    id: 'over',
//                //    dataLabels: {
//                //        enabled: true
//                //    },
//                //    marker: {
//                //        radius: 1.5,
//                //        fillColor: 'red'
//                //    }
//                //}, {
//                //    id: 'look',
//                //    dataLabels: {
//                //        enabled: true
//                //    },
//                //    marker: {
//                //        radius: 3
//                //    }
//                //}, {
//                //    id: 'put',
//                //    dataLabels: {
//                //        enabled: true
//                //    },
//                //    marker: {
//                //        radius: 2
//                //    }
//                //}, {
//                //    id: 'run',
//                //    dataLabels: {
//                //        enabled: true
//                //    },
//                //    marker: {
//                //        radius: 1.5
//                //    }
//                //}, {
//                //    id: 'take',
//                //    dataLabels: {
//                //        enabled: true
//                //    },
//                //    marker: {
//                //        radius: 2.5
//                //    }
//                //}, {
//                //    id: 'down on',
//                //    dataLabels: {
//                //        enabled: true
//                //    },
//                //    marker: {
//                //        radius: 1.5,
//                //        fillColor: 'red'
//                //    }
//                //}, {
//                //    id: 'keep',
//                //    dataLabels: {
//                //        enabled: true
//                //    },
//                //    marker: {
//                //        radius: 1.5,
//                //        fillColor: 'red'
//                //    }
//                //}, {
//                //    id: 'fun of',
//                //    dataLabels: {
//                //        enabled: true
//                //    },
//                //    marker: {
//                //        radius: 1.5,
//                //        fillColor: 'red'
//                //    }
//                //}, {
//                //    id: 'care of',
//                //    dataLabels: {
//                //        enabled: true
//                //    },
//                //    marker: {
//                //        radius: 1.5,
//                //        fillColor: 'red'
//                //    }
//                //}, {
//                //    id: 'by',
//                //    dataLabels: {
//                //        enabled: true
//                //    },
//                //    marker: {
//                //        radius: 1.5,
//                //        fillColor: 'red'
//                //    }
//                //}, {
//                //    id: 'out of',
//                //    dataLabels: {
//                //        enabled: true
//                //    },
//                //    marker: {
//                //        radius: 1.5,
//                //        fillColor: 'red'
//                //    }
//                //}],

//                //data: [
//                //    ['abide', 'by'],
//                //    ['account', 'for'],
//                //    ['add', 'up'],
//                //    ['allow', 'for'],
//                //    ['apply', 'for'],
//                //    ['back', 'away'],
//                //    ['back', 'down'],
//                //    ['back', 'up'],
//                //    ['bank', 'on'],
//                //    ['black', 'out'],
//                //    ['block', 'off'],
//                //    ['blow', 'up'],
//                //    ['boot', 'up'],
//                //    ['break', 'away'],
//                //    ['break', 'down'],
//                //    ['break', 'into'],
//                //    ['break', 'out'],
//                //    ['break', 'up'],
//                //    ['bring', 'up'],
//                //    ['brush', 'up'],
//                //    ['bump', 'into'],
//                //    ['burn', 'out'],
//                //    ['call', 'back'],
//                //    ['call', 'off'],
//                //    ['calm', 'down'],
//                //    ['carry', 'on'],
//                //    ['carry', 'out'],
//                //    ['check', 'in'],
//                //    ['check', 'out'],
//                //    ['clam', 'up'],
//                //    ['clamp', 'down on'],
//                //    ['count', 'on'],
//                //    ['cut', 'down on'],
//                //    ['cut', 'out'],
//                //    ['die', 'down'],
//                //    ['drag', 'on'],
//                //    ['draw', 'up'],
//                //    ['dress', 'up'],
//                //    ['ease', 'off'],
//                //    ['end', 'in'],
//                //    ['end', 'up'],
//                //    ['figure', 'out'],
//                //    ['fill', 'out'],
//                //    ['find', 'out'],
//                //    ['focus', 'on'],
//                //    ['get', 'along'],
//                //    ['get', 'at'],
//                //    ['get', 'away'],
//                //    ['get', 'by'],
//                //    ['get', 'in'],
//                //    ['get', 'into'],
//                //    ['get', 'off'],
//                //    ['get', 'on'],
//                //    ['get', 'out'],
//                //    ['get', 'over'],
//                //    ['get', 'rid of'],
//                //    ['get', 'together'],
//                //    ['get', 'up'],
//                //    ['give', 'in'],
//                //    ['give', 'up'],
//                //    ['grow', 'up'],
//                //    ['hand', 'in'],
//                //    ['hand', 'out'],
//                //    ['hang', 'out'],
//                //    ['hang', 'up'],
//                //    ['hold', 'on'],
//                //    ['hurry', 'up'],
//                //    ['iron', 'out'],
//                //    ['join', 'in'],
//                //    ['join', 'up'],
//                //    ['keep', 'on'],
//                //    ['keep', 'up with'],
//                //    ['kick', 'off'],
//                //    ['leave', 'out'],
//                //    ['let', 'down'],
//                //    ['look', 'after'],
//                //    ['look', 'down on'],
//                //    ['look', 'on'],
//                //    ['look', 'for'],
//                //    ['look', 'forward to'],
//                //    ['look', 'up to'],
//                //    ['make', 'fun of'],
//                //    ['make', 'up'],
//                //    ['mix', 'up'],
//                //    ['move', 'in'],
//                //    ['move', 'out'],
//                //    ['nod', 'off'],
//                //    ['own', 'up'],
//                //    ['pass', 'away'],
//                //    ['pass', 'out'],
//                //    ['pay', 'back'],
//                //    ['put', 'off'],
//                //    ['put', 'on'],
//                //    ['put', 'out'],
//                //    ['put', 'up'],
//                //    ['pick', 'up'],
//                //    ['point', 'out'],
//                //    ['rely', 'on'],
//                //    ['rule', 'out'],
//                //    ['run', 'away'],
//                //    ['run', 'into'],
//                //    ['run', 'out of'],
//                //    ['set', 'off'],
//                //    ['set', 'up'],
//                //    ['show', 'off'],
//                //    ['show', 'up'],
//                //    ['shut', 'up'],
//                //    ['sit', 'down'],
//                //    ['stand', 'up'],
//                //    ['take', 'after'],
//                //    ['take', 'care of'],
//                //    ['take', 'off'],
//                //    ['take', 'on'],
//                //    ['take', 'out'],
//                //    ['tell', 'off'],
//                //    ['think', 'over'],
//                //    ['try', 'on'],
//                //    ['turn', 'down'],
//                //    ['use', 'up'],
//                //    ['watch', 'out'],
//                //    ['wear', 'out'],
//                //    ['work', 'out'],
//                //    ['wipe', 'off']
//                //]
//            }]
//        });
//    });

//}


//function highchartsDGThree(id, transitions, myNodesJson) {
//    const el = document.getElementById(`${id}`)

//    var myNodes = JSON.parse(myNodesJson);

//    el.addEventListener('click', function () {

//        Highcharts.chart('container2', {

//            chart: {
//                type: 'networkgraph'
//            },

//            plotOptions: {
//                networkgraph: {
//                    layoutAlgorithm: {
//                        enableSimulation: true
//                    }
//                }
//            },

//            series: [{
//                link: {
//                    width: 2
//                },
//                dataLabels: {
//                    enabled: true
//                },
//                data: [{
//                    from: 'Europe',
//                    to: 'UK'
//                }, {
//                    from: 'Europe',
//                    to: 'Poland',
//                    color: 'red',
//                    width: 4,
//                    dashStyle: 'dot'
//                }, {
//                    from: 'Europe',
//                    to: 'Italy'
//                }, {
//                    from: 'UK',
//                    to: 'London'
//                }, {
//                    from: 'UK',
//                    to: 'Bristol'
//                }, {
//                    from: 'London',
//                    to: 'London Centre'
//                }, {
//                    from: 'Poland',
//                    to: 'Warsaw'
//                }, {
//                    from: 'Poland',
//                    to: 'Krakow',
//                    color: 'green'
//                }, {
//                    from: 'Italy',
//                    to: 'Roma'
//                }, {
//                    from: 'Italy',
//                    to: 'Piza'
//                }],
//                nodes: [{
//                    id: 'Krakow',
//                    color: 'orange'
//                }, {
//                    id: 'Italy',
//                    color: 'black'
//                }, {
//                    id: 'Poland',
//                    color: 'black'
//                }]
//            }]

//        });
//    });
//}


//function EchartsGraph(id) {
//    const el = document.getElementById(`${id}`)

//    console.log(`${id}`)

//    // Initialize the echarts instance based on the prepared dom
//    var myChart = echarts.init(document.getElementById(`${id}`));

//    // Specify the configuration items and data for the chart
//    var option = {
//        title: {
//            text: 'ECharts Getting Started Example'
//        },
//        tooltip: {},
//        legend: {
//            data: ['sales']
//        },
//        xAxis: {
//            data: ['Shirts', 'Cardigans', 'Chiffons', 'Pants', 'Heels', 'Socks']
//        },
//        yAxis: {},
//        series: [
//            {
//                name: 'sales',
//                type: 'bar',
//                data: [5, 20, 36, 10, 10, 20]
//            }
//        ]
//    };

//    myChart.setOption(option);
//}


//function barChartInitial(id) {

//    var myChart2 = echarts.init(document.getElementById(`${id}`));



//    var option = {
//        title: {
//            text: 'چارت میله ای',
//        },
//        textStyle: {
//            fontFamily: 'Iranian Sans'
//        },
//        tooltip: {
//            trigger: 'item',
//            axisPointer: {
//                type = 'shadow',
//            },
//            textStyle: {
//                fontFamily = "Iranian Sans",
//            }
//        },
//        legend: {
//            data: [ "جنگل", "کوهستان", "کویر", "دشت" ],
//            textStyle: {
//                fontFamily = "Iranian Sans",
//            }
//        },
//        yAxis: {
//            type: 'value',
//        },
//        xAxis: {
//            type: 'category',
//            boundaryGap: false,
//            data: ["2012", "2013", "2014", "2015", "2016"]
//        },
//        series: [
//            {
//                name: 'جنگل',
//                type: 'bar',
//                label: {
//                    show: true,
//                    position = 'insideBottom',
//                    align : 'center',
//                    rotate: 90,
//                    textStyle: {
//                        fontFamily = "Iranian Sans",
//                    }
//                },
//                data: [320, 332, 301, 334, 390],
//            },
//            {
//                name: 'کوهستان',
//                type: 'bar',
//                label: {
//                    show: true,
//                    position = 'insideBottom',
//                    align : 'center',
//                    rotate: 90,
//                    textStyle: {
//                        fontFamily = "Iranian Sans",
//                    }
//                },
//                data: [220, 182, 191, 234, 290],
//            },
//            {
//                name: 'کویر',
//                type: 'bar',
//                label: {
//                    show: true,
//                    position = 'insideBottom',
//                    align : 'center',
//                    rotate: 90,
//                    textStyle: {
//                        fontFamily = "Iranian Sans",
//                    }
//                },
//                data: [150, 232, 201, 154, 190],
//            },
//            {
//                name: 'دشت',
//                type: 'bar',
//                label: {
//                    show: true,
//                    position = 'insideBottom',
//                    align : 'center',
//                    rotate: 90,
//                    textStyle: {
//                        fontFamily = "Iranian Sans",
//                    }
//                },
//                data: [98, 77, 101, 99, 40],
//            },

//        ]
//    };

//    myChart2.setOption(option);
//};











////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


//function highchartsDG(id, transitions) {
//    const el = document.getElementById(`${id}`)

//    el.addEventListener('click', function () {
//        Highcharts.chart(container2, {
//            chart: {
//                type: 'networkgraph',
//                plotBorderWidth: 1
//            },
//            title: {
//                text: 'Networkgraph initial positions'
//            },
//            plotOptions: {
//                networkgraph: {
//                    keys: ['from', 'to']
//                }
//            },
//            series: [{
//                layoutAlgorithm: {
//                    enableSimulation: true,
//                    initialPositions: function () {
//                        const chart = this.series[0].chart,
//                            width = chart.plotWidth,
//                            height = chart.plotHeight;

//                        this.nodes.forEach(function (node) {
//                            // If initial positions were set previously, use that
//                            // positions. Otherwise use random position:
//                            node.plotX = node.plotX === undefined ?
//                                Math.random() * width : node.plotX;
//                            node.plotY = node.plotY === undefined ?
//                                Math.random() * height : node.plotY;
//                        });
//                    }
//                },
//                name: 'K8',
//                data: transitions,
//            }]
//        });
//    });

//}

//*********************************************************************************************** */
//*********************************************************************************************** */
//*********************************************************************************************** */+












//Highcharts.addEvent(
//    Highcharts.Series,
//    'afterSetOptions',
//    function (e) {

//        const colors = Highcharts.getOptions().colors,
//            nodes = {};

//        let i = 0;

//        if (e.options.id === 'NazmDb-Graph') {
//            e.options.data.forEach(function (link) {
//                if (link[0] === '34567') {
//                    nodes['34567'] = {
//                        id: '34567',
//                        marker: {
//                            radius:40
//                        }
//                    };
//                    nodes[link[1]] = {
//                        id: link[1],
//                        marker: {
//                            radius: 20
//                        },
//                        color: colors[i++]
//                    };
//                } else if (nodes[link[0]] && nodes[link[0]].color) {
//                    nodes[link[1]] = {
//                        id: link[1],
//                        color: nodes[link[0]].color
//                    };
//                }
//            });

//            e.options.nodes = Object.keys(nodes).map(function (id) {
//                return nodes[id];
//            });
//        }
//    }
//);







//*********************************************************************************************** */
//*********************************************************************************************** */
//*********************************************************************************************** */


//function highchartsDG(id, transitions, myNodesJson) {
//    const el = document.getElementById(`${id}`)

//    var myNodes = JSON.parse(myNodesJson);

//    el.addEventListener('click', function () {

//        //Highcharts.addEvent(
//        //    Highcharts.Series,
//        //    'afterSetOptions',
//        //    function (e) {

//        //        const colors = Highcharts.getOptions().colors,
//        //            nodes = {};

//        //        let i = 0;

//        //        if (e.options.id === 'NazmDb-Graph') {
//        //            e.options.data.forEach(function (link) {
//        //                if (link[0] === '34567') {
//        //                    nodes['34567'] = {
//        //                        id: '34567',
//        //                        marker: {
//        //                            radius:40
//        //                        }
//        //                    };
//        //                    nodes[link[1]] = {
//        //                        id: link[1],
//        //                        marker: {
//        //                            radius: 20
//        //                        },
//        //                        color: colors[i++]
//        //                    };
//        //                } else if (nodes[link[0]] && nodes[link[0]].color) {
//        //                    nodes[link[1]] = {
//        //                        id: link[1],
//        //                        color: nodes[link[0]].color
//        //                    };
//        //                }
//        //            });

//        //            e.options.nodes = Object.keys(nodes).map(function (id) {
//        //                return nodes[id];
//        //            });
//        //        }
//        //    }
//        //);


//        Highcharts.chart('container2', {
//            chart: {
//                type: 'networkgraph',
//            },
//            title: {
//                text: 'Network Graph',
//                align: 'left'
//            },
//            subtitle: {
//                text: 'Nazmaran Co.',
//                align: 'left'
//            },

//            plotOptions: {

//                networkgraph: {
//                    keys: ['from', 'to'],
//                    layoutAlgorithm: {
//                        enableSimulation: true,
//                        linkLength: 90,
//                        //friction: -0.9
//                    },
//                    point: {
//                        events: {
//                            click(e) {
//                                handlePointClick(e.point);
//                            }
//                        }
//                    }
//                },
//                series: {
//                    dataLabels: {
//                        enabled: true,
//                        linkFormat: '',
//                        style: {
//                            fontSize: '1em',
//                            fontWeight: 'normal'
//                        }
//                    },
//                }
//            },
//            series: [{
//                tooltip: {
//                    nodeFormatter: function () {
//                        //return `<table><tr><th colspan="2"><h4>${this.id}</h4><hr/></th></tr><tr><td style="color: ${this.marker.fillColor}">${this.id} </td><td style="text-align: right"><b>${this.id} EUR</b></td></tr></table>`;
//                        return `<p style=""><b>${this.title}</b><br></br><b style="color: ${this.marker.fillColor}">${this.marker.radius} Link's</b><br>
//                        ${this.description}</p>`;
//                    },
//                },
//                id: 'NazmDb-Graph',
//                nodes: myNodes,
//                data: transitions,
//            }],
//        });
//    });
//}










