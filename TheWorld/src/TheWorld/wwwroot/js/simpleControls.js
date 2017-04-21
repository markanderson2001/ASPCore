// simpleControls.js

//#region Directives - Reusable Control : WaitCursor

    (function () {
        "use strict";

        angular.module("simpleControls", [])
            //. provides the ClueNet syntax eg: .Directives
              .directive("waitCursor", waitCursor);
        
        function waitCursor() {
            return {
                scope: {
                    show: "=displayWhen"
                },
                restrict: "E",  //restricted to only the element style
                templateUrl: "/views/waitCursor.html"
            };
        }

    })();

//#endregion