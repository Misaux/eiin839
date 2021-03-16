var stations;


function retrieveAllContracts() {
    var targetUrl = "https://api.jcdecaux.com/vls/v3/contracts?apiKey=" + document.getElementById("apiKey").value; //64346e2dbae9c6364bd75b2ee766c8e33623b537
    var requestType = "GET";

    var caller = new XMLHttpRequest();
    caller.open(requestType, targetUrl, true);
    caller.setRequestHeader("Accept", "application/json");
    caller.onload = contractsRetrieved;

    caller.send();
}

function contractsRetrieved() {
    var response = JSON.parse(this.responseText);
    response.forEach(contract => {
        var option = document.createElement("option");
        option.setAttribute("value", contract.name);
        document.getElementById("list").appendChild(option);
    });
}

function retrieveContractStations(){
    var targetUrl="https://api.jcdecaux.com/vls/v1/stations?contract=" + document.getElementById("contracts_list").value + "&apiKey=" + document.getElementById("apiKey").value;
    var requestType = "GET";

    var caller = new XMLHttpRequest();
    caller.open(requestType, targetUrl, true);
    caller.setRequestHeader("Accept", "application/json");
    caller.onload = contractStationRetrieved;

    caller.send();
}

function contractStationRetrieved(){
    stations=JSON.parse(this.responseText);
    console.log(stations);
}

function getDistanceFrom2GpsCoordinates(lat1, lon1, lat2, lon2) {
    // Radius of the earth in km
    var earthRadius = 6371;
    var dLat = deg2rad(lat2-lat1);
    var dLon = deg2rad(lon2-lon1);
    var a =
        Math.sin(dLat/2) * Math.sin(dLat/2) +
        Math.cos(deg2rad(lat1)) * Math.cos(deg2rad(lat2)) *
        Math.sin(dLon/2) * Math.sin(dLon/2)
    ;
    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1-a));
    var d = earthRadius * c; // Distance in km
    return d;
}

function deg2rad(deg) {
    return deg * (Math.PI/180)
}


function getClosestStation() {
    var latitude = document.getElementById("lat").value;
    var longitude = document.getElementById("lng").value;

    var nearest = stations[0].name;
    var bestDistance = getDistanceFrom2GpsCoordinates(latitude, longitude, stations[0].position.lat, stations[0].position.lng);

    stations.forEach(station => {
        var currentDistance = getDistanceFrom2GpsCoordinates(latitude, longitude, station.position.lat, station.position.lng);
        if (currentDistance < bestDistance) {
            bestDistance = currentDistance;
            nearest = station.name;
        }
    })

    console.log(nearest);
}