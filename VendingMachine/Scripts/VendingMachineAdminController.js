(function () {
    var app = angular.module('VendingMachine', ['ngFileUpload']);

    app.config(['$qProvider', function ($qProvider) {
        $qProvider.errorOnUnhandledRejections(false);
    }]);

    app.controller('VendingMachineAdminController', ['$scope', '$http', 'Upload', '$timeout', function ($scope, $http, Upload, $timeout) {
        $scope.cashBox = null;
        $scope.storeItems = null;
        $scope.adminKey = null;

        $scope.updateVendingMachineAdmin = function () {
            console.log('updateVendingMachineAdmin invoked');
            $scope.getList();
            $scope.getStoreItemsList();
            $scope.getAdminKey();
            console.log('updateVendingMachineAdmin completed');
        };

        $scope.reloadPage = function () { $window.location.reload(); }


        $scope.getAdminKey = function () {
            console.log('create invoked');

            $http.get('/CashBox/GetAdminKey').then(
                function (successResponse) {
                    console.log('getAdminKey function completed with success');
                    $scope.adminKey = successResponse.data.value;
                },
                function (errorResponse) {
                    console.warn('getAdminKey function completed with error');
                });
        };

        $scope.changeAdminKey = function (key) {
            console.log('changeAdminKey function invoked with id: ', key);

            var req = {
                method: 'POST',
                url: '/CashBox/ChangeAdminKey',
                headers: {
                    'Content-Type': 'application/json'
                },
                data: {
                    adminKey: key,
                }
            };

            $http(req).then(
                function (successResponse) {
                    console.log('changeAdminKey function completed with success');
                    $scope.updateVendingMachineAdmin();
                },
                function (errorResponse) {
                    console.warn('changeAdminKey function completed with error');
                    $scope.updateVendingMachineAdmin();
                });
        };

        $scope.import = function (file) {
            console.log('import function invoked.');

            file.upload = Upload.upload({
                url: '/CashBox/Import',
                data: { uploadJsonFile: file },
            });

            file.upload.then(function (response) {
                $timeout(function () {
                    file.result = response.data;
                });
                $scope.updateVendingMachineAdmin();
            }, function (response) {
                if (response.status > 0)
                    $scope.errorMsg = response.status + ': ' + response.data;
            }, function (evt) {
                // Math.min is to fix IE which reports 200% sometimes
                file.progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
            });
        };

        $scope.importDrinks = function (file) {
            console.log('importDrinks function invoked.');

            file.upload = Upload.upload({
                url: '/Store/Import',
                data: { uploadJsonFile: file },
            });

            file.upload.then(function (response) {
                $timeout(function () {
                    file.result = response.data;
                });
            }, function (response) {
                if (response.status > 0)
                    $scope.errorMsg = response.status + ': ' + response.data;
                $scope.updateVendingMachineAdmin();
            }, function (evt) {
                // Math.min is to fix IE which reports 200% sometimes
                file.progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
            });
        };

        $scope.getList = function () {
            $http.get('/CashBox/GetList').then(
                function (successResponse) {
                    console.log('getList completed with success');
                    $scope.cashBox  = successResponse.data.value;
                },
                function (errorResponse) {
                    console.warn('getList completed with error');
                    alert(errorResponse.data.responseText);
                    console.warn($scope.data.responseText);
                });
        };

        $scope.getStoreItemsList = function () {
            $http.get('/Store/GetList').then(
                function (successResponse) {
                    console.log('getStoreItemsList completed with success');
                    $scope.storeItems = successResponse.data.value;
                },
                function (errorResponse) {
                    console.warn('getStoreItemsList completed with error');
                    alert(errorResponse.data.responseText);
                    console.warn($scope.data.responseText);
                });
        };

        $scope.export = function () {
            window.open('/CashBox/Export', '_blank', '');
        }

        $scope.exportDrinks = function () {
            window.open('/Store/Export', '_blank', '');
        }

        $scope.create = function () {
            console.log('create invoked');

            $http.post('/CashBox/Create').then(
                function (successResponse) {
                    console.log('create function completed with success');
                    $scope.updateVendingMachineAdmin();
                },
                function (errorResponse) {
                    console.warn('create function completed with error');
                });
        };

        $scope.createDrink = function () {
            console.log('createDrink invoked');

            $http.post('/Store/Create').then(
                function (successResponse) {
                    console.log('createDrink function completed with success');
                    $scope.updateVendingMachineAdmin();
                },
                function (errorResponse) {
                    console.warn('createDrink function completed with error');
                });
        };

        $scope.delete = function (id) {
            console.log('delete function invoked with id: ', id);

            var req = {
                method: 'POST',
                url: '/CashBox/Delete',
                headers: {
                    'Content-Type': 'application/json'
                },
                data: {
                    id: id,
                }
            };

            $http(req).then(
                function (successResponse) {
                    console.log('delete function completed with success');
                    $scope.updateVendingMachineAdmin();
                },
                function (errorResponse) {
                    console.warn('delete function completed with error');
                });
        };

        $scope.deleteStoreItem = function (id) {
            console.log('deleteStoreItem function invoked with id: ', id);

            var req = {
                method: 'POST',
                url: '/Store/Delete',
                headers: {
                    'Content-Type': 'application/json'
                },
                data: {
                    id: id,
                }
            };

            $http(req).then(
                function (successResponse) {
                    console.log('deleteStoreItem function completed with success');
                    $scope.updateVendingMachineAdmin();
                },
                function (errorResponse) {
                    console.warn('deleteStoreItem function completed with error');
                });
        };

        $scope.edit = function (id) {
            console.log('edit function invoked with id: ', id);

            var req = {
                method: 'POST',
                url: '/CashBox/Edit',
                headers: {
                    'Content-Type': 'application/json'
                },
                data: {
                    id: id,
                }
            };

            $http(req).then(
                function (successResponse) {
                    console.log('edit function completed with success');
                    $scope.updateVendingMachineAdmin();
                },
                function (errorResponse) {
                    console.warn('edit function completed with error');
                });
        };

        $scope.editStoreItem = function (id) {
            console.log('editStoreItem function invoked with id: ', id);

            var req = {
                method: 'POST',
                url: '/Store/Edit',
                headers: {
                    'Content-Type': 'application/json'
                },
                data: {
                    id: id,
                }
            };

            $http(req).then(
                function (successResponse) {
                    console.log('editStoreItem function completed with success');
                    $scope.updateVendingMachineAdmin();
                },
                function (errorResponse) {
                    console.warn('editStoreItem function completed with error');
                });
        };


        $scope.apply = function (id, name, amount, quantity, isDisable, file) {
            console.log('apply function invoked.');

            if (file == null) {
                console.log('only post without file upload');

                var req = {
                    method: 'POST',
                    url: '/CashBox/ApplyProps',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    data: {
                        id: id,
                        name: name,
                        amount: amount,
                        quantity: quantity,
                        isDisable: isDisable
                    }
                };

                $http(req).then(
                    function (successResponse) {
                        console.log('editStoreItem function completed with success');
                        $scope.updateVendingMachineAdmin();
                    },
                    function (errorResponse) {
                        console.warn('editStoreItem function completed with error');
                        $scope.updateVendingMachineAdmin();
                    });

            }
            else {
                try {
                    file.upload = Upload.upload({
                        url: '/CashBox/Apply',
                        data: {
                            id: id,
                            name: name,
                            amount: amount,
                            quantity: quantity,
                            isDisable: isDisable,
                            uploadImageFile: file
                        }
                    });

                    file.upload.then(
                        function (successResponse) {
                            console.log('editStoreItem function completed with success');
                            $scope.updateVendingMachineAdmin();
                        },
                        function (errorResponse) {
                            console.warn('editStoreItem function completed with error');
                            $scope.updateVendingMachineAdmin();
                        });


                } catch (e) {
                    console.log("Got an error!", e);
                    $scope.reloadPage();
                }
            }

        };

        $scope.applyStoreItem = function (id, name, amount, quantity, file) {
            console.log('applyStoreItem function invoked.');

            if (file == null) {
                console.log('only post without file upload');

                var req = {
                    method: 'POST',
                    url: '/Store/ApplyProps',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    data: {
                        id: id,
                        name: name,
                        amount: amount,
                        quantity: quantity,
                    }
                };

                $http(req).then(
                    function (successResponse) {
                        console.log('editStoreItem function completed with success');
                        $scope.updateVendingMachineAdmin();
                    },
                    function (errorResponse) {
                        console.warn('editStoreItem function completed with error');
                        $scope.updateVendingMachineAdmin();
                    });

            }
            else {
                file.upload = Upload.upload({
                    url: '/Store/Apply',
                    data: {
                        id: id,
                        name: name,
                        amount: amount,
                        quantity: quantity,
                        uploadImageFile: file
                    }
                });

                file.upload.then(function (response) {
                    $timeout(function () {
                        file.result = response.data;
                    });
                    $scope.updateVendingMachineAdmin();
                }, function (response) {
                    if (response.status > 0)
                        $scope.errorMsg = response.status + ': ' + response.data;
                }, function (evt) {
                    // Math.min is to fix IE which reports 200% sometimes
                    file.progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                });

            }

        };

    }]);
})();
