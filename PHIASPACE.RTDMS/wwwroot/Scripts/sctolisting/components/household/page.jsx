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
                "residential_type": {
                    text: "Residential Type", type: "select",
                    options: {
                        "1": "Bungalow",
                        "2": "Traditional/Hut",
                        "3": "1 Storey",
                        "4": "2 Storey",
                        "5": "3 Storey",
                        "6": "More than 3 Storey",
                        "7": "Make Shift",
                        "8": "Shed/Container/Silos",
                        "9": "Other"
                    },
                    is_equals_only: true
                },
                "client_id": { text: "Id", type: "number" },
                "structure_id": { text: "Structure", type: "number" },
                "members": { text: "Size", type: "number" },
                "under_15": { text: "Under 15 Count", type: "number" },
                "start_date": { text: "Start Date", type: "date" },
                //"end_date": { text: "End Date", type: "date" },
                //"submission_date": { text: "Submission Date", type: "date" },
                "latitude": { text: "Latitude", type: "number" },
                "longitude": { text: "Longitude", type: "number" },
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
        $("#hhTable").DataTable({ paging: false, info: false, searching: false, order: []});
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
                <HouseholdSearchData cluster={this.state.data.cluster} data={this.state.data.filter} fields={this.state.fields} />
                {
                    itemsCount > 0 ? (
                        <div>
                            <h6 style={{ marginBottom: "0px" }}>Showing {itemsCount} {itemsCount == 1 ? "record" : "records"}</h6>
                            <table id="hhTable" className="table table-striped table-hover table-responsive" style={{ backgroundColor: "white" }}>
                                <thead>
                                    <tr>
                                        <th>Id</th>
                                        <th>Head Name / Nickname</th>
                                        <th>Size</th>
                                        <th>Ages</th>
                                        <th>Under 15</th>
                                        <th>Structure</th>
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
                                                <td>{hasCluster ? item.client_id : item.unique_client_id}</td>
                                                <td className="td-break-words">{item.head_name} / {item.head_nickname}</td>
                                                <td>{item.member_count}</td>
                                                <td className="td-break-words" style={{ minWidth: 75 }}>{item.member_ages.replace(/,/g, ", ")}</td>
                                                <td>{item.member_under_15_count}</td>
                                                <td>{item.structure_client_id}</td>
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