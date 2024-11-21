items = [];


function edit() {
    $("#edit").show();
    $("#list").hide();
}

function list() {
    $("#edit").hide();
    $("#list").show();
}

function add_item() {
    //validate controls
    if (!$("#pack_type").val()) {
        $("#pack_type").addClass("has-error");
        return;
    } else {
        $("#pack_type").removeClass("has-error");
    }
    if ($("#quantity").val() === "" || isNaN($("#quantity").val())) {
        $("#quantity").addClass("has-error");
        return;
    } else {
        $("#quantity").removeClass("has-error");
    }
  
    var item = {
        id: items.length + 1,
        commodity_id: $("#commodity_id").val(),
        description: $("#description").val(),
        quantity_expected: $("#quantity").val(),
        pack_size: $("#pack_type").val()
    };

    items.push(item);

    var commodity_name = $('#commodity_id').find(":selected").text();
    var tr = "<tr><td>" + commodity_name + "</td>";
    tr += "<td>" + $("#description").val() + "</td>";
    tr += "<td>" + $("#quantity").val() + "</td>";
    tr += "<td>" + $("#pack_type").find(":selected").text() + "</td>";
    //tr += "<td>" + $("#unit_cost").val() + "</td>";
    //tr += "<td>" + $("#unit_cost").val() * $("#quantity").val() + "</td>";
    tr += "<td><button type='button' onclick='remove_item("+ item.id+",this)' class='btn btn-xs btn-danger'><i class='icon-trash'></i></button></td>";
    tr += "</tr>";

    $("#tb_order > tbody").append(tr);
}

//remove an item from the list
function remove_item(id, row) {
    //remove the item from the items list
    items = items.filter(function (el) {
        return el.id !== id;
    });
    //remove the row from the table
    $(row).parent().parent().remove();
}

function approve_order() {
    if (items.length < 1) {
        alert("Nothing to approve");
        return true;
    } else {
        $('#frm_order').submit(function() {
            var itm = { itms: items };
            var result = JSON.stringify(itm);
            $("#items").val(result);
            $(this).append('<input type="hidden" name="status" value="46" /> ');

            return true;
        });

        $('#frm_order').submit();
    }
}

function add_order() {
    if (items.length < 1) {
        alert("Nothing to save");
        return true;
    } else {
        $('#frm_order').submit(function() {
            var itm = { itms: items };
            var result = JSON.stringify(itm);
            $("#items").val(result);
            $(this).append('<input type="hidden" name="status" value="2" /> ');

            return true;
        });

        $('#frm_order').submit();
    }
}


function stamp_order() {
    if (items.length < 1) {
        alert("Nothing to save and stamp");
        return true;
    } else {
        $('#frm_order').submit(function() {

            var itm = { itms: items };
            var result = JSON.stringify(itm);
            $("#items").val(result);
            $(this).append('<input type="hidden" name="status" value="3" /> ');

            return true;
        });

        $('#frm_order').submit();
    }
}


function receive_order() {
    $('#frm_order').submit(function () {


        var itm = { itms: items };
        var result = JSON.stringify(itm);
        $("#items").val(result);
        $(this).append('<input type="hidden" name="status" value="4" /> ');

        return true;
    });

    $('#frm_order').submit();
}

function batch_order() {
    $('#frm_order').submit(function () {


        var itm = { itms: items };
        var result = JSON.stringify(itm);
        $("#items").val(result);
        $(this).append('<input type="hidden" name="status" value="11" /> ');

        return true;
    });

    $('#frm_order').submit();
}


function cancel_order() {
    $('#frm_order').submit(function () {


        var itm = { itms: items };
        var result = JSON.stringify(itm);
        $("#items").val(result);
        $(this).append('<input type="hidden" name="status" value="4" /> ');

        return true;
    });

    $('#frm_order').submit();
}

function clone_row(row) {

    var index = $("#tb_order  tr").index(row);

    var new_row = $(row).parent().parent().clone();

    $('#tb_order > tbody > tr').eq(index).after(new_row);

    $('.pickadate').pickadate();
}

function getBatchValue(id) {
    $.getJSON("GetPackSize/?id=" + id, function (data) {
        var items = "<option value=''>Select Batch</option>"
        $.each(data, function () {
            items += "<option value='" + this.id + "'>" + this.name + "</option>";
        });
        $("#pack_type").html(items);
    });
}

function approveRequestOrder(id) {
    $.post('ApproveReq/?id=' + id, function (data) {
        $('#tb_order_approve').DataTable().ajax.reload();
    });
}

function displayRequestOrder(id) {
    $('#tb_order_items').show(500);
    $.get('GetRequestOrder/?id=' + id, function (data) {
        var order = JSON.parse(data)
        $("#tb_order_items tbody").empty();
        for (i = 0; i < order.length; i++) {
            $("#tb_order_items tbody").append("<tr />");
            Object.keys(order[i]).forEach(function (k) {
                $($("#tb_order_items tbody tr")[i]).append(`<td>${order[i][k]}</td>`);
            });
        }
        $('#tb_order_items').DataTable();
    });
}

function displaySelectedState(value) {
    if (value === "") {
        $("#state_id").html("");
    }
    $.ajax({
        url: 'GetStatesByZone/?Zone=' + value,
        type: "GET",
        dataType: "JSON",
        data: { Zone: value },
        success: function (states) {
            $("#state_id").html(""); // clear before appending new list 
            $.each(states, function (i, state) {
                $("#state_id").append(
                    $('<option></option>').val(state.Id).html(state.Name));
            });
        }
    });
}

function displaySelectedTeams(value) {
    if (value === "") {
        $("#field_team").html("");
    }
    $.ajax({
        url: 'GetFieldTeams/?Zone=' + value,
        type: "GET",
        dataType: "JSON",
        data: { Zone: value },
        success: function (teams) {
            $("#field_team").html("");
            $.each(teams, function (i, team) {
                $("#field_team").append(
                    $('<option></option>').val(team.Id).html(team.Name));
            });
        }
    });
}

function displaySelectedPerson(value) {
    if (value === "") {
        $("#person_id").html("");
    }
    $.ajax({
        url: 'GetTeamPersons/?Zone=' + value,
        type: "GET",
        dataType: "JSON",
        data: { Zone: value },
        success: function (persons) {
            $("#person_id").html("");
            $.each(persons, function (i, person) {
                $("#person_id").append(
                    $('<option></option>').val(person.Id).html(person.Name));
            });
        }
    });
}