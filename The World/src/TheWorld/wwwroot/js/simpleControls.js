// simpleControls.js

//#region Directives - Reusable Control : WaitCursor

(function () {
    "use strict";

    angular.module("simpleControls", [])
      .directive("waitCursor", waitCursor);

    function waitCursor() {
        return {
            scope: {
                show: "=displayWhen"
            },
            restrict: "E",//restrict it to only the element style
            templateUrl: "/views/waitCursor.html"//all clientside i.e. inside wwwroot
        };
    }

})();

//#endregion