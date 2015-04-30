
'use strict';

jamatModule.factory('financeRepository',
    ['$resource', function($resource) {

        var _getAllChandaYear = function () {
            return $resource('/api/finance').query();
        };

        var _getChandaYear = function (id) {
            return $resource('/api/finance/' + id).get();
        };

        var _getAuxilaryIncome = function (id) {
            return $resource('/api/finance/getAuxilaryIncome/' + id).query();
        };


        var _addFinanceYear = function (year) {
            return $resource('/api/finance').save(year);
        };

        var _addAuxilaryBudget = function (id, notes) {
            return $resource('/api/finance/setAuxilaryBudget/' + id + "/" + notes).save();
        };

        var _getAuxilaryBudget = function (yearId) {
            return $resource('/api/finance/getBudget/' + yearId).get();
        };


        return{
            getAllChandaYear: _getAllChandaYear,
            getChandaYear: _getChandaYear,
            getAuxilaryIncome:_getAuxilaryIncome,
            addFinanceYear: _addFinanceYear,
            addAuxilaryBudget: _addAuxilaryBudget,
            getAuxilaryBudget: _getAuxilaryBudget

        };

    }
]);