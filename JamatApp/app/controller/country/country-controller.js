/// <reference path="../../module/jamat-module.js" />

'use strict';
jamatModule.controller('CountryController',
[
    '$scope', '$location', '$routeParams', 'countryRepository', 
    function ($scope, $location, $routeParams, countryRepository) {

        console.log("country controller");

        $scope.isBusy = false;


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

        $scope.save = function (country) {
            $scope.errors = [];

            countryRepository.addCountry(country).$promise.then(
                function () {

                    //appRepository.showSuccessGritterNotification();

                    console.log("save - Successfully !");
                    $location.url('/jamat/country');
                }, function (response) {
                    // failure case
                    console.log("save - Error !");
                    //appRepository.showErrorSuccessGritterNotification();
                    $scope.errors = response.data;
                }
            );
        };

        $scope.saveRegion = function (region) {
            $scope.errors = [];

            countryRepository.addRegion(region).$promise.then(
                function () {

                    //appRepository.showSuccessGritterNotification();

                    console.log("region save - Successfully !");
                    $location.url('/jamat/country');
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
