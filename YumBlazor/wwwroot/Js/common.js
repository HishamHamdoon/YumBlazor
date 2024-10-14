window.showToaster = function (type, message) {
    if (type == "success") {
        toastr.success(message);
    }
    if (type == "error") {
        toastr.error(message);
    }
    if (type == "info") {
        toastr.info(message);
    }
    if (type == "warning") {
        toastr.warning(message);
    }
}

function ShowConfimationModal() {
    bootstrap.Modal.getOrCreateInstance(document.getElementById('deleteModal')).show();
}
function HideConfimationModal() {
    bootstrap.Modal.getOrCreateInstance(document.getElementById('deleteModal')).hide();
}