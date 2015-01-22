/// <reference path="jalsa-repository.js" />


'use strict';
jamatModule.controller('JalsaController',
[
    '$scope', '$location', '$routeParams', 'jalsaRepository', 'validationRepository', 'countryRepository', 'ModalService',
    function ($scope, $location, $routeParams, jalsaRepository, validationRepository, countryRepository, ModalService) {

        console.log("jalsa controller");

        $scope.isBusy = false;

        var daysDiff = function (start, end) {
            return moment(end).diff(moment(start), 'day');
        };


        // bootstrap tab setting property and function for angularjs
        $scope.tab = 1;       // set active tab bydefault

        // set which tab to activate
        $scope.setTab = function (setTab) {
            this.tab = setTab;
        };

        $scope.loadJalsa = function () {
            $scope.isBusy = true;
            $scope.jalsas = jalsaRepository.getAllJalsa($routeParams.id);
            
            $scope.jalsas.$promise.then(function (response) {
                //alert("success");
                    //console.log(response.endDate);
                    $scope.totalDays = daysDiff(response.startDate, response.endDate);
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


    }
]);
