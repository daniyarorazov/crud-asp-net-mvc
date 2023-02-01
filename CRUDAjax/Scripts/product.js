$(document).ready(function () {
    // Load data when page is loaded
    loadData();
});

// Create a function, for divide price number ex. from 10000 -> 10 000
function divideWithSpace(num) {
    let numString = num.toString();
    let dividedString = '';

    for (let i = numString.length - 1; i >= 0; i--) {
        dividedString = numString[i] + dividedString;
        if ((numString.length - i) % 3 === 0 && i !== 0) {
            dividedString = ' ' + dividedString;
        }
    }

    return dividedString;
}

// Load data of products from database using ajax query
function loadData() {
    $.ajax({
        // Url, from we get data in json format
        url: "/Home/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        // If success query
        success: function (result) {
            var html = '';        
            // Create html block and put data for fields
            $.each(result, function (key, item) {     
                html += '<div class="col">';
                html += '<div class="card">';
                html += '<div class="card-body">';
                html += '<img class="card-img w-5 h-2" src="/Data/images/'  + item.ImageName + '" />';
                html += '<h5 class="card-title">' + item.Name + '</h5>';
                html += '<span class="card-subtitle"><b>Category:</b> ' + item.Category + '</span>';
                html += '<br />';
                html += '<span class="card-subtitle"><b>Price:</b> ' + divideWithSpace(item.Price) + ' Kč</span>';
                html += '<div class="card-group">';
                html += '<a href="#" onclick="return getbyID(' + item.ProductId + ')" class="btn btn-warning w-100 mt-2">Edit</a>';
                html += '<button value="Delete"  onclick="Delele(' + item.ProductId + ')" class="btn btn-danger w-100  mt-2">Delete</a>';
                html += '</div>';
                html += '</div>';
                html += '</div>';
                html += '</div>';
            });
            // Put html code to div block with id #cardsBlock
            $('#cardsBlock').html(html);
        },
        // If error, return an error to the console
        error: function (errormessage) {
            console.log(errormessage.responseText);
        }
    });
}

// Function of adding new product
function Add() {
    // Get the result of the validate function
    var res = validate();
    var formData = new FormData();
    // Get the first file from the input with ID "Image"
    var file = document.getElementById("Image").files[0];
    // Add the file to the form data with the key "file"
    formData.append("file", file);
    // If the result of the validate function is false, return false
    if (res == false) {
        return false;
    }
    // Getting full path to Image, remove all useless elements from path and get normal name of image
    var fullPath = $('#Image').val();
    var pathElements = fullPath.split("\\");
    var imageName = pathElements[pathElements.length - 1];

    // Create product object with value of product
    var prdObj = {
        ProductId: $('#ProductId').val(),
        Name: $('#Name').val(),
        Category: $('#Category').val(),
        Price: $('#Price').val(),
        ImageName: imageName,
    };

    $.ajax({
        url: "/Home/Add",
        data: JSON.stringify(prdObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        // If success, load data in the page, hide modal
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            HandleFormSubmit();
        },
        // If error, returns error to console
        error: function (errormessage) {
            console.log(errormessage.responseText);
        }
    });

}

// Function of getting specific product by id
function getbyID(PrdID) {
    $.ajax({
        url: "/Home/getbyID/" + PrdID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        // If success, for fields we put info about product
        success: function (result) {
            $('#ProductId').val(result.ProductId);
            $('#Name').val(result.Name);
            $('#Category').val(result.Category);
            $('#Price').val(result.Price);
            
            // Show modal
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        // If error, returns error to console
        error: function (errormessage) {
            console.log(errormessage.responseText);
        }
    });
    return false;
}

// Function for updating product info
function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    // Getting full path to Image, remove all useless elements from path and get normal name of image
    var fullPath = $('#Image').val();
    var pathElements = fullPath.split("\\");
    var imageName = pathElements[pathElements.length - 1];

    // Creating product object with info
    var prdObj = {
        ProductId: $('#ProductId').val(),
        Name: $('#Name').val(),
        Category: $('#Category').val(),
        Price: $('#Price').val(),
        ImageName: imageName,
    };

    // Ajax query for updating info
    $.ajax({
        url: "/Home/Update",
        data: JSON.stringify(prdObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        // If success, load data in the home page and close modal
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#ProductId').val("");
            $('#Name').val("");
            $('#Category').val("");
            $('#Price').val("");
            HandleFormSubmit();
        },
        // If error, returns to console error
        error: function (errormessage) {
            console.log(errormessage.responseText);
        }
    });
}


// Function for deleting product with a specific ID Product usign ajax query
function Delele(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/Home/Delete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            // If succes, show new Data list in the page
            success: function (result) {
                loadData();
            },
            // If error, returns error for console
            error: function (errormessage) {
                console.log(errormessage.responseText);
            }
        });

    }
}

// This function is designed to clear all fields after closing the modal window
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

// Validate function for validate forms. If field will be empty, with errors, the field will turn red
function validate() {
    var isValid = true;
    // If #Name field is empty
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
