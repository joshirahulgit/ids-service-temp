(function () {
    var app = angular.module('schedulerConfigApp', []),
        uri = 'api/Account?enumType=',
        errorMessage = function (data, status) {
            return 'Error: ' + status +
                (data.Message !== undefined ? (' ' + data.Message) : '');
        };



    var configItem = function (code, name, order) {
        var code = code;
        var name = name;
        var order = order;

        this.getCode = function () {
            return code;
        };

        this.getName = function () {
            return name;
        };

        this.getOrder = function () {
            return order;
        };
    };

    var setItems = function ($http, items) {
        if (!(items instanceof Array))
            return;

        for (var i in items) {
            $http({
                method: 'GET',
                url: uri
            }).then(function successCallback(response) {
                items[i].items = response.data.AccountEnums;
            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
            });
        }
    };

    var getConfigItemList = function () {
        var configItems = [];

        var hcpcs = new configItem('HCPCS', 'HCPCS List', 1);
        configItems.push(hcpcs);
        var HeardOfUs = new configItem('HeardOfUs', 'Heard Of Us List', 2);
        configItems.push(HeardOfUs);
        var PatientAilment = new configItem('PatientAilment', 'Patient Ailment List', 3);
        configItems.push(PatientAilment);
        var ReferralGroups = new configItem('ReferralGroups', 'Referral Groups', 4);
        configItems.push(ReferralGroups);
        var DiagnosisFlags = new configItem('DiagnosisFlags', 'Diagnosis Flags', 5);
        configItems.push(DiagnosisFlags);
        var CCTypes = new configItem('CCTypes', 'Payment Card Types', 6);
        configItems.push(CCTypes);
        var PaymentStatuses = new configItem('PaymentStatuses', 'Payment Statuses', 7);
        configItems.push(PaymentStatuses);
        var MammoBirads = new configItem('MammoBirads', 'Mammo Birads Codes', 8);
        configItems.push(MammoBirads);
        var MammoBreastDensity = new configItem('MammoBreastDensity', 'Mammo Breast Density List', 9);
        configItems.push(MammoBreastDensity);
        var MammoLaterality = new configItem('MammoLaterality', 'Mammo Laterality List', 10);
        configItems.push(MammoLaterality);
        var MammoNodalStatus = new configItem('MammoNodalStatus', 'Mammo Nodal Statuses List', 11);
        configItems.push(MammoNodalStatus);
        var MammoTumorSize = new configItem('MammoTumorSize', 'Mammo Tumor Size List', 12);
        configItems.push(MammoTumorSize);
        var MammoBiopsyType = new configItem('MammoBiopsyType', 'Mammo Biopsy Type List', 13);
        configItems.push(MammoBiopsyType);

        return configItems;
    };

    var attachNgClasses = function (items) {
        if (!(items instanceof Array))
            return;

        for (var i in items) {
            items[i].accordionBodyVisible = false;
        }
    };

    app.controller('lookupCtrl', ['$http', '$scope', function ($http, $scope) {


        //Handler to load EnumType childen
        var getItems = function (configItem) {

            $http({
                method: 'GET',
                url: uri + configItem.getCode()
            }).then(function successCallback(response) {
                configItem.Children = response.data.AccountEnums;
                //$scope.$apply();
            }, function errorCallback(response) {
                configItem.Children = [];
                // called asynchronously if an error occurs
                // or server returns response with an error status.
            });
        };


        var configItems = getConfigItemList();
        attachNgClasses(configItems);

        //Add handler to EnumTypes to load child.
        for (var i in configItems) {
            getItems(configItems[i]);
        }

        //setItems($http, configItems);
        $scope.configItems = configItems;
        $scope.accordionClickHandler = function (item) {
            if (item && item.accordionBodyVisible != undefined) {
                if (item.accordionBodyVisible)
                    item.accordionBodyVisible = false;
                else
                    item.accordionBodyVisible = true;
            }
        };


        //$scope.getAllFromCustomer = function () {
        //    if ($scope.customerId.length == 0) return;
        //    $http.get(uri + '/' + $scope.customerId)
        //        .success(function (data, status) {
        //            $scope.complaints = data; // show current complaints
        //            if ($scope.customerIdSubscribed &&
        //                $scope.customerIdSubscribed.length > 0 &&
        //                $scope.customerIdSubscribed !== $scope.customerId) {
        //                // unsubscribe to stope to get notifications for old customer
        //                hub.server.unsubscribe($scope.customerIdSubscribed);
        //            }
        //            // subscribe to start to get notifications for new customer
        //            hub.server.subscribe($scope.customerId);
        //            $scope.customerIdSubscribed = $scope.customerId;
        //        })
        //        .error(function (data, status) {
        //            $scope.complaints = [];
        //            $scope.errorToSearch = errorMessage(data, status);
        //        })
        //};
        //$scope.postOne = function () {
        //    $http.post(uri, {
        //        COMPLAINT_ID: 0,
        //        CUSTOMER_ID: $scope.customerId,
        //        DESCRIPTION: $scope.descToAdd
        //    })
        //        .success(function (data, status) {
        //            $scope.errorToAdd = null;
        //            $scope.descToAdd = null;
        //        })
        //        .error(function (data, status) {
        //            $scope.errorToAdd = errorMessage(data, status);
        //        })
        //};
        //$scope.putOne = function () {
        //    $http.put(uri + '/' + $scope.idToUpdate, {
        //        COMPLAINT_ID: $scope.idToUpdate,
        //        CUSTOMER_ID: $scope.customerId,
        //        DESCRIPTION: $scope.descToUpdate
        //    })
        //        .success(function (data, status) {
        //            $scope.errorToUpdate = null;
        //            $scope.idToUpdate = null;
        //            $scope.descToUpdate = null;
        //        })
        //        .error(function (data, status) {
        //            $scope.errorToUpdate = errorMessage(data, status);
        //        })
        //};
        //$scope.deleteOne = function (item) {
        //    $http.delete(uri + '/' + item.COMPLAINT_ID)
        //        .success(function (data, status) {
        //            $scope.errorToDelete = null;
        //        })
        //        .error(function (data, status) {
        //            $scope.errorToDelete = errorMessage(data, status);
        //        })
        //};
        //$scope.editIt = function (item) {
        //    $scope.idToUpdate = item.COMPLAINT_ID;
        //    $scope.descToUpdate = item.DESCRIPTION;
        //};
        $scope.toShow = function () { return $scope.complaints && $scope.complaints.length > 0; };

        // at initial page load
        $scope.orderProp = 'COMPLAINT_ID';
    }]);
})();
