var Center;
(function (Center) {
    var Controller = (function () {
        function Controller($scope, $http, $filter, Hub) {
            this.httpService = $http;
            this.scope = $scope;
            this.refreshAuctions($scope);
            var controller = this;
            this.hub = new Hub('auction', {
                // client-side methods
                listeners: {
                    'refreshauctions': function (msg) {
                        controller.refreshAuctions($scope);
                        toastr.success(msg);
                    }
                },
                //handle connection error
                errorHandler: function (error) {
                    console.error(error);
                },
                stateChanged: function (state) {
                    switch (state.newState) {
                        case $.signalR.connectionState.connecting:
                            console.log('连接中');
                            break;
                        case $.signalR.connectionState.connected:
                            console.log('已连接');
                            break;
                        case $.signalR.connectionState.reconnecting:
                            console.log('重新连接');
                            break;
                        case $.signalR.connectionState.disconnected:
                            console.log('断开');
                            break;
                    }
                }
            });
        }
        Controller.prototype.refreshAuctions = function (scope) {
            this.getAuctions(function (data) {
                if (scope.auctions != data) {
                    scope.auctions = data;
                }
            });
        };
        Controller.prototype.getAuctions = function (successCallback) {
            this.httpService.get('/api/Auctions/').then(function (response) {
                successCallback(response.data);
            });
        };
        return Controller;
    }());
    Center.Controller = Controller;
})(Center || (Center = {}));
angular.module('lswapp', ['SignalR'])
    .controller("Center.Controller", Center.Controller);
