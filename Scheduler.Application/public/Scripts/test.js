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

        $scope.toShow = function () { return $scope.complaints && $scope.complaints.length > 0; };

        // at initial page load
        $scope.orderProp = 'COMPLAINT_ID';
    }]);
})();
