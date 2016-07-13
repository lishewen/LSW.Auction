var Auction;
(function (Auction) {
    var Controller = (function () {
        function Controller($scope, $http, $filter, Hub) {
            this.httpService = $http;
            this.scope = $scope;
            this.refreshAuctionRecords($scope);
            this.Init($scope);
            var controller = this;
            this.hub = new Hub('auction', {
                // client-side methods
                listeners: {
                    'refreshauctionrecords': function (msg) {
                        controller.refreshAuctionRecords($scope);
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
            $scope.dobid = function () {
                controller.postAuctionRecords($scope.myauctionrecord, function () {
                    controller.Init($scope);
                }, function (data) {
                    if (data.Message)
                        alert(data.Message);
                    else
                        alert(data);
                    controller.refreshAuctionRecords($scope);
                    controller.Init($scope);
                });
            };
        }
        Controller.prototype.Init = function (scope) {
            scope.myauctionrecord = new Models.AuctionRecord;
            scope.myauctionrecord.mid = mid;
            scope.myauctionrecord.gid = id;
            scope.myauctionrecord.name = '我的出价';
        };
        Controller.prototype.refreshAuctionRecords = function (scope) {
            this.getAuctionRecords(function (data) {
                if (scope.auctionrecords != data) {
                    scope.auctionrecords = data;
                }
            });
        };
        Controller.prototype.getAuctionRecords = function (successCallback) {
            this.httpService.get('/api/AuctionRecords/' + id).success(function (data, status) {
                successCallback(data);
            });
        };
        Controller.prototype.postAuctionRecords = function (model, successCallback, errorCallback) {
            this.httpService.post('/api/AuctionRecords', model).success(function (data, status) {
                successCallback(data);
            }).error(function (data) {
                errorCallback(data);
            });
        };
        return Controller;
    }());
    Auction.Controller = Controller;
})(Auction || (Auction = {}));
angular.module('lswapp', ['SignalR'])
    .controller("Auction.Controller", Auction.Controller);
