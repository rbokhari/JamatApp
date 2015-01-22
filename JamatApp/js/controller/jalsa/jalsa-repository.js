

'use strict';

jamatModule.factory('jalsaRepository', ['$resource', '$http', function ($resource, $http) {

    var _getAllJalsa = function (id) {
        return $resource('/api/jalsa/' + id).get(); // can use get() instead of query(), but using query() because it except to return back array of objects
    };

    return {
        getAllJalsa: _getAllJalsa
    };

}]);
