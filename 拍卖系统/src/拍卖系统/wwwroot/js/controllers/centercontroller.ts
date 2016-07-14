namespace Center {
	export interface Scope extends angular.IScope {
		auctions: Array<Models.Auction>
	}
	export class Controller {
		private httpService: angular.IHttpService;
		public scope: Scope;
		private hub: ngSignalr.Hub;

		constructor($scope: Scope, $http: angular.IHttpService, $filter: angular.IFilterService, Hub: ngSignalr.HubFactory) {
			this.httpService = $http;
			this.scope = $scope;
			this.refreshAuctions($scope);

			var controller = this;

			this.hub = new Hub('auction', {
				// client-side methods
				listeners: {
					'refreshauctions': (msg: string) => {
						controller.refreshAuctions($scope);
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
		}

		refreshAuctions(scope: Scope) {
			this.getAuctions(function (data: Array<Models.Auction>) {
				if (scope.auctions != data) {
					scope.auctions = data;
				}
			});
		}

		getAuctions(successCallback: Function): void {
			this.httpService.get('/api/Auctions/').success(function (data, status) {
				successCallback(data);
			});
		}
	}
}

angular.module('lswapp', ['SignalR'])
	.controller("Center.Controller", Center.Controller);