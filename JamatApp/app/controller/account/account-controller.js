
//'use strict';

jamatModule.controller('AccountController',
[
    '$scope', 'authRepository', '$location', '$window', 'tajneedRepository', 'localStorageService',
    function ($scope, authRepository, $location, $window, tajneedRepository,localStorageService) {
        
        //console.log("account controller");
        $scope.isLoginProcess = false;
        $scope.loginData = {
            userName: "",
            password: ""
        };
        $scope.message = "";
        $scope.login = function () {
            $scope.isLoginProcess = true;
            authRepository.login($scope.loginData)
                .then(function(response) {
                //authRepository.fillAuthData();
                    if (response.userId) {
                        $window.location.href = '/jamat';
                    } else {
                        $scope.isLoginProcess = false;
                        $scope.message = "Invalid Username or Password !";
                    }
                },
                function (err) {
                    console.log(err);
                    //console.log(err.status);
                    //$scope.message = err.error_description;
                    //$scope.message = err.error;
                    $scope.isLoginProcess = false;
                    if (err.status == undefined) {
                        $scope.message = "Invalid Username or Password !";
                    }
                    else if (err.status == 500) {
                        $scope.message = "User not allowed to login system !";
                    }
                });
        };

        $scope.logOut = function () {
            
            authRepository.logOut();
            $window.location.href = '/Login';
        };

        $scope.fullName = "loading";

        //var userData = localStorageService.get('userData');
        ////console.log(userData.userId);
        //if (userData) {
            
        //    $scope.tajneedData = tajneedRepository.getTajneedById(userData.userId);
        //    $scope.tajneedData.$promise
        //        .then(function (res) {
        //            console.log(res);
        //            $scope.fullName = res[0].firstName;
        //        }, function (error) {
        //            alert("user data error");
        //        });

        //}

        $scope.authData = function () {
            var userData = localStorageService.get('userData');
            $scope.tajneedData = tajneedRepository.getTajneedById(userData.userId);
            $scope.tajneedData.$promise
                .then(function (res) {
                    $scope.fullName = res[0].firstName;
                }, function (error) {
                    alert("user data error");
                });

            authRepository.fillAuthData();
            //$scope.authentication = authRepository.authentication;
        };

       /* $scope.redirect = function () {
            authRepository.fillAuthData()
                .then(function (res) {
                    $scope.authentication = authRepository.authentication;
                    if ($scope.authentication.moduleId == appModules.HRMS_Module) {
                        // check language also here
                        $window.location.href = '/';
                    } else if ($scope.authentication.moduleId == appModules.INV_Module) {
                        $window.location.href = '/';
                    }
                },
                function (err) {
                    console.log(err);
                    $window.location.href = '/Login';
                });
        };
        */
    }
]);
