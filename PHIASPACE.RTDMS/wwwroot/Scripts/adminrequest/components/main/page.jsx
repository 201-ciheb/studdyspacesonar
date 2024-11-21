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
                "id": { text: "Request Id", type: "number" },
                "date": { text: "Date Requested", type: "text" },
                "deadline": { text: "Deadline", type: "date" },
                "amount": { text: "Amount Requested", type: "number" },
                //"naiis_status": {
                //    text: "NAIIS Status", type: "select",
                //    options: {
                //        "processing": "Processing",
                //        "approved": "Approved",
                //        "disapproved": "Disapproved"
                //    },
                //    is_equals_only: true
                //},
                //"hq_status": {
                //    text: "HQ Status", type: "select",
                //    options: {
                //        "processing": "Processing",
                //        "approved": "Approved",
                //        "disapproved": "Disapproved"
                //    },
                //    is_equals_only: true
                //}
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
        $("textarea[maxlength]").bind('input propertychange', function () {
            let maxLength = $(this).attr('maxlength');
            if ($(this).val().length > maxLength) {
                $(this).val($(this).val().substring(0, maxLength));
            }
        });
    }

    handleAddNew(e) {
        let $inputs = $('input, textarea', "#addNewForm");
        let gotFocus = false;
        let hasErrors = false;
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

        if (hasErrors) return;
        //dismiss
        //submit
        $("#addNewModal").modal('hide');
        //clearForm
        document.getElementById("addNewForm").reset();
    }

    render() {
        let type = this.state.data.type;

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
                <MainSearchData type={this.state.data.type} data={this.state.data.filter} fields={this.state.fields} />
                {
                    itemsCount > 0 ? (
                        <div>
                            <h6 style={{ marginBottom: "0px" }}>Showing {itemsCount} {itemsCount == 1 ? "record" : "records"}</h6>
                            <table id="mainTable" className="table table-striped table-hover table-responsive" style={{ backgroundColor: "white" }}>
                                <thead>
                                    <tr>
                                        <th>Id</th>
                                        <th>Description</th>
                                        <th>Amount Requested (&#8358;)</th>
                                        <th>Requester</th>
                                        <th>Requested On</th>
                                        <th>Status</th>
                                        <th>NAIIS Comment</th>
                                        <th>HQ Comment</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {items.map((item, i) => {
                                        let naiisCommentTag = [];
                                        if (item.naiis_admin_email) {
                                            naiisCommentTag.push(<Fragment><br /><strong>Contact:</strong> {item.naiis_admin_name} ({item.naiis_admin_email})</Fragment>);
                                        }
                                        if (item.naiis_admin_status_text) {
                                            naiisCommentTag.push(<Fragment><br /><strong>Status:</strong> {item.naiis_admin_status_text}</Fragment>);
                                        }
                                        if (item.naiis_admin_status_comment) {
                                            naiisCommentTag.push(<Fragment><br /><strong>Comment:</strong> {item.naiis_admin_status_comment}</Fragment>);
                                        }

                                        let hqCommentTag = [];
                                        if (item.hq_admin_email) {
                                            hqCommentTag.push(<Fragment><br /><strong>Contact:</strong> {item.hq_admin_name} ({item.hq_admin_email})</Fragment>);
                                        }
                                        if (item.hq_admin_status_text) {
                                            hqCommentTag.push(<Fragment><br /><strong>Status:</strong> {item.hq_admin_status_text}</Fragment>);
                                        }
                                        if (item.hq_admin_status_comment) {
                                            hqCommentTag.push(<Fragment><br /><strong>Comment:</strong> {item.hq_admin_status_comment}</Fragment>);
                                        }

                                        return (
                                            <tr key={item.id}>
                                                <td>{item.id}</td>
                                                <td className="td-break-words" style={{ minWidth: 100 }}>{item.description}</td>
                                                <td>{item.amount}</td>
                                                <td className="td-break-words">{item.creator_name} ({item.creator_email})</td>
                                                <td className="td-break-words">{dateFns.format(item.date_created, "MMM D, YYYY [at] h:mm a")}</td>
                                                <td>{item.status_text.replace(/_/g, " ")}</td>
                                                <td className="td-break-words" style={{ minWidth: 100 }}>{naiisCommentTag}</td>
                                                <td className="td-break-words" style={{ minWidth: 100 }}>{hqCommentTag}</td>
                                                <td><a className="btn btn-default" href={"/AdminRequest/" + type + "/" + item.id} target="_blank">View More...</a></td>
                                            </tr>
                                        );
                                    })}
                                </tbody>
                            </table>
                        </div>
                    ) : <h4 className="text-center">No data to show.</h4>
                }
                {paginationTag}
                <div id="addNewModal" className="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
                    <div className="modal-dialog" role="document">
                        <div className="modal-content">
                            <div className="modal-header">
                                <button type="button" className="close" data-dismiss="modal" style={{ fontSize: "35px", marginTop: -8 }}>&times;</button>
                                <h4 className="modal-title">New {type.replace(/_/g, " ")}</h4>
                            </div>
                            <div className="modal-body">
                                <form id="addNewForm" className="form-horizontal" method="post" action={"/AdminRequest/" + this.props.type + "/Create"}>
                                    <div className="form-group">
                                        <label htmlFor="descriptionInput" className="col-sm-2 control-label">Description</label>
                                        <div className="col-sm-10">
                                            <textarea maxLength={850} className="form-control" rows="5" id="descriptionInput" name="description" required="required"></textarea>
                                        </div>
                                    </div>
                                    <div className="form-group">
                                        <label htmlFor="amountInput" className="col-sm-2 control-label">Amount</label>
                                        <div className="col-sm-10">
                                            <input type="number" step="any" className="form-control" id="amountInput" name="amount" placeholder="amount being requested for in naira" required="required" />
                                        </div>
                                    </div>
                                    <div className="form-group">
                                        <label htmlFor="deadlineInput" className="col-sm-2 control-label">Deadline</label>
                                        <div className="col-sm-10">
                                            <input type="date" className="form-control" id="deadlineInput" name="deadline" placeholder="need-by date, if any." />
                                        </div>
                                    </div>
                                </form>
                            </div>
                            <div className="modal-footer">
                                <button type="button" className="btn btn-default" data-dismiss="modal">Cancel</button>
                                <button type="button" className="btn btn-primary" id="sendNewButton" onClick={(e) => { this.handleAddNew(e); }}>Send</button>
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