
'use strict';

jamatModule.factory('countryRepository', ['$resource', '$http', function ($resource, $http) {

    var _getAllCountries = function () {
        return $resource('/api/country').query(); // can use get() instead of query(), but using query() because it except to return back array of objects
    };

    var _getCountryById = function (id) {
        return $resource('/api/country/' + id).get();
    };

    var _addCountry = function (country) {
        return $resource('/api/country').save(country);
    };

    var _editCountry = function (country) {
        return $http.put('/api/country/' + country.countryId, country);
    };


    var _getAllRegionByCountryId = function (id) {
        return $resource('/api/region/' + id).query(); // can use get() instead of query(), but using query() because it except to return back array of objects
    };

    var _getRegionById = function (id, rId) {
        return $resource('/api/country/' + id + "/Region/"+ rId).get();
    };

    var _addRegion = function (region) {
        return $resource('/api/country').save(region);
    };

    var _editRegion = function (region) {
        return $http.put('/api/country/' + region.regionId, region);
    };

    return {
        getAllCountries: _getAllCountries,
        getCountryById: _getCountryById,
        addCountry: _addCountry,
        editCountry: _editCountry,
        getAllRegionsByCountryId: _getAllRegionByCountryId,
        getRegionById: _getRegionById,
        addRegion: _addRegion,
        editRegion: _editRegion

    };

}]);
