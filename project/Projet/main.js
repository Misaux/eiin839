var view = new ol.View({
    center: ol.proj.fromLonLat([7.0985774, 43.6365619]), // <-- Those are the GPS coordinates to center the map to.
    zoom: 10 // You can adjust the default zoom.
});

var map = new ol.Map({
    target: 'map', // <-- This is the id of the div in which the map will be built.
    layers: [
        new ol.layer.Tile({
            source: new ol.source.OSM()
        })
    ],

    view: view,

});

var geolocation = new ol.Geolocation({
    // enableHighAccuracy must be set to true to have the heading value.
    trackingOptions: {
        enableHighAccuracy: true,
    },
    projection: view.getProjection(),
});

var positionFeature = new ol.Feature();
positionFeature.setStyle(
    new ol.style.Style({
        image: new ol.style.Circle({
            radius: 6,
            fill: new ol.style.Fill({
                color: '#3399CC',
            }),
            stroke: new ol.style.Stroke({
                color: '#fff',
                width: 2,
            }),
        }),
    })
);

var accuracyFeature = new ol.Feature();
geolocation.on('change:accuracyGeometry', function () {
    accuracyFeature.setGeometry(geolocation.getAccuracyGeometry());
});

var flag = false;

geolocation.on('change:position', function () {
    var coordinates = geolocation.getPosition();
    positionFeature.setGeometry(coordinates ? new ol.geom.Point(coordinates) : null);

    if (!flag) {
        view.centerOn(coordinates, map.getSize(), [map.getSize()[0] / 2, map.getSize()[1] / 2]);
        view.setZoom(15);


        var geocord = ol.proj.transform(coordinates, 'EPSG:3857', 'EPSG:4326');
        var geocode = "https://nominatim.openstreetmap.org/reverse?format=json&lat=" + geocord[1] + "&lon=" + geocord[0] + "&zoom=18&addressdetails=1";
        $.getJSON(geocode, function (data) {
            document.getElementById('start').value = data.address.road + ", " + data.address.town;
        });

        flag = true;
    }
});

new ol.layer.Vector({
    map: map,
    source: new ol.source.Vector({
        features: [accuracyFeature, positionFeature],
    }),
});

geolocation.setTracking(true);

document.getElementById('draw_path').onclick = function () {
    // Create an array containing the GPS positions you want to draw

    var start = "https://nominatim.openstreetmap.org/?addressdetails=1&q=" + document.getElementById('start').value + "&format=json&limit=1";
    start = encodeURI(start);
    var startcoords;
    $.getJSON(start, function (data) {
        startcoords = [data[0].lat, data[0].lon];
    }).then(function () {
        var end = "https://nominatim.openstreetmap.org/?addressdetails=1&q=" + document.getElementById('end').value + "&format=json&limit=1";
        var endcoords;
        end = encodeURI(end);
        $.getJSON(end, function (data) {
            endcoords = [data[0].lat, data[0].lon];
        }).then(function () {
            $.ajax({
                type: "GET",
                url: "http://localhost:8733/RoutingWithBikes/RoutingWithBikes/rest/FullPath?start_lat=" + startcoords[0] + "&start_lng=" + startcoords[1] + "&end_lat=" + endcoords[0] + "&end_lng=" + endcoords[1],
                dataType: "xml",
                success: function (data) {

                    var points = $(data).find('GetFullPathResult')[0].childNodes[0].childNodes;

                    for (i = 1; i < points.length; i++) {

                        var coords = [[points[i - 1].childNodes[0].childNodes[0].nodeValue, points[i - 1].childNodes[1].childNodes[0].nodeValue], [points[i].childNodes[0].childNodes[0].nodeValue, points[i].childNodes[1].childNodes[0].nodeValue]];
                        var lineString = new ol.geom.LineString(coords);

                        // Transform to EPSG:3857
                        lineString.transform('EPSG:4326', 'EPSG:3857');

                        // Create the feature
                        var feature = new ol.Feature({
                            geometry: lineString,
                            name: 'Line'
                        });

                        // Configure the style of the line
                        var lineStyle = new ol.style.Style({
                            stroke: new ol.style.Stroke({
                                color: '#ffcc33',
                                width: 10
                            })
                        });

                        var source = new ol.source.Vector({
                            features: [feature]
                        });

                        var vector = new ol.layer.Vector({
                            source: source,
                            style: [lineStyle]
                        });

                        map.addLayer(vector);
                    }

                    var points = $(data).find('GetFullPathResult')[0].childNodes[1].childNodes;

                    for (i = 1; i < points.length; i++) {

                        var coords = [[points[i - 1].childNodes[0].childNodes[0].nodeValue, points[i - 1].childNodes[1].childNodes[0].nodeValue], [points[i].childNodes[0].childNodes[0].nodeValue, points[i].childNodes[1].childNodes[0].nodeValue]];
                        var lineString = new ol.geom.LineString(coords);

                        // Transform to EPSG:3857
                        lineString.transform('EPSG:4326', 'EPSG:3857');

                        // Create the feature
                        var feature = new ol.Feature({
                            geometry: lineString,
                            name: 'Line'
                        });

                        // Configure the style of the line
                        var lineStyle = new ol.style.Style({
                            stroke: new ol.style.Stroke({
                                color: '#9D1919',
                                width: 10
                            })
                        });

                        var source = new ol.source.Vector({
                            features: [feature]
                        });

                        var vector = new ol.layer.Vector({
                            source: source,
                            style: [lineStyle]
                        });

                        map.addLayer(vector);
                    }

                    var points = $(data).find('GetFullPathResult')[0].childNodes[2].childNodes;

                    for (i = 1; i < points.length; i++) {

                        var coords = [[points[i - 1].childNodes[0].childNodes[0].nodeValue, points[i - 1].childNodes[1].childNodes[0].nodeValue], [points[i].childNodes[0].childNodes[0].nodeValue, points[i].childNodes[1].childNodes[0].nodeValue]];
                        var lineString = new ol.geom.LineString(coords);

                        // Transform to EPSG:3857
                        lineString.transform('EPSG:4326', 'EPSG:3857');

                        // Create the feature
                        var feature = new ol.Feature({
                            geometry: lineString,
                            name: 'Line'
                        });

                        // Configure the style of the line
                        var lineStyle = new ol.style.Style({
                            stroke: new ol.style.Stroke({
                                color: '#782FDA',
                                width: 10
                            })
                        });

                        var source = new ol.source.Vector({
                            features: [feature]
                        });

                        var vector = new ol.layer.Vector({
                            source: source,
                            style: [lineStyle]
                        });

                        map.addLayer(vector);
                    }
                }
            });

        });
    });

};