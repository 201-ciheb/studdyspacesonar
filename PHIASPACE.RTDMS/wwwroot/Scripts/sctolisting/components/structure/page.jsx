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
            fields: {
                "client_id": { text: "Id", type: "text" },
                "start_date": { text: "Start Date", type: "date" },
                //"end_date": { text: "End Date", type: "date" },
                //"submission_date": { text: "Submission Date", type: "date" },
                "latitude": { text: "Latitude", type: "number" },
                "longitude": { text: "Longitude", type: "number" },
                "mapper": { text: "Mapper Code", type: "number" },
                "mobile_network": {
                    text: "Mobile Network", type: "select",
                    options: {
                        "1": "MTN",
                        "2": "Glo",
                        "3": "Airtel",
                        "4": "9mobile",
                        "5": "Spectranet",
                        "6": "Ntel",
                        "7": "Visafone",
                        "8": "Starcomms",
                        "9": "M-Tel",
                        "11": "MainOne",
                        "12": "Swift"
                    },
                    is_equals_only: true
                },
            }
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
        $("#structuresTable").DataTable({ paging: false, info: false, searching: false, order: []});
    }

    render() {
        var centerPoint = this.state.data.center_point;

        var items = centerPoint ? [centerPoint, ...this.state.data.items] : [...this.state.data.items];

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
                <StructureSearchData cluster={this.state.data.cluster} data={this.state.data.filter} fields={this.state.fields} />
                {
                    itemsCount > 0 ? (
                        <div>
                            <h6 style={{ marginBottom: "0px" }}>Showing {itemsCount} {itemsCount == 1 ? "record" : "records"}</h6>
                            <table id="structuresTable" className="table table-striped table-hover" style={{ backgroundColor: "white" }}>
                                <thead>
                                    <tr>
                                        <th>Id</th>
                                        <th>Address</th>
                                        <th>Residential?</th>
                                        <th>Type</th>
                                        <th>Households</th>
                                        <th>Coordinates</th>
                                        <th>Visited On</th>
                                        <th>Mapper / Code</th>
                                        <th>Best Mobile Network</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {items.map((item, i) => {
                                        return (
                                            <tr key={item.id}>
                                                <td>{hasCluster ? item.client_id : item.unique_client_id}</td>
                                                <td>{item.address}</td>
                                                <td>{item.is_residential ? "Yes" : "No"}</td>
                                                <td>{
                                                    item.is_residential ?
                                                        (item.residential_type_text.toLowerCase() != "other" || !item.residential_type_other ?
                                                            item.residential_type_text : item.residential_type_other)
                                                        : (item.is_center_point ? "Center Point" : item.non_residential_type_text)
                                                }
                                                </td>
                                                <td>{item.is_residential ? item.hh_count : "N/A"}</td>
                                                <td>{item.latitude + ", " + item.longitude}</td>
                                                <td>{dateFns.format(item.start_time, "MMM D, YYYY [at] h:mm a")}</td>
                                                <td>{item.mapper_name} / {item.mapper_code}</td>
                                                <td>{item.is_center_point ? item.mobile_network_text : "N/A"}</td>
                                            </tr>
                                        );
                                    })}
                                </tbody>
                            </table>
                        </div>
                    ) : <h4 className="text-center">No data to show.</h4>
                }
                {paginationTag}
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