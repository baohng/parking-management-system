function changeUrl() {
    // Get the parking space ID from the clicked div
    var parkingSpaceId = $(this).attr('parking-space-id');

    // Do something with the parking space ID, like passing it to a URL
    var url = '/Reservations/Create?parkingSpaceId=' + parkingSpaceId;
    window.location.href = url;
}