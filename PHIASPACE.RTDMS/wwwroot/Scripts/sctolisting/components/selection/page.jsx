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

        this.state = {
            data: initialData || {},
            fields: {}
        };
    }

    componentWillReceiveProps(props) {
        var newState = { data: props.data || {} };

        if (!this.state) {
            this.state = newState;
        } else {
            this.setState(Object.assign({}, this.state, newState));
        }
    }

    componentDidMount() {
        $("#selectionTable").DataTable({ paging: false, info: false, searching: false, order: []});
    }

    handleSaveSelection(e) {
        //disable the button
        var $saveButton = $("#saveButton");
        $saveButton.attr("disabled", "disabled");
        $saveButton.html("working...");

        //create the selection object and post
        var selection = {
            cluster: this.state.data.cluster,
            limit: this.state.data.filter.limit,
            total_hh_count: this.state.data.total_hh_count,
            seed: this.state.data.seed,
            divisions: this.state.data.divisions,
            households: this.state.data.items.map((item, i) => {
                return {
                    row_num: this.state.data.row_numbers[i],
                    id: item.id,
                    client_id: item.client_id,
                    unique_client_id: item.unique_client_id
                };
            })
        };

        //get the anti-forgery token and add to data being sent
        var token = $("[name='__RequestVerificationToken']", ReactDOM.findDOMNode(this)).val();
        selection.__RequestVerificationToken = token;

        var rThis = this;

        $.ajax({
            type: 'POST',
            url: location.protocol + '//' + location.host + "/api/scto_listing/selection?cluster=" + this.state.data.cluster,
            data: selection,
            cache: false,
            dataType: 'json',
            success: function (data) {
                console.log(data);
                alert("Selection saved successfully.");
                rThis.setState(Object.assign({}, rThis.state,
                    { data: Object.assign(rThis.state.data, { is_existing_selection: true }) }));
            },
            error: function (data, status, httpError) {
                console.log(data);
                alert("Error saving selection.");
                $saveButton.html("Accept this version for cluster");
                $saveButton.removeAttr("disabled");
            }
        });
    }

    render() {
        var items = this.state.data.items || [];

        var itemsCount = items.length;

        var paginationTag = null;
        var prevTag = null;
        var nextTag = null;
        var homeTag = null;

        var hasCluster = this.state.data.cluster !== null;

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
                paginationTag = <nav className="text-center" aria-label="navigation" style={{ width: "100%", marginTop: 20, marginBottom: 20, zIndex: 998 }}>
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
                <SelectionSearchData cluster={this.state.data.cluster}
                    data={this.state.data.filter} fields={this.state.fields}
                    anti_forgery_token_tag={this.state.data.anti_forgery_token_tag} />
                {
                    itemsCount > 0 ? (
                        <div>
                            <h6 style={{ marginBottom: 5 }}>Showing {itemsCount} {itemsCount == 1 ? "record" : "records"}</h6>
                            <form className="form-inline" style={{ display: "inline-block", marginBottom: -7.5, width: "100%" }}>
                                <div className="form-group">
                                    <div className="input-group">
                                        <div className="input-group-addon">Number Of Households In Cluster</div>
                                        <input name="total_hh_count" type="number" min="1" className="form-control"
                                            readOnly={true}
                                            defaultValue={this.state.data.total_hh_count} />
                                    </div>
                                </div>
                                <div className="form-group">
                                    <div className="input-group">
                                        <div className="input-group-addon">Seed</div>
                                        <input name="seed" type="number" min="0" className="form-control"
                                            readOnly={true}
                                            defaultValue={this.state.data.seed + 1} />
                                    </div>
                                </div>
                                <div className="form-group">
                                    <div className="input-group">
                                        <div className="input-group-addon">Step</div>
                                        <input name="divisions" type="number" step="any" className="form-control"
                                            readOnly={true}
                                            defaultValue={this.state.data.divisions} />
                                    </div>
                                </div>
                                {
                                    !this.state.data.is_existing_selection ?
                                        <div className="form-group pull-right">
                                            <button id="saveButton" type="button" className="btn btn-primary"
                                                onClick={(e) => { $("#saveSelectionModal").modal(); }}>Accept this version for cluster</button>
                                        </div>
                                        : null
                                }
                            </form>
                            <table id="selectionTable" className="table table-striped table-hover table-responsive" style={{ backgroundColor: "white" }}>
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Id</th>
                                        <th>Head Name / Nickname</th>
                                        <th>Size</th>
                                        <th>Ages</th>
                                        <th>Under 15</th>
                                        <th>Structure Type</th>
                                        <th>Address</th>
                                        <th>Coordinates</th>
                                        <th>Visited On</th>
                                        <th>Mapper / Code</th>
                                        <th>Comment</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {items.map((item, i) => {
                                        return (
                                            <tr key={item.id}>
                                                <td>{this.state.data.row_numbers[i]}</td>
                                                <td>{item.unique_client_id}</td>
                                                <td className="td-break-words">{item.head_name} / {item.head_nickname}</td>
                                                <td>{item.member_count}</td>
                                                <td className="td-break-words">{item.member_ages}</td>
                                                <td>{item.member_under_15_count}</td>
                                                <td>
                                                    {
                                                        item.residential_type_text.toLowerCase() != "other"
                                                            || !item.residential_type_other ?
                                                            item.residential_type_text : item.residential_type_other
                                                    }
                                                </td>
                                                <td className="td-break-words">{item.address}</td>
                                                <td>{item.latitude + ", " + item.longitude}</td>
                                                <td className="td-break-words">{dateFns.format(item.start_time, "MMM D, YYYY [at] h:mm a")}</td>
                                                <td className="td-break-words">{item.mapper_name} / {item.mapper_code}</td>
                                                <td className="td-break-words">{item.comment}</td>
                                            </tr>
                                        );
                                    })}
                                </tbody>
                            </table>
                        </div>
                    ) : <h4 className="text-center">No data to show.</h4>
                }
                {paginationTag}
                
                <div id="saveSelectionModal" className="modal fade" role="dialog">
                    <div className="modal-dialog" role="document">
                        <div className="modal-content">
                            <div className="modal-header">
                                <button type="button" className="close" data-dismiss="modal">&times;</button>
                                <h4 className="modal-title">Accept Selections</h4>
                            </div>
                            <div className="modal-body">
                                <p>Accepting this version of randomly selected households would override previously saved selections for this cluster if any.</p>
                                <p>Are you sure you want to proceed?</p>
                            </div>
                            <div className="modal-footer">
                                <button type="button" className="btn btn-primary" data-dismiss="modal">Cancel</button>
                                <button type="button" className="btn btn-default"
                                    onClick={(e) => { this.handleSaveSelection(e); }} data-dismiss="modal">Yes, Continue</button>
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