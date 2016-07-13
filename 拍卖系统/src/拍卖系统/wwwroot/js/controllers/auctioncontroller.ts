declare var id: number;
declare var mid: number;

namespace Auction {
	export interface Scope extends angular.IScope {
		auctionrecords: Array<Models.AuctionRecord>
		myauctionrecord: Models.AuctionRecord;
		dobid: Function;
	}
	export class Controller {
		private httpService: angular.IHttpService;
		public scope: Scope;
		private hub: ngSignalr.Hub;

		constructor($scope: Scope, $http: angular.IHttpService, $filter: angular.IFilterService, Hub: ngSignalr.HubFactory) {
			this.httpService = $http;
			this.scope = $scope;
			this.refreshAuctionRecords($scope);
			this.Init($scope);

			var controller = this;

			this.hub = new Hub('auction', {
				// client-side methods
				listeners: {
					'refreshauctionrecords': (msg: string) => {
						controller.refreshAuctionRecords($scope);
						toastr.success(msg);
					}
				},
				//handle connection error
				errorHandler: function (error) {
					console.error(error);
				},
				stateChanged: (state: SignalR.StateChanged) => {
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

			$scope.dobid = () => {
				controller.postAuctionRecords($scope.myauctionrecord, () => {
					controller.Init($scope);
				}, (data) => {
					if (data.Message)
						alert(data.Message);
					else
						alert(data);
					controller.refreshAuctionRecords($scope);
					controller.Init($scope);
				});
			}
		}

		Init(scope: Scope) {
			scope.myauctionrecord = new Models.AuctionRecord;
			scope.myauctionrecord.mid = mid;
			scope.myauctionrecord.gid = id;
			scope.myauctionrecord.name = '我的出价';
		}

		refreshAuctionRecords(scope: Scope) {
			this.getAuctionRecords(function (data: Array<Models.AuctionRecord>) {
				if (scope.auctionrecords != data) {
					scope.auctionrecords = data;
				}
			});
		}

		getAuctionRecords(successCallback: Function): void {
			this.httpService.get('/api/AuctionRecords/' + id).success(function (data, status) {
				successCallback(data);
			});
		}

		postAuctionRecords(model: Models.AuctionRecord, successCallback: Function, errorCallback: Function): void {
			this.httpService.post('/api/AuctionRecords', model).success(function (data, status) {
				successCallback(data);
			}).error(function (data) {
				errorCallback(data);
			});
		}
	}
}

angular.module('lswapp', ['SignalR'])
	.controller("Auction.Controller", Auction.Controller);