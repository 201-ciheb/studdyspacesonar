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
    if ($("#quantity_expected").val() === "" || isNaN($("#quantity_expected").val())) {
        $("#quantity_expected").addClass("has-error");
        return;
    } else {
        $("#quantity_expected").removeClass("has-error");
    }

    var item = {
        id: items.length + 1, commodity_id: $("#commodity_id").val(), quantity_expected: $("#quantity_expected").val(), pack_size: $("#pack_type").val()
    };


    items.push(item);
    var commodity_name = $('#commodity_id').find(":selected").text();
    var tr = "<tr><td>" + commodity_name + "</td>";
    tr += "<td>" + $("#description").val() + "</td>";
    //tr += "<td></td><td></td>";
    tr += "<td>" + $("#quantity_expected").val() + "</td>";
    tr += "<td>" + $("#pack_type").find(":selected").text() + "</td>";
    tr += "<td></td>";
    tr += "<td><button type='button' onclick='remove_item("+ item.id+",this)' class='btn btn-xs btn-danger'><i class='icon-trash'></i></button></td>";
    tr += "</tr>";

    $("#tb_order > tbody").append(tr);

    $("#quantity_expected").val("");
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


function add_order() {
    $('#frm_order').submit(function () {


        var itm = { itms: items };
        var result = JSON.stringify(itm);
        $("#items").val(result);
        $(this).append('<input type="hidden" name="status" value="2" /> ');
        
        return true;
    });

    $('#frm_order').submit();
}


function stamp_order() {
    $('#frm_order').submit(function () {

        var itm = { itms: items };
        var result = JSON.stringify(itm);
        $("#items").val(result);
        $(this).append('<input type="hidden" name="status" value="3" /> ');

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