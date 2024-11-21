class NewActivityData extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            data: props.data || {},
            isMounted: true
        };
    }

    componentWillReceiveProps(props) {
        let newState = { data: props.data || {} };

        if (!this.state) {
            this.state = newState;
        } else {
            this.setState(Object.assign({}, this.state, newState));
            $('.form-group.has-error', ReactDOM.findDOMNode(this)).removeClass('has-error');
        }
    }

    setData(data) {
        this.setState(Object.assign({}, this.state, {
            data: Object.assign(this.state.data, data)
        }));
    }

    setErrorData(errorData, httpError, httpStatus, data) {
        var _errorObj = null;

        if (errorData && errorData.responseText) {
            _errorObj = JSON.parse(errorData.responseText);
        }

        this.setState(Object.assign({}, this.state, {
            error: _errorObj || httpError || httpStatus,
            data: Object.assign(this.state.data, data || {})
        }));
    }

    handleCancel(e) {
        if (this.props.onCancel) {
            this.props.onCancel(e);
        }
    }

    handleOk(e) {
        let gotFocus = false;
        let hasErrors = false;

        let node = ReactDOM.findDOMNode(this);

        $addNewActivityForm = $("#addNewActivityForm", node);
        $inputs = $('input, textarea, select', $addNewActivityForm);
        $inputs.one('invalid', function (event) {
            //set an error state
            let $formGroup = $(event.target).closest(".form-group");
            $formGroup.addClass("has-error").on("input", (e) => {
                $formGroup.removeClass("has-error");
            });

            if (!gotFocus) {
                gotFocus = true;
                setTimeout(function () { $(event.target).focus(); }, 50);
            }
        });
        $inputs.each(function (index, target) {
            hasErrors = !target.checkValidity() || hasErrors;
        });

        if (hasErrors) {
            e.preventDefault();
            e.stopPropagation();

            setTimeout(function () {
                alert("Please fill in the fields highlighted. They are required.");
            }, 100);

            return false;
        }

        //continue submit
        let comment = $("#commentInput", $addNewActivityForm).val();
        let target_email = $("#targetInput", $addNewActivityForm).val();
        let tag = $("#tagInput", $addNewActivityForm).val();
        let cc = $("#ccInput", $addNewActivityForm).val();

        var activity = {
            ticket_id: this.state.data.ticket_id,
            type: this.state.data.activity_id,
            type_text: this.state.data.activity_value,
            comment: comment,
            target_email: target_email,
            tag: tag,
            cc: cc,
            creator_email: this.state.data.logged_in_user_email
        };

        $inputs.prop("disabled", true);
        $("button", $addNewActivityForm).prop("disabled", true);

        this.setErrorData(null);

        var $progress = $("#progress", node);
        $progress.css("display", "");

        var rThis = this;

        $.ajax({
            type: 'POST',
            url: location.protocol + '//' + location.host + "/api/ticket/" + activity.ticket_id + "/" + this.state.data.activity_value,
            data: activity,
            cache: false,
            success: function (data) {
                console.log(data);

                if (!rThis.state.isMounted)
                    return;

                if (rThis.props.onOk) {
                    rThis.props.onOk(e, activity, data);
                } else {
                    $progress.css("display", "none");
                    $addNewActivityForm.reset();
                    $inputs.prop("disabled", false);
                    $("button", $addNewActivityForm).prop("disabled", false);
                }
            },
            error: function (errorData, httpStatus, httpError) {
                console.log(errorData);

                if (!rThis.state.isMounted)
                    return;

                $progress.css("display", "none");

                rThis.setErrorData(errorData, httpError, httpStatus, {});

                $inputs.prop("disabled", false);
                $("button", $addNewActivityForm).prop("disabled", false);
            }
        });

        return true;
    }

    componentDidMount() {
        $("[maxlength]", ReactDOM.findDOMNode(this)).bind('input propertychange', function () {
            let maxLength = $(this).attr('maxlength');
            if ($(this).val().length > maxLength) {
                $(this).val($(this).val().substring(0, maxLength));
            }
        });
    }

    componentWillUnmount() {
        this.state.isMounted = false;
    }

    getErroneousError(data) {
        var _data = data || this.state.data;

        return this.state.error;
    }

    render() {
        let data = this.state.data || {};

        var error = this.getErroneousError(data);
        var errorTag = null;

        if (error) {
            errorTag = <div className="bg-danger" style={{ maxHeight: 180, overflowY: "auto", paddingRight: 10, paddingLeft: 10, borderRadius: "3px", marginBottom: 15 }}>
                {error.Message ? error.Message : error}
            </div>;
        }

        return (
            <div className="panel-body ticket-new-activity" style={{ borderTop: 0, borderBottom: "1px solid #ddd", padding: 0, paddingBottom: 5 }}>
                <div id="progress" className="progress progress-striped active" style={{ height: 1.3, display: "none" }}>
                    <div className="progress-bar progress-bar-success" role="progressbar" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style={{ width: "100%" }}></div>
                </div>
                <form id="addNewActivityForm" className="form-horizontal" style={{ marginTop: 15, marginLeft: 20, marginRight: 20, marginBottom: 0 }}>
                    {errorTag}
                    {
                        data.activity_value === "Prioritize" /*Prioritize Activity*/ ?
                            <div className="form-group">
                                <label htmlFor="tagInput" className="col-sm-2 control-label">New Priority</label>
                                <div className="col-sm-10">
                                    <select id="tagInput" name="tag" className="form-control" required="required">
                                        <option value="1">Low</option>
                                        <option value="2">Medium</option>
                                        <option value="3">High</option>
                                    </select>
                                </div>
                            </div>
                            : null
                    }
                    {
                        data.activity_value === "Assign" /*Assign Activity*/ ?
                            <div className="form-group">
                                <label htmlFor="targetInput" className="col-sm-2 control-label">Assign To</label>
                                <div className="col-sm-10">
                                    <input id="targetInput" name="target_email" className="form-control" placeholder="email of person to be assigned this ticket" required="required" type="email"></input>
                                </div>
                            </div>
                            : null
                    }
                    <div className="form-group">
                        <label htmlFor="commentInput" className="col-sm-2 control-label">Comment</label>
                        <div className="col-sm-10">
                            <textarea maxLength={850} className="form-control" rows="5" id="commentInput" name="comment" required="required" placeholder="detailed explanation of your action"></textarea>
                        </div>
                    </div>
                    <div className="form-group">
                        <label htmlFor="tagInput" className="col-sm-2 control-label">Cc (if any)</label>
                        <div className="col-sm-10">
                            <input id="ccInput" maxLength={125} name="cc" className="form-control" placeholder="emails separated by semi-colon (;)"></input>
                        </div>
                    </div>
                    <div className="form-group">
                        <div className="col-sm-offset-2 col-sm-10">
                            <button id="submitButton" type="submit" className="btn btn-primary" onClick={(e) => { this.handleOk(e); }}>{data.activity_value}{data.activity_value == "Comment" ? " on" : ""} Ticket</button>
                            <div className="btn-group pull-right" role="group">
                                <button type="reset" className="btn btn-default" onClick={(e) => { $('.form-group.has-error', this.form).removeClass('has-error'); }}>Reset</button>
                                <button type="button" className="btn btn-default" onClick={(e) => { this.handleCancel(e); }}>Cancel</button>
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        );
    }
}