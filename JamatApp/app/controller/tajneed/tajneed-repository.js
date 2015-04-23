

'use strict';

jamatModule.factory('tajneedRepository', ['$resource', '$http', function ($resource, $http) {

    var _getAllTajneed = function () {
        return $resource('/api/tajneed').query(); // can use get() instead of query(), but using query() because it except to return back array of objects
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
