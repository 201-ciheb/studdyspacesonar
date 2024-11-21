function get_completion() {
    $.get(baseUrl() + "dashapi/GetCompletion", function (data) {
        if (data.Table.length > 0) {
            for (var i = 0; i < 6; i++) {
                var no = i + 1;
                $("#st_name_" + no).text(data.Table[i].state_name + " - " + data.Table[i].total_eas);
                $("#total_teams_" + no).text(data.Table[i].not_reviewed);
                $("#active_teams_" + no).text(data.Table[i].not_reviewed);
                $("#complete_hh_" + no).text(data.Table[i].hh_visited);
                $("#complete_interviews_" + no).text(data.Table[i].hh_completed);
                //$("#hh_rr_" + no).text(data.Table[i].HRR + "%");
                //$("#id_rr_" + no).text(data.Table[i].IRR + "%");
                //$("#bd_rr_" + no).text(data.Table[i].BRR + "%");
            }
        }

        $("#int_hh").text(data.Table1[0].hh);
        $("#int_id").text(data.Table2[0].ind);
       
    });
}

function get_lab_summary() {

}