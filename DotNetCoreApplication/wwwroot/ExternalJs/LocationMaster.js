$(document).ready(function () {
    PopulateLocationTable();
});
function SaveLocation() {
    var LocName = $("#LocationName").val();
    var LocDesc = $("#Description").val();
    var LocId = $("#LocationId").val();
    if (LocName == "") {
        alert("Please enter location name")
    }
    else {
        $.ajax({
            url: "/Location/SaveLocation",
            type: "POST",
            dataType: "JSON",
            contentType: 'application/x-www-form-urlencoded; charset=utf-8',
            data: {
                LocationName: LocName,
                Description: LocDesc,
                LocationId: LocId
            },
            success: function (data) {
                if (data != null) {
                    alert(data);
                    PopulateLocationTable();
                    ClearLocationForm();
                }
                else {
                    alert(data);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert('Error Fetching!');
            }
        });
    }
}
function PopulateLocationTable() {
    $.ajax({
        url: "/Location/GetAllLocation",
        type: "POST",
        dataType: "JSON",
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: '{EditId:""}',
        success: function (data) {
            if (data.length != 0) {
                $("#LocBody").html("");
                $.each(data, function (index, value) {
                    var ss = "'";
                    var _string = '<tr><td>' + value.locationName + '</td><td>' + value.description + '</td>';
                    _string += '<td><a onclick="EditLocationDetails(' + ss + value.locationId + ss + ')"><i class="fa fa-edit">Edit</i></a>&nbsp;&nbsp;';
                    _string += '<a onclick="DeleteLocationDetails(' + ss + value.locationId + ss + ')"><i class="fa fa-remove">Delete</i></a>&nbsp;&nbsp;</td>';
                    _string += '</tr>';
                    $(_string).appendTo("#LocBody");
                });
            }
        },
        error: function (e) { }
    })
}
function ClearLocationForm() {
    $("#LocationName").val("");
    $("#Description").val("");

}

function EditLocationDetails(LocId) {
    $("#BtnCancel").show();
    $.ajax({
        url: "/Location/EditLocation",
        type: "POST",
        dataType: "JSON",
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: {
            locId: LocId
        },
        success: function (data) {
            if (data != null) {
                $("#LocationName").val(data.locationName);
                $("#Description").val(data.description);
                $("#LocationId").val(data.locationId);
            }
        },
        error: function (e) {

        }
    })
}

function DeleteLocationDetails(LocId) {
    $.ajax({
        url: "/Location/DeleteLocation",
        type: "POST",
        dataType: "JSON",
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: {
            LocationId: LocId
        },
        success: function (data) {
            if (data != null) {
                alert(data);
                PopulateLocationTable();
            }
        },
        error: function (e) { }
    })
}