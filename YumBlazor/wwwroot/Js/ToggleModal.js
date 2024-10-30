function ShowConfimationModal()
{
    bootstrap.Modal.getOrCreateInstance(document.getElementById('deleteModal').show());
}
function HideConfimationModal() {
    bootstrap.Modal.getOrCreateInstance(document.getElementById('deleteModal').hide());
}