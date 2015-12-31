var jamatModule = angular.module("jamatModule",
    [
        'ngRoute', 'ngResource', 'angularModalService', 'ngAnimate',
        'angularUtils.directives.dirPagination', 'angularModalService', 'LocalStorageModule', 'ui.bootstrap', 'angularFileUpload'
    ])
    .constant("_", 
        window._
    )
    .constant("VALIDATIONS", {
        "AUXILARY": "1",
        "NATIONALITY": "2",
        "CHANDA_TYPE": "4",
        "COUNTRY": "5",
        "TAJNEED_TYPE": "6",
        "CHANDA_BUDGET_STATUS": 7,
        "TAJNEED_STATUS": 8,
        "MONTHS": 9,
        "TAJNEED_DOCUMENT_TYPE": 10

    })
    .constant("VALIDATION_DETAILS", {
        "CHANDA_BUDGET_STATUS_INITIATE": 24,
        "CHANDA_BUDGET_STATUS_FIRST_APPROVAL": 25,
        "CHANDA_BUDGET_STATUS_SECOND_APPROVAL": 26,
        "CHANDA_BUDGET_STATUS_APPROVED": 27
    })
    .config(function ($routeProvider, $locationProvider, $httpProvider) {

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
                templateUrl: '/templates/tajneed/tajneed-edit.html',
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
                templateUrl: '/templates/chanda/chanda-type.html',
                controller: 'ChandaController'
            });
        $routeProvider
            .when('/jamat/chandatype/add', {
                templateUrl: '/templates/chanda/chanda-type-add.html',
                controller: 'ChandaController'
            });
        $routeProvider
            .when('/jamat/chandatype/edit/:id', {
                templateUrl: '/templates/chanda/chanda-type-edit.html',
                controller: 'ChandaController'
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
            .when('/jamat/chandapaid/add/', {
                templateUrl: '/templates/finance/chanda-paid-add.html',
                controller: 'ChandaController'
            });

        $routeProvider
            .otherwise({ redirectTo: '/jamat' });

        $locationProvider.html5Mode({ enabled: true, requireBase: false });

    });
