
// center of the map
var center = [51.505, -0.09];

// Create the map
var map = L.map('map').setView(center, 3);

// Set up the OSM layer
L.tileLayer(
    'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a>',
    maxZoom: 18
}).addTo(map);

var clickedPlain = L.icon({
    iconUrl: 'https://st2.depositphotos.com/8430356/11390/v/950/depositphotos_113900678-stock-illustration-plain-icon-isolated-on-white.jpg',
    iconSize: [25, 25], // size of the icon
});
var markersMap = new Map()
function clearMap() {
    console.log("KK");
    map.eachLayer(function (layer) {
        if (layer instanceof L.Marker) {
            map.removeLayer(layer);
        }
    });
}

function addIconToMap(latitude, longitude, flight) {
    var plain = L.icon({
        iconUrl: 'https://image.flaticon.com/icons/png/512/9/9890.png',
        iconSize: [25, 25], // size of the icon
    });
    var marker = L.marker([latitude, longitude], { icon: plain }).addTo(map).on('click', function (e) {
        this.setIcon(clickedPlain);
    });
    markersMap.set(flight.flight_id, marker);
}

function deleteMarker(flight) {

    map.removeLayer(markersMap.get(flight.id));
}

