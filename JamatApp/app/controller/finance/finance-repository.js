
'use strict';

jamatModule.factory('financeRepository',
[
    '$resource', function($resource) {


        var _getAllChandaYear = function () {
            return $resource('/api/finance').query();
        };

        var _addFinanceYear = function (year) {
            console.log(year);
            return $resource('/api/finance').save(year);
        };


        return{
            getAllChandaYear: _getAllChandaYear,
            addFinanceYear: _addFinanceYear

        };


    }
]);