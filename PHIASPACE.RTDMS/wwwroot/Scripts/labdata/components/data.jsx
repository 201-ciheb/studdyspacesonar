class DataItem extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            data: this.props.data, isMounted: true,
            stages: {
                awaiting_review: "awaiting_review",
                in_review: "in_review",
                approved: "approved",
                disapproved: "disapproved"
            }
        };

        if (this.state.data.isNew && this.state.data.newFile) {
            //upload the file
            var formData = new FormData();
            formData.append('file', this.state.data.newFile);

            //discard the newfile
            this.state.data.newFile = undefined;

            var rThis = this;

            $.ajax({
                type: 'POST',
                url: location.protocol + '//' + location.host + "/api/lab_data/?type=" + this.state.data.type
                    + (this.props.canBeReviewed ? "&is_for_review=true" : ""),
                data: formData,
                xhr: function () {
                    var myXhr = $.ajaxSettings.xhr();
                    if (myXhr.upload) {
                        myXhr.upload.addEventListener('progress', function (evt) {
                            if (evt.lengthComputable) {
                                var percentComplete = evt.loaded / evt.total;
                                percentComplete = parseInt(percentComplete * 100);
                                console.log(percentComplete);

                                rThis.setState(Object.assign({}, rThis.state, { progress: percentComplete / 4.0 }));
                            }
                        }, false);
                    }
                    return myXhr;
                },
                cache: false,
                contentType: false,
                processData: false,

                success: function (data) {
                    console.log(data);

                    if (!rThis.state.isMounted)
                        return;

                    if (!rThis.props.isHome) {
                        //go full home
                        window.location.href = "?";
                        return;
                    }

                    //update
                    data = Object.assign(data, { isNew: undefined, newFile: undefined });
                    var isAwaitingReview = data.stage == rThis.state.stages.awaiting_review;

                    if (data.stage_status == "successful" && isAwaitingReview) {
                        //make progress 100%
                        rThis.setState(Object.assign({}, rThis.state, { progress: 100.0, data: Object.assign(rThis.state.data, data) }));
                    } else {
                        rThis.setData(data);
                    }

                    if (data.stage_status == "processing" || isAwaitingReview) {
                        setTimeout(() => { rThis.pollAndUpdate(); }, 1000); //after one second
                    }
                },
                error: function (errorData, httpStatus, httpError) {
                    console.log(errorData);

                    if (!rThis.state.isMounted)
                        return;

                    rThis.setErrorData(errorData, httpError, httpStatus, { isNew: undefined, newFile: undefined });
                }
            });
        } else {
            if (this.state.data.stage_status == "processing") {
                this.pollAndUpdate();
            } else if (this.state.data.stage_status == "erroneous") {
                this.state.error = this.getErroneousError();
            }
        }
    }

    componentWillUnmount() {
        this.state.isMounted = false;
    }

    componentWillReceiveProps(props) {
        var newState = { data: props.data };

        if (!this.state) {
            this.state = newState;
        } else {
            this.setState(Object.assign({}, this.state, newState));
        }
    }

    componentDidMount() {
        var rThis = this;

        $('.data-date', ReactDOM.findDOMNode(this)).on('refreshDate', function (e) {
            e.target.innerHTML = rThis.getDateCreated();
        });
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
            data: Object.assign(this.state.data, { stage_status: "erroneous" }, data || {})
        }));
    }

    getErroneousError(data) {
        var _data = data || this.state.data;

        if (_data.stage_status == "erroneous" && !this.state.error) {
            //create errors
            var error = { Message: " Please review the file, delete this upload, and try again." };

            if (_data.lines_left_count > 0) {
                var prefix = null;

                if (_data.lines_left_count < 10 && _data.lines_failed) {
                    error.failedLines = JSON.parse(_data.lines_failed);
                    prefix = "Lines " + error.failedLines.join(", ") + " from the data file could not be saved.";
                }

                if (!prefix) {
                    prefix = _data.lines_left_count + " lines from the data file could not be saved.";
                }

                error.Message = prefix + error.Message;
            } else {
                error.Message = "An error occurred." + error.Message;
            }

            return error;
        }

        return this.state.error;
    }

    pollAndUpdate() {
        var rThis = this;

        $.ajax({
            type: 'GET',
            url: location.protocol + '//' + location.host + "/api/lab_data/" + this.state.data.id,
            cache: false,
            contentType: "application/json",
            success: function (data) {
                console.log(data);

                if (!rThis.state.isMounted)
                    return;

                //calculate new progress. upload progress takes just 25 percent. so the db save progress occupies the remaining 75.
                var newProgress = null;

                if (data && data.stage == rThis.state.stages.approved) {
                    newProgress = 25.0 + ((data.line_count - data.lines_left_count) * 75.0 / data.line_count);
                }

                //update
                rThis.setState(Object.assign({}, rThis.state, { progress: newProgress, data: Object.assign(rThis.state.data, data), error: rThis.getErroneousError(data) }));

                if (data.stage_status == "processing") {
                    setTimeout(() => { rThis.pollAndUpdate(); }, 1000); //after one second
                } else {
                    //clear progress after few seconds
                    setTimeout(() => { rThis.setState(Object.assign({}, rThis.state, { progress: undefined })); }, 3000);
                }
            },
            error: function (errorData, httpStatus, httpError) {
                console.log(errorData);

                if (!rThis.state.isMounted)
                    return;

                if (httpError && httpError.toLowerCase() === "not found") {
                    //remove this item
                    rThis.handleDelete(null);
                    return;
                }

                setTimeout(() => { rThis.pollAndUpdate(); }, 1000); //after one second
            }
        });
    }

    getDateCreated() {
        var text = null;

        if (!this.state.isActualDateCreated) {
            text = dateFns.distanceInWordsToNow(!this.props.canBeReviewed || this.state.data.stage == this.state.stages.awaiting_review ? this.state.data.date_created : this.state.data.date_modified) + " ago";
        } else {
            text = dateFns.format(!this.props.canBeReviewed || this.state.data.stage == this.state.stages.awaiting_review ? this.state.data.date_created : this.state.data.date_modified, "MMM D, YYYY [at] h:mm a");
        }

        return text;
    }

    getFileNameBody() {
        var fileName = this.state.data.client_filename;
        return fileName.substr(0, fileName.length - 7);
    }

    getFileNameSuffix() {
        var fileName = this.state.data.client_filename;
        return fileName.substr(fileName.length - 7);
    }

    handleDelete(e) {
        if (e != null) {
            e.preventDefault();
            e.stopPropagation();
        }

        if (this.props.onDeleteFile) {
            this.props.onDeleteFile(this.state.data, e);
            return false;
        }

        return true;
    }

    handleApprove(e) {
        if (e != null) {
            e.preventDefault();
            e.stopPropagation();
        }

        this.setData({ stage_status: "processing" });

        var rThis = this;

        $.ajax({
            type: 'POST',
            url: location.protocol + '//' + location.host + "/api/lab_data/" + this.state.data.id + "/approve",
            cache: false,
            contentType: "application/json",
            success: function (data) {
                console.log(data);

                if (!rThis.state.isMounted)
                    return;

                rThis.setData(data);

                rThis.pollAndUpdate(); //refresh
            },
            error: function (errorData, httpStatus, httpError) {
                console.log(errorData);

                if (!rThis.state.isMounted)
                    return;

                rThis.setErrorData(errorData, httpError, httpStatus, null);

                //if (errorData != null && errorData.stage != rThis.state.stages.approved) {
                //    setTimeout(() => { rThis.handleApprove(e); }, 1000); //after one second, try again
                //}
            }
        });

        return false;
    }

    handleDisapprove(e) {
        if (e != null) {
            e.preventDefault();
            e.stopPropagation();
        }

        this.setData({ stage_status: "processing" });

        var rThis = this;

        $.ajax({
            type: 'POST',
            url: location.protocol + '//' + location.host + "/api/lab_data/" + this.state.data.id + "/disapprove",
            cache: false,
            contentType: "application/json",
            success: function (data) {
                console.log(data);

                if (!rThis.state.isMounted)
                    return;

                rThis.setData(data);

                rThis.pollAndUpdate(); //refresh
            },
            error: function (data, status, httpError) {
                console.log(data);

                if (!rThis.state.isMounted)
                    return;

                rThis.setErrorData(errorData, httpError, httpStatus, null);

                //if (data != null && data.stage != rThis.state.stages.disapproved) {
                //    setTimeout(() => { rThis.handleDisapprove(e); }, 1000); //after one second, try again
                //}
            }
        });

        return false;
    }

    handleDownload(e) {
        if (e != null) {
            e.preventDefault();
            e.stopPropagation();
        }

        this.setData({ stage_status: "processing" });

        window.location.href = "/LabData/" + this.state.data.id + "/Download";

        var rThis = this;

        //refresh. try 3 times
        setTimeout(() => { rThis.pollAndUpdate(); }, 500);
        setTimeout(() => { rThis.pollAndUpdate(); }, 1500);
        setTimeout(() => { rThis.pollAndUpdate(); }, 2500);

        return false;
    }

    toggleOptions(e) {
        $('#optionsDropdown', ReactDOM.findDOMNode(this)).dropdown('toggle');
    }

    render() {
        var data = this.state.data;

        var countLeftColor = "bg-warn";

        if (data.stage_status == "successful") {
            countLeftColor = "bg-success";
        } else if (data.stage_status == "erroneous") {
            countLeftColor = "bg-danger";
        }

        var countLeft = data.lines_left_count;

        if (data.stage == this.state.stages.awaiting_review) {
            if (data.stage_status == "successful") {
                countLeftColor = "bg-info";
            }
            countLeft = <span className="fas fa-asterisk" aria-hidden="true" style={{ marginTop: 16.6, marginLeft: 1.2 }}></span>;
            //countLeft = <span className="fas fa-angle-double-up" aria-hidden="true" style={{ marginTop: 15, marginLeft: 1 }}></span>;
            //countLeft = <span className="fas fa-upload" aria-hidden="true" style={{ marginTop: 15, marginLeft: 1 }}></span>;
        } else if (data.stage == this.state.stages.in_review) {
            if (data.stage_status == "successful") {
                countLeftColor = "bg-grey";
            }

            countLeft = <span className="fas fa-arrow-down" aria-hidden="true" style={{ marginTop: 16.6, marginLeft: 1.2 }}></span>;
            //countLeft = <span className="fas fa-asterisk" aria-hidden="true" style={{ marginTop: 16.6, marginLeft: 1.2 }}></span>;
            //countLeft = <span className="fas fa-download" aria-hidden="true" style={{ marginTop: 15, marginLeft: 1 }}></span>;
            //countLeft = <span className="fas fa-angle-double-down" aria-hidden="true" style={{ marginTop: 16.6, marginLeft: 1.2 }}></span>;
        } else if (data.stage == this.state.stages.approved) {
            if (!data.lines_left_count && data.stage_status == "successful") {
                countLeftColor = "bg-success";
                countLeft = <span className="glyphicon glyphicon-ok" aria-hidden="true" style={{ marginTop: 16.5, marginLeft: 0 }}></span>;
                //countLeft = <span className="fas fa-thumbs-up" aria-hidden="true" style={{ marginTop: 15 }}></span>;

                /*HACK: a little hack for delete time*/
                /*THIS IS NOT STANDARD CODE*/
                data.lines_left_count = countLeft;
            }
        } else if (data.stage == this.state.stages.disapproved) {
            if (data.stage_status == "successful") {
                countLeftColor = "bg-warning";
            }
            countLeft = <span className="fas fa-thumbs-down" aria-hidden="true" style={{ marginTop: 18 }}></span>;
        }

        var countLeftClassName = "status-icon " + countLeftColor + " text-center img-circle pull-left";

        var error = this.state.error;
        var footer = null;

        if (error) {
            footer = <div className="panel-footer bg-danger" style={{ maxHeight: 180, overflowY: "auto", paddingRight: 10, paddingLeft: 10 }}>
                {error.Message ? error.Message : error}
            </div>;
        }

        var progressTag = null;
        var progress = this.state.progress;

        var optionsTag = null;

        if (progress) {
            progressTag = <div className="panel-heading progress" style={{ padding: "unset", height: "2px", borderRadius: 0, borderBottomColor: "transparent" }}>
                <div className={"progress-bar " + (data.stage_status == "erroneous" ? "progress-bar-danger" : "progress-bar-success")}
                    role="progressbar" aria-valuenow="40" aria-valuemin="0" aria-valuemax="100" style={{ width: progress + "%" }}>
                </div>
            </div>;
        }

        if (data.stage_status != "processing" || !isNaN(data.lines_left_count) || !data.isNew) {
            var deleteTag = data.stage != this.state.stages.in_review || data.stage_status != "processing" ?
                <li>
                    <a href="#" className="media" onClick={(e) => { return this.handleDelete(e); }}>
                        <div className="media-left">
                            <h5 className="glyphicon glyphicon-remove media-object" aria-hidden="true"></h5>
                        </div>
                        <div className="media-body">
                            <h5 className="media-heading">Delete</h5>
                            <div>Removes file and all its records permanently.</div>
                        </div>
                    </a>
                </li>
                : null;

            var approvalTags = data.stage == this.state.stages.in_review && data.stage_status == "successful" ?
                (
                    [
                        <li key="1">
                            <a href="#" className="media" onClick={(e) => { return this.handleApprove(e); }}>
                                <div className="media-left">
                                    {//<h5 className="glyphicon glyphicon-ok media-object" aria-hidden="true" style={{ fontSize: 16.5 }}></h5>
                                    }
                                    <h5 className="far fa-thumbs-up media-object" aria-hidden="true" style={{ fontSize: 16.5 }}></h5>
                                </div>
                                <div className="media-body">
                                    <h5 className="media-heading">Approve</h5>
                                    <div>All good. Accept.</div>
                                </div>
                            </a>
                        </li>,
                        <li key="2">
                            <a href="#" className="media" onClick={(e) => { return this.handleDisapprove(e); }}>
                                <div className="media-left">
                                    <h5 className="far fa-thumbs-down media-object" aria-hidden="true" style={{ fontSize: 16.5 }}></h5>
                                </div>
                                <div className="media-body">
                                    <h5 className="media-heading">Disapprove</h5>
                                    <div>Not good. Review/Reject.</div>
                                </div>
                            </a>
                        </li>
                    ]
                )
                : null;

            var downloadTag = data.stage_status != "processing" ?
                <li>
                    <a href="#" className="media" onClick={(e) => { return this.handleDownload(e); }}>
                        <div className="media-left">
                            <h5 className="fas fa-download media-object" aria-hidden="true" style={{ fontSize: 16.5 }}></h5>
                        </div>
                        <div className="media-body">
                            <h5 className="media-heading">Download{data.stage == this.state.stages.awaiting_review ? " for review" : ""}</h5>
                            <div>Saves file to your computer{data.stage == this.state.stages.awaiting_review ? ", and marks it as being 'in review'" : ""}.</div>
                        </div>
                    </a>
                </li>
                : null;

            optionsTag =
                downloadTag || approvalTags || deleteTag ? (
                    <div className="dropdown" style={{ position: "absolute", right: 10, top: 13 }}>
                        <button id="optionsDropdown" type="button" className="close"
                            data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"
                        onClick={(e) => {
                            e.stopPropagation();
                            return false;
                        }} >
                            <span className="glyphicon glyphicon-option-horizontal" aria-hidden="true"></span>
                        </button>
                        <ul className="dropdown-menu" aria-labelledby="optionsDropdown"
                            style={{ left: "unset", right: -5, marginTop: -3, minWidth: downloadTag && data.stage == this.state.stages.awaiting_review ? 218 : "unset" }}>
                            {downloadTag}
                            {
                                downloadTag && (approvalTags || deleteTag) ?
                                    <li role="separator" className="divider"></li>
                                    : null
                            }
                            {approvalTags}
                            {
                                approvalTags && deleteTag ?
                                    <li role="separator" className="divider"></li>
                                    : null
                            }
                            {deleteTag}
                        </ul>
                    </div>
                ) : null;
        }

        return (
            <div className="data-item panel panel-default" onClick={(e) => {
                e.preventDefault();
                this.toggleOptions(e);
                return false;
            }}>
                {progressTag}
                <div className="panel-body" style={{ padding: "10px", cursor: "pointer"}}>
                    <div className={countLeftClassName}
                        style={{ minWidth: "56px", width: "auto", height: "56px", lineHeight: "56px", margin: "0px", fontSize: "30px", marginRight: "10px" }}>
                        {countLeft}
                    </div>
                    <div style={{ marginLeft: 66, marginRight: 20 }}>
                        <h5 data-full={data.client_filename} data-suffix={this.getFileNameSuffix()}
                            className="fileName" style={{ margin: 0, marginTop: 2, lineHeight: "0.9", wordWrap: "break-word" }}>
                            <span className="fileName-inner">{this.getFileNameBody()}</span>
                        </h5>
                        <h6 style={{ margin: "-6px 0px 0px", lineHeight: "1.5" }}>{data.modifier_name}{this.props.canBeReviewed && data.stage != this.state.stages.awaiting_review ? <i style={{ fontSize: "10px" }}> ({data.stage})</i> : ""}</h6>
                        <p style={{ margin: "3px 0px 0px", marginTop: -3 }}>{data.line_count > 0 ? data.line_count : "--"} records</p>
                    </div>
                    {optionsTag}
                    <p className="data-date" style={{ margin: "0px", cursor: "pointer", position: "absolute", right: 10, bottom: 10 }} onClick={(e) => {
                        e.stopPropagation();
                        //yes set the state like this. we don't want to trigger a refresh.
                        this.state.isActualDateCreated = !this.state.isActualDateCreated;
                        e.target.innerHTML = this.getDateCreated();
                        return false;
                    }}>{this.getDateCreated()}</p>
                </div>
                {footer}
            </div>
        );
    }
}