/// <reference path="../../module/jamat-module.js" />


'use strict';

jamatModule.factory('validationRepository', ['$resource', function ($resource) {

    var _getAllDetailsByValidationId = function (id) {
        return $resource('/api/validation/' + id + '/GetValidationDetailByValidationId').query();
    };

    var _getSingleDetailByValidationId = function (vid) {
        return $resource('/api/validation/' + vid).get();
    };

    return {

        getAllDetailsByValidationId: _getAllDetailsByValidationId,
        getSingleDetailByValidationId: _getSingleDetailByValidationId

        //    get: function() {
        //        return $resource('/api/validation').query(); // can use get() instead of query(), but using query() because it except to return back array of objects
        //    }
    };

}]);
