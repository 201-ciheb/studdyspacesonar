class MainSearchData extends SearchData {
    constructor(props) {
        super(props);
    }

    render() {
        var filter = this.state.data || {};
        var fields = this.state.other || {};
        var fieldsLeft = this.getFieldsLeft() || {};

        var addFilterTag = null;

        if (Object.keys(fieldsLeft).length > 0) {
            addFilterTag = <div className="form-group">
                <button type="button" className="close form-control" style={{ paddingLeft: 5 }}
                    onClick={(e) => { this.handleFilterAdd(e); }}>
                    <span className="glyphicon glyphicon-plus" aria-hidden="true"></span>
                </button>
            </div>;
        }

        return (
            <form id="searchForm" className="form-inline" style={{ display: "inline-block", width: "100%" }} method="GET" action="/Ticket">
                <div className="form-group">
                    <div className="input-group">
                        <div className="input-group-addon">Status</div>
                        <select className="form-control" id="statusInput" name="is_closed" defaultValue={(filter.is_closed || false) + ""}>
                            <option value={false}>Active</option>
                            <option value={true}>Closed</option>
                        </select>
                    </div>
                </div>
                <div className="form-group">
                    <div className="input-group">
                        <div className="input-group-addon">Assigned</div>
                        <select className="form-control" id="assignedInput" name="is_assigned" defaultValue={filter.is_assigned + "" || ""}>
                            <option value="">All</option>
                            <option value={true}>Yes</option>
                            <option value={false}>No</option>
                        </select>
                    </div>
                </div>
                <div className="form-group">
                    <div className="input-group">
                        <div className="input-group-addon">Text</div>
                        <input type="text" className="form-control" id="searchInput"
                            name="search_term" defaultValue={filter.search_term || ""} />
                    </div>
                </div>
                {
                    Object.keys(fields).map((f) => {
                        var v = fields[f];
                        v.from = v.from || "";
                        v.to = v.to || "";

                        var items = {};
                        items[f] = this.props.fields[f];
                        items = { ...items, ...fieldsLeft };
                        return (
                            <Filter key={f} items={items} isEqualsOnly={items[f].is_equals_only}
                                data={{
                                    field: f,
                                    fieldText: items[f].text,
                                    fieldType: items[f].type,
                                    fieldOptions: items[f].options,
                                    from: v.from, to: v.to
                                }}
                                onChange={(o, n, f, t) => { this.handleFilterChange(o, n, f, t); }}
                                onDelete={(f, d, e) => { this.handleFilterDelete(f, d, e); }} />
                        );
                    })
                }
                {addFilterTag}
                <div className="form-group">
                    <div className="btn-group">
                        <button id="filterButton" type="submit" className="btn btn-primary"
                            style={{ borderRight: 0 }}
                            onClick={(e) => { e.preventDefault(); this.handleSearch(e); return true; }}>Filter</button>
                        <button id="resetButton" type="button" className="btn btn-default"
                            style={{ borderLeft: 0 }}
                            onClick={(e) => {
                                e.preventDefault();
                                window.location.href = "?";
                                return true;
                            }}>Reset</button>
                    </div>
                </div>
                {
                    //<div className="form-group pull-right">
                    //    <button id="exportButton" type="submit" className="btn btn-default"
                    //        onClick={(e) => {
                    //            e.preventDefault();
                    //            this.handleSearch(e, "/Ticket/" + this.props.action + "/Export");
                    //            return true;
                    //        }}>Export To CSV</button>
                    //</div>
                }
            </form>
        );
    }
}