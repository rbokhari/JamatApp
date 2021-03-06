﻿/// <reference path="../../module/jamat-module.js" />


'use strict';

jamatModule.factory('validationRepository', ['$resource', '$http', 'VALIDATIONS', function ($resource, $http, VALIDATIONS) {

    var _getAllDetailsByValidationId = function (id) {
        return $resource('/api/validation/' + id + '/GetValidationDetailByValidationId').query();
    };

    var _getSingleDetailByValidationId = function (id) {
        return $resource('/api/validation/' + id).get();
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

    var _getAllTajneedStatus = function () {
        return $resource('/api/validation/' + VALIDATIONS.TAJNEED_STATUS + '/GetValidationDetailByValidationId').query();
    };

    var _getAllMonths = function () {
        return $resource('/api/validation/' + VALIDATIONS.MONTHS + '/GetValidationDetailByValidationId').query();
    };

    var _addValidation = function (validationType) {
        return $resource('/api/validation/').save(validationType);
    };

    var _editValidation = function (validationType) {
        return $http.put('/api/validation/', validationType);
    };

    return {

        getAllDetailsByValidationId: _getAllDetailsByValidationId,
        getSingleDetailByValidationId: _getSingleDetailByValidationId,
        getAllAuxilary: _getAllAuxilary,
        getAllNationality: _getAllNationality,
        getAllChandaType: _getAllChandaType,
        getAllCountry: _getAllCountry,
        getAllTajneedType: _getAllTajneedType,
        getAllTajneedStatus: _getAllTajneedStatus,
        getAllMonths: _getAllMonths,
        addValidation: _addValidation,
        editValidation: _editValidation

        //    get: function() {
        //        return $resource('/api/validation').query(); // can use get() instead of query(), but using query() because it except to return back array of objects
        //    }
    };

}]);
