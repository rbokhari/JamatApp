/// <reference path="jalsa-repository.js" />


'use strict';
jamatModule.controller('JalsaController',
[
    '$scope', '$location', '$routeParams', 'jalsaRepository', 'validationRepository', 'countryRepository', 'ModalService',
    function ($scope, $location, $routeParams, jalsaRepository, validationRepository, countryRepository, ModalService) {

        console.log("jalsa controller");

        $('#mnuDashboard').removeClass('active');
        $('#mnuJalsa').addClass('active');
        $('#mnuJalsaList').addClass('active');

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

        var getTotalCount = function() {
            $scope.ansars = 0;
            $scope.khudams = 0;
            $scope.atfals = 0;
            $scope.nasarats = 0;
            $scope.lajnas = 0;
            $scope.childs = 0;
            $scope.grands = 0;

            $scope.jalsaDaysSummary.forEach(function (days) {

                $scope.ansars += days.totalAnsar1;
                $scope.khudams += days.totalKhudam1;
                $scope.atfals += days.totalAtfal1;
                $scope.nasarats += days.totalNasarat1;
                $scope.lajnas += days.totalLajnat1;
                $scope.childs += days.totalChild1;
                $scope.grands += days.grandTotal1;
            });
        };

        var getTotalDayCount = function () {
            $scope.ansars1 = 0;
            $scope.khudams1 = 0;
            $scope.atfals1 = 0;
            $scope.nasrats1 = 0;
            $scope.lajnas1 = 0;
            $scope.childs1 = 0;
            $scope.grands1 = 0;

            $scope.jalsaDays.forEach(function (days) {

                $scope.ansars1 += days.ansar;
                $scope.khudams1 += days.khuddam;
                $scope.atfals1 += days.atfal;
                $scope.nasrats1 += days.nassrat;
                $scope.lajnas1 += days.lajnaat;
                $scope.childs1 += days.child;
                $scope.grands1 += days.total;
            });
        };

        $scope.JalsaList = function() {
            $scope.isBusy = true;
            $scope.jalsa = jalsaRepository.getJalsaList();
            
            $scope.jalsa.$promise.then(function (response) {
                //alert("success");
            }, function () {
                //alert("error");
            })
            .then(function() { $scope.isBusy = false; });

        };

        $scope.loadJalsa = function () {
            $scope.isBusy = true;
            $scope.jalsaId = $routeParams.id;
            //alert($routeParams.day);
            $scope.jalsa = jalsaRepository.getJalsaById($scope.jalsaId);

            $scope.jalsa.$promise.then(function (response) {
                //alert("success");
                $scope.totalDays = daysDiff(response.startDate, response.endDate);
            }, function () {
                //alert("error");
            });

            $scope.dayId = $routeParams.day;
            if ($routeParams.day != undefined) {
                $scope.jalsaDays = jalsaRepository.getAllJalsaDay($scope.jalsaId, $scope.dayId);

                $scope.jalsaDays.$promise.then(function(response) {
                        //alert("success");
                        getTotalDayCount();
                        //$scope.totalDays = daysDiff(response.startDate, response.endDate);
                    }, function() {
                        //alert("error");
                    })
                    .then(function() { $scope.isBusy = false; });
            }

            $scope.jalsaDaysSummary = jalsaRepository.getAllJalsaDaySummary($routeParams.id);
            $scope.jalsaDaysSummary.$promise.then(function (response) {
                //alert("success");
                    getTotalCount();
                }, function () {
                //alert("error");
            })
            .then(function () { $scope.isBusy = false; });

            $scope.jalsaDaysCountrySummary = jalsaRepository.getAllJalsaDayByCountrySummary($routeParams.id);
            $scope.jalsaDaysCountrySummary.$promise.then(function (response) {
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

        // Modal service start ----------------
        $scope.addJalsa = function () {
            ModalService.showModal({
                templateUrl: "/templates/jalsa/jalsa-add.html",
                controller: "JalsaModalController",
                inputs: {
                    title: "Add New Jalsa",
                    parentId: 0,
                    parentDay:0,
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    //employee[0].employeePassports.splice(0, 0, resultEmployeePassport.data);
                    //console.log("show passport close : " + result.newPassport.id);
                    $scope.jalsa.push(result.resultData);
                    //$scope.complexResult = "Name: " + result.name + ", age: " + result.age;
                    //$('.modal').modal('hide');
                    //modal.element.close();
                });

            });
        };

        $scope.addJalsaDay = function (day) {
            ModalService.showModal({
                templateUrl: "/templates/jalsa/jalsa-day-add.html",
                controller: "JalsaModalController",
                inputs: {
                    title: "Add New Attandence",
                    parentId: $routeParams.id,
                    parentDay : day,
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    //employee[0].employeePassports.splice(0, 0, resultEmployeePassport.data);
                    //$scope.jalsa.push(result.resultData);
                    //modal.element.close();
                    //alert("CLOSE MODAL");
                    loadJalsa();
                });
            });
        };

    }
]);
