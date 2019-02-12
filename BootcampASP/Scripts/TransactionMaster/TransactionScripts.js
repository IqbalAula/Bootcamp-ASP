var Suppliers = []
//fetch supppliers from database
function LoadSupplier(element) {
    if (Suppliers.length == 0) {
        //ajax function for fetch data
        $.ajax({
            type: "GET",
            url: 'http://localhost:55403/Transactions/getItemSuppliers/',
            success: function (data) {
                Suppliers = data;
                //render suppplier
                renderSupplier(element);
            }
        })
    }
    else {
        //render suppplier to the element
        renderSupplier(element);
    }
}

function renderSupplier(element) {
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select'));
    $.each(Suppliers, function (i, val) {
        $ele.append($('<option/>').val(val.Id).text(val.Name));
    })
}

//fetch items
function LoadItem(Id) {    
    $.ajax({
        type: "GET",
        url: "Transactions/getItems/",
        data: { 'Id': $(Id).val() },
        success: function (data) {
            //render items to appropriate dropdown
            renderItem($(Id).parents('.mycontainer').find('select.item'), data);
        },
        error: function (error) {
            console.log(error);
        }
    })
}

function renderItem(element, data) {
    //render item
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select'));
    $.each(data, function (i, val) {
        $ele.append($('<option/>').val(val.Id).text(val.Name));
    })
}

$(document).ready(function () {
    //Add button click event
    $('#add').click(function () {
        //validation and add transaction items
        var isAllValid = true;
        if ($('#itemSupplier').val() == "0") {
            isAllValid = false;
            $('#itemSupplier').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#itemSupplier').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#item').val() == "0") {
            isAllValid = false;
            $('#item').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#item').siblings('span.error').css('visibility', 'hidden');
        }

        if (!($('#quantity').val().trim() != '' && (parseInt($('#quantity').val()) || 0))) {
            isAllValid = false;
            $('#quantity').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#quantity').siblings('span.error').css('visibility', 'hidden');
        }

        //if (!($('#rate').val().trim() != '' && !isNaN($('#rate').val().trim()))) {
        //    isAllValid = false;
        //    $('#rate').siblings('span.error').css('visibility', 'visible');
        //}
        //else {
        //    $('#rate').siblings('span.error').css('visibility', 'hidden');
        //}

        if (isAllValid) {
            var $newRow = $('#mainrow').clone().removeAttr('id');
            $('.pc', $newRow).val($('#itemSupplier').val());
            $('.item', $newRow).val($('#item').val());

            //Replace add button with remove button
            $('#add', $newRow).addClass('remove').val('Remove').removeClass('btn-success').addClass('btn-danger');

            //remove id attribute from new clone row
            $('#itemSupplier,#item,#quantity,#rate,#add', $newRow).removeAttr('id');
            $('span.error', $newRow).remove();
            //append clone row
            $('#transactiondetailsItems').append($newRow);

            //clear select data
            $('#itemSupplier,#item').val('0');
            $('#quantity,#rate').val('');
            $('#transactionItemError').empty();
        }

    })

    //remove button click event
    $('#transactiondetailsItems').on('click', '.remove', function () {
        $(this).parents('tr').remove();
    });

    $('#submit').click(function () {
        debugger;
        var isAllValid = true;

        //validate transaction items
        $('#transactionItemError').text('');
        var list = [];
        var errorItemCount = 0;
        $('#transactiondetailsItems tbody tr').each(function (index, ele) {
            if (
                $('select.item', this).val() == "0" ||
                (parseInt($('.quantity', this).val()) || 0) == 0 
                //||
                //$('.rate', this).val() == "" ||
                //isNaN($('.rate', this).val())
                ) {
                errorItemCount++;
                $(this).addClass('error');
            } else {
                var transactionItem = {
                    ItemID: $('select.item', this).val(),
                    Quantity: parseInt($('.quantity', this).val())//,
                    //Rate: parseFloat($('.rate', this).val())
                }
                list.push(transactionItem);
            }
        })

        if (errorItemCount > 0) {
            $('#transactionItemError').text(errorItemCount + " invalid entry in transaction item list.");
            isAllValid = false;
        }

        if (list.length == 0) {
            $('#transactionItemError').text('At least 1 transaction item required.');
            isAllValid = false;
        }

        //if ($('#transactionNo').val().trim() == '') {
        //    $('#transactionNo').siblings('span.error').css('visibility', 'visible');
        //    isAllValid = false;
        //}
        //else {
        //    $('#transactionNo').siblings('span.error').css('visibility', 'hidden');
        //}

        if ($('#transactionDate').val().trim() == '') {
            $('#transactionDate').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#transactionDate').siblings('span.error').css('visibility', 'hidden');
        }

        if (isAllValid) {
            var data = {
                //TransactionNo: $('#transactionNo').val().trim(),
                TransactionDateString: $('#transactionDate').val().trim(),
                //Description: $('#description').val().trim(),
                TransactionDetails: list
            }

            $(this).val('Please wait...');

            $.ajax({
                type: 'POST',
                url: '/Transactions/save',
                data: JSON.stringify(data),
                contentType: 'application/json',
                success: function (data) {
                    if (data.status) {
                        alert('Successfully saved');
                        //here we will clear the form
                        list = [];
                        //$('#transactionNo,#transactionDate,#description').val('');
                        $('#transactionDate').val('');
                        $('#transactiondetailsItems').empty();
                    }
                    else {
                        alert('Error');
                    }
                    $('#submit').val('Save');
                },
                error: function (error) {
                    console.log(error);
                    $('#submit').val('Save');
                }
            });
        }

    });

});

LoadSupplier($('#itemSupplier'));