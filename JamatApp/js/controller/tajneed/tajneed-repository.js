

'use strict';

jamatModule.factory('tajneedRepository', ['$resource', '$http', function ($resource, $http) {

    var _getAllTajneed = function () {
        return $resource('/api/tajneed').query(); // can use get() instead of query(), but using query() because it except to return back array of objects
    };

    var _getTajneedById = function (id) {
        return $resource('/api/tajneed/' + id).get();
    };

    var _addTajneed = function (tajneed) {
        return $resource('/api/tajneed').save(tajneed);
    };

    var _editTajneed = function (tajneed) {
        return $http.put('/api/tajneed/' + tajneed.id, tajneed);
    };

    return {
        getAllTajneed: _getAllTajneed,
        getTajneedById: _getTajneedById,
        addTajneed: _addTajneed,
        editTajneed: _editTajneed
    };

}]);
