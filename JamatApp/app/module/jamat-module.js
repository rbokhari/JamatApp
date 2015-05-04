﻿var jamatModule = angular.module("jamatModule",
    [
        'ngRoute', 'ngResource', 'angularModalService', 'ngAnimate',
        'angularUtils.directives.dirPagination', 'angularModalService', 'LocalStorageModule',
    ])
    .constant("VALIDATIONS", {
        "AUXILARY": "1",
        "NATIONALITY": "2",
        "CHANDA_TYPE": "4",
        "COUNTRY": "5",
        "TAJNEED_TYPE": "6"

    })
    .config(function ($routeProvider, $locationProvider, $httpProvider) {
        console.log('jamat module router call !');

        $httpProvider.interceptors.push('authInterceptorService');

    $routeProvider
        .when('/', {
             redirectTo: '/jamat' 
        });

        $routeProvider
            .when('/jamat', {
                templateUrl: '/templates/dashboard.html',
                controller: 'DashboardController'
            });

        $routeProvider
            .when('/jamat/tajneed', {
                templateUrl: '/templates/tajneed/tajneed-list.html',
                controller: 'TajneedController'
            });

        $routeProvider
            .when('/jamat/tajneed/add', {
                templateUrl: '/templates/tajneed/tajneed-add.html',
                controller: 'TajneedController'
            });
        $routeProvider
            .when('/jamat/tajneed/edit/:id', {
                templateUrl: '/templates/tajneed/department-edit.html',
                controller: 'TajneedController'
            });

        $routeProvider
            .when('/jamat/tajneed/detail/:id', {
                templateUrl: '/templates/tajneed/tajneed-detail.html',
                controller: 'TajneedController'
            });

        $routeProvider
            .when('/jamat/jalsa/', {
                templateUrl: '/templates/jalsa/jalsaList.html',
                controller: 'JalsaController'
            });

        $routeProvider
            .when('/jamat/jalsa/:id', {
                templateUrl: '/templates/jalsa/jalsa.html',
                controller: 'JalsaController'
            });

        $routeProvider
            .when('/jamat/jalsa/:id/:day', {
                templateUrl: '/templates/jalsa/jalsaDays.html',
                controller: 'JalsaController'
            });

    $routeProvider
        .when('/jamat/chanda/', {
            templateUrl: '/templates/finance/collection-sheet-list.html',
            controller: 'TajneedController'
        });


    $routeProvider
        .when('/jamat/chandatype/', {
            templateUrl: '/templates/finance/chanda-type.html',
            controller: 'FinanceController'
        });

    $routeProvider
        .when('/jamat/chandayear/', {
            templateUrl: '/templates/finance/chanda-year.html',
            controller: 'FinanceController'
        });

    $routeProvider
        .when('/jamat/chandayear/promise/:id', {
            templateUrl: '/templates/finance/chanda-year-promise.html',
            controller: 'FinanceController'
        });

        $routeProvider
            .otherwise({ redirectTo: '/jamat' });

        $locationProvider.html5Mode({ enabled: true, requireBase: false });

    });