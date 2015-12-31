
jamatModule.controller('TajneedModalController',
[
    '$scope', 'tajneedRepository', 'validationRepository', 'title', 'close',
    'parentId', 'documentTypeId', 'resultData', '$timeout', 'tajneedIncome', '$upload', 

    function ($scope, tajneedRepository, validationRepository, title, close,
        parentId, documentTypeId, resultData, $timeout, tajneedIncome, $upload) {

        $scope.resultData = {};
        $scope.title = title;
        $scope.parentId = parentId;
        $scope.tajneedIncome = tajneedIncome;

        $scope.Types = validationRepository.getAllDetailsByValidationId(6);

        $scope.saveTajneedIncome = function (tajneedIncome) {
            $scope.errors = [];
            tajneedIncome.tajneedId = parentId;
            console.log(tajneedIncome);
            tajneedRepository.addTajneedIncome(tajneedIncome)
                .$promise
                .then(
                    function(result) {
                        // success case
                        $scope.resultData = result;
                        //appRepository.showAddSuccessGritterNotification();
                        $scope.close();
                        $('#dvIncome').modal('hide');
                        //$location.url('/HRMSPortal/employee/detail/' + resultEmployeePassport.id);
                    }, function(response) {
                        // failure case
                        console.log("tajneed save - Error !");
                        //appRepository.showErrorGritterNotification();
                        $scope.errors = response.data;
                    }
                );
        };

        $scope.close = function () {
            console.log("close function modal controller :");
            close({
                resultData: $scope.resultData
            }, 500); // close, but give 500ms for bootstrap to animate

        };

        $scope.upload = [];
        //$scope.fileUploadObj = { testString1: "Test string 1", testString2: "Test string 2" };

        $scope.isUpload = false;
        $scope.onFileSelect = function (parentId, $files) {
            console.log(parentId);
            //$scope.isUpload = true;
            //$files: an array of files selected, each file has name, size, and type.
            for (var i = 0; i < $files.length; i++) {
                var $file = $files[i];
                (function (index) {
                    $scope.upload[index] = $upload.upload({
                        url: "/api/tajneed/upload", // webapi url
                        method: "POST",
                        data: {
                            parentId: parentId,
                            typeId: documentTypeId
                        },
                        file: $file
                    }).progress(function (evt) {
                        // get upload percentage
                        console.log('percent: ' + parseInt(100.0 * evt.loaded / evt.total));
                    }).success(function (data, status, headers, config) {
                        // file is uploaded successfully
                        $scope.resultData = data;
                        //appRepository.showAddSuccessGritterNotification();
                        $scope.isUpload = false;
                        $scope.close();
                        $('#dvImage').modal('hide');
                        //$scope.employee[0].empPicture = data.empPicture;
                        console.log(data);
                    }).error(function (data, status, headers, config) {
                        // file failed to upload
                        //appRepository.showErrorGritterNotification();
                        $scope.isUpload = false;
                        console.log(data);
                    });
                })(i);
            }
        };

        $scope.abortUpload = function (index) {
            $scope.upload[index].abort();
        };

    }
]);