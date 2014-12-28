/// <reference path="../../module/jamat-module.js" />

'use strict';
jamatModule.controller('TajneedController',
[
    '$scope', '$location', '$routeParams', 'tajneedRepository', 'validationRepository', 'countryRepository',
    function ($scope, $location, $routeParams, tajneedRepository, validationRepository, countryRepository) {

        console.log("tajneed controller");

        $scope.isBusy = false;

        // bootstrap tab setting property and function for angularjs
        $scope.tab = 1;       // set active tab bydefault

        // set which tab to activate
        $scope.setTab = function (setTab) {
            this.tab = setTab;
        };

        // verify if tab is selected or not, use for ng-class 
        $scope.isTabSelected = function (checkTab) {
            return this.tab === checkTab;
        };


        $scope.loadTajneed = function () {
            $scope.isBusy = true;
            $scope.tajneeds = tajneedRepository.getAllTajneed();

            $scope.tajneeds.$promise.then(function () {
                //alert("success");
            }, function () {
                //alert("error");
            })
            .then(function () { $scope.isBusy = false; });
        };

        $scope.loadAuxilary = function () {
            $scope.Auxilaries = validationRepository.getAllDetailsByValidationId(1);
        };

        $scope.loadNationality = function () {
            $scope.Nationalities = validationRepository.getAllDetailsByValidationId(2);
        };

        $scope.loadCountry = function () {
            $scope.isBusy = true;
            $scope.Countries = countryRepository.getAllCountries();

            $scope.Countries.$promise.then(function () {
                //alert("success");
            }, function () {
                //alert("error");
            })
            .then(function () { $scope.isBusy = false; });
        };

        $scope.loadRegionsByCountryId = function (id) {
            $scope.Regions = countryRepository.getAllRegionsByCountryId(id);
        };


        $scope.save = function(tajneed) {
            $scope.errors = [];

            tajneedRepository.addTajneed(tajneed).$promise.then(
                function () {

                    //appRepository.showSuccessGritterNotification();

                    console.log("save - Successfully !");
                    $location.url('/jamat/tajneed');
                }, function (response) {
                    // failure case
                    console.log("save - Error !");
                    //appRepository.showErrorSuccessGritterNotification();
                    $scope.errors = response.data;
                }
            );
        };

    }
]);
