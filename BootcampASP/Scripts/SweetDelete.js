function deleteItem(Id) {
    debugger;
    swal({
        title: "Are you sure?",
        text: "Are you sure that you want to delete this Order?",
        type: "warning",
        showCancelButton: true,
        closeOnConfirm: false,
        confirmButtonText: "Yes, delete it!",
        confirmButtonColor: "#ec6c62"
    }, function () {
        $.ajax({
            url: "http://localhost:55403/Items/Delete/",
            data: { "Id": Id },
            type: "POST",
            success: function (response) {
                swal({
                    title: "Deleted",
                    text: "Items is deleted!",
                    type: "success"
                },
                function () {
                    window.location.href = '/Items/Index/';
                });
            },
            error: function (response) {
                swal("Oopss", "We couldn't connect to the server", "error")
            }
        });
    });
}