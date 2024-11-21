class SearchData extends React.Component {
    render() {
        var filter = this.props.data || {};

        var since = filter.since || "";
        if (since)
            since = since.substr(0, since.indexOf('T'));

        var till = filter.till || "";
        if (till)
            till = till.substr(0, till.indexOf('T'));

        return (
            <form id="searchForm" className="form-inline"  method="GET" action="?">
                <div className="form-group">
                    <div className="input-group">
                        <div className="input-group-addon">Stage</div>
                        <select className="form-control" id="stageInput" name="stage" defaultValue={filter.stage || ""}>
                            <option value="">All</option>
                            <option value="awaiting_review">Awaiting Review</option>
                            <option value="in_review">In Review</option>
                            <option value="approved">Approved</option>
                            <option value="disapproved">Disapproved</option>
                        </select>
                    </div>
                </div>
                <div className="form-group">
                    <div className="input-group">
                        <div className="input-group-addon">Status</div>
                        <select className="form-control" id="stageStatusInput" name="stage_status" defaultValue={filter.stage_status || ""}>
                            <option value="">All</option>
                            <option value="processing">Processing</option>
                            <option value="successful">Successful</option>
                            <option value="erroneous">Erroneous</option>
                        </select>
                    </div>
                </div>
                <div className="form-group">
                    <div className="input-group">
                        <div className="input-group-addon">From</div>
                        <input type="date" className="form-control" id="startDateInput" placeholder="start date" name="since" defaultValue={since} />
                    </div>
                </div>
                <div className="form-group">
                    <div className="input-group">
                        <div className="input-group-addon">To</div>
                        <input type="date" className="form-control" id="endDateInput" placeholder="stop date" name="till" defaultValue={till} />
                    </div>
                </div>
                <div className="form-group">
                    <div className="input-group">
                        <div className="input-group-addon">Text</div>
                        <input type="text" className="form-control" id="searchInput" placeholder="search term" name="search_term" defaultValue={filter.search_term || ""} />
                    </div>
                </div>
            </form>
        );
    }
}