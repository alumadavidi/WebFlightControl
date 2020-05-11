/* Events fired on the drop target */
function allowDrop(event) {
    event.preventDefault();
    var img = document.getElementById("draganddropimage");
    if (window.getComputedStyle(img).visibility === "hidden") {
        img.style.visibility = "visible";
    }
    $("#flightlist").hide();
    $("#draganddropimage").show();
}

function dragLeave(event) {
    $("#draganddropimage").hide();
    $("#flightlist").show();
}

function drop(event) {
    event.preventDefault();
    $("#draganddropimage").hide();
    console.log(event.dataTransfer);
    $("#flightlist").show();

}