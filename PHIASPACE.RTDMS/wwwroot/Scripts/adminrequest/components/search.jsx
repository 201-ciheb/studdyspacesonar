class SearchData extends React.Component {
    constructor(props) {
        super(props);

        //create the other
        var other = {};

        var dataKeys = Object.keys(props.data);
        for (var dataKey of dataKeys) {
            if (props.data[dataKey] !== null && props.data[dataKey] !== "") {
                var otherKey = dataKey;
                if (dataKey.endsWith("_from")) {
                    otherKey = dataKey.substr(0, dataKey.indexOf("_from"));
                    var fromStr = props.data[dataKey];
                    if (props.fields[otherKey].type == "date") {
                        //remove trailing T
                        fromStr = fromStr.substr(0, fromStr.indexOf('T'))
                    }
                    other[otherKey] = { from: fromStr, to: (other[otherKey] || {}).to || "" };
                } else if (dataKey.endsWith("_to")) {
                    otherKey = dataKey.substr(0, dataKey.indexOf("_to"));
                    var toStr = props.data[dataKey];
                    if (props.fields[otherKey].type == "date") {
                        //remove trailing T
                        toStr = toStr.substr(0, toStr.indexOf('T'))
                    }
                    other[otherKey] = { from: (other[otherKey] || {}).from || "", to: toStr };
                } else if (props.fields[otherKey] !== undefined) {
                    var valStr = props.data[dataKey];
                    other[otherKey] = { from: valStr, to: valStr };
                }
            }
        }

        this.state = {
            data: props.data || {},
            other: other
        };
    }

    getFormUrlEncodedData() {
        var form = document.getElementById('searchForm');
        var data = new FormData(form);

        var urlEncodedData = "";
        var urlEncodedDataPairs = [];

        // Turn the data object into an array of URL-encoded key/value pairs.
        for (var entry of data.entries()) {
            var value = entry[1];

            if (!value && value !== 0)
                continue;

            urlEncodedDataPairs.push(encodeURIComponent(entry[0]) + '=' + encodeURIComponent(value));
        }

        if (urlEncodedDataPairs.length > 0) {
            // Combine the pairs into a single string and replace all %-encoded spaces to 
            // the '+' character; matches the behaviour of browser form submissions.
            urlEncodedData = urlEncodedDataPairs.join('&').replace(/%20/g, '+');
        }

        return urlEncodedData;
    }

    handleSearch(e, url) {
        e.preventDefault();

        var urlEncodedData = this.getFormUrlEncodedData();

        var location = window.location;

        if (url) {
            window.location = url + "?" + urlEncodedData;
        } else {
            window.location.href = "?" + urlEncodedData;
        }
    }

    handleFilterChange(oldField, field, from, to) {
        var selectedFields = this.state.other || {};

        var changed = selectedFields[field] === undefined || oldField != field;

        if (oldField != field) {
            //to maintain the order, carefully replace the property
            var newSelectedFields = {};

            for (var key of Object.keys(selectedFields)) {
                if (key != oldField) {
                    newSelectedFields[key] = selectedFields[key];
                } else {
                    //use the new field
                    newSelectedFields[field] = null;
                }
            }

            //finally set the new selectedFields
            selectedFields = newSelectedFields;
        }

        selectedFields[field] = { from, to };

        if (changed) {
            this.setState(Object.assign({}, this.state, { other: selectedFields }));
        }
    }

    handleFilterDelete(field, filterData, e) {
        var selectedFields = this.state.other || {};

        if (selectedFields[field] != undefined) {
            delete selectedFields[field];

            this.setState(Object.assign({}, this.state, { other: selectedFields }));
        }
    }

    handleFilterAdd(e) {
        var fieldsLeft = this.getFieldsLeft();

        var newField = Object.keys(fieldsLeft)[0];

        this.handleFilterChange(newField, newField, null, null);
    }

    getFieldsLeft() {
        var selectedFields = this.state.other || {};
        var allFields = this.props.fields;
        var fieldsLeft = { ...allFields };

        for (var field of Object.keys(selectedFields)) {
            delete fieldsLeft[field];
        }

        return fieldsLeft;
    }
}