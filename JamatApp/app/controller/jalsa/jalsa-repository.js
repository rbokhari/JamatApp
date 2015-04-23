

'use strict';

jamatModule.factory('jalsaRepository', ['$resource', '$http', function ($resource, $http) {

    var _getJalsaList = function () {
        return $resource('/api/jalsa/').query(); // can use get() instead of query(), but using query() because it except to return back array of objects
    };

    var _getAllJalsaDay = function (id, day) {
        return $resource('/api/jalsa/?id=' + id + '&day=' + day).query(); // can use get() instead of query(), but using query() because it except to return back array of objects
    };

    var _getJalsaById = function (id) {
        return $resource('/api/jalsa/?id=' + id).get(); 
    };

    var _getAllJalsaDaySummary = function (id) {
        return $resource('/api/jalsa/GetCount/?id=' + id).query(); // can use get() instead of query(), but using query() because it except to return back array of objects
    };

    var _getAllJalsaDayByCountrySummary = function (id) {
        return $resource('/api/jalsa/GetCountByCountry/?id=' + id).query(); // can use get() instead of query(), but using query() because it except to return back array of objects
    };

    var _addJalsa = function (jalsa) {
        return $resource('/api/jalsa').save(jalsa);
    };

    var _addJalsaDay = function (jalsaDay) {
        return $resource('/api/jalsa/SaveAttendance').save(jalsaDay);
    };


    return {
        getJalsaList: _getJalsaList,
        getAllJalsaDay: _getAllJalsaDay,
        getJalsaById: _getJalsaById,
        getAllJalsaDaySummary: _getAllJalsaDaySummary,
        getAllJalsaDayByCountrySummary: _getAllJalsaDayByCountrySummary,
        addJalsa: _addJalsa,
        addJalsaDay: _addJalsaDay
    };

}]);
