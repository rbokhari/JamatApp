﻿/// <reference path="../../module/jamat-module.js" />

'use strict';
jamatModule.controller('TajneedController',
[
    '$scope', '$location', '$routeParams', 'appRepository', 'tajneedRepository', 'validationRepository', 'countryRepository', 'ModalService', '_',
    function ($scope, $location, $routeParams, appRepository, tajneedRepository, validationRepository, countryRepository, ModalService, _) {

        console.log("tajneed controller");

        $('#mnuDashboard').removeClass('active');
        $('#mnuTajneed').addClass('active');
        $('#mnuTajneedList').addClass('active');

        //appRepository.showPageBusyNotification();

        $scope.isBusy = false;

        // bootstrap tab setting property and function for angularjs
        $scope.tab = 1;       // set active tab bydefault

        // set which tab to activate
        $scope.setTab = function (setTab) {
            this.tab = setTab;
        };

        // verify if tab is selected or not, use for ng-class 
        $scope.isTabSelected = function (checkTab) {
            return this.tab === checkTab;
        };

        $scope.filter = {
            regionId: 0,
            nationalityId: 0,
            auxilaryId: 0,
            isMosi: 0
        };

        $scope.searchTajneed = function (filter) {
            //$scope.loadTajneed();
            $scope.isBusy = true;
            tajneedRepository.getAllTajneed()
                .then(function (res) {
                    $scope.tajneeds = _.filter(res, function (data) {
                        if (filter.regionId == 0 && filter.nationalityId == 0 && filter.auxilaryId == 0 && filter.isMosi == 0) return true;
                        var result = true;
                        if (filter.regionId !== 0) {
                            result = (data.regionId == filter.regionId) && result;
                        }
                        if (filter.nationalityId !== 0) {
                            result = (data.nationalityId == filter.nationalityId) && result;
                        }
                        if (filter.auxilaryId !== 0) {
                            result = (data.auxilaryId == filter.auxilaryId) && result;
                        }
                        if (filter.isMosi !== 0) {
                            result = (data.isMosi == 1) && result;
                        }
                        return result;
                    });
                    $scope.isBusy = false;
                }, function (err) { });

        };

        $scope.clearSearch = function () {
            $scope.filter = {
                regionId: 0,
                nationalityId: 0,
                auxilaryId: 0,
                isMosi: 0
            };
            tajneedRepository.getAllTajneed()
                .then(function (res) {
                    $scope.tajneeds = res;
                }, function (err) { });
        };

        $scope.loadTajneed = function () {
            $scope.isBusy = true;
            tajneedRepository.getAllTajneed()
                .then(function (res) {
                    $scope.tajneeds = res;
            }, function(err) { })
            .then(function() {
                $scope.isBusy = false;
            });
        };

        $scope.forceTajneedRefresh = function () {
            $scope.isBusy = true;
            var data = tajneedRepository.getAllTajneed(true);
            data.then(function (res) {
                $scope.tajneeds = res;
            }, function (err) { })
            .then(function () {
                $scope.isBusy = false;
            });
        };

        var getCollectionSheetCount = function () {
            $scope.chandaMajlis = 0;
            $scope.chandaIjtima = 0;
            $scope.chandaTotal = 0;

            $scope.tajneeds.forEach(function (chanda) {
                //console.log(chanda.tajneedIncomes[0]);
                //console.log(chanda.tajneedIncomes.length);
                if (chanda.auxilaryId==2 && chanda.tajneedIncomes.length>0) {
                    //console.log("inside :" + chanda.tajneedIncomes);
                    
                    $scope.chandaMajlis += ((chanda.tajneedIncomes[0].incomeAmount * 12) / 100);
                    $scope.chandaIjtima += ((chanda.tajneedIncomes[0].incomeAmount * 2.5) / 100);

                    $scope.chandaTotal += ((chanda.tajneedIncomes[0].incomeAmount * 14.5) / 100);
                }
            });
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

        $scope.loadStatus = function () {
            $scope.isBusy = true;
            $scope.statuses = validationRepository.getAllTajneedStatus();

            $scope.statuses.$promise.then(function () {
                //alert("success");
            }, function () {
                //alert("error");
            })
            .then(function () { $scope.isBusy = false; });
        };


        $scope.loadRegionsByCountryId = function (id) {
            $scope.Regions = countryRepository.getAllRegionsByCountryId(id);
        };

        if ($routeParams.id != undefined) {
            $scope.isBusy = false;
            $scope.tajneed = tajneedRepository.getTajneedById($routeParams.id);
            $scope.tajneed.$promise
                .then(function () {
                    $scope.loadRegionsByCountryId($scope.tajneed[0].countryId);
                }, function () { })
                .then(function () { $scope.isBusy = true; });
        }

        // Modal service start ----------------
        $scope.showIncome = function (id) {
            ModalService.showModal({
                templateUrl: "/templates/tajneed/tajneed-income.html",
                controller: "TajneedModalController",
                inputs: {
                    title: "Add New Income",
                    parentId: id,
                    tajneedIncome: {},
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    //employee[0].employeePassports.splice(0, 0, resultEmployeePassport.data);
                    //console.log("show passport close : " + result.newPassport.id);
                    $scope.tajneed[0].tajneedIncomes.push(result.resultData);
                    //$scope.complexResult = "Name: " + result.name + ", age: " + result.age;
                    //$('.modal').modal('hide');
                    //modal.element.close();
                });

            });
        };

        $scope.editIncome = function (income) {
            //console.log(passport);
            ModalService.showModal({
                templateUrl: "/templates/hrms/employee/employee-passport.html",
                controller: "EmployeeModalController",
                inputs: {
                    title: "Update Passport",
                    parentId: income.tajneedId,
                    tajneedIncome: income,
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
            });
        };

        $scope.deleteIncome = function (income) {
            var x;
            if (confirm("Are you sure to delete this record ?") == true) {
                employeeRepository.deleteEmployeePassport(passport)
                    .$promise
                    .then(function () {
                        appRepository.showDeleteGritterNotification();
                        $scope.employee[0].employeePassports.pop(passport);
                    }, function (error) {
                        appRepository.showErrorGritterNotification();
                    });
            }
        };

        $scope.save = function(tajneed) {
            $scope.errors = [];
            tajneedRepository.addTajneed(tajneed)
                .$promise
                .then(
                function (response) {

                    //appRepository.showSuccessGritterNotification();
                    console.log(response);
                    console.log("save - Successfully !");
                    $location.url('/jamat/tajneed/detail/' + response.id);
                }, function (response) {
                    // failure case
                    console.log("save - Error !");
                    //appRepository.showErrorSuccessGritterNotification();
                    $scope.errors = response.data;
                }
            );
        };

        $scope.getFile = function (type, item) {
            console.log(type);
            tajneedRepository.getPdfFile(type, item)
                .then(function (response) {
                    console.log("response come");
                    var file = new Blob([response], {
                        type: 'application/csv'
                    });
                    var fileURL = URL.createObjectURL(file);
                    var a = document.createElement('a');
                    a.href = fileURL;
                    a.target = '_blank';
                    a.download = 'TajneedList.' + type;
                    document.body.appendChild(a);
                    a.click();
                }, function (error) {
                    console.log("error", error);
                });
        };

        $scope.showDocumentForm = function (id, typeId) {

            ModalService.showModal({
                templateUrl: "/templates/tajneed/tajneed-document.html",
                controller: "TajneedModalController",
                inputs: {
                    title: "Update Document",
                    parentId: id,
                    documentTypeId: typeId,
                    tajneedIncome: {},
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    console.log("result", result);
                    if (typeId == 56) {
                        $("#imgPassport1").attr('src', 'data:image/png;base64,'+result.resultData.cardImage);
                    }else if (typeId == 57) {
                        $("#imgPassport2").attr('src', 'data:image/png;base64,' + result.resultData.cardImage);
                    }else if (typeId == 58) {
                        $("#imgCard1").attr('src', 'data:image/png;base64,' + result.resultData.cardImage);
                    }else if (typeId == 59) {
                        $("#imgCard2").attr('src', 'data:image/png;base64,' + result.resultData.cardImage);
                    }
                    //$scope.employee[0].empPicture = result.resultData.empPicture;
                });

            });

        };

    }
]);
