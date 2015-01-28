

'use strict';

jamatModule.factory('jalsaRepository', ['$resource', '$http', function ($resource, $http) {

    var _getAllJalsaDay = function (id,day) {
        return $resource('/api/jalsa/?id=' + id + '&day=' + day).query(); // can use get() instead of query(), but using query() because it except to return back array of objects
    };

    var _getJalsaById = function (id) {
        return $resource('/api/jalsa/?id=' + id).get(); 
    };

    var _getAllJalsaDaySummary = function (id) {
        return $resource('/api/jalsa/GetCount/?id=' + id).query(); // can use get() instead of query(), but using query() because it except to return back array of objects
    };

    return {
        getAllJalsaDay: _getAllJalsaDay,
        getJalsaById: _getJalsaById,
        getAllJalsaDaySummary: _getAllJalsaDaySummary
    };

}]);
