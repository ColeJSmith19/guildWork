$(document).ready(function () {

    loadDvds();

    $('#add-button').click(function (event) {

        var haveValidationErrors = checkAndDisplayValidationErrors($('#add-form').find('input'));

        if (haveValidationErrors) {
            return false;
        }

        $.ajax({
            type: 'POST',
            url: 'http://localhost:57688/dvd',
            data: JSON.stringify({
                Title: $('#add-title').val(),
                ReleaseYear: $('#add-year').val(),
                Director: $('#add-director').val(),
                Rating: $('#add-rating').val(),
                Notes: $('#add-notes').val()
            }),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            'dataType': 'json',
            success: function () {
                $('#errorMessages').empty();
                $('#add-title').val('');
                $('#add-year').val('');
                $('#add-director').val('');
                $('#add-rating').val('');
                $('#add-notes').val('');
                loadDvds();
            },
            error: function () {
                $('#errorMessages')
                    .append($('<li>')
                        .attr({ class: 'list-group-item list-group-item-danger' })
                        .text('Error calling web service. Please try again later.'));
            }
        })

    });

    $('#search-button').click(function (event) {
        var searchRow = $('#searchRows');
        var searchBy = $('#searchCategoryDropdown').val();
        var searchFor = $('#searchTermTextbox').val();
        var searchResults = searchDvds(searchBy, searchFor);
        $.each(searchResults, function (index, dvd) {
            var title = dvd.Title;
            var releaseDate = dvd.ReleaseYear;
            var director = dvd.Director;
            var rating = dvd.Rating;
            var dvdId = dvd.DvdID;
            var row = '<tr>';
            row += '<td>' + title + '</td>';
            row += '<td>' + releaseDate + '</td>';
            row += '<td>' + director + '</td>';
            row += '<td>' + rating + '</td>';
            row += '</tr>';
            searchRow.append(row);
        });
        showSearchForm();
    });

    $('#close-search-button').click(function (event) {
        clearSearchTable();
        hideSearchForm();
    });

    $('#edit-button').click(function (event) {

        var haveValidationErrors = checkAndDisplayValidationErrors($('#edit-form').find('input'));

        if (haveValidationErrors) {
            return false;
        }

        $.ajax({
            type: 'PUT',
            url: 'http://localhost:57688/dvd/' + $('#edit-dvd-id').val(),
            data: JSON.stringify({
                DvdID: $('#edit-dvd-id').val(),
                Title: $('#edit-title').val(),
                ReleaseYear: $('#edit-year').val(),
                Director: $('#edit-director').val(),
                Rating: $('#edit-rating').val(),
                Notes: $('#edit-notes').val()
            }),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            'dataType': 'json',
            success: function () {
                $('#errorMessages').empty();
                hideEditForm();
                loadDvds();
            },
            error: function () {
                $('#errorMessages')
                    .append($('<li>')
                        .attr({ class: 'list-group-item list-group-item-danger' })
                        .text('Error calling web service. Please try again later.'));
            }
        })
    });
});

var allDvds = [];

function loadDvds() {
    clearDVDTable();
    var dvdRow = $('#dvdRows');

    $.ajax({
        type: 'GET',
        url: 'http://localhost:57688/dvds',
        success: function (dvdArray) {
            allDvds = dvdArray;
            $.each(dvdArray, function (index, dvd) {
                var title = dvd.Title;
                var releaseDate = dvd.ReleaseYear;
                var director = dvd.Director;
                var rating = dvd.Rating;
                var dvdId = dvd.DvdID;
                var row = '<tr>';
                row += '<td>' + title + '</td>';
                row += '<td>' + releaseDate + '</td>';
                row += '<td>' + director + '</td>';
                row += '<td>' + rating + '</td>';
                row += '<td><a onclick="showEditForm(' + dvdId + ')">Edit</a></td>';
                row += '<td><a onclick="deleteDvd(' + dvdId + ')">Delete</a></td>';
                row += '</tr>';

                dvdRow.append(row);
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

function clearDVDTable() {
    $('#dvdRows').empty();
}
function clearSearchTable() {
    $('#searchRows').empty();
}

function hideEditForm() {
    $('#errorMessages').empty();

    $('#edit-title').val('');
    $('#edit-year').val('');
    $('#edit-director').val('');
    $('#edit-rating').val('');
    $('#edit-notes').val('');


    $('#editDvdDiv').hide();
    $('#dvdTableDiv').show();
}
function showEditForm(dvdId) {
    $('#errorMessages').empty();

    $.ajax({
        type: 'GET',
        url: 'http://localhost:57688/dvd/' + dvdId,
        success: function (data, status) {
            $('#edit-title').val(data.Title);
            $('#edit-year').val(data.ReleaseYear)
            $('#edit-director').val(data.Director)
            $('#edit-rating').val(data.Rating)
            $('#edit-notes').val(data.Notes)
            $('#edit-dvd-id').val(data.DvdID);
        },
        error: function () {
            $('#errorMessages')
                .append($('<li>')
                    .attr({ class: 'list-group-item list-group-item-danger' })
                    .text('Error calling web service. Please try again later.'));
        }
    });

    $('#dvdTableDiv').hide();
    $('#editDvdDiv').show();
}
function hideAddForm() {
    $('#errorMessages').empty();

    $('#addDvdDiv').hide();
    $('#dvdTableDiv').show();
}
function showAddForm() {
    $('#errorMessages').empty();

    $('#dvdTableDiv').hide();
    $('#addDvdDiv').show();
}
function hideSearchForm() {
    $('#errorMessages').empty();

    $('#dvdTableDiv').show();
    $('#searchDiv').hide();
}
function showSearchForm() {
    // $('#errorMessages').empty();

    $('#searchDiv').show();
    $('#dvdTableDiv').hide();
}
function deleteDvd(dvdId) {
    var confirmation = confirm("Are you sure you want to delete?")
    if (confirmation) {
        $.ajax({
            type: 'DELETE',
            url: 'http://localhost:57688/dvd/' + dvdId,
            success: function () {
                loadDvds();
            }
        });
    }
}
function checkAndDisplayValidationErrors(input) {
    $('#errorMessages').empty();

    var errorMessages = [];

    input.each(function () {
        if (!this.validity.valid) {
            var errorField = $('label[for=' + this.id + ']').text();
            errorMessages.push(errorField + ' ' + this.validationMessage);
        }
    });

    if (errorMessages.length > 0) {
        $.each(errorMessages, function (index, message) {
            $('#errorMessages').append($('<li>').attr({ class: 'list-group-item list-group-item-danger' }).text(message));
        });
        return true;
    } else {
        return false;
    }
}
function searchDvds(searchBy, searchFor) {
    var searchResults = [];
    allDvds.forEach(function (key) {
        console.log(key[searchBy]);
        console.log(allDvds);
        if (key[searchBy] == searchFor) {
            searchResults.push(key)
        }
    })
    return searchResults;
}