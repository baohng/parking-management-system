document.addEventListener('DOMContentLoaded', function () {
    changeUrl();
});

function changeUrl() {
    // select all elements with class "parking-space"
    const parkingSpaces = document.querySelectorAll('.seat');

    // add a click event listener to each parking space element
    parkingSpaces.forEach(parkingSpace => {
        parkingSpace.addEventListener('click', () => {
            // get the "parking-space-id" attribute of the clicked element
            const parkingSpaceId = parkingSpace.getAttribute('parking-space-id');

            // now you can use this ID to construct the URL for creating a reservation
            const url = '/Reservations/Create?parkingSpaceId=' + parkingSpaceId;
            window.location.href = url;
        });
    });
}