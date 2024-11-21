app.service('userservice', function ($http) {
 

    this.getbatches = function () {
        return $http.get(baseUrl() + "batch/getbatches");
    }

    this.get_commodity_batches = function (id) {
        return $http.get(baseUrl() + "batch/GetCommodityBatches/"+id);
    }

    this.getlocations = function () {
        return $http.get(baseUrl() + "util/getlocations");
    }

    this.get_pack_types = function () {
        return $http.get(baseUrl() + "util/getpacktypes");
    }


    this.getcommodities = function () {
        return $http.get(baseUrl() + "batch/getcommodities");
    }

    this.get_stock_summary = function (id) {
        return $http.get(baseUrl() + "stock/GetSummaryStocks/"+id);
    }

    this.get_batch_stocks = function (id) {
        return $http.get(baseUrl() + "stock/GetBatchStocks/" + id);
    }


    this.post = function (user) {
        var request = $http({
            method: "post",
            url: baseUrl() + "batch/PostBatch",
            data: user
        });
        return request;
    }


    this.post_shipment = function (shipment) {
        var request = $http({
            method: "post",
            url: baseUrl() + "stock/PostShipment",
            data: shipment
        });
        return request;
    }

});