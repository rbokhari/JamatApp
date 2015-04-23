/// <reference path="validation-repository.js" />
/// <reference path="~/Scripts/angular.js" />

'use strict';

jamatModule.controller('ValidationController',
    ['$scope', 'validationRepository', '$location', '$routeParams',
        function ($scope, validationRepository, $location, $routeParams) {

            console.log("validation controller");
            
            $scope.isBusy = true;

            $scope.validationDetails = validationRepository.get();
            $scope.validationDetails.$promise.then(function () {
                //alert("success");
            }, function () {
                //alert("error");
            })
            .then(function () { $scope.isBusy = false; });

            //alert($routeParams.id);
            if ($routeParams.id != undefined) {
                $scope.validationDetail = validationRepository.getSingleDetailByValidationId($routeParams.id);
            }

            $scope.save = function (validationDetail) {
                $scope.errors = [];

                validationRepository.save(validationDetail).$promise.then(
                    function () {
                        // success case
                        $.gritter.add({
                            title: 'Validation',
                            text: 'Added Successfully !',
                            time: 2000,
                            position: 'center'
                        });

                        console.log("save - Successfully !");
                        $location.url('/jamatapp/validation');
                    }, function (response) {
                        // failure case
                        console.log("save - Error !");
                        $scope.errors = response.data;
                    }
                );
            };

            $scope.saveAddNew = function (validationDetail) {
                $scope.errors = [];

                var clearDept = {
                    departmentCode: "",
                    departmentName: "",
                    statusId: ""
                };

                validationRepository.save(validationDetail).$promise.then(
                    function () {
                        // success case
                        $scope.departmentForm.$setPristine();
                        $scope.department = clearDept;
                        console.log("saveAddNew - Successfully !");

                    }, function (response) {
                        // failure case
                        $scope.errors = response.data;
                        console.log("saveAddNew - Error !");
                    }
                );
            };

            $scope.edit = function (department) {
                $scope.errors = [];

                departmentEditRepository.put($routeParams.id, department).then(
                    function () {
                        // success case
                        console.log("edit - Successfully !");
                        $location.url('/HRMSPortal/department');
                    }, function (response) {
                        // failure case
                        console.log("edit - Error !");
                        $scope.errors = response.data;
                    }
                );
            };

        }]);
