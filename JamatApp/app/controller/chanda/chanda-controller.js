'use strict';
jamatModule.controller('ChandaController',
[
    '$scope', '$location', '$routeParams', 'appRepository', 'validationRepository', 'financeRepository', 'chandaRepository', 'ModalService', 'localStorageService',
    function ($scope, $location, $routeParams, appRepository, validationRepository, financeRepository, chandaRepository, ModalService, localStorageService) {

        //console.log("chanda controller");

        $scope.isBusy = false;

        //console.log("tajneedata",$scope.tajneedData[0].firstName);
        //appRepository.showPageBusyNotification();

        var userDetail = localStorageService.get('userDetail');

        var cDetail = {
            'chandaId': 0,
            'chandaTypeId': 0,
            'chandaAmount': 0,
            'paidDate': 0
        };

        $scope.chanda = {
            id: 0,
            issuedBy: userDetail.id, // $scope.tajneedData[0].id,
            issuedByName: userDetail.fullName,
            chandaDetails: [cDetail],
            totalAmount: 0
        };

        var cChandaHeadDetail = {
            subHeadId: 0,
            ChandaHeadId: 0,
            subHeadName: '',
            description: '',
            statusId:1
        }

        $scope.chandaType = {
            id: 0,
            validationId: 4,
            nameEn: '',
            nameAr: '',
            description: '',
            isActive: 1,
            subTypeDetails: [cChandaHeadDetail]
        }

        $scope.loadChandaType = function () {
            $scope.isBusy = true;
            $scope.chandaTypes = validationRepository.getAllChandaType();
            $scope.chandaTypes.$promise.then(function () {
                //alert("success");
            }, function () {
                //alert("error");
            })
                .then(function () {
                    $scope.isBusy = false;
                });
        };

        $scope.getChandaType = function() {
            $scope.isBusy = true;

            $scope.chandaType = validationRepository.getSingleDetailByValidationId($routeParams.id);
            $scope.chandaType.$promise.then(function (response) {
                //alert("success");
                chandaRepository.getSubHeadsById($routeParams.id).$promise
                    .then(function (res) {
                        $scope.chandaType.subTypeDetails = res;
                    }, function (err) {

                    });
            }, function () {
                //alert("error");
            })
                .then(function () {
                    $scope.isBusy = false;
                });
        }

        $scope.addChandaSubType = function () {
            $scope.chandaType.subTypeDetails.push({});
        };

        $scope.addChandaType = function (chandaType) {
            validationRepository.addValidation(chandaType)
                .$promise
                .then(function (res) {
                    console.log(res);
                    $location.url('/jamat/chandatype/');
            }, function (err) { });
        }

        $scope.updateChandaType = function(chandaType) {
            validationRepository.editValidation(chandaType)
                .$promise
                .then(function (res) {
                    $location.url('/jamat/chandatype/');
                }, function (err) { });
        }

        $scope.calculateTotal = function () {
            var valTotal = 0;
            //console.log($scope.chanda.chandaDetails.length);
            angular.forEach($scope.chanda.chandaDetails, function (value, key) {
                console.log(value.chandaAmount);
                //if (key === 'chandaAmount') {
                    valTotal += Number(value.chandaAmount);
                //}
            });
            $scope.chanda.totalAmount = valTotal;
            //return valTotal;
        }

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
        $scope.chandaSubs = [];
        $scope.getSubHead = function(id, index) {
            $scope.chandaSubs[index] = chandaRepository.getSubHeadsById(id);
            $scope.chandaSubs[index].$promise
                .then(function () { }, function () { });
        }

        $scope.addDetail = function () {
            
            $scope.chanda.chandaDetails.push({});
        };

        $scope.removeDetail = function (detail) {
            
            $scope.chanda.chandaDetails.pop(detail);
            $scope.calculateTotal();
        };

        $scope.saveChanda = function (chanda) {
            $scope.errors = [];
           
            chandaRepository.addChanda(chanda)
                .$promise
                .then(function (result) {

                    $location.url('/jamat/tajneed/detail/' + chanda.tajneedId);

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
