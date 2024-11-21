app.service('commodityservice', function ($http) {
    //PostSmsByDate
    this.getsmsbydate = function (date) {
        //var request = $http({
        //    method: 'post',
        //    url: baseUrl() + "sms/PostSmsByDate",
        //    data: date
        //});
        //return request;

        return $http.get(baseUrl() + "sms/getsmsbydate/" + date);
    }

    this.getbatches = function () {
        return $http.get(baseUrl() + "batch/getbatches");
    }

    this.get_commodity_batches = function (id) {
        return $http.get(baseUrl() + "batch/GetCommodityBatches/"+id);
    }

    this.getlocations = function () {
        return $http.get(baseUrl() + "util/GetLabLocations");
    }

    this.get_pack_types = function () {
        return $http.get(baseUrl() + "util/getpacktypes");
    }


    this.getcommodities = function () {
        return $http.get(baseUrl() + "batch/getcommodities");
    }

    this.get_stock_summary = function () {
        return $http.get(baseUrl() + "stock/GetSummaryStocks");
    }

    this.get_store_stock_summary = function (location_id) {
        return $http.get(baseUrl() + "stock/GetSummaryStoreStocks/"+location_id);
    }

    this.get_batch_stocks = function (id) {
        return $http.get(baseUrl() + "stock/GetBatchStocks/" + id);
    }


    this.post = function (batch) {
        var request = $http({
            method: "post",
            url: baseUrl() + "batch/PostBatch",
            data: batch
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