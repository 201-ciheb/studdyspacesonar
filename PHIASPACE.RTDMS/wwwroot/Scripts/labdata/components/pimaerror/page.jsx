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
        window.location.href = "/labdata/pima/error_summary?" + this.getUrlEncodedData(e);
    }

    handleDownload(e) {
        e.preventDefault();
        window.location.href = "/LabData/Pima/Error_Summary/Download?" + this.getUrlEncodedData(e);
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
        $("#beadsTable").DataTable({ paging: false, info: false, searching: false, order: [] });
        $("#cd4Table").DataTable({ paging: false, info: false, searching: false, order: [] });
    }

    render() {
        var beadsItems = this.state.data.beads_items || [];
        var cd4Items = this.state.data.cd4_items || [];

        var beadsItemsCount = beadsItems.length;
        var cd4ItemsCount = cd4Items.length;

        return (
            <div className="data-page">
                <div className="container">
                    <SearchData data={this.state.data.filter} />
                    <div style={{ marginTop: 7 }}>
                        <div className="btn-group">
                            <button id="filterButton" type="submit" className="btn btn-primary"
                                style={{ borderRight: 0 }}
                                onClick={(e) => { e.preventDefault(); this.handleSearch(e); return true; }}>Filter</button>
                            <button id="resetButton" type="reset" className="btn btn-default"
                                style={{ borderLeft: 0 }}
                                onClick={(e) => {
                                    e.preventDefault();
                                    window.location.href = "?";
                                    return true;
                                }}>Reset</button>
                        </div>
                        <button id="exportButton" type="submit" className="btn btn-default"
                            style={{ marginLeft: 25 }}
                            onClick={(e) => { e.preventDefault(); this.handleDownload(e); return true; }}>Export To Excel</button>
                    </div>
                </div>
                <div className="container-fluid">
                    <div className="row">
                        <div className="col-sm-7">
                            <h3 style={{ marginBottom: 0 }}>CD4</h3>
                            {
                                cd4ItemsCount > 0 ? (
                                    <div>
                                        <h6 style={{ marginBottom: "0px", marginTop: 0 }}>Showing {cd4ItemsCount} {cd4ItemsCount == 1 ? "record" : "records"}</h6>
                                        <table id="cd4Table" className="table table-striped table-hover table-responsive" style={{ backgroundColor: "white" }}>
                                            <thead>
                                                <tr>
                                                    <th>S/N</th>
                                                    <th>Operator</th>
                                                    <th>Error Count</th>
                                                    <th>Beads Error Counts</th>
                                                    <th>Samples</th>
                                                    <th>Result Dates</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                {cd4Items.map((item, i) => {
                                                    return (
                                                        <tr key={i}>
                                                            <td>{i + 1}</td>
                                                            <td>{item.operator}</td>
                                                            <td>{item.error_count}</td>
                                                            <td className="td-break-words">{item.beads_error_counts}</td>
                                                            <td className="td-break-words">{item.samples}</td>
                                                            <td className="td-break-words">{item.result_dates}</td>
                                                        </tr>
                                                    );
                                                })}
                                            </tbody>
                                        </table>
                                    </div>
                                ) : <h4 className="text-center">No data to show.</h4>
                            }
                        </div>
                        <div className="col-sm-5">
                            <h3 style={{ marginBottom: 0 }}>BEADS</h3>
                            {
                                beadsItemsCount > 0 ? (
                                    <div>
                                        <h6 style={{ marginBottom: "0px", marginTop: 0 }}>Showing {beadsItemsCount} {beadsItemsCount == 1 ? "record" : "records"}</h6>
                                        <table id="beadsTable" className="table table-striped table-hover table-responsive" style={{ backgroundColor: "white" }}>
                                            <thead>
                                                <tr>
                                                    <th>S/N</th>
                                                    <th>Device Id</th>
                                                    <th>Error Count</th>
                                                    <th>Samples</th>
                                                    <th>Result Dates</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                {beadsItems.map((item, i) => {
                                                    return (
                                                        <tr key={i}>
                                                            <td>{i + 1}</td>
                                                            <td>{item.device_id}</td>
                                                            <td>{item.error_count}</td>
                                                            <td className="td-break-words">{item.samples}</td>
                                                            <td className="td-break-words">{item.result_dates}</td>
                                                        </tr>
                                                    );
                                                })}
                                            </tbody>
                                        </table>
                                    </div>
                                ) : <h4 className="text-center">No data to show.</h4>
                            }
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