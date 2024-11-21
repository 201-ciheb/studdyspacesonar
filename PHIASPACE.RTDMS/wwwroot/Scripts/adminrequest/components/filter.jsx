class Filter extends React.Component {
    constructor(props) {
        super(props);

        this.state = { data: props.data || {} };

        if (!props.isEqualsOnly && this.state.data.from != this.state.data.to) {
            this.state.mode = "from";
        } else {
            this.state.mode = "equals";
        }
    }

    handleChange(e) {
        let thisNode = ReactDOM.findDOMNode(this);
        let field = $(".filter-field", thisNode).val();
        let mode = $(".filter-mode", thisNode).val();
        let from = $(".filter-from", thisNode).val();
        let to = null;

        if (mode == "from") {
            if (mode == this.state.mode) {
                to = $(".filter-to", thisNode).val();
            }
            else {
                to = this.state.data.to;
            }
        } else {
            to = from;
        }

        let data = this.state.data;

        if (data.fieldType === "number" && ((e && e.nativeEvent && e.nativeEvent.data === ".")
            || (from && from.toString().endsWith("."))
            || (to && to.toString().endsWith(".")))) {
            //reject this update
            return true;
        }

        if (data.field != field
            || this.state.mode != mode
            || data.from != from
            || data.to != to) {

            let oldField = data.field;

            this.setState(Object.assign({}, this.state, {
                mode: mode,
                data: Object.assign(data, { from: from, to: to, field: field })
            }));

            if (this.props.onChange) {
                this.props.onChange(oldField, field, from, to);
                return true;
            }
        }

        return false;
    }

    handleDelete(e) {
        if (this.props.onDelete) {
            this.props.onDelete(this.state.data.field, this.state.data, e);
            return true;
        }

        return false;
    }

    render() {
        let data = this.state.data || {};
        let items = { ...this.props.items };
        let field = data.field;
        let fieldText = data.fieldText;
        let fieldType = data.fieldType;
        let fieldOptions = data.fieldOptions;
        let isEqualsOnly = this.props.isEqualsOnly;

        if (items[field] === undefined) {
            items[field] = { text: fieldText, type: fieldType };
        }

        let from = data.from;
        let to = data.to;

        let fromTag = null, toTag = null;

        let mode = this.state.mode;

        if (fieldType !== "select") {
            fromTag = <input type={fieldType || "text"} className="form-control filter-from" placeholder="value"
                style={{ borderRight: mode == "from" ? 0 : null }} value={from}
                step={fieldType == "number" ? "any" : null}
                onChange={(e) => { this.handleChange(e); }} />;
        } else {
            fromTag = <select className="form-control filter-from" placeholder="choose value"
                style={{ borderRight: mode == "from" ? 0 : null }}
                defaultValue={from}
                onChange={(e) => { this.handleChange(e); }}>
                {
                    Object.keys(fieldOptions).map((k) => {
                        return (
                            <option key={k} value={k}>{fieldOptions[k]}</option>
                        );
                    })
                }
            </select>;
        }

        if (mode == "from") {
            //add the to tag
            toTag = [
                <div key={0} className="input-group-addon" style={{ borderRight: 0 }}>To</div>,
                fieldType !== "select" ?
                    <input key={1} type={fieldType || "text"} className="form-control filter-to" placeholder="stop value" value={to}
                        step={fieldType == "number" ? "any" : null}
                        onChange={(e) => { this.handleChange(e); }} />
                    :
                    <select key={1} className="form-control filter-to" placeholder="choose stop value"
                        defaultValue={to}
                        onChange={(e) => { this.handleChange(e); }}>
                        {
                            Object.keys(fieldOptions).map((k) => {
                                return (
                                    <option key={k} value={k}>{fieldOptions[k]}</option>
                                );
                            })
                        }
                    </select>
            ];
        }

        return (
            <div className="filter form-group">
                {
                    !isEqualsOnly ?
                        [
                            <input key={0} type="hidden" name={field + "_from"} defaultValue={from} />,
                            <input key={1} type="hidden" name={field + "_to"} defaultValue={to} />
                        ]
                        : <input type="hidden" name={field} defaultValue={from} />
                }
                <div className="input-group">
                    <div className="input-group-addon" style={{ paddingTop: 0, paddingBottom: 0 }}>
                        <select className="filter-field"
                            style={{ border: 0, textAlign: "center", textAlignLast: "center", backgroundColor: "transparent" }}
                            defaultValue={field}
                            onChange={(e) => { this.handleChange(e); }}>
                            {
                                Object.keys(items).map((k) => {
                                    return (
                                        <option key={k} value={k}>{items[k].text}</option>
                                    );
                                })
                            }
                        </select>
                    </div>
                    {
                        !isEqualsOnly ?
                            <div className="input-group-addon" style={{ padding: 0, borderRight: 0 }}>
                                <select className="filter-mode"
                                    style={{ border: 0, textAlign: "center", textAlignLast: "center", backgroundColor: "transparent" }}
                                    defaultValue={mode}
                                    onChange={(e) => { this.handleChange(e); }}>
                                    <option value="from">From</option>
                                    <option value="equals">Equals</option>
                                </select>
                            </div>
                            : <input type="hidden" className="filter-mode" defaultValue={mode} />
                    }
                    {fromTag}
                    {toTag}
                    <div className="input-group-addon">
                        <button type="button" className="close"
                            onClick={(e) => { this.handleDelete(e); }}>
                            <span className="glyphicon glyphicon-remove" aria-hidden="true"></span>
                        </button>
                    </div>
                </div>
            </div>
        );
    }
}