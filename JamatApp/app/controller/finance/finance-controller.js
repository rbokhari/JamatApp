
'use strict';

jamatModule.controller('FinanceController',
[
    '$scope', 'validationRepository', 'financeRepository', 'ModalService',
    function ($scope, validationRepository, financeRepository, ModalService) {

        console.log("finance controller");
        $scope.isBusy = false;

        $scope.loadChandaType = function() {
            $scope.isBusy = true;
            $scope.chandaTypes = validationRepository.getAllChandaType();
            $scope.chandaTypes.$promise.then(function() {
                    //alert("success");
                }, function() {
                    //alert("error");
                })
                .then(function() {
                    $scope.isBusy = false;
                });
        };

        $scope.loadChandaYear = function() {
            $scope.isBusy = true;
            $scope.chandaYears = financeRepository.getAllChandaYear();
            $scope.chandaYears.$promise.then(function () {
                    //alert("success");
                }, function() {
                    //alert("error");
                })
                .then(function() {
                    $scope.isBusy = false;
                });
        };

        $scope.addFinanceYear = function (year) {
            ModalService.showModal({
                templateUrl: "/templates/finance/chanda-year-add.html",
                controller: "FinanceModalController",
                inputs: {
                    title: "Add New Finance Year",
                    parentId: 0,
                    parentYear: year,
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    //employee[0].employeePassports.splice(0, 0, resultEmployeePassport.data);
                    $scope.chandaYears.push(result.resultData);
                    //modal.element.close();
                    //alert("CLOSE MODAL");
                });
            });
        };


    }
]);

