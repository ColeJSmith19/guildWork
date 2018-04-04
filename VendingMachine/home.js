$(document).ready(function () {
    loadItems();


    $('#purchase-button').click(function (event) {
        var purchaseTotal = $('#amountOutput').val();
        var purchaseId = $('#ItemInput').val();
        // if(!purchaseTotal){
        //     purchaseTotal = 0;
        // }
        $.ajax({
            type: 'GET',
            url: 'http://localhost:8080/money/' + purchaseTotal + '/item/' + purchaseId,
            success: function (data, status) {
              
                var change = 0;
                var quarters = data.quarters;
                var dimes = data.dimes;
                var nickels = data.nickels;
                change = (quarters * .25) + (dimes * .1) + (nickels * .05)

                if (quarters > 0) {
                    $('#changeQuarters').val(quarters + ' quarter(s)');
                }

                if (dimes > 0) {
                    $('#changeDimes').val(dimes + ' dime(s)');
                }

                if (nickels > 0) {
                    $('#changeNickels').val(nickels + 'nickel(s)');
                }

                if (change > 0) {
                    $('#changeOutput').val('$' + change.toFixed(2));
                }

                $('#amountOutput').val("");
                $('#messageOutput').val("Thank you!");
                loadItems();
            },
            error: function (data, status) {
                $('#messageOutput').val(data.responseJSON.message)
            }
        });
    });

    $('#change-button').click(function (event) {
        newAmount = 0;
        $('#changeOutput').val("");
        $('#changeQuarters').val("");
        $('#changeDimes').val("");
        $('#changeNickels').val("");
        $('#amountOutput').val("");
        $('#messageOutput').val("");
        $('#ItemInput').val("");
    });

})


function loadItems() {
    clearItemsRow();
    var itemRow = $('#itemRows');

    $.ajax({
        type: 'GET',
        url: 'http://localhost:8080/items',
        success: function (itemArray) {
            allItems = itemArray;
            $.each(itemArray, function (index, items) {
                var id = items.id;
                var name = items.name;
                var price = items.price;
                var quantity = items.quantity;

                var row = '<button class="col-sm-4" onclick="fillPurchaseOutput(' + id + ')">';
                row += id;
                row += '<br/>';
                row += name;
                row += '<br/>';
                row += '$' + price.toFixed(2);
                row += '<br/>';
                row += 'Quantity Left:' + quantity;
                row += '</button>';

                itemRow.append(row);
            });
        },
        error: function () {
            $('#errorMessages')
                .append($('<li>')
                    .attr({ class: 'list-group-item list-group-item-danger' })
                    .text('Error calling web service. Please try again later.'));
        }
    });
}

var newAmount = 0;
function fillPurchaseOutput(id) {
    $('#ItemInput').val(id);
}
function fillTotalOutput(amount) {
    newAmount += amount;
    $('#amountOutput').val(newAmount.toFixed(2));
}
function addDollar(amount) {
    fillTotalOutput(amount);
}
function addQuarter(amount) {
    fillTotalOutput(amount);
}
function addDime(amount) {
    fillTotalOutput(amount);
}
function addNickel(amount) {
    fillTotalOutput(amount);
}
function DisplayChange(change) {
    $('#changeOutput').val(change);
}
function clearItemsRow() {
    $('#itemRows').empty();
}