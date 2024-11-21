app.controller('commoditycontroller', function ($scope, commodityservice) {

    getcommodities();
    getlocations();
    get_pack_types();
    load_batches();
    //loadallsms();
  

    $scope.open_new = function () {
        $scope.addnew = 1;
    }

    $scope.close_new = function () {
        $scope.addnew = 0;
    }

    function load_batches() {
      
        var request = commodityservice.getbatches();
        request.then(function (pl) {
            $scope.batches = pl.data;
        });
    }

    function loadallsms() {
        var request = smsservice.getallsms();

        request.then(function (pl) {
            $scope.messages = pl.data;
        });
    }

    function getcommodities() {
        var request = commodityservice.getcommodities();

        request.then(function (pl) {
            $scope.commodities = pl.data;
         
        });
    }

    function getstocks() {
        var request = commodityservice.get_stock_summary();

        request.then(function (pl) {
            $scope.stocks = pl.data;

        });
    }


    $scope.get_batch_stocks=function(batch_id,balance,commodity_name) {
        var request = commodityservice.get_batch_stocks(batch_id);
        $scope.balance = balance;
        $scope.commodity_name = commodity_name;

        request.then(function (pl) {
            $scope.stocks = pl.data;

        });
    }


    function getlocations() {
        var request = commodityservice.getlocations();

        request.then(function (pl) {
            $scope.locations = pl.data;

        });
    }

    function get_pack_types() {
        var request = commodityservice.get_pack_types();
        request.then(function (pl) {
            $scope.pack_types = pl.data;

        });
    }

    $scope.add_batch = function () {
        var batch = { commodity_id: $scope.commodity_id, quantity: $scope.quantity, expiry_date: $scope.expiry_date, pack_type: $scope.pack_type,delivery_date:$scope.delivery_date,batch_code:$scope.batch_code,location_id:$scope.location_id };

        var request = commodityservice.post(batch);
        //alert("Success");
        request.then(function (rs) {
            $scope.msg_text = "";
            $scope.id = "";
            $scope.sender = "";
            $scope.msg = rs.data;
        });
    }
});