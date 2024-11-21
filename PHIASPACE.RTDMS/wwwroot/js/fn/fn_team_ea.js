function get_ea_by_states() {
    var id = $("#state").val();
    $.get(baseUrl() + "ea/GetStateEas/" + id , function (data) {
    

        for (var i = 0; i < data.length; i++) {
            var tr = '<tr><td><input type="checkbox" name="eas" value="' + data[i].id + '"/> <td>' + data[i].id + '</td></td><td>' + data[i].ea_name + '</td><td>' + data[i].lga_name + '</td></tr>';
           
            $('#tb > tbody').append(tr);
        }

        $("#tb_display").show();

    });
}

function show_team_details(id) {
    $(this).closest("tr").addClass("selected").siblings().removeClass("selected");
    $('#tb_report > tbody').empty();
    $('#tb_team_members > tbody').empty();
    $("#ea_id").val(id);

    var url = "/dmdashboard/getreport?ea_id=" + id;
    $("#btn_field_report").attr('href', url);

    $.get("/dashapi/GetTeamEaSummary/" + id, function (data) {

        //var data = JSON.parse(rs);
        
        $('#tb_report > tbody').append(data.report);

       
        $('#tb_team_members > tbody').append(data.team);

        if (data.active == 1) {
            $("#btn_validate").attr("disabled", "disabled");
        }
        else {
            $("#btn_validate").removeAttr("disabled");
        }
        $("#date_closed").val(data.date_closed);
        if (data.date_closed == null || data.date_closed == "") {
            $("#btn_field_completed").removeAttr("disabled");
        }
        else {
            $("#btn_field_completed").attr("disabled", "disabled");
            $("#date_closed").addClass("border-success-400");
        }
        //$('#tb_report > tbody > tr').each(function () {
        //    $(this).css('background-color', '');
        //});

       // $('#tr_tm_' + id).css('background-color', 'yellow')
        //for (var i = 0; i < data.length; i++) {
        //    // var tr = '<tr><td></td><td>' + data[i].title + '</td><td>' + data[i].issue_content + '</td></tr>';

        //    var tr = "";
        //    tr += '<tr><td> <strong>' + data[i].title + '</strong> <span class="pull-right">' + data[i].date_created + '</span></td></tr><tr>';
        //    tr += '<td>' + data[i].issue_content + '</td></tr><tr><td>';
        //    tr += '<a href="#" data-toggle="modal" data-target="#myModal" onclick="get_issue(' + data[i].id + ')"><i class="icon-pen"></i></a><span class="label bg-orange pull-right">' + data[i].status_id + '</span></td></tr>';




        //    $('#tb > tbody').append(tr);
        //}

        //$("#tb_display").show();

    });
}

function load_team_comments(team_id, ea_id) {
    $('#team_id').val(team_id);
    $('#ea_id').val(ea_id);

    $.get(baseUrl() + "issueapi/GetPMIssues/" + team_id, function (data) {

        //var data = JSON.parse(rs);
        $('#tb > tbody').empty();
        for (var i = 0; i < data.length; i++) {
           // var tr = '<tr><td></td><td>' + data[i].title + '</td><td>' + data[i].issue_content + '</td></tr>';
            var status = "Open";
            if (data[i].status_id == 2) {
                status = "Closed";
            } else if (data[i].status_id == 3) {
                status = "Discarded";
            }
            var tr = "";
            tr += '<tr><td> <strong>' + data[i].title + '</strong> <span class="pull-right">' + data[i].date_created + '</span></td></tr><tr>';
            tr += '<td>' + data[i].issue_content + '</td></tr><tr><td>';
            tr += '<a href="#" onclick="get_issue(' + data[i].id + ')"><i class="icon-pen"></i></a><span class="label bg-orange pull-right">' + status + '</span></td></tr>';




            $('#tb > tbody').append(tr);
        }

        //$("#tb_display").show();

    });

    //$.get(baseUrl() + "ea/GetStateEas/" + id, function (data) {


    //    for (var i = 0; i < data.length; i++) {
    //        var tr = '<tr><td><input type="checkbox" name="eas" value="' + data[i].id + '"/> </td><td>' + data[i].ea_name + '</td><td>' + data[i].lga_name + '</td></tr>';

    //        $('#tb > tbody').append(tr);
    //    }

    //    $("#tb_display").show();

    //});
}

function get_team_issues(id) {

    $.get(baseUrl() + "issueapi/GetPMIssues/" + id, function (data) {

        var rs = JSON.parse(data);

        for (var i = 0; i < data.length; i++) {
            var tr = '<tr><td></td><td>' + data[i].title + '</td><td>' + data[i].issue_content + '</td></tr>';

            $('#tb > tbody').append(tr);
        }

        //$("#tb_display").show();

    });
}

function field_completed() {
    var data = { ea_id: $("#ea_id").val(), date: $("#date_closed").val() };
    $.ajax({
        type: "POST",
        url: "/dmdashboard/FieldClosed",
        data: JSON.stringify(data),
        success: function (data) {
            if (data == 1) {
                $("#btn_field_completed").attr("disabled", "disabled");
                $("#date_closed").addClass("border-success-400");
            }
            else {
                $("#date_closed").addClass("border-danger-400");
                $("#btn_field_completed").removeAttr("disabled");
            }

           
        }

    });
}

function pop_report() {
    var url = "/dmdash/getreport?ea_id=" + $("#ea_id").val();
    $("#btn_field_report").attr('href', url);
    $("#link").click();
}

function dm_validated() {
    var data = { ea_id: $("#ea_id").val()};
    $.ajax({
        type: "POST",
        url: "/dmdashboard/DmValidated",
        data: JSON.stringify(data),
        success: function (data) {
            if (data == 1) {
                $("#btn_validate").attr("disabled", "disabled");
              
            }
            else {
                $("#btn_validate").removeAttr("disabled");
            }


        }

    });
}