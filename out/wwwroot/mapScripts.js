// center of the map
let center = [51.505, -0.09];

// Create the map
let map = L.map('map').setView(center, 3);

//if there is a clicked flight cancle it when map clicked
map.on('click', function () {
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

//set icon to plane
let plain = L.icon({
    iconUrl: 'https://image.flaticon.com/icons/png/512/9/9890.png',
    iconSize: [25, 25], // size of the icon
});

//set icon to clicked plane
let clickedPlane = L.icon({
    iconUrl: 'https://cdn1.iconfinder.com/data/icons/Map-Markers-Icons-Demo-PNG/256/Map-Marker-Ball-Azure.png',
    iconSize: [40, 40], // size of the icon
});

//Map for all markers in map by id of flight
let markersMap = {}
//var to hold the clicked marker (if undefined - no clicked marker)
let clickedMarker = "undefined";
//lines of segment for flight plan
let polyline;
// add icon of flight to map
function addIconToMap(latitude, longitude, flightId) {
    let marker = L.marker([latitude, longitude], { icon: plain }).addTo(map).on('click', function () {
        //if there is no clicked marker
        if (clickedMarker == "undefined") {
            this.setIcon(clickedPlane);
            clickedMarker = this;
            highlightElements(flightId);
            showFlightDetails(flightId, this);
        }
        //there is clicked other marker
        else if (clickedMarker != marker) {
            clickedMarker.fire('click');
            this.setIcon(clickedPlane);
            clickedMarker = this;
            highlightElements(flightId);
            showFlightDetails(flightId, this);
        }
        //this marker is clicked
        else
        {
            this.setIcon(plain);
            clickedMarker = "undefined";
            unHighlightElements(flightId);
            map.removeLayer(polyline);
            cleanAndHideDataTable();
        }
    });
    //add marker to map as value and id flight as key
    markersMap[flightId] = marker;
}
//delete marker from map
function deleteMarker(flightId) {
    let marker = markersMap[flightId];
    //if it's clicked unclick it and then delete
    if (clickedMarker == marker) {
        marker.fire('click');
    } 
    map.removeLayer(marker);
}
//when click on row do the same as click on it's marker
function rowClick(event, flight) {
    let id = event.target.id;
    //if the click is on delete buton don't click
    if (id != "trash" && id != "delButton") {
        markersMap[flight.id].fire('click');
    }
}
//move the marker to the cur latlng
function updateMrkerLatlng(latitude, longitude, flightId){
    let marker = markersMap[flightId];
    marker.setLatLng([latitude, longitude]);
}
//Drow segments lines on map
function drowSegLines(init, segments) {
    let polylinePoints = []
    polylinePoints.push([init.latitude, init.longitude]);
    segments.forEach(function (segnent) {
        polylinePoints.push([segnent.latitude, segnent.longitude]);
    });
    polyline = L.polyline(polylinePoints, { color: 'red' }).addTo(map);
}
//get the clicked marker
function getClickedMarker() {
    return clickedMarker;
}
