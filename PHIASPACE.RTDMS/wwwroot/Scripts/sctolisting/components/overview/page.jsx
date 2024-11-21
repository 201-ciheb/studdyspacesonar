class DataPage extends React.Component {
    constructor(props) {
        super(props);

        var initialData = this.props.initialData;

        if (!initialData) {
            var $initialDataInput = $("#initialDataInput");
            initialData = $initialDataInput.val();
            initialData = JSON.parse(initialData);
            $initialDataInput.remove();
        }

        this.state = {
            data: initialData || {},
            isMounted: true
        };
    }

    componentWillReceiveProps(props) {
        var newState = { data: props.data || {} };

        if (!this.state) {
            this.state = newState;
        } else {
            this.setState(Object.assign({}, this.state, newState));
        }
    }

    componentWillUnmount() {
        this.state.isMounted = false;
    }

    componentDidMount() {
        this.pollAndUpdate();
    }

    pollAndUpdate() {
        var rThis = this;

        $.ajax({
            type: 'GET',
            url: //location.protocol + '//' + location.host +
                "/api/scto_listing/",
            cache: false,
            contentType: "application/json",
            success: function (data) {
                console.log(data);

                if (!rThis.state.isMounted)
                    return;

                //update
                if (data && Object.keys(data).length > 0) {
                    rThis.setState(Object.assign({}, rThis.state, { data: data }));
                }

                setTimeout(() => { rThis.pollAndUpdate(); }, 5000); //after 5 seconds
            },
            error: function (data, status, httpError) {
                console.log(data);

                if (!rThis.state.isMounted)
                    return;

                setTimeout(() => { rThis.pollAndUpdate(); }, 5000); //after 5 seconds
            }
        });
    }

    componentDidUpdate(prevProps, prevState) {
        //check for zonal changes and show an indicator on the divs
        var oldData = prevState.data;
        var data = this.state.data;

        if (!oldData || !data)
            return;

        var oldZoneStats = oldData.zone_stats;
        var zoneStats = data.zone_stats;

        if (!oldZoneStats || !zoneStats)
            return;

        for (var zone of Object.keys(zoneStats)) {
            var zoneStat = zoneStats[zone];
            var oldZoneStat = oldZoneStats[zone];

            if (!oldZoneStat || (oldZoneStat.clusters_visited_count != zoneStat.clusters_visited_count
                || oldZoneStat.households_visited_count != zoneStat.households_visited_count)) {
                //something has changed
                let $panel = $(".zone-panel", "#" + zone.toLowerCase() + "_panel");
                $panel.addClass("stats-changed");

                setTimeout(() => {
                    $panel.removeClass("stats-changed");
                }, 2000)
            }
        }
    }

    render() {
        var data = this.state.data;
        var zoneStats = data.zone_stats || {};

        zoneStats = Object.assign({
            "NORTH_WEST": {}, "NORTH_EAST": {}, "NORTH_CENTRAL": {},
            "SOUTH_EAST": {}, "SOUTH_SOUTH": {}, "SOUTH_WEST": {}
        }, zoneStats);

        for (var zone of Object.keys(zoneStats)) {
            var zoneStat = zoneStats[zone];

            if (!zoneStat.clusters_visited_count)
                zoneStat.clusters_visited_count = 0;

            if (!zoneStat.households_visited_count)
                zoneStat.households_visited_count = 0;
        }

        return (
            <div className="data-page">
                <div className="row" style={{ position: "absolute", top: 10, width: "100%" }}>
                    <div id="north_west_panel" className="col-sm-3" style={{ marginLeft: -10 }}>
                        <div className="panel panel-default zone-panel">
                            <div className="panel-body text-center">
                                {zoneStats.NORTH_WEST.clusters_visited_count} &mdash; {zoneStats.NORTH_WEST.households_visited_count}
                                <div className="zone-name">NW</div>
                            </div>
                        </div>
                    </div>
                    <div id="north_central_panel"  className="col-sm-3" style={{ marginLeft: "12.5000000025%" }}>
                        <div className="panel panel-default zone-panel">
                            <div className="panel-body text-center">
                                {zoneStats.NORTH_CENTRAL.clusters_visited_count} &mdash; {zoneStats.NORTH_CENTRAL.households_visited_count}
                                <div className="zone-name">NC</div>
                            </div>
                        </div>
                    </div>
                    <div id="north_east_panel" className="col-sm-3" style={{ marginLeft: "12.5000000025%" }}>
                        <div className="panel panel-default zone-panel">
                            <div className="panel-body text-center">
                                {zoneStats.NORTH_EAST.clusters_visited_count} &mdash; {zoneStats.NORTH_EAST.households_visited_count}
                                <div className="zone-name">NE</div>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="row" style={{ position: "fixed", bottom: 0, width: "100%" }}>
                    <div id="south_west_panel" className="col-sm-3" style={{ marginBottom: -10, marginLeft: -10 }}>
                        <div className="panel panel-default zone-panel">
                            <div className="panel-body text-center">
                                {zoneStats.SOUTH_WEST.clusters_visited_count} &mdash; {zoneStats.SOUTH_WEST.households_visited_count}
                                <div className="zone-name">SW</div>
                            </div>
                        </div>
                    </div>
                    <div id="south_south_panel" className="col-sm-3" style={{ marginBottom: -10, marginLeft: "12.5000000025%" }}>
                        <div className="panel panel-default zone-panel">
                            <div className="panel-body text-center">
                                {zoneStats.SOUTH_SOUTH.clusters_visited_count} &mdash; {zoneStats.SOUTH_SOUTH.households_visited_count}
                                <div className="zone-name">SS</div>
                            </div>
                        </div>
                    </div>
                    <div id="south_east_panel" className="col-sm-3" style={{ marginBottom: -10, marginLeft: "12.5000000025%" }}>
                        <div className="panel panel-default zone-panel">
                            <div className="panel-body text-center">
                                {zoneStats.SOUTH_EAST.clusters_visited_count} &mdash; {zoneStats.SOUTH_EAST.households_visited_count}
                                <div className="zone-name">SE</div>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="middle">
                    <div className="col-sm-4 col-sm-offset-2">
                        <div className="panel panel-default center-panel">
                            <div className="panel-body text-center">{data.total_clusters_visited_count}</div>
                            <div className="panel-footer text-center">clusters</div>
                        </div>
                    </div>
                    <div className="col-sm-4">
                        <div className="panel panel-default center-panel">
                            <div className="panel-body text-center">{data.total_households_visited_count}</div>
                            <div className="panel-footer text-center">households</div>
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