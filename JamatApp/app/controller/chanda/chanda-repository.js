
'use strict';

jamatModule.factory('chandaRepository', ['$resource', '$http', function ($resource, $http) {

    var getSubHeadsById = function (id) {
        return $resource('/api/chanda/GetChandaSubHead/' + id).query(); // can use get() instead of query(), but using query() because it except to return back array of objects
    };

    var addChanda = function (chanda) {
        return $resource('/api/chanda/add/').save(chanda);
    }

    return {
        getSubHeadsById: getSubHeadsById,
        addChanda: addChanda

    };

}]);
