
jamatModule.controller('TajneedLookupModalController',
[
    '$scope', 'tajneedRepository', 'validationRepository', 'title', 'close',
    'parentId', 'resultData', '$timeout', 

    function ($scope, tajneedRepository, validationRepository, title, close,
        parentId, resultData, $timeout) {

        $scope.resultData = {};
        $scope.title = title;
        $scope.parentId = parentId;

        $scope.tajneeds = tajneedRepository.getAllTajneed();

        $scope.selectTajneed = function (tajneed) {
            $scope.resultData = tajneed;
            $scope.close();
            $('#dvTajneedLookup').modal('hide');
        };

        $scope.close = function () {
            console.log("close function modal controller :");
            close({
                resultData: $scope.resultData
            }, 500); // close, but give 500ms for bootstrap to animate

        };

    }
]);