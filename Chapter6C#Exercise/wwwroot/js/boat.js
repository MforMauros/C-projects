function confirmDelete(boatId) {
    if (confirm("Are you sure you want to delete this boat?")) {
        window.location.href = "/Boats/" + boatId + "/Delete"
    }
}