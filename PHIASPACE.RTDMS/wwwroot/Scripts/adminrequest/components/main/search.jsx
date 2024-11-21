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
            <form id="searchForm" className="form-inline" style={{ display: "inline-block", width: "100%" }} method="GET" action={"/AdminRequest/" + this.props.type}>
                <div className="form-group">
                    <div className="input-group">
                        <div className="input-group-addon">Status</div>
                        <select className="form-control" id="statusInput" name="status" defaultValue={filter.status || ""}>
                            <option value="">All</option>
                            <option value="1">Processing</option>
                            <option value="2">Admin Notified</option>
                            <option value="3">Seen at NAIIS</option>
                            <option value="4">Referred to HQ</option>
                            <option value="5">Disapproved at NAIIS</option>
                            <option value="6">Seen at HQ</option>
                            <option value="7">Processing at HQ</option>
                            <option value="8">Approved at HQ</option>
                            <option value="9">Disapproved at HQ</option>
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
                <div className="form-group">
                    <button id="addNewButton" type="button" className="btn btn-primary"
                        onClick={(e) => {
                            e.preventDefault();
                            $("#addNewModal").modal("show");
                            return true;
                        }}>Make New Request</button>
                </div>
                <div className="form-group pull-right">
                    <button id="exportButton" type="submit" className="btn btn-default"
                        onClick={(e) => {
                            e.preventDefault();
                            this.handleSearch(e, "/AdminRequest/" + this.props.type + "/Export");
                            return true;
                        }}>Export To CSV</button>
                </div>
            </form>
        );
    }
}