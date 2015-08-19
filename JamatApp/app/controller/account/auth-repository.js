//'use strict';
jamatModule.factory('authRepository', [
    '$http', '$q', 'accountRepository', 'localStorageService',
    function ($http, $q, accountRepository, localStorageService) {

        var serviceBase =  'http://localhost:91/';    //'http://amc.azurewebsites.net/';
        var authServiceFactory = {};

        var _authentication = {
            isAuth: false,
            tajneedId: 0,
            userName: "",
            fullName: "",
            empPicture: "",
            email: "",
            phone: "",
            roles: "",
            roleId: "",
            moduleId: "",
            isHRMSModule: false,
            isINVModule: false
        };

        //var _saveRegistration = function (registration) {
        //    _logOut();
        //    return $http.post(serviceBase + 'api/account/register', registration).then(function (response) {
        //        return response;
        //    });
        //};

        var _lockLogin = function (loginData) {

            var deferred = $q.defer();

            $http.post('/api/account/?userName=' + loginData.userName + '&password=' + loginData.password)
                .success(function (response) {

                    _authentication.isAuth = true;
                    _authentication.userName = loginData.userName;

                    deferred.resolve(response);

                }).error(function (err, status) {
                    _logOut();
                    deferred.reject(err);
                });

            return deferred.promise;

        };

        var _login = function (loginData) {
            var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

            var deferred = $q.defer();

            accountRepository.getUserByUserName(loginData.userName, loginData.password)
                .$promise
                .then(function (res) {
                console.log(res);
                if (res.userId) {
                    $http.post('token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
                        .success(function(response) {
                            console.log("token done");
                            localStorageService.set('authorizationData', { token: response.access_token, userId: res.tajneedId, role: "role" });
                            localStorageService.set('userData', { userId: res.tajneedId, userRole: 0 });

                            deferred.resolve(res);

                        }).error(function(err, status) {
                            _logOut();
                            console.log("inner error " + err + " status " + status);
                            deferred.reject(err);
                        });
                } else {
                    deferred.reject(res);
                }
                //deferred.resolve(res);
                }, function(error) {

                    console.log("error is : " + error);
                    //alert("not done");
                    //$scope.message = "User not allowed to login this system.";
                    deferred.reject(error);
                });

            return deferred.promise;
        };

        var _logOut = function () {

            console.log("logOut from System");


            localStorageService.remove('authorizationData');
            localStorageService.remove('userData');
            
            _authentication.isAuth = false;
            _authentication.userName = "";

        };

        var _fillAuthData = function () {

            var deferred = $q.defer();
            var authData = localStorageService.get('authorizationData');

            if (authData != null) {
                $http.get('/api/tajneed/' + authData.userId)
                    .success(function (response) {
                        console.log("fillAuthData response", response);
                        _authentication.isAuth = true;
                        //_authentication.userName = response.userName;
                        _authentication.tajneedId = response.id;
                        _authentication.fullName = response.firstName + ' ' + response.lastName;
                        //_authentication.employeeId = response.id;
                        //_authentication.departmentName = response.postedTo;
                        //_authentication.empPicture = response.empPicture;
                        //_authentication.email = response.email;
                        _authentication.phone = response.mobileNumber;

                    }).error(function (err, status) {
                        //_logOut();
                        console.log(err);
                        deferred.reject(err);
                    });
            } else {
                deferred.reject();
            }
            return deferred.promise;

        };

        function employeeDataPost(userName) {
            //console.log("employeedata:" + userName);

            var deferred = $q.defer();

            $http.get('/api/employee/GetEmployeeByUserName/?userName=' + userName)
                .success(function (response) {

                    _authentication.isAuth = true;
                    _authentication.userName = response.userName;
                    _authentication.fullName = response.employeeName;
                    _authentication.employeeId = response.id;
                    _authentication.departmentName = response.postedTo;
                    _authentication.empPicture = response.empPicture;
                    _authentication.email = response.email;
                    _authentication.phone = response.phone;

                    accountRepository.getUserById(response.id)
                        .$promise
                        .then(function (res) {
                            //console.log("employeedata fullname:" + _authentication.fullName);
                            _authentication.moduleId = res.moduleId;

                            accountRepository.getRoleById(res.roleId)
                                .$promise
                                .then(function (response1) {
                                    //console.log(_authentication);
                                    _authentication.roles = response1.roleName;
                                    _authentication.roleId = response1.roleId;

                                    //localStorageService.set('userData', { userName: userName, userId: response.id, role: _authentication.roles, roleId: _authentication.roleId });
                                });
                        });

                    deferred.resolve(response);

                }).error(function (err, status) {
                    //_logOut();
                    console.log(err);
                    deferred.reject(err);
                });

            return deferred.promise;


        }

        function employeeData(userName) {
            //console.log("employeedata:" + userName);
            accountRepository.getUserDetailByUserName(userName)
                .$promise
                .then(function (response) {
                    //console.log(response);
                    _authentication.isAuth = true;
                    _authentication.userName = response.userName;
                    _authentication.fullName = response.employeeName;
                    _authentication.employeeId = response.id;
                    _authentication.departmentName = response.postedTo;
                    _authentication.empPicture = response.empPicture;
                    _authentication.email = response.email;
                    _authentication.phone = response.phone;

                    accountRepository.getUserById(response.id)
                        .$promise
                        .then(function (res) {
                            //console.log("employeedata fullname:" + _authentication.fullName);
                            _authentication.moduleId = res.moduleId;

                            accountRepository.getRoleById(res.roleId)
                                .$promise
                                .then(function (response1) {
                                    //console.log(_authentication);
                                    _authentication.roles = response1.roleName;
                                    _authentication.roleId = response1.roleId;

                                    localStorageService.set('userData', { userName: userName, userId: response.id, role: _authentication.roles, roleId: _authentication.roleId });
                                });
                        });


                }, function (err) {
                    _logOut();
                    _authentication.isAuth = false;
                });
        }

        var _isHRMSModule = function () {
            return true;// $scope.authentication.moduleId == appModules.HRMS_Module;
        };

        //authServiceFactory.saveRegistration = _saveRegistration;
        authServiceFactory.login = _login;
        authServiceFactory.logOut = _logOut;
        authServiceFactory.fillAuthData = _fillAuthData;
        authServiceFactory.authentication = _authentication;
        authServiceFactory.lockLogin = _lockLogin;
        authServiceFactory.isHRMSModule = _isHRMSModule;

        return authServiceFactory;

    }
]);