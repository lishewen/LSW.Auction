﻿declare var id: number;
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
				//server side methods
				methods: ['JoinRoom', 'LeaveRoom'],
				//query params sent on initial connection
				queryParams: {
					'mid': id.toString()
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
							this.joinroom();
							break;
						case $.signalR.connectionState.reconnecting:
							console.log('重新连接');
							break;
						case $.signalR.connectionState.disconnected:
							console.log('断开');
							this.leaveroom();
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

		public joinroom() {
			this.hub.invoke('JoinRoom', 'room' + id);
		}

		public leaveroom() {
			this.hub.invoke('LeaveRoom', 'room' + id);
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
			this.httpService.get('/api/AuctionRecords/' + id).then((response) => {
				successCallback(response.data);
			});
		}

		postAuctionRecords(model: Models.AuctionRecord, successCallback: Function, errorCallback: Function): void {
			this.httpService.post('/api/AuctionRecords', model).then((response) => {
				successCallback(response.data);
			}, (reason) => {
				errorCallback(reason);
			});
		}
	}
}

angular.module('lswapp', ['SignalR'])
	.controller("Auction.Controller", Auction.Controller);