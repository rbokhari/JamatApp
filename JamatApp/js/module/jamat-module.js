var jamatModule = angular.module("jamatModule",
    [
        'ngRoute', 'ngResource', 'angularModalService', 'ngAnimate',
        'angularUtils.directives.dirPagination'
    ])
    .config(function($routeProvider, $locationProvider) {
        console.log('jamat module router call !');

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
            .otherwise({ redirectTo: '/jamat' });

        $locationProvider.html5Mode({ enabled: true, requireBase: false });

    });
