/// <reference path="../../module/jamat-module.js" />

'use strict';
jamatModule.controller('DashboardController',
[
    '$scope', '$location', '$routeParams','tajneedRepository',
    function ($scope, $location, $routeParams, tajneedRepository) {

        console.log("dashboard controller");

        $scope.isBusy = true;

        tajneedRepository.getTajneedCount()
        .then(function (response) {
            //console.log(response.data);
            //alert("yes " + response);
            $scope.totalTajneed = response.data;
        });
        

        tajneedRepository.getCountByAuxilary()
        .$promise.then(function (response) {
            $scope.countTajneedAuxilary = response;
            var responseData = [];
            var colorsData = ['#B2A7D1', '#AB92D6', '#A180B5', '#91749A', '#9D649D', '#ABCAF6'];
            angular.forEach(response, function (value, key) {
                responseData.push({ value: (response[key].countTotal / 241) * 100, label: response[key].countName, formatted: response[key].countTotal});
                $scope.countTajneedAuxilary[key].bgColor = colorsData[key];
            });
            Morris.Donut({
                element: 'graph-donut-auxilary',
                data: responseData,
                backgroundColor: false,
                labelColor: '#fff',
                colors: colorsData,
                formatter: function (x, data) { return data.formatted; }
            });
        });

        tajneedRepository.getCountByRegion()
        .$promise.then(function (response) {
            $scope.countTajneedRegion = response;
            var responseData = [];
            var colorsData = ['#7DA8AF', '#DAD8A7', '#919399', '#F7C176', '#F8F7DC', '#F4DFC8', '#DBD0C3'];
            angular.forEach(response, function (value, key) {
                responseData.push({ value: (response[key].countTotal / 241) * 100, label: response[key].countName, formatted: response[key].countTotal });
                $scope.countTajneedRegion[key].bgColor = colorsData[key];
            });
            Morris.Donut({
                element: 'graph-donut-region',
                data: responseData,
                backgroundColor: false,
                labelColor: '#fff',
                colors: colorsData,
                formatter: function (x, data) { return data.formatted; }
            });
        });

        tajneedRepository.getCountByNationality()
            .$promise.then(function(response) {
                $scope.countTajneedNationality = response;
                var responseData = [];
                var colorsData = ['#EB9644', '#B67136', '#814D27', '#4C2818', '#17040A', '#F4DFC8', '#DBD0C3'];
                angular.forEach(response, function(value, key) {
                    responseData.push({ value: (response[key].countTotal / 241) * 100, label: response[key].countName, formatted: response[key].countTotal });
                    $scope.countTajneedNationality[key].bgColor = colorsData[key];
                });
                Morris.Donut({
                    element: 'graph-donut-nationality',
                    data: responseData,
                    backgroundColor: false,
                    labelColor: '#fff',
                    colors: colorsData,
                    formatter: function(x, data) { return data.formatted; }
                });
            });

        tajneedRepository.getCountByWassiyat()
            .$promise.then(function(response) {
                $scope.countTajneedWassiyat = response;
                var responseData = [];
                var colorsData = ['#919399', '#F7C176', '#919399', '#F7C176', '#F8F7DC', '#F4DFC8', '#DBD0C3'];
                angular.forEach(response, function(value, key) {
                    responseData.push({ value: (response[key].countTotal / 241) * 100, label: response[key].countName, formatted: response[key].countTotal });
                    $scope.countTajneedWassiyat[key].bgColor = colorsData[key];
                });
                Morris.Donut({
                    element: 'graph-donut-wassiyat',
                    data: responseData,
                    backgroundColor: false,
                    labelColor: '#fff',
                    colors: colorsData,
                    formatter: function(x, data) { return data.formatted; }
                });
            });
    }
]);
