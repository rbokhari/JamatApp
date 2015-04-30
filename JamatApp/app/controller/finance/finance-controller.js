
'use strict';

jamatModule.controller('FinanceController',
[
    '$scope', '$routeParams', 'validationRepository', 'financeRepository', 'ModalService',
    function ($scope, $routeParams, validationRepository, financeRepository, ModalService) {

        console.log("finance controller");
        $scope.isBusy = false;
        $scope.finance = "";

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

        $scope.loadChandaYearPromise = function () {
            $scope.isBusy = true;
            $scope.income = 0;
            $scope.chandaYearPromise = financeRepository.getChandaYear($routeParams.id);
            $scope.chandaYearPromise
                .$promise
                .then(function (response) {
                    //alert("success" + response.yearBudget.length);
                    $scope.chandaYearPromise.notes = "";
                    if (response.yearBudget.length == 0) {
                        console.log("calculate income");
                        $scope.getAuxilaryIncome = financeRepository.getAuxilaryIncome(response.auxilaryId);
                        $scope.getAuxilaryIncome.$promise.then(function() {
                            console.log($scope.getAuxilaryIncome[0].incomeTotal);
                            $scope.income = $scope.getAuxilaryIncome[0].incomeTotal;
                            
                        }, function () { });
                        
                    } else {
                        console.log("load from db");
                        //alert(response.yearBudget[0].totalIncome);
                        $scope.chandaYearPromise.notes = response.yearBudget[0].description;
                        $scope.income = response.yearBudget[0].totalIncome;
            }
                }, function () {
                //alert("error");
            })
                .then(function () {
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

        $scope.saveFinanceBudget = function (finance) {
            //console.log(finance);
            financeRepository.addAuxilaryBudget(finance.yearId, finance.notes)
                .$promise
                .then(
                    function (resultJalsa) {
                        $scope.resultData = resultJalsa;
                        $scope.visible = true;
                        //appRepository.showErrorGritterNotification();
                    }, function (response) {
                        console.log("finance year save - Error !");
                        //appRepository.showErrorGritterNotification();
                        $scope.errors = response.data;
                    }
                );
            
        };

        $scope.getFinanceBudget = function () {
            //console.log(finance);
            financeRepository.getAuxilaryBudget($routeParams.id)
                .$promise
                .then(
                    function (result) {
                        $scope.chandaYearPromise = {};
                        $scope.chandaYearPromise.yearId = result.yearId;
                        $scope.chandaYearPromise.
                        $scope.visible = true;
                        //appRepository.showErrorGritterNotification();
                    }, function (response) {
                        console.log("finance year save - Error !");
                        //appRepository.showErrorGritterNotification();
                        $scope.errors = response.data;
                    }
                );

        };

    }
]);

