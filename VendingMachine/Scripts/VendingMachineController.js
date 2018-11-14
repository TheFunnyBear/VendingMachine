(function () {
    var appVendingMachineModule = angular.module('VendingMachineModule', [
    ]);

    appVendingMachineModule.config(['$qProvider', function ($qProvider) {
        $qProvider.errorOnUnhandledRejections(false);
    }]);

    appVendingMachineModule.controller('VendingMachineController', ['$scope', '$http', '$templateCache', function ($scope, $http, $templateCache) {
        $scope.balance = 0;
        $scope.store = null;
        $scope.change = null;
        $scope.basket = null;
        $scope.cashBox = null;

        $isOneRubleEnable = false;
        $isTwoRublesEnable = false;
        $isFiveRublesEnable = false;
        $isTenRublesEnable = false

        $scope.data = null;
        $scope.status = null;
        $scope.headers = null
        $scope.config = null;
        $scope.statusText = null;

        $scope.updateBoard = function () {
            console.log('updateBoard invoked');
            $scope.getBalance();
            $scope.getCoinSlots();
            $scope.getStore();
            $scope.getChangeCoins();
            $scope.getBasket();
            console.log('updateBoard completed');
        };

        $scope.getBalance = function () {
            $http.get('/VendingMachine/GetBalance').then(
                function (successResponse) {
                    console.log('getBalance completed with success');
                    $scope.balance = successResponse.data.balance;
                },
                function (errorResponse) {
                    console.warn('getBalance completed with error');
                    alert(errorResponse.data.responseText);
                    console.warn($scope.data.responseText);
                });
        };

        $scope.getStore = function () {
            $http.get('/VendingMachine/GetStore').then(
                function (successResponse) {
                    console.log('getStore completed with success');
                    $scope.store = successResponse.data.store;
                },
                function (errorResponse) {
                    console.warn('getStore completed with error');
                    alert(errorResponse.data.responseText);
                    console.warn($scope.data.responseText);
                });
        };

        $scope.getCoinSlots = function () {
            console.log('getCoinSlots function invoked');

            $http.get('/VendingMachine/GetCoinSlots').then(
                function (successResponse) {
                    console.log('getCoinSlots completed with success');
                    $scope.cashBox = successResponse.data.cashBox;
                },
                function (errorResponse) {
                    console.warn('getCoinSlots completed with error');
                    alert(errorResponse.data.responseText);
                    console.warn($scope.data.responseText);
                });
        };

        $scope.getChangeCoins = function () {
            $http.get('/VendingMachine/GetChangeCoins').then(
                function (successResponse) {
                    console.log('getChangeCoins completed with success');
                    $scope.change = successResponse.data.change;
                },
                function (errorResponse) {
                    console.warn('getChangeCoins completed with error');
                    alert(errorResponse.data.responseText);
                    console.warn($scope.data.responseText);
                });
        };

        $scope.getBasket = function () {
            $http.get('/VendingMachine/GetBasket').then(
                function (successResponse) {
                    console.log('getBasket completed with success');
                    $scope.basket = successResponse.data.basket;
                },
                function (errorResponse) {
                    console.warn('getBasket completed with error');
                    alert(errorResponse.data.responseText);
                    console.warn($scope.data.responseText);
                });
        };

        $scope.insetCoin = function (coin) {
            console.log('buy function invoked with id: ', coin);

            var req = {
                method: 'POST',
                url: '/VendingMachine/InsetCoin',
                headers: {
                    'Content-Type': 'application/json'
                },
                data: {
                    coin: coin,
                }
            };

            $http(req).then(
                function (successResponse) {
                    console.log('insetCoin function completed with success');
                    $scope.updateBoard();
                },
                function (errorResponse) {
                    console.warn('insetCoin function completed with error');
                });
        };

        $scope.buy = function (id) {
            console.log('buy function invoked with id: ', id);

            var req = {
                method: 'POST',
                url: '/VendingMachine/Buy',
                headers: {
                    'Content-Type': 'application/json'
                },
                data: {
                    id: id,
                }
            };

            $http(req).then(
                function (successResponse) {
                    console.log('buy function completed with success');
                    $scope.updateBoard();
                },
                function (errorResponse) {
                    console.warn('buy function completed with error');
                });
        };

        $scope.clearBasket = function () {
            console.log('clearBasket invoked');

            var req = {
                method: 'POST',
                url: '/VendingMachine/ClearBasket',
            };

            $http(req).
                then(function (response) {
                    console.log('then invoked');
                    $scope.data = angular.fromJson(response.data);
                    $scope.status = response.status;
                    $scope.config = response.config;
                    $scope.statusText = response.statusText;
                    console.log('$scope.data: ', $scope.data);
                    console.log('$scope.data.success: ', $scope.data.success);
                    console.log('$scope.data.responseText: ', $scope.data.responseText);
                    console.log('$scope.status: ', $scope.status);
                    console.log('$scope.headers: ', $scope.headers);
                    console.log('$scope.config: ', $scope.config);
                    console.log('$scope.statusText: ', $scope.statusText);

                    if ($scope.data.success == false) {
                        console.warn('clearBasket completed with error');
                        alert($scope.data.responseText);
                        console.warn($scope.data.responseText);
                    } else {
                        console.log('clearBasket completed with success');
                        $scope.updateBoard();
                    }

                });
        };

        $scope.takeChange = function () {
            console.log('takeChange invoked');

            var req = {
                method: 'POST',
                url: '/VendingMachine/TakeChange',
            };

            $http(req).
                then(function (response) {
                    console.log('then invoked');
                    $scope.data = angular.fromJson(response.data);
                    $scope.status = response.status;
                    $scope.config = response.config;
                    $scope.statusText = response.statusText;
                    console.log('$scope.data: ', $scope.data);
                    console.log('$scope.data.success: ', $scope.data.success);
                    console.log('$scope.data.responseText: ', $scope.data.responseText);
                    console.log('$scope.status: ', $scope.status);
                    console.log('$scope.headers: ', $scope.headers);
                    console.log('$scope.config: ', $scope.config);
                    console.log('$scope.statusText: ', $scope.statusText);

                    if ($scope.data.success == false) {
                        console.warn('takeChange completed with error');
                        alert($scope.data.responseText);
                        console.warn($scope.data.responseText);
                    } else {
                        console.log('takeChange completed with success');
                        $scope.updateBoard();
                    }

                });
        };

        $scope.clearChange = function () {
            console.log('clearChange invoked');

            var req = {
                method: 'POST',
                url: '/VendingMachine/ClearChange',
            };

            $http(req).
                then(function (response) {
                    console.log('then invoked');
                    $scope.data = angular.fromJson(response.data);
                    $scope.status = response.status;
                    $scope.config = response.config;
                    $scope.statusText = response.statusText;
                    console.log('$scope.data: ', $scope.data);
                    console.log('$scope.data.success: ', $scope.data.success);
                    console.log('$scope.data.responseText: ', $scope.data.responseText);
                    console.log('$scope.status: ', $scope.status);
                    console.log('$scope.headers: ', $scope.headers);
                    console.log('$scope.config: ', $scope.config);
                    console.log('$scope.statusText: ', $scope.statusText);

                    if ($scope.data.success == false) {
                        console.warn('clearChange completed with error');
                        alert($scope.data.responseText);
                        console.warn($scope.data.responseText);
                    } else {
                        console.log('clearChange completed with success');
                        $scope.updateBoard();
                    }

                });
        };

        $scope.cantTransmit = function (errorMessage) {
            // handle errors here
            alert(errorMessage);
        };

    }]);
})();
