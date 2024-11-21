class NewData extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            allowed_mimes:
                [
                    "text/csv",
                    "application/vnd.ms-excel",
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "application/zip",
                    "application/x-zip",
                    "application/zip-compressed",
                    "application/x-zip-compressed",
                    "multipart/zip",
                    "multipart/x-zip",
                    "application/gzip",
                    "application/x-gzip",
                    "application/gzip-compressed",
                    "application/x-gzip-compressed",
                    "multipart/gzip",
                    "multipart/x-gzip"
                ]
        };
    }

    handleInputChange(e, input) {
        input = input || e.target;

        var files = input.files;
        if (files && files.length) {
            for (var i = 0; i < files.length; i++) {
                var file = files[i];
                if (this.state.allowed_mimes.indexOf(file.type) < 0) {
                    //not csv
                    this.setState(Object.assign({}, this.state, { error: "One or more files are in the wrong format. Only CSV, XLS, and XLSX files are allowed. Please try again." }));
                    return true;
                }
                //if (file.type != "text/csv"
                //    && file.type != "application/vnd.ms-excel"
                //    && file.type != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") {
                //    //not csv
                //    this.setState(Object.assign({}, this.state, { error: "One or more files are in the wrong format. Only CSV, XLS, and XLSX files are allowed. Please try again." }));
                //    return true;
                //}
            }

            if (this.props.onNewFiles) {
                this.props.onNewFiles(files);

                //clear the input value
                input.value = "";

                if (!/safari/i.test(navigator.userAgent)) {
                    input.type = "";
                    input.type = "file"
                }
            }
            else {
                this.setState(Object.assign({}, this.state, { error: "Error processing files. Please try again." }));
                return true;
            }
        }

        if (this.state.error) {
            this.setState(Object.assign({}, this.state, { error: null }));
        }

        return true;
    }

    render() {
        var btnClass = "btn-primary";

        var error = this.state.error;
        var errorTag = null;

        if (error) {
            btnClass = "btn-danger";
            errorTag = <p className="text-danger" style={{ marginBottom: 0 }}>{error}</p>;
        }

        return (
            <div className="new-data">
                <input type="file" id="fileInput" name="newFile" style={{ display: "none" }} accept=".csv,.xls,.xlsx,.zip,.gz" multiple="multiple"
                    onChange={(e) => {
                        this.handleInputChange(e, e.target);
                    }} />
                {
                    <button type="button" className={"btn " + btnClass} onClick={() => { $('#fileInput').trigger('click'); }}>Add New Upload</button>
                }
                {errorTag}
            </div>
        );
    }
}