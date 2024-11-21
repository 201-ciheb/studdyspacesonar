class DataPage extends React.Component {
    constructor(props) {
        super(props);

        var initialData = this.props.initialData;

        if (!initialData) {
            var $initialDataInput = $("#initialDataInput");
            initialData = $initialDataInput.val();
            initialData = JSON.parse(initialData);
            $initialDataInput.remove();
        }

        this.state = { data: initialData || {} };
    }

    componentWillReceiveProps(props) {
        var newState = { data: props.data || {} };

        if (!this.state) {
            this.state = newState;
        } else {
            this.setState(Object.assign({}, this.state, newState));
        }
    }

    handleNewFiles(files) {
        if (files && files.length) {
            var newItems = [];

            //add the new files to the items
            var newFileIndex = this.state.newFileIndex || 0;

            for (var i = files.length - 1; i >= 0; i--) {
                var file = files[i];
                newItems.push({
                    isNew: true,
                    newFile: file,
                    id: --newFileIndex,
                    client_filename: file.name,
                    line_count: "--",
                    lines_left_count: "--",
                    creator_name: this.state.data.logged_in_user,
                    creator_email: this.state.data.logged_in_user_email,
                    modifier_name: this.state.data.logged_in_user,
                    modifier_email: this.state.data.logged_in_user_email,
                    type: this.state.data.type,
                    stage: "awaiting_review",
                    status: "processing",
                    date_created: new Date().toISOString(),
                    date_modified: new Date().toISOString()
                });
            }

            if (this.state.data.items) {
                this.state.data.items.forEach(function (item) {
                    newItems.push(item);
                });
            }

            this.setState(Object.assign({}, this.state, { newFileIndex: newFileIndex,  data: Object.assign(this.state.data, { items: newItems }) }));
            return true;
        }

        return false;
    }

    handleDeleteFile(item, e, immediately) {
        var rThis = this;

        var removeItem = () => {
            //remove the item
            var items = rThis.state.data.items;

            for (var i = items.length - 1; i >= 0; i--) {
                if (items[i] === item) {
                    items.splice(i, 1);
                    break;
                }
            }

            //update state
            rThis.setState(Object.assign({}, rThis.state));
        };

        if (immediately) {
            $.ajax({
                type: 'DELETE',
                url: location.protocol + '//' + location.host + "/api/lab_data/" + item.id,
                success: function (data) {
                    console.log("successfully deleted: " + item.id);
                    console.log(data);
                    removeItem();
                },
                error: function (data, status, httpError) {
                    console.log("error deleting: " + item.id);
                    console.log(data);

                    if (httpError && httpError.toLowerCase() === "not found") {
                        //remove the item still
                        removeItem();
                    }
                }
            });
        } else {
            var realStageStatus = item.stage_status;
            item.stage_status = "processing";

            //refresh
            this.setState(Object.assign({}, this.state));

            $("#deleteDataModal").modal("show").one('click', (ee) => {
                if (ee.target.id === "continueButton") {
                    //perform delete
                    rThis.handleDeleteFile(item, e, true);
                } else {
                    //cancel delete
                    //restore
                    item.stage_status = realStageStatus;
                    //refresh
                    rThis.setState(Object.assign({}, rThis.state));
                }

                //release
                item = null;
                e = null;
                realStageStatus = null;
            });
        }

        return false;
    }

    getUrlEncodedData(e) {
        var form = document.getElementById('searchForm');
        var data = new FormData(form);

        var urlEncodedData = "";
        var urlEncodedDataPairs = [];

        // Turn the data object into an array of URL-encoded key/value pairs.
        for (var entry of data.entries()) {
            var value = entry[1];

            if (!value && value !== 0)
                continue;

            urlEncodedDataPairs.push(encodeURIComponent(entry[0]) + '=' + encodeURIComponent(value));
        }

        //if (urlEncodedDataPairs.length == 0)
        //    return;

        // Combine the pairs into a single string and replace all %-encoded spaces to 
        // the '+' character; matches the behaviour of browser form submissions.
        urlEncodedData = urlEncodedDataPairs.join('&').replace(/%20/g, '+');
        return urlEncodedData;
    }

    handleSearch(e) {
        e.preventDefault();
        window.location.href = "?" + this.getUrlEncodedData(e);
    }

    handlePimaErrorSummary(e) {
        e.preventDefault();
        var win = window.open("/LabData/Pima/Error_Summary?" + this.getUrlEncodedData(e), '_blank');
        win.focus();
    }

    getThreeItems(items, itemsCount, startIdx) {
        var children = [];

        for (var j = startIdx; j < itemsCount && j < (startIdx + 3); j++) {
            var item = items[j];
            children.push(<div key={item.id} className="col col-sm-12 col-md-4">
                <DataItem data={item} onDeleteFile={(i, e) => { this.handleDeleteFile(i, e); }} isHome={this.state.data.is_home} canBeReviewed={this.state.data.can_be_reviewed} />
            </div>);
        }

        return children;
    }

    componentDidMount() {
        setInterval(() => {
            $('.data-date').trigger('refreshDate');
        }, 3 * 60 * 1000); //every three minutes
    }

    render() {
        var items = this.state.data.items || [];

        var itemsCount = items.length;

        var itemRows = [];

        if (itemsCount > 0) {
            itemRows.push((
                <h6 key={-1} style={{ marginBottom: "3px", marginTop: 0, marginLeft: 1 }}>Showing {itemsCount} {itemsCount == 1 ? "record" : "records"}</h6>
            ));

            //using the following style because of a layout error in bootstrap.
            //so each three items are arranged in a row
            for (var i = 0; i < itemsCount; i = i + 3) {
                itemRows.push((
                    <div key={i} className="row">
                        {this.getThreeItems(items, itemsCount, i)}
                    </div>
                ));
            }
        }

        if (itemRows.length == 0) {
            //no items
            itemRows.push((<div key="0" className="row">
                <h4 className="text-center" style={{ marginTop: 0 }}>No data to show.</h4>
            </div>));
        }

        var paginationTag = null;
        var prevTag = null;
        var nextTag = null;
        var homeTag = null;

        var pagination = this.state.data.pagination;

        if (pagination) {
            if (pagination.prev) {
                prevTag = <li>
                    <a href={pagination.prev} aria-label="Previous">
                        <span aria-hidden="true">&laquo; Prev</span>
                    </a>
                </li>;
            }

            if (pagination.next) {
                nextTag = <li>
                    <a href={pagination.next} aria-label="Next">
                        <span aria-hidden="true">Next &raquo;</span>
                    </a>
                </li>;
            }

            if (pagination.home) {
                homeTag = <li>
                    <a href={pagination.home} aria-label="Home">
                        <span aria-hidden="true"> Home </span>
                    </a>
                </li>;
            }

            if (prevTag || nextTag || homeTag) {
                paginationTag = <nav className="text-center" aria-label="navigation" style={{ width: "100%", marginBottom: 20, zIndex: 998 }}>
                    <ul className="pagination pagination-lg">
                        {prevTag}
                        {homeTag}
                        {nextTag}
                    </ul>
                </nav>;
            }
        }

        return (
            <div className="data-page">
                <div style={{ marginBottom: 15 }}>
                    <SearchData data={this.state.data.filter} />
                    <div style={{ marginTop: 7 }}>
                        <div className="btn-group">
                            <button id="filterButton" type="submit" className="btn btn-primary"
                                style={{ borderRight: 0 }}
                                onClick={(e) => { e.preventDefault(); this.handleSearch(e); return true; }}>Filter</button>
                            {
                                this.state.data.type == "Pima" ?
                                    <button id="filterButton" type="button" className="btn btn-default"
                                        style={{ borderLeft : 0 }}
                                        onClick={(e) => { e.preventDefault(); this.handlePimaErrorSummary(e); return true; }}>View Error Summary</button>
                                    : null
                            }
                            
                            <button id="resetButton" type="reset" className="btn btn-default"
                                style={{ borderLeft: this.state.data.type != "Pima" ? 0 : null }}
                                onClick={(e) => {
                                    e.preventDefault();
                                    window.location.href = "?";
                                    return true;
                                }}>Reset</button>
                        </div>
                        <NewData type={this.state.data.type} onNewFiles={(files) => { this.handleNewFiles(files); }} />
                    </div>
                </div>
                {itemRows}
                {paginationTag}
                <div id="deleteDataModal" className="modal fade" role="dialog">
                    <div className="modal-dialog" role="document">
                        <div className="modal-content">
                            <div className="modal-header">
                                <button type="button" className="close" data-dismiss="modal" style={{ fontSize: "35px", marginTop: -8 }}>&times;</button>
                                <h4 className="modal-title">Delete File & Records</h4>
                            </div>
                            <div className="modal-body">
                                <h6 style={{ marginTop: 0, marginBottom: 0 }}>This will <strong>permanently</strong> delete this file and all its records from the server.</h6>
                                <h6 style={{ marginTop: 0, marginBottom: 0 }}>Are you sure you want to proceed?</h6>
                            </div>
                            <div className="modal-footer">
                                <button type="button" className="btn btn-primary" data-dismiss="modal">Cancel</button>
                                <button type="button" className="btn btn-default" data-dismiss="modal" id="continueButton">Yes, Continue</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

function renderApp() {
    // This code starts up the React app when it runs in a browser. It sets up the routing
    // configuration and injects the app into a DOM element.
    ReactDOM.render(
        <DataPage />,
        document.getElementById('reactApp')
    );
}

if (typeof window !== 'undefined') {
    renderApp();
}