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
                "hours": { text: "Trip Hours", type: "number" },
                "neighbour_hours": { text: "Neighbour Trip Hours", type: "number" },
                "start_date": { text: "Start Date", type: "date" },
                //"end_date": { text: "End Date", type: "date" },
                //"submission_date": { text: "Submission Date", type: "date" },
                "mapper": { text: "Mapper Code", type: "number" }
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
        $("#facilityTable").DataTable({ paging: false, info: false, searching: false, order: []});
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
                <FacilitySearchData cluster={this.state.data.cluster} data={this.state.data.filter} fields={this.state.fields} />
                {
                    itemsCount > 0 ? (
                        <div>
                            <h6 style={{ marginBottom: "0px" }}>Showing {itemsCount} {itemsCount == 1 ? "record" : "records"}</h6>
                            <table id="facilityTable" className="table table-striped table-hover table-responsive" style={{ backgroundColor: "white" }}>
                                <thead>
                                    <tr>
                                        <th>Cluster</th>
                                        <th>Facility</th>
                                        <th>Trip Hours</th>
                                        <th>Neighbouring State</th>
                                        <th>Neighbouring Facility</th>
                                        <th>Neighbour Trip Hours</th>
                                        <th>Recorded On</th>
                                        <th>Mapper / Code</th>
                                        <th>Comment</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {items.map((item, i) => {
                                        return (
                                            <tr key={item.id}>
                                                <td>{item.cluster_key}</td>
                                                <td>{item.name}</td>
                                                <td>{item.approx_distance_hours}</td>
                                                <td>{item.has_neighbour ? item.neighbour_state : "N/A"}</td>
                                                <td>{item.has_neighbour ? item.neighbour_facility : "N/A"}</td>
                                                <td>{item.has_neighbour ? item.neighbour_approx_distance_hours : "N/A"}</td>
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