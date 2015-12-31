jamatModule.controller('FinanceModalController',
[
    '$scope', 'financeRepository', 'title', 'close',
    'parentId', 'resultData', '$timeout', 'validationRepository','parentYear',
    function ($scope, financeRepository, title, close,
        parentId, resultData, $timeout, validationRepository, parentYear) {

        console.log("jalsa modal controller");

        $scope.resultData = {};

        $scope.chandaTypes = validationRepository.getAllChandaType();

        $scope.chandaTypes.$promise.then(function () {
            //alert("success");
        }, function () {
            //alert("error");
        })
        .then(function () { $scope.isBusy = false; });

        $scope.auxilaries = validationRepository.getAllAuxilary();
        $scope.auxilaries.$promise.then(function () {
            alert("success");
        }, function () {
            //alert("error");
        });

        $scope.saveFinanceYear = function (year) {
            $scope.errors = [];

            financeRepository.addFinanceYear(year)
                .$promise
                .then(
                    function (resultJalsa) {
                        // success case
                        $scope.resultData = resultJalsa;
                        //appRepository.showAddSuccessGritterNotification();
                        $scope.close();
                        $('#dvModal').modal('hide');
                    }, function (response) {
                        // failure case
                        console.log("finance year save - Error !");
                        //appRepository.showErrorGritterNotification();
                        $scope.errors = response.data;
                    }
                );
            //.then(function () { $scope.close(); });
        };


        $scope.close = function () {
            console.log("close function modal controller :");
            close({
                resultData: $scope.resultData
            }, 500); // close, but give 500ms for bootstrap to animate

        };

    }
]);
