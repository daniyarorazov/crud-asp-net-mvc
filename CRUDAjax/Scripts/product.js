$(document).ready(function () {
    loadData();
});

function loadData() {
    $.ajax({
        url: "/Home/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {     
                html += '<div class="col">';
                html += '<div class="card">';
                html += '<div class="card-body">';
                html += '<h5 class="card-title">' + item.Name + '</h5>';
                html += '<span class="card-subtitle"><b>Category:</b> ' + item.Category + '</span>';
                html += '<br />';
                html += '<span class="card-subtitle"><b>Price:</b> ' + item.Price + ' Kč</span>';
                html += '<div class="card-group">';
                html += '<a href="#" onclick="return getbyID(' + item.ProductId + ')" class="btn btn-warning w-100 mt-2">Edit</a>';
                html += '<a href="#" onclick="Delele(' + item.ProductId + ')" class="btn btn-danger w-100  mt-2">Delete</a>';
                html += '</div>';
                html += '</div>';
                html += '</div>';
                html += '</div>';
            });
            $('#cardsBlock').html(html);
        },
        error: function (errormessage) {
            console.log(errormessage.responseText);
        }
    });
}

function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var prdObj = {
        ProductId: $('#ProductId').val(),
        Name: $('#Name').val(),
        Category: $('#Category').val(),
        Price: $('#Price').val(),
    };
    $.ajax({
        url: "/Home/Add",
        data: JSON.stringify(prdObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function getbyID(PrdID) {
    $.ajax({
        url: "/Home/getbyID/" + PrdID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#ProductId').val(result.ProductId);
            $('#Name').val(result.Name);
            $('#Category').val(result.Category);
            $('#Price').val(result.Price);

            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var prdObj = {
        ProductId: $('#ProductId').val(),
        Name: $('#Name').val(),
        Category: $('#Category').val(),
        Price: $('#Price').val(),
    };
    $.ajax({
        url: "/Home/Update",
        data: JSON.stringify(prdObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#ProductId').val("");
            $('#Name').val("");
            $('#Category').val("");
            $('#Price').val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function Delele(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/Home/Delete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

function clearTextBox() {
    $('#ProductId').val("");
    $('#Name').val("");
    $('#Category').val("");
    $('#Price').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#Name').css('border-color', 'lightgrey');
    $('#Category').css('border-color', 'lightgrey');
    $('#Price').css('border-color', 'lightgrey');
}
function validate() {
    var isValid = true;
    if ($('#Name').val().trim() == "") {
        $('#Name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Name').css('border-color', 'lightgrey');
    }
    if ($('#Category').val().trim() == "") {
        $('#Category').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Category').css('border-color', 'lightgrey');
    }
    if ($('#Price').val().trim() == "") {
        $('#Price').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Price').css('border-color', 'lightgrey');
    }
   
    return isValid;
}  
