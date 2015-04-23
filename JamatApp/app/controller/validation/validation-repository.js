/// <reference path="../../module/jamat-module.js" />


'use strict';

jamatModule.factory('validationRepository', ['$resource', 'VALIDATIONS', function ($resource, VALIDATIONS) {

    var _getAllDetailsByValidationId = function (id) {
        return $resource('/api/validation/' + id + '/GetValidationDetailByValidationId').query();
    };

    var _getSingleDetailByValidationId = function (vid) {
        return $resource('/api/validation/' + vid).get();
    };

    var _getAllAuxilary = function () {
        return $resource('/api/validation/' + VALIDATIONS.AUXILARY + '/GetValidationDetailByValidationId').query();
    };


    var _getAllNationality = function () {
        return $resource('/api/validation/' + VALIDATIONS.NATIONALITY + '/GetValidationDetailByValidationId').query();
    };


    var _getAllChandaType = function () {
        return $resource('/api/validation/' + VALIDATIONS.CHANDA_TYPE + '/GetValidationDetailByValidationId').query();
    };


    var _getAllCountry = function () {
        return $resource('/api/validation/' + VALIDATIONS.COUNTRY + '/GetValidationDetailByValidationId').query();
    };


    var _getAllTajneedType = function () {
        return $resource('/api/validation/' + VALIDATIONS.TAJNEED_TYPE + '/GetValidationDetailByValidationId').query();
    };


    return {

        getAllDetailsByValidationId: _getAllDetailsByValidationId,
        getSingleDetailByValidationId: _getSingleDetailByValidationId,
        getAllAuxilary: _getAllAuxilary,
        getAllNationality: _getAllNationality,
        getAllChandaType: _getAllChandaType,
        getAllCountry: _getAllCountry,
        getAllTajneedType: _getAllTajneedType

        //    get: function() {
        //        return $resource('/api/validation').query(); // can use get() instead of query(), but using query() because it except to return back array of objects
        //    }
    };

}]);
