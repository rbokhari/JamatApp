/**
 * AngularJS example directive. 
 * Allows you to modify the page DOM and CSS styles before printing by exexuting the passed expression. 
 * Will force a $digest phase before the print operation.
 * 
 * Example: <div ng-class="{printed: isPrinted}" on-before-print="printed = true" />
 */
jamatModule.directive('onBeforePrint', ['$window', '$rootScope', '$timeout', function onBeforePrint($window, $rootScope, $timeout) {
    var beforePrintDirty = false;
    var listeners = [];

    var beforePrint = function () {
        if (beforePrintDirty) return;

        beforePrintDirty = true;

        if (listeners) {
            for (var i = 0, len = listeners.length; i < len; i++) {
                listeners[i].triggerHandler('beforePrint');
            }

            var scopePhase = $rootScope.$$phase;

            // This must be synchronious so we call digest here.
            if (scopePhase != '$apply' && scopePhase != '$digest') {
                $rootScope.$digest();
            }
        }

        $timeout(function () {
            // This is used for Webkit. For some reason this gets called twice there.
            beforePrintDirty = false;
        }, 100, false);
    };

    if ($window.matchMedia) {
        var mediaQueryList = $window.matchMedia('print');
        mediaQueryList.addListener(function (mql) {
            if (mql.matches) {
                beforePrint();
            }
        });
    }

    $window.onbeforeprint = beforePrint;

    return function (scope, iElement, iAttrs) {
        function onBeforePrint() {
            scope.$eval(iAttrs.onBeforePrint);
        }

        listeners.push(iElement);
        iElement.on('beforePrint', onBeforePrint);

        scope.$on('$destroy', function () {
            iElement.off('beforePrint', onBeforePrint);

            var pos = -1;

            for (var i = 0, len = listeners.length; i < len; i++) {
                var currentElement = listeners[i];

                if (currentElement === iElement) {
                    pos = i;
                    break;
                }
            }

            if (pos >= 0) {
                listeners.splice(pos, 1);
            }
        });
    };
}])