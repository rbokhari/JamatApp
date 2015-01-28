/// <reference path="jalsa-repository.js" />


'use strict';
jamatModule.controller('JalsaController',
[
    '$scope', '$location', '$routeParams', 'jalsaRepository', 'validationRepository', 'countryRepository', 'ModalService',
    function ($scope, $location, $routeParams, jalsaRepository, validationRepository, countryRepository, ModalService) {

        console.log("jalsa controller");

        $scope.isBusy = false;

        var daysDiff = function (start, end) {
            //return moment(end).diff(moment(start), 'day');
            var diff = moment(end).diff(moment(start), 'day');
            var timeArray = [];
            for (var i = 0; i <= diff; i++) {
                timeArray.push(moment(new Date(moment(start).add(i, 'days'))).format('LL'));
            }
            //return new Array(new String("1"));
            return timeArray;
        };


        // bootstrap tab setting property and function for angularjs
        $scope.tab = 1;       // set active tab bydefault

        // set which tab to activate
        $scope.setTab = function (setTab) {
            this.tab = setTab;
        };

        $scope.ansars = 0;
        $scope.khudams = 0;
        $scope.atfals = 0;
        $scope.nasarats = 0;
        $scope.lajnas = 0;
        $scope.childs = 0;
        $scope.grands = 0;


        var getTotalCount = function() {
            $scope.jalsaDaysSummary.forEach(function (days) {

                $scope.ansars += days.totalAnsar;
                $scope.khudams += days.totalKhudam;
                $scope.atfals += days.totalAtfal;
                $scope.nasarats += days.totalNasarat;
                $scope.lajnas += days.totalLajnat;
                $scope.childs += days.totalChild;
                $scope.grands += days.grandTotal;
            });
        };

        var getTotalDayCount = function () {

            $scope.jalsaDays.forEach(function (days) {

                $scope.ansars += days.ansar;
                $scope.khudams += days.khuddam;
                $scope.atfals += days.atfal;
                $scope.nasarats += days.nassrat;
                $scope.lajnas += days.lajnaat;
                $scope.childs += days.child;
                $scope.grands += days.total;
            });
        };

        $scope.loadJalsa = function () {
            $scope.isBusy = true;
            $scope.jalsaId = $routeParams.id;
            //alert($routeParams.day);
            $scope.jalsa = jalsaRepository.getJalsaById($scope.jalsaId);

            $scope.jalsa.$promise.then(function (response) {
                //alert("success");
                //console.log(response.endDate);
                $scope.totalDays = daysDiff(response.startDate, response.endDate);
            }, function () {
                //alert("error");
            });

            $scope.dayId = $routeParams.day;
            if ($routeParams.day != undefined) {
                $scope.jalsaDays = jalsaRepository.getAllJalsaDay($scope.jalsaId, $scope.dayId);
                
                $scope.jalsaDays.$promise.then(function (response) {
                        //alert("success");
                        getTotalDayCount();
                        //$scope.totalDays = daysDiff(response.startDate, response.endDate);
                    }, function() {
                        //alert("error");
                    })
                    .then(function () { $scope.isBusy = false; });

                
            }

            $scope.jalsaDaysSummary = jalsaRepository.getAllJalsaDaySummary($routeParams.id);
            $scope.jalsaDaysSummary.$promise.then(function (response) {
                //alert("success");
                    getTotalCount();
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
