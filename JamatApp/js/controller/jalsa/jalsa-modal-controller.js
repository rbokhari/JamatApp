jamatModule.controller('JalsaModalController',
[
    '$scope', 'jalsaRepository', 'title', 'close',
    'parentId', 'resultData', '$timeout', 'countryRepository','parentDay',
    function ($scope, jalsaRepository, title, close,
        parentId, resultData, $timeout, countryRepository, parentDay) {

        console.log("jalsa modal controller");

        $scope.resultData = {};
        console.log(parentDay);
        $scope.day = parentDay;

        $scope.Countries = countryRepository.getAllCountries();

        $scope.Countries.$promise.then(function () {
            //alert("success");
        }, function () {
            //alert("error");
        })
        .then(function () { $scope.isBusy = false; });

        $scope.loadCountry1 = function () {
            $scope.isBusy = true;
            //alert("load country");
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

        $scope.testLoad = function() {
            alert("test load");
        };

        $scope.saveJalsa = function (jalsa) {
            $scope.errors = [];

            jalsaRepository.addJalsa(jalsa)
                .$promise
                .then(
                    function (resultJalsa) {
                        // success case
                        $scope.resultData = resultJalsa;
                        //appRepository.showAddSuccessGritterNotification();
                        $scope.close();
                        $('#dvModal').modal('hide');
                    }, function (response) {
                        // failure case
                        console.log("jalsa save - Error !");
                        //appRepository.showErrorGritterNotification();
                        $scope.errors = response.data;
                    }
                );
            //.then(function () { $scope.close(); });
        };

        $scope.saveJalsaDay = function (day) {
            $scope.errors = [];
            day.jalsaDayId = 0;
            day.jalsaId = parentId;
            jalsaRepository.addJalsaDay(day)
                .$promise
                .then(
                    function (resultJalsa) {
                        // success case
                        $scope.resultData = resultJalsa;

                        day.fmailyPersonName = "";
                        day.contactNo = "";
                        day.ansar = 0;
                        day.khuddam = 0;
                        day.atfals = 0;
                        day.lajnaat = 0;
                        day.nassrat = 0;
                        day.child = 0;

                        //appRepository.showAddSuccessGritterNotification();
                        //$scope.close();
                        //$('#dvModal').modal('hide');
                    }, function (response) {
                        // failure case
                        console.log("jalsa day save - Error !");
                        //appRepository.showErrorGritterNotification();
                        $scope.errors = response.data;
                    }
                );
            //.then(function () { $scope.close(); });
        };

        $scope.close = function () {
            console.log("close function modal controller :");
            close({
                resultData: $scope.resultData
            }, 500); // close, but give 500ms for bootstrap to animate

        };

    }
]);
