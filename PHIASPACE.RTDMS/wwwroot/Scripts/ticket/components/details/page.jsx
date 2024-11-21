class DetailsPage extends React.Component {
    constructor(props) {
        super(props);

        let initialData = this.props.initialData;

        if (!initialData) {
            let $initialDataInput = $("#initialDataInput");
            initialData = $initialDataInput.val();
            initialData = JSON.parse(initialData);
            $initialDataInput.remove();
        }

        this.state = {
            data: initialData || {},
            activity_past_tense: {
                "Open": "Opened",
                "Comment": "Commented",
                "Prioritize": "Prioritized",
                "Assign": "Assigned",
                "Resolve": "Resolved",
                "Abandon": "Abandoned",
                "Close": "Closed",
            },
            isMounted: true
        };
    }

    componentWillReceiveProps(props) {
        let newState = { data: props.data || {} };

        if (!this.state) {
            this.state = newState;
        } else {
            this.setState(Object.assign({}, this.state, newState));
        }
    }

    componentDidMount() {
        $("[maxlength]").bind('input propertychange', function () {
            let maxLength = $(this).attr('maxlength');
            if ($(this).val().length > maxLength) {
                $(this).val($(this).val().substring(0, maxLength));
            }
        });

        this.scrollToActivitiesBottom();

        let rThis = this;

        $('#activitiesContainer', ReactDOM.findDOMNode(this)).scroll(function (e) {
            let pos = $(e.target).scrollTop();
            if (pos == 0) {
                //load more activities
                rThis.pollLoadMoreEarlierActivies();
            }
        });

        setTimeout(() => { rThis.pollLoadMoreLaterActivies(); }, 60000); //after 1 min
    }

    componentWillUnmount() {
        this.state.isMounted = false;
    }

    pollLoadMoreEarlierActivies() {
        if (!this.state.is_loading_earlier_activities && !this.state.loaded_all_earlier_activities) {
            this.state.is_loading_earlier_activities = true;

            $progressContainer = $("#activitiesProgressContainer", ReactDOM.findDOMNode(this));
            $progressContainer.css("display", "");

            let rThis = this;

            let activities = this.state.data.activities || [];
            let firstActivityId = activities.length > 0 ? activities[0].id : "";

            $.ajax({
                type: 'GET',
                url: location.protocol + '//' + location.host + "/api/ticket/" + this.state.data.ticket.id + "/activities?earlier_than=" + firstActivityId,
                cache: false,
                contentType: "application/json",
                success: function (data) {
                    console.log(data);

                    if (!rThis.state.isMounted)
                        return;

                    rThis.state.loaded_all_earlier_activities = ((data || {}).activities || []).length == 0;

                    //update
                    rThis.handleNewData(data, () => {
                        rThis.state.is_loading_earlier_activities = false;
                        $progressContainer.css("display", "none");
                    });
                },
                error: function (errorData, httpStatus, httpError) {
                    console.log(errorData);

                    if (!rThis.state.isMounted)
                        return;

                    setTimeout(() => { rThis.pollLoadMoreEarlierActivies(); }, 1000); //after one second
                }
            });
        }
    }

    pollLoadMoreLaterActivies() {
        if (!this.state.is_loading_later_activities) {
            this.state.is_loading_later_activities = true;

            let rThis = this;

            let activities = this.state.data.activities || [];
            let lastActivityId = activities.length > 0 ? activities[activities.length - 1].id : "";

            $.ajax({
                type: 'GET',
                url: location.protocol + '//' + location.host + "/api/ticket/" + this.state.data.ticket.id + "/activities?later_than=" + lastActivityId,
                cache: false,
                contentType: "application/json",
                success: function (data) {
                    console.log(data);

                    if (!rThis.state.isMounted)
                        return;

                    //update
                    rThis.handleNewData(data, () => {
                        rThis.state.is_loading_later_activities = false;

                        if (!rThis.is_loading_earlier_activities)
                            rThis.scrollToActivitiesBottom();

                        setTimeout(() => { rThis.pollLoadMoreLaterActivies(); }, 60000); //after 1 min
                    });
                },
                error: function (errorData, httpStatus, httpError) {
                    console.log(errorData);

                    if (!rThis.state.isMounted)
                        return;

                    setTimeout(() => { rThis.pollLoadMoreLaterActivies(); }, 60000); //after 1 min
                }
            });
        }
    }

    handleWatchToggle(e) {
        let rThis = this;

        let action = this.state.data.is_watcher_logged_in ? "unwatch" : "watch";

        $.ajax({
            type: 'POST',
            url: location.protocol + '//' + location.host + "/api/ticket/" + this.state.data.ticket.id + "/" + action + "?logged_in_user_email=" + this.state.data.logged_in_user_email,
            cache: false,
            contentType: "application/json",
            success: function (data) {
                console.log(data);

                if (!rThis.state.isMounted)
                    return;

                //update
                rThis.handleNewData(data);

                if (action === "watch")
                    alert("You are now subscribed to mail notifications from this ticket.");
                else if (action === "unwatch")
                    alert("You have unsubscribed from notifications from this ticket.");
            },
            error: function (errorData, httpStatus, httpError) {
                console.log(errorData);

                if (!rThis.state.isMounted)
                    return;

                alert("An error occurred while performing the action. Message: " + ((errorData || {}).responseText || ""));
            }
        });
    }

    getDateCreated() {
        let text = null;

        if (!this.state.isActualDateCreated) {
            text = dateFns.distanceInWordsToNow(this.state.data.ticket.date_created) + " ago";
        } else {
            text = dateFns.format(this.state.data.ticket.date_created, "MMM D, YYYY [at] h:mm a");
        }

        return text;
    }

    getAssigneeStatusDate() {
        let text = null;

        if (!this.state.isActualStatusDate) {
            text = dateFns.distanceInWordsToNow(this.state.data.ticket.assignee_status_date) + " ago";
        } else {
            text = dateFns.format(this.state.data.ticket.assignee_status_date, "MMM D, YYYY [at] h:mm a");
        }

        return text;
    }

    handleNewActivityCancel(e) {
        this.setState(Object.assign({}, this.state, { new_activity_open: false }));
    }

    resolveNewActivities(newActivities) {
        let sentActivities = newActivities || [];
        let oldActivities = this.state.data.activities || [];

        sentActivities.forEach((activity) => {
            let previousActivities = oldActivities.filter((a) => a.id == activity.id);
            let previousActivity = previousActivities.length == 1 ? previousActivities[0] : null;

            if (previousActivity) {
                Object.assign(previousActivity, activity);
            } else {
                let i = 0;
                for (l = oldActivities.length; i < l; i++) {
                    if (activity.id < oldActivities[i].id) {
                        break;
                    }
                }

                oldActivities.splice(i, 0, activity);
            }
        });

        return oldActivities;
    }

    handleNewData(responseData, callback) {
        if (responseData) {
            responseData.activities = this.resolveNewActivities(responseData.activities);
            responseData.ticket = Object.assign(this.state.data.ticket, responseData.ticket || {});

            this.setState(Object.assign({}, this.state,
                {
                    data: Object.assign(this.state.data, responseData),
                }), () => {
                    if (callback)
                        callback();
                });
        }
    }

    handleNewActivityOk(e, activity, responseData) {
        //clear the new_activity
        Object.assign(this.state, {
            new_activity_open: false,
            new_activity: null
        });

        this.handleNewData(responseData, this.scrollToActivitiesBottom);
    }

    scrollToActivitiesBottom() {
        let activitiesContainer = document.getElementById("activitiesContainer");
        activitiesContainer.scrollTop = activitiesContainer.scrollHeight;
    }

    render() {
        let data = this.state.data;
        //data.is_assignee_logged_in = true;
        //data.is_creator_logged_in = true;
        //data.is_opener_logged_in = true;

        let ticket = data.ticket || {};
        //ticket.is_assigned = false;
        //ticket.is_closed = true;
        //ticket.details = "gfggkhjhjh hjhjhjhjhk jhkhjhkhkhk kkkjhjhtdsdfgx dfgs fgdfg bgfdhfgdhdfhf ghfjhgfjghf gjgdrtdtdyug jfgjhfg jtdtrseterstd ghfjgf.";
        //ticket.priority = 2;
        //ticket.priority_text = "Medium";

        let activities = data.activities || [];

        let panelHeadingClass = " panel-default"

        if (!ticket.is_closed) {
            switch (ticket.priority) {
                case 1: //low
                    panelHeadingClass = " ticket-priority-low";
                    break;
                case 2: //medium
                    panelHeadingClass = " ticket-priority-medium";
                    break;
                case 3: //high
                    panelHeadingClass = " ticket-priority-high";
                    break;
                case 4: //Urgent
                    panelHeadingClass = " ticket-priority-urgent";
                    break;
            }
        }

        let ticketActions = [];

        if (ticket.is_closed) {
            ticketActions.push({
                id: 1,
                value: "Open"
            });
        } else {
            ticketActions.push({
                id: 2,
                value: "Comment"
            });
            if (ticket.priority_text !== "Urgent" &&
                ((!ticket.is_resolved && data.is_assignee_logged_in)
                    || (ticket.is_abandoned && data.is_opener_closer_logged_in))) {
                ticketActions.push({
                    id: 3,
                    value: "Prioritize"
                });
            }
            if (!ticket.is_assigned || ticket.is_abandoned || (data.is_assignee_logged_in && !ticket.is_resolved)) {
                ticketActions.push({
                    id: 4,
                    value: "Assign"
                });
            }
            if (data.is_assignee_logged_in && !ticket.is_resolved && !ticket.is_abandoned) {
                ticketActions.push({
                    id: 5,
                    value: "Resolve"
                });
                ticketActions.push({
                    id: 6,
                    value: "Abandon"
                });
            }
            if (data.is_opener_closer_logged_in || (data.is_assignee_logged_in && ticket.is_resolved)) {
                ticketActions.push({
                    id: 7,
                    value: "Close"
                });
            }
        }

        let isWatching = data.is_watcher_logged_in;
        let watchTitle = isWatching ? "Unfollow this ticket" : "Follow this ticket";
        let watchIconClass = isWatching ? "glyphicon glyphicon-eye-close" : "glyphicon glyphicon-eye-open";

        return (
            <div className="row">
                <div className="col-sm-offset-3 col-sm-6">
                    <div className={"panel" + panelHeadingClass}>
                        <div className="panel-heading">
                            <div className="row" style={{ margin: 0 }}>
                                <h1 className="pull-left" style={{ margin: 0 }}>{ticket.title}</h1>
                                <h1 className="pull-right" style={{ margin: 0 }}>{!ticket.is_closed ? ticket.priority_text : "Closed"}</h1>
                            </div>
                            <div className="row" style={{ margin: 0 }}>
                                <h6 className="pull-left" style={{ margin: 0 }}><a href={"mailto:" + ticket.creator_email} title={ticket.creator_email} style={{ color: "inherit" }}><strong>{ticket.creator_name}</strong></a></h6>
                                <h6 className="pull-right" style={{ margin: 0 }}><strong>{ticket.is_assigned ? (<a href={"mailto:" + ticket.assignee_email} title={ticket.assignee_email} style={{ color: "inherit" }}>{ticket.assignee_name || ticket.assignee_email}</a>) : "unassigned"}</strong></h6>
                            </div>
                            <div className="row" style={{ margin: 0 }}>
                                <p className="pull-left" style={{ margin: 0, cursor: "pointer" }}
                                    onClick={(e) => {
                                        e.stopPropagation();
                                        //yes set the state like this. we don't want to trigger a refresh.
                                        this.state.isActualDateCreated = !this.state.isActualDateCreated;
                                        e.target.innerHTML = this.getDateCreated();
                                        return false;
                                    }}>{this.getDateCreated()}</p>
                                <p className="pull-right" style={{ margin: 0 }}>
                                    {ticket.is_assigned ? <span>handler <strong>{ticket.assignee_status_text.toLowerCase()}</strong></span> : null} <span
                                        style={{ cursor: "pointer" }}
                                        onClick={(e) => {
                                            e.stopPropagation();
                                            //yes set the state like this. we don't want to trigger a refresh.
                                            this.state.isActualStatusDate = !this.state.isActualStatusDate;
                                            e.target.innerHTML = this.getAssigneeStatusDate();
                                            return false;
                                        }}>{this.getAssigneeStatusDate()}</span>
                                </p>
                            </div>
                        </div>
                        <div className="panel-body">
                            <h6 style={{ margin: 0 }}><strong>{ticket.category_text} {ticket.type_text}{ticket.deadline ? " (Due Date: " + dateFns.format(ticket.deadline, "MMM D, YYYY [at] h:mm a") + ")" : "" } &mdash; </strong>{ticket.details}</h6>
                        </div>
                        <div className="panel-body ticket-actions-container" style={{ padding: 0 }}>
                            <div className="btn-group btn-group-justified ticket-actions" role="group">
                                {
                                    ticketActions.map((ta) => {
                                        return (
                                            <div key={ta.id} className="btn-group" role="group">
                                                <button type="button"
                                                    className="btn btn-default ticket-action"
                                                    onClick={(e) => {
                                                        this.setState(Object.assign({}, this.state, { new_activity_open: true, new_activity: { ticket_id: ticket.id, activity_id: ta.id, activity_value: ta.value, logged_in_user_email: this.state.data.logged_in_user_email } }));
                                                    }}>{ta.value}</button>
                                            </div>
                                        );
                                    })
                                }
                            </div>
                        </div>
                        {
                            this.state.new_activity_open ?
                                <NewActivityData data={this.state.new_activity}
                                    onCancel={(e) => { this.handleNewActivityCancel(e); }}
                                    onOk={(e, activity, response) => { this.handleNewActivityOk(e, activity, response); }}/>
                                : null
                        }
                        {
                            activities.length > 0 ?
                                [
                                    <div key={0} className="panel-body" style={{ border: 0, paddingTop: 15, paddingBottom: 5 }}>
                                        <h4 style={{ margin: 0 }}><strong>Activities</strong></h4>
                                    </div>,
                                    <ul key={1} className="list-group" id="activitiesContainer">
                                        <li key={-1} id="activitiesProgressContainer" className="list-group-item" style={{ display: "none" }}>
                                            <div className="progress progress-striped active" style={{ height: 1.3 }}>
                                                <div className="progress-bar progress-bar-success" role="progressbar" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style={{ width: "100%" }}></div>
                                            </div>
                                        </li>
                                        {
                                            activities.map(a => {
                                                let summaryText = "";
                                                if (a.type_text !== "Comment") {
                                                    summaryText = this.state.activity_past_tense[a.type_text].toLowerCase() + " ticket";
                                                }

                                                if (a.type_text === "Assign") {
                                                    summaryText = summaryText + " to " + a.target_email;
                                                }

                                                if (a.type_text === "Prioritize") {
                                                    summaryText = summaryText + " as " + a.tag_text;
                                                }

                                                let ccTag = null;

                                                if (a.cc) {
                                                    ccTag = <p style={{ margin: 0, marginBottom: 5 }}><i><strong>Cc:</strong> {a.cc}</i></p>;
                                                }

                                                return (
                                                    <li key={a.id} className="list-group-item">
                                                        <h6 style={{ margin: 0 }}><a href={"mailto:" + a.creator_email} title={a.creator_email}><strong>{a.creator_name}</strong></a> {summaryText ? <strong> {summaryText}</strong> : ""} <span className="pull-right">{dateFns.distanceInWordsToNow(a.date_created).replace(/about /g, '') + " ago"}</span></h6>
                                                        {ccTag}
                                                        <p style={{ margin: 0 }}>{a.comment}</p>
                                                    </li>
                                                );
                                            })
                                        }
                                    </ul>
                                ]
                                : null
                        }
                    </div>
                </div>
                <div className="col-sm-3">
                    <button type="button" className="btn btn-default" aria-label="Follow" title={watchTitle}
                        onClick={(e) => { this.handleWatchToggle(e); }}
                        disabled={(data.is_assignee_logged_in && !data.is_abandoned) || data.is_opener_closer_logged_in}>
                        <span className={watchIconClass} aria-hidden="true"></span>
                    </button>
                </div>
            </div>
        );
    }

    //setState(newState, callback) {
    //    super.setState(newState, callback);
    //}
}

function renderApp() {
    // This code starts up the React app when it runs in a browser. It sets up the routing
    // configuration and injects the app into a DOM element.
    ReactDOM.render(
        <DetailsPage />,
        document.getElementById('reactApp')
    );
}

if (typeof window !== 'undefined') {
    renderApp();
}