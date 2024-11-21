class StructureSearchData extends SearchData {
    constructor(props) {
        super(props);
    }

    handleResidentialChange(e) {
        var val = "";

        if (e.target.value == "true") {
            val = true;
        } else if (e.target.value == "false") {
            val = false;
        }

        this.setState(Object.assign({}, this.state, { data: Object.assign(this.state.data, { is_residential: val }) }));
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

        var residentialTypeTag = null;
        var nonResidentialTypeTag = null;

        if (filter.is_residential === true) {
            residentialTypeTag = <div className="form-group">
                <div className="input-group">
                    <div className="input-group-addon">Residential Type</div>
                    <select className="form-control" id="residentialTypeInput" name="residential_type" defaultValue={filter.residential_type || ""}>
                        <option value="">All</option>
                        <option value="1">Bungalow</option>
                        <option value="2">Traditional/Hut</option>
                        <option value="3">1 Storey</option>
                        <option value="4">2 Storey</option>
                        <option value="5">3 Storey</option>
                        <option value="6">More than 3 Storey</option>
                        <option value="7">Make Shift</option>
                        <option value="8">Shed/Container/Silos</option>
                        <option value="9">Other</option>
                    </select>
                </div>
            </div>;
        } else if (filter.is_residential === false) {
            nonResidentialTypeTag = <div className="form-group">
                <div className="input-group">
                    <div className="input-group-addon">Non-Residential Type</div>
                    <select className="form-control" id="residentialTypeInput" name="non_residential_type" defaultValue={filter.non_residential_type || ""}>
                        <option value="">All</option>
                        <option value="1">Uncompleted</option>
                        <option value="2">Completed But Unoccupied</option>
                        <option value="3">Dilapidated</option>
                        <option value="4">Temple/Church/Mosque</option>
                        <option value="5">School/Hospital</option>
                        <option value="6">Office/Shops/Warehouse</option>
                        <option value="7">Other</option>
                    </select>
                </div>
            </div>;
        }

        return (
            <form id="searchForm" className="form-inline" style={{ display: "inline-block", width: "100%" }} method="GET" action="/SctoListing/Structure">
                <div className="form-group">
                    <div className="input-group">
                        <div className="input-group-addon">Study</div>
                        <select className="form-control" id="studyInput" name="study" defaultValue={!isNaN(filter.study) ? filter.study : ""}>
                            <option value="">All</option>
                            <option value="0">PILOT</option>
                            <option value="1">WEB 1</option>
                            <option value="2">WEB 2</option>
                            <option value="3">WEB 3</option>
                            <option value="4">WEB 4</option>
                            <option value="5">WEB 5</option>
                            <option value="6">WEB 6</option>
                        </select>
                    </div>
                </div>
                <div className="form-group">
                    <div className="input-group">
                        <div className="input-group-addon">State</div>
                        <select className="form-control" id="stateInput" name="state" defaultValue={filter.state || ""}>
                            <option value="">All</option>
                            <option value="43">ABIA</option>
                            <option value="25">ADAMAWA</option>
                            <option value="52">AKWA IBOM</option>
                            <option value="41">ANAMBRA</option>
                            <option value="21">BAUCHI</option>
                            <option value="54">BAYELSA</option>
                            <option value="31">BENUE</option>
                            <option value="24">BORNO</option>
                            <option value="51">CROSS RIVER</option>
                            <option value="55">DELTA</option>
                            <option value="45">EBONYI</option>
                            <option value="56">EDO</option>
                            <option value="62">EKITI</option>
                            <option value="44">ENUGU</option>
                            <option value="34">FCT ABUJA</option>
                            <option value="22">GOMBE</option>
                            <option value="42">IMO</option>
                            <option value="17">JIGAWA</option>
                            <option value="15">KADUNA</option>
                            <option value="16">KANO</option>
                            <option value="14">KATSINA</option>
                            <option value="12">KEBBI</option>
                            <option value="37">KOGI</option>
                            <option value="36">KWARA</option>
                            <option value="66">LAGOS</option>
                            <option value="33">NASARAWA</option>
                            <option value="35">NIGER</option>
                            <option value="65">OGUN</option>
                            <option value="61">ONDO</option>
                            <option value="63">OSUN</option>
                            <option value="64">OYO</option>
                            <option value="32">PLATEAU</option>
                            <option value="53">RIVERS</option>
                            <option value="11">SOKOTO</option>
                            <option value="26">TARABA</option>
                            <option value="23">YOBE</option>
                            <option value="13">ZAMFARA</option>
                        </select>
                    </div>
                </div>
                <div className="form-group">
                    <div className="input-group">
                        <div className="input-group-addon">Cluster</div>
                        <input type="number" min="1" className="form-control" id="clusterInput"
                            name="cluster" defaultValue={this.props.cluster || ""} style={{ maxWidth: 70 }}/>
                    </div>
                </div>
                <div className="form-group">
                    <div className="input-group">
                        <div className="input-group-addon">Type</div>
                        <select className="form-control" id="typeInput" name="is_residential" defaultValue={filter.is_residential !== null? filter.is_residential : ""}
                            onChange={(e) => { this.handleResidentialChange(e); }}>
                            <option value="">All</option>
                            <option value="true">Residential</option>
                            <option value="false">Non-residential</option>
                        </select>
                    </div>
                </div>
                {residentialTypeTag}
                {nonResidentialTypeTag}
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
                        {
                            this.props.cluster ? (
                                <button id="resetClusterButton" type="button" className="btn btn-default"
                                    style={{ borderLeft: 0 }}
                                    onClick={(e) => {
                                        e.preventDefault();
                                        window.location.href = "?cluster=" + this.props.cluster;
                                        return true;
                                    }}>Reset Cluster</button>
                            ): null
                        }
                        <button id="resetButton" type="button" className="btn btn-default"
                            style={{ borderLeft: 0 }}
                            onClick={(e) => {
                                e.preventDefault();
                                window.location.href = "?";
                                return true;
                            }}>Reset</button>
                    </div>
                </div>
                <div className="form-group pull-right">
                    <button id="exportButton" type="submit" className="btn btn-default"
                        onClick={(e) => {
                            e.preventDefault();
                            this.handleSearch(e, "/SctoListing/Structure/Export");
                            return true;
                        }}>Export To CSV</button>
                </div>
            </form>
        );
    }
}