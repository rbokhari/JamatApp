

'use strict';

jamatModule.factory('tajneedRepository', ['$resource', '$http', '$q', 'localStorageService', function ($resource, $http, $q, localStorageService) {

    var _getAllTajneed = function (forceRefresh) {
        //return $resource('/api/tajneed').query(); // can use get() instead of query(), but using query() because it except to return back array of objects
        if (typeof forceRefresh === 'undefined') { forceRefresh = false;}
        var req = {
            method: 'GET',
            url: '/api/tajneed'
        };
        var deferred = $q.defer();
        var tajneedData = null;

        if (!forceRefresh){ tajneedData = localStorageService.get('alltajneeds') }

        if (tajneedData) {
            console.log("found from storage");
            deferred.resolve(tajneedData);
        } else {
            console.log("fetch from server");
            $http(req)
                .success(function (res) {
                    localStorageService.set('alltajneeds', res);
                    deferred.resolve(res);
                })
                .error(function (err) {
                    deferred.reject(err);
                });
        }
        return deferred.promise;
    };

    var _getTajneedCount = function () {
        return $http.get('/api/tajneed/GetTajneedCount'); 
    };

    var _getTajneedById = function (id) {
        return $resource('/api/tajneed/' + id).query();
    };

    var _addTajneed = function (tajneed) {
        return $resource('/api/tajneed/').save(tajneed);
    };

    var _editTajneed = function (tajneed) {
        return $http.put('/api/tajneed/' + tajneed.id, tajneed);
    };

    var _addTajneedIncome = function (tajneedIncome) {
        return $resource('/api/tajneed/' + tajneedIncome.tajneedId + '/PostTajneedIncome').save(tajneedIncome);
    };


    var _getCountByAuxilary = function() {
        return $resource('/api/tajneed/getTajneedAuxilary').query();
    };

    var _getCountByRegion = function () {
        return $resource('/api/tajneed/getTajneedRegion').query();
    };

    var _getCountByNationality = function () {
        return $resource('/api/tajneed/getTajneedNationality').query();
    };

    var _getCountByWassiyat = function () {
        return $resource('/api/tajneed/getTajneedWassiyat').query();
    };

    return {
        getAllTajneed: _getAllTajneed,
        getTajneedById: _getTajneedById,
        addTajneed: _addTajneed,
        editTajneed: _editTajneed,
        addTajneedIncome: _addTajneedIncome,
        getTajneedCount: _getTajneedCount,
        getCountByAuxilary: _getCountByAuxilary,
        getCountByRegion: _getCountByRegion,
        getCountByNationality: _getCountByNationality,
        getCountByWassiyat: _getCountByWassiyat
    };

}]);
