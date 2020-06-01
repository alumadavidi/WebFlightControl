
// center of the map
let center = [51.505, -0.09];

// Create the map
let map = L.map('map').setView(center, 3);

map.on('click', function (e) {
    if (clickedMarker != "undefined") {
        clickedMarker.fire('click');
        clickedMarker = "undefined";
    }
});
// Set up the OSM layer
L.tileLayer(
    'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a>',
    maxZoom: 18
}).addTo(map);

let clickedPlain = L.icon({
    iconUrl: 'https://cdn1.iconfinder.com/data/icons/Map-Markers-Icons-Demo-PNG/256/Map-Marker-Ball-Azure.png',
    iconSize: [25, 25], // size of the icon
});
let plain = L.icon({
    iconUrl: 'https://image.flaticon.com/icons/png/512/9/9890.png',
    iconSize: [25, 25], // size of the icon
});
let markersMap = {}
let clickedMarker = "undefined";
let polyline;
function clearMap() {
    console.log("KK");
    map.eachLayer(function (layer) {
        if (layer instanceof L.Marker) {
            map.removeLayer(layer);
        }
    });
}


function addIconToMap(latitude, longitude, flightId) {
    let marker = L.marker([latitude, longitude], { icon: plain }).addTo(map).on('click', function (e) {
        if (clickedMarker == "undefined") {
            this.setIcon(clickedPlain);
            clickedMarker = this;
            highlightElements(flightId);
            showFlightDetails(flightId, this);

        }
        else if (clickedMarker != marker) {
            clickedMarker.fire('click');
            this.setIcon(clickedPlain);
            clickedMarker = this;
            highlightElements(flightId);
            showFlightDetails(flightId, this);
        } else {
            this.setIcon(plain);
            clickedMarker = "undefined";
            unHighlightElements(flightId);
            map.removeLayer(polyline);
            cleanAndHideDataTable();
        }
    });
    markersMap[flightId] = marker;
}

function deleteMarker(flightId) {
    let marker = markersMap[flightId]
    if (clickedMarker == marker) {
        marker.fire('click');
    } 
    map.removeLayer(marker);

}

function rowClick(event, flight) {
    let id = event.target.id;
    if (id != "trash" && id != "delButton") {
        markersMap[flight.id].fire('click');
    }
}


function updateMrkerLatlng(latitude, longitude, flightId){
    let marker = markersMap[flightId];
    marker.setLatLng([latitude, longitude]);
}
//initial
function drowSegLines(init, segments) {
    let polylinePoints = []
    polylinePoints.push([init.latitude, init.longitude]);
    segments.forEach(function (segnent) {
        polylinePoints.push([segnent.latitude, segnent.longitude]);
    });
    polyline = L.polyline(polylinePoints, { color: 'red' }).addTo(map);
}

function getClickedMarker() {
    return clickedMarker;
}
