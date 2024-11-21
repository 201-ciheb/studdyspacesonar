app.controller('stockcontroller', function ($scope, commodityservice) {

    get_stock_summary();
    getlocations();
    $scope.carts = [];

    $scope.validate_quantity = function () {
        if ($scope.quantity > $scope.actual_quantity) {
            $scope.quantity = 0;
        }
        if ($scope.quantity < 0) {
            $scope.quantity = 0;
        }
    }

    $scope.add_to_cart = function () {
        $scope.carts.push({ batch_code: $scope.batch_code, commodity_name: $scope.commodity_name,quantity:$scope.quantity });
        $scope.addnew = 0;
        $scope.batch_code = "";
        $scope.quantity = "";
    }

    $scope.open_new = function (batch_code, commodity_name,quantity) {
        $scope.addnew = 1;
        $scope.batch_code = batch_code;
        $scope.commodity_name = commodity_name;
        $scope.actual_quantity = quantity;
    }

    $scope.close_new = function () {
        $scope.addnew = 0;
        $scope.batch_code = "";
    }

    function get_stock_summary() {
        var request = commodityservice.get_stock_summary();

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


    $scope.get_commodity_batches=function (id) {
        var request = commodityservice.get_commodity_batches(id);

        request.then(function (pl) {
            $scope.batches = pl.data;

        });
    }


    $scope.post_shipment = function () {
        var shipment = { carts: $scope.carts, location_id: $scope.location_id, comment: $scope.comment };

        var request = commodityservice.post_shipment(shipment);
        
        request.then(function (rs) {
            window.location.reload(true);
            $scope.carts = [];
            $scope.location_id = "";
            $scope.comment = "";
           
        });
    }
    

});
