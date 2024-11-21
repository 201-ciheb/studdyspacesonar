function load_form(form_id, line_number,form_name) {

    $.get(baseUrl() + "dashapi/get_form/" + form_id + "_" + line_number, function (data) {
        //$(".table-responsive").html(data);
        $("#form_title").text(form_name);

        for (var key in data[0]) {

            var value = data[0][key];
            var tr = '<tr> <td><strong>' + key + '</strong></td><td>' + value +'</td></tr>';
           // contact_detail += ' <span class="pull-right">' + value + ' </span>' + key + '</li>';

            $('#tb').append(tr);

        }

    });
}

function load_hh_form(ea_id, hh_id, ctrl) {
    $("#ea_id").val(ea_id);
    $("#hh_id").val(hh_id);
    $("#household_id").val(hh_id);
    $(this).closest("tr").addClass("selected").siblings().removeClass("selected");

    $.get(baseUrl() + "dashapi/GetHHSummary/" + ea_id + "_" + hh_id, function (data) {
        //$(".table-responsive").html(data);
        $("#header").empty();
        $("#header").append(data.header);
        $("#person_header").empty();
        $("#person_header").append(data.person_header);
        $("#hh_qs").empty();
        $("#hh_qs").append(data.hh_qs);
        $("#tb_data > thead").empty();
        $("#tb_data > thead").html(data.table_header);
        $("#tb_data > tbody").empty();
        $("#tb_data > tbody").html(data.table_content);

        $("#li_tickets").show();
        //load the issues
        try {
            var issues = JSON.parse(data.issues);
           
            var tbody = "";
            for (var i = 0; i < issues.length;i++) {
                var issue = issues[i];

                var status = "Open";
                if (issue.status_id == 2) {
                    status = "Closed";
                } else if (issue.status_id == 3) {
                    status = "Discarded";
                }

                tbody += "<tr>";
                tbody += "<td> <strong>" + issue.title + "</strong><span class='pull-right>" + issue.date_created + "</span></td></tr>";
                tbody += "<tr><td>" + issue.issue_content + "</td></tr>";
                tbody += " <tr><td><a href='#' data-toggle='modal' data-target='#myModal' onclick=get_issue('" + issue.id + "') ><i class='icon-pen'></i></a><span class='label bg-orange pull-right'>" + status + "</span></td>";
                tbody += "</tr>";
            }


            $("#tb_issues > tbody").empty();
            $("#tb_issues > tbody").append(tbody);
        }
        catch (err) {
            console.log(err);
        }
        
    });
}


function add_house_hold(cluster_no,hh_no){
    var html='<tr><td><a href="#" class="btn bg-danger-400 btn-rounded btn-icon btn-xs"><i class="icon-cancel-circle2"></i></a></td>';
    html+='<td>'+cluster_no+'</td>';
    html+='<td><a href="#" class="label bg-blue"> </a> </td></tr>';
    $("#tb_line > tbody").append(html);
                                                
}

function load_from_tree(id) {

    var href = "";
    var currentLocation = window.location;
    href = currentLocation.host + "?location_id=" + id;
    //window.location.replace(href);
    //$(window).attr("location", href);

    window.location.replace(currentLocation.origin + currentLocation.pathname + "?location_id=" + id)
   
}

function mark_as_completed(ea, hh) {
    var data = { ea_id: ea, hh_id: hh };
    $.ajax({
        type: "POST",
        url: baseUrl() + "dashapi/MarkCompleted",
        data: JSON.stringify(data),
        success: function (data) {
            $("#tp_" + ea + "_" + hh).html('<i class="icon-check" style="color:green"></i>');
        }
       
    });
}


function run_qc(ea, hh) {
    $("#qc_img").show();
    $("#ls_discussions").empty();

    var data = { ea_id: ea, hh_id: hh };
    $.ajax({
        type: "POST",
        url: baseUrl() + "dashapi/run_hh_qc",
        data: JSON.stringify(data),
        success: function (data) {
            $("#qc_img").hide();
            for (var i = 0; i < data.length; i++) {
                var even = "";
                var li = "<li class='media " + even + "'>";
                li += '<div class="media-body"><div class="media-content" style="background-color:#EF5350;color: #fff">' + data[i];
                li += '</div>';
                li += ' </div></li>';

                $("#ls_discussions").append(li);
            }
        }
    });
}