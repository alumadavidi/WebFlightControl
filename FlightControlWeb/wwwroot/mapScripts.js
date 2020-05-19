
// center of the map
var center = [51.505, -0.09];

// Create the map
var map = L.map('map').setView(center, 3);

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

var clickedPlain = L.icon({
    iconUrl: 'https://cdn1.iconfinder.com/data/icons/Map-Markers-Icons-Demo-PNG/256/Map-Marker-Ball-Azure.png',
    iconSize: [25, 25], // size of the icon
});
var plain = L.icon({
    iconUrl: 'https://image.flaticon.com/icons/png/512/9/9890.png',
    iconSize: [25, 25], // size of the icon
});
var markersMap = {}
var clickedMarker = "undefined";
var polyline;
function clearMap() {
    console.log("KK");
    map.eachLayer(function (layer) {
        if (layer instanceof L.Marker) {
            map.removeLayer(layer);
        }
    });
}


function addIconToMap(latitude, longitude, flightId) {
    var marker = L.marker([latitude, longitude], { icon: plain }).addTo(map).on('click', function (e) {
        if (clickedMarker == "undefined") {
            this.setIcon(clickedPlain);
            clickedMarker = this;
            highlightElements(flightId);
            showFlightDetails(flightId);

        }
        else if (clickedMarker != marker) {
            clickedMarker.fire('click');
            this.setIcon(clickedPlain);
            clickedMarker = this;
            highlightElements(flightId);
            showFlightDetails(flightId);

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
    var marker = markersMap[flightId]
    if (clickedMarker == marker) {
        marker.fire('click');
    } 
    map.removeLayer(marker);

}

function rowClick(event, flight) {
    console.log(event.target);
    var id = event.target.id;
    if (id != "trash" && id != "delButton") {
        markersMap[flight.id].fire('click');
    }
}


function updateMrkerLatlng(latitude, longitude, flightId){
    var marker = markersMap[flightId];
    marker.setLatLng([latitude, longitude]);
}
//initial
function drowSegLines(segments) {
    var polylinePoints = []
    segments.forEach(function (segnent) {
        polylinePoints.push([segnent.latitude, segnent.longitude]);
    });
    polyline = L.polyline(polylinePoints).addTo(map);
}
