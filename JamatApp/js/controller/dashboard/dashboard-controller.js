/// <reference path="../../module/jamat-module.js" />

'use strict';
jamatModule.controller('DashboardController',
[
    '$scope', '$location', '$routeParams',
    function($scope, $location, $routeParams) {

        console.log("dashboard controller");

        $scope.isBusy = true;


    }
]);
