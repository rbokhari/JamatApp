'use strict';
jamatModule.controller('ChandaController',
[
    '$scope', '$location', '$routeParams', 'validationRepository', 'financeRepository', 'chandaRepository', 'ModalService',
    function ($scope, $location, $routeParams, validationRepository, financeRepository, chandaRepository, ModalService) {

        console.log("chanda controller");

        $scope.isBusy = false;

        console.log("tajneedata",$scope.tajneedData);

        var cDetail = {
            'chandaId': 0,
            'chandaTypeId': 0,
            'chandaAmount': 0,
            'paidDate': 0
        };

        $scope.chanda = {
            id:0,
            chandaDetails: [cDetail]
        };

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

        $scope.getSubHead = function(id) {
            $scope.chandaSubs = chandaRepository.getSubHeadsById(id);
            $scope.chandaSubs.$promise
                .then(function () { }, function () { });
        }

        $scope.addDetail = function () {
            console.log("addDetail");
            $scope.chanda.chandaDetails.push({});
        };

        $scope.removeDetail = function (detail) {
            console.log("removeDetail");
            $scope.chanda.chandaDetails.pop(detail);
        };

        $scope.saveChanda = function (chanda) {
            $scope.errors = [];
           
            chandaRepository.addChanda(chanda)
                .$promise
                .then(function (result) {

                    $location.url('/jamat');

                }, function (error) {
                    
                });
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
                    $scope.chanda.tajneedId = result.resultData.id;
                    $scope.chanda.gsmNo = result.resultData.mobileNumber;
                    $scope.chanda.tajneedName = result.resultData.firstName + " " + result.resultData.lastName + " / " + result.resultData.fatherName;
                    
                    //$scope.tajneed[0].tajneedIncomes.push(result.resultData);
                });

            });
        }

    }
]);
