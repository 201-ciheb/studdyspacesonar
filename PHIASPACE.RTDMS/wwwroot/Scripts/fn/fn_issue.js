function get_issue_data(id) {
    $.get(baseUrl() + "issueapi/getissue/" + id, function (data) {
        $("#form_title").text("Edit DM Ticket");
        $("#is_new").val("0");
        $("#title").val(data.title);
        $("#status_id").val(data.status_id);
        $("#issue_type_id").val(data.issue_type_id);
        $("#id").val(data.id);
        $("#ea_id").val(data.ea_id);
        $("#household_id").val(data.household_id);
        $("#ea_id").val(data.ea_id);
        $("#issue_content").val(data.issue_content);
        $("#btn").text("Update Query");
    });
}




function get_pm_issue(id) {
    $.get(baseUrl() + "issueapi/GetPMIssue/" + id, function (data) {
        $("#form_title").text("Edit DM Ticket");
        $("#is_new").val("0");
        $("#title").val(data.title);
        $("#status_id").val(data.status_id);
        $("#issue_type_id").val(data.issue_type_id);
        $("#id").val(data.id);
        $("#ea_id").val(data.ea_id);
        $("#team_id").val(data.team_id);
        $("#ea_id").val(data.ea_id);
        $("#issue_content").val(data.issue_content);
        $("#btn").text("Update Query");
    });
}

function get_pm_discussions(id) {

    $("#ls_discussions").empty();
    $.get(baseUrl() + "issueapi/GetPMDiscussions/" + id, function (data) {

        for (var i = 0; i < data.length; i++) {
            var even = "";
            if (i % 2==0) {
                even = " reversed";
            }

            var li = "<li class='media " + even + "'>";
            li += '<div class="media-body"><div class="media-content">' + data[i].Body;
            li += '</div><span class="media-annotation display-block mt-10">' + moment(data[i].date_added).fromNow() + '<a href="#"><i class="icon-pin-alt position-right text-muted"></i></a></span>';
            li += ' </div></li>';

            $("#ls_discussions").append(li);
        }
    });
}


function get_discussions(id) {

    $("#ls_discussions").empty();
    $.get(baseUrl() + "issueapi/GetDiscussions/" + id, function (data) {

        for (var i = 0; i < data.length; i++) {
            var even = "";
            if (i % 2 == 0) {
                even = " reversed";
            }
           

            var li = "<li class='media " + even + "'>";
            li += '<div class="media-body"><div class="media-content">' + data[i].Body;
            li += '</div><span class="media-annotation display-block mt-10">' + data[i].AddedBy + ' - ' + moment(data[i].DateAdded).fromNow() + '<a href="#"><i class="icon-pin-alt position-right text-muted"></i></a></span>';
            li += ' </div></li>';

            $("#ls_discussions").append(li);
        }
    });
}

function post_discussion() {
    var data = { body: $("#body").val(), issue_id: $("#issue_id").val(), issue_type: $("#issue_type").val() };
    $.ajax({
        type: "POST",
        url: baseUrl() + "issueapi/PostDiscussion",
        data: JSON.stringify(data),
        success: function (rs) {
            get_discussions(rs);
            $("body").val("");
            $("#issue_id").val("");
        }

    });
}

function post_issue_discussion() {
    var data = { body: $("#body").val(), issue_id: $("#issue_id").val(), issue_type: $("#issue_type").val() };
    $.ajax({
        type: "POST",
        url: baseUrl() + "issueapi/PostADiscussion",
        data: JSON.stringify(data),
        success: function (rs) {
            get_discussions(rs);
            $("body").val("");
            $("#issue_id").val("");
        }

    });
}

function clear_ticket() {
    $("#form_title").text("New DM Ticket");
    $("#is_new").val("1");
    $("#title").val('');
    $("#status_id").val('');
    $("#issue_type_id").val('');
    $("#id").val('');
    //$("#ea_id").val(data.ea_id);
    //$("#household_id").val(data.household_id);
    //$("#ea_id").val(data.ea_id);
    $("#issue_content").val('');
    $("#btn").text("Add Ticket");
}

function get_issue(id) {
    $.get(baseUrl() + "issueapi/GetAIssue/" + id, function (data) {
        $("#form_title").text("Edit DM Ticket");
        $("#is_new").val("0");
        $("#title").val(data.title);
        $("#status_id").val(data.status_id);
        $("#issue_type_id").val(data.issue_type_id);
        $("#id").val(data.id);
        $("#ea_id").val(data.ea_id);
        $("#household_id").val(data.household_id);
        $("#ea_id").val(data.ea_id);
        $("#issue_content").val(data.issue_content);
        $("#id").val(data.id);
        $("#state_id").val(data.state_id);
        $("#lab_id").val(data.lab_id);

        $("#priority").val(data.priority);
        $("#impact").val(data.impact);
        $("#resolution").val(data.resolution);

        $("#btn").text("Update Query");
    });
}