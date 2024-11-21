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

    if ($("#unit_price").val() === "" || isNaN($("#unit_price").val())) {
        $("#unit_price").addClass("has-error");
        return;
    } else {
        $("#unit_price").removeClass("has-error");
    }
  
    var item = {
        id: items.length + 1, commodity_id: $("#commodity_id").val(), description: $("#description").val(), quantity_expected: $("#quantity").val(), pack_size: $("#pack_type").val(), unit_price: $("#unit_price").val()
    };
    items.push(item);

    var commodity_name = $('#commodity_id').find(":selected").text();
    var tr = "<tr><td>" + commodity_name + "</td>";
    tr += "<td>" + $("#description").val() + "</td>";
    tr += "<td>" + $("#quantity").val() + "</td>";
    tr += "<td>" + $("#pack_type").find(":selected").text() + "</td>";
    tr += "<td>" + $("#unit_price").val() + "</td>";
    tr += "<td>" + $("#unit_price").val() * $("#quantity").val() + "</td>";
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


function update_order() {
    if (items.length < 1) {
        alert("Nothing to update");
        return true;
    } else {
        $('#frm_order').submit(function () {


            var itm = { itms: items };
            var result = JSON.stringify(itm);
            $("#items").val(result);
            $(this).append('<input type="hidden" name="status" value="2" /> ');

            return true;
        });

        $('#frm_order').submit();
    }
}


function update_cost(id,ctrl) {
    for (var i = 0; i < items.length; i++) {
        if (items[i].id === id) {
            items[i].unit_cost = $(ctrl).val();
            var cost = parseFloat($(ctrl).val());
            var quantity = parseFloat(items[i].quantity_expected);
            $("#tt_"+items[i].id).text(cost * quantity);
        }
    }
}

function approve_order() {
    if (items.length < 1) {
        alert("Nothing to approve");
        return true;
    } else {
        $('#frm_order').submit(function () {
            var itm = { itms: items };
            var result = JSON.stringify(itm);
            $("#items").val(result);
            $(this).append('<input type="hidden" name="status" value="46" /> ');

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

function check_rqnumber() {
    $.get("/purchaseorder/checkRequestNo/" + $("#requisition_no").val(), function (data) {
        //$(".table-responsive").html(data);
        if (data == "false" || data=="False") {
            $("#div_btn").show();
        } else {
            $("#div_btn").hide();
        }

    });
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