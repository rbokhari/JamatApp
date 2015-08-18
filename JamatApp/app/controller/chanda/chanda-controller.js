'use strict';
jamatModule.controller('ChandaController',
[
    '$scope', '$location', '$routeParams', 'validationRepository', 'financeRepository', 'ModalService',
    function ($scope, $location, $routeParams, validationRepository, financeRepository, ModalService) {

        console.log("chanda controller");

        $scope.isBusy = false;

        var cDetail = {
            'chandaId': 0,
            'chandaTypeId': 0,
            'chandaAmount': 0,
            'paidDate': 0
        };

        $scope.chanda = {
            id:0,
            chandaDetail: [cDetail]
        };

        console.log("Chanda Detail length : " + $scope.chanda.chandaDetail.length);

        $scope.loadChandaPaidDef = function () {
            $scope.chandaTypes = validationRepository.getAllChandaType();
            $scope.chandaTypes.$promise
                .then(function () {}, function () {});

            $scope.months = validationRepository.getAllMonths();
            $scope.months.$promise
                .then(function () { }, function () { });

            $scope.chandaYears = financeRepository.getAllChandaYear();
            $scope.chandaYears.$promise
                .then(function () {}, function () {});

        };

        $scope.addDetail = function () {
            console.log("addDetail");
            $scope.chanda.chandaDetail.push({});
        };

        $scope.removeDetail = function (detail) {
            console.log("removeDetail");
            $scope.chanda.chandaDetail.pop(detail);
        };

        $scope.saveChanda = function (country) {
            $scope.errors = [];
           
        };

        $scope.loadTajneed = function () {

            ModalService.showModal({
                templateUrl: "/templates/tajneed/tajneed-lookup.html",
                controller: "TajneedLookupModalController",
                inputs: {
                    title: "Select Tajneed",
                    parentId: 0,
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    console.log("result", result.resultData);
                    $scope.chanda.tajneedName = result.resultData.firstName + " " + result.resultData.lastName + " / " + result.resultData.fatherName;
                    $scope.chanda.tajneedId = result.resultData.id;
                    //$scope.tajneed[0].tajneedIncomes.push(result.resultData);
                });

            });
        }

        $scope.saveRegion = function (chanda) {
            $scope.errors = [];

            
        };

    }
]);
