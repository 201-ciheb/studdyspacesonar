class DataPage extends React.Component {
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
            fields: {
                "category": { text: "Category", type: "text", is_equals_only: true },
                "type": {
                    text: "Type", type: "select",
                    options: {
                        "1": "Issue",
                        "2": "Question",
                        "3": "Suggestion"
                    },
                    is_equals_only: true
                },
                "priority": {
                    text: "Priority", type: "select",
                    options: {
                        "1": "Low",
                        "2": "Medium",
                        "3": "High",
                        "4": "Urgent"
                    }
                },
                "id": { text: "Ticket Id", type: "number" },
                "date": { text: "Date Created", type: "text" },
                "deadline": { text: "Deadline", type: "date" },
                "project": { text: "Project", type: "text", is_equals_only: true }
            }
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
        $("#mainTable").DataTable({ paging: false, info: false, searching: false, order: [] });
    }

    render() {
        let action = this.state.data.action;

        let items = this.state.data.items || [];

        let itemsCount = items.length;

        let paginationTag = null;
        let prevTag = null;
        let nextTag = null;
        let homeTag = null;

        let hasCluster = this.state.data.cluster !== null;

        let pagination = this.state.data.pagination;

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
                <MainSearchData action={action} data={this.state.data.filter} fields={this.state.fields} />
                {
                    itemsCount > 0 ? (
                        <div>
                            <h6 style={{ marginBottom: "0px" }}>Showing {itemsCount} {itemsCount == 1 ? "record" : "records"}</h6>
                            <table id="mainTable" className="table table-hover table-responsive" style={{ backgroundColor: "white" }}>
                                <thead>
                                    <tr>
                                        <th>Id</th>
                                        <th>Title</th>
                                        <th>Status</th>
                                        <th>Creator</th>
                                        <th>Assigned To</th>
                                        <th>Category</th>
                                        <th>Type</th>
                                        <th>Priority</th>
                                        <th>Created On</th>
                                        <th>Due Date</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {items.map((item, i) => {
                                        let bgClass = ""

                                        if (!item.is_closed) {
                                            switch (item.priority) {
                                                case 1: //low
                                                    bgClass = "ticket-priority-low";
                                                    break;
                                                case 2: //medium
                                                    bgClass = "ticket-priority-medium";
                                                    break;
                                                case 3: //high
                                                    bgClass = "ticket-priority-high";
                                                    break;
                                                case 4: //Urgent
                                                    bgClass = "ticket-priority-urgent";
                                                    break;
                                            }
                                        }

                                        let href = "/Ticket/" + item.id;

                                        return (
                                            <tr key={item.id} className={bgClass} style={{ cursor: "pointer" }}
                                                onClickCapture={(e) => {
                                                    $("a[href='" + href + "']").get(0).click();
                                                }}>
                                                <td><strong>{item.id}</strong></td>
                                                <td className="td-break-words"><strong>{item.title}</strong></td>
                                                <td className="td-break-words">{!item.is_closed ? "Active" : "Closed"}, Handler {item.assignee_status_text.replace(/_/g, " ")}</td>
                                                <td className="td-break-words" style={{ minWidth: 100 }}>{item.creator_name} ({item.creator_email})</td>
                                                <td className="td-break-words" style={{ minWidth: 100 }}>{item.is_assigned ? (item.assignee_name || item.assignee_email.substr(0, item.assignee_email.indexOf('@'))) + " (" + item.assignee_email + ")" : ""}</td>
                                                <td>{item.category_text}</td>
                                                <td>{item.type_text}</td>
                                                <td>{item.priority_text}</td>
                                                <td className="td-break-words">{dateFns.format(item.date_created, "MMM D, YYYY [at] h:mm a")}</td>
                                                <td className="td-break-words">{item.deadline ? dateFns.format(item.deadline, "MMM D, YYYY [at] h:mm a") : ""}</td>
                                                <td><a className="btn btn-default" href={href} target="_blank">View More...</a></td>
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