class SelectionSearchData extends SearchData {
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
            <form id="searchForm" className="form-inline" style={{ display: "inline-block" }} method="POST" action="/SctoListing/Selection">
                <div dangerouslySetInnerHTML={{
                    __html: this.props.anti_forgery_token_tag
                }} ></div>
                <div className="form-group">
                    <div className="input-group">
                        <div className="input-group-addon">Cluster</div>
                        <input id="clusterInput" type="number" min="1" className="form-control" id="clusterInput"
                            name="cluster" defaultValue={this.props.cluster} style={{ maxWidth: 75 }}/>
                    </div>
                </div>
                {
                    //<div className="form-group">
                    //    <div className="input-group">
                    //        <div className="input-group-addon">Limit</div>
                    //        <input type="number" min="1" max="100" className="form-control" id="hhCountInput"
                    //            name="limit" defaultValue={filter.limit || 25} />
                    //    </div>
                    //</div>
                
                    //Object.keys(fields).map((f) => {
                    //    var v = fields[f];
                    //    v.from = v.from || "";
                    //    v.to = v.to || "";

                    //    var items = {};
                    //    items[f] = this.props.fields[f];
                    //    items = { ...items, ...fieldsLeft };
                    //    return (
                    //        <Filter key={f} items={items} data={{ field: f, fieldText: items[f].text, fieldType: items[f].type, from: v.from, to: v.to }}
                    //            onChange={(o, n, f, t) => { this.handleFilterChange(o, n, f, t); }}
                    //            onDelete={(f, d, e) => { this.handleFilterDelete(f, d, e); }} />
                    //    );
                    //})
                }
                {addFilterTag}
                <div className="form-group">
                    <div className="btn-group">
                        <button id="filterButton" type="submit" className="btn btn-primary"
                            style={{ borderRight: 0 }}
                            onClick={(e) => {
                                e.preventDefault();
                                window.location.href = "?cluster=" + $("#clusterInput").val();
                                return true;
                            }}>Load Selected Households</button>
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
                    <button id="randomizeButton" type="submit" className="btn btn-default">Select Random Households</button>
                </div>
            </form>
        );
    }
}