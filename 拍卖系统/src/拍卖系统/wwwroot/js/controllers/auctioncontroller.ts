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

			$scope.dobid = () => {
				controller.postAuctionRecords($scope.myauctionrecord, () => {
					controller.Init($scope);
				});
			}
		}

		Init(scope: Scope) {
			scope.myauctionrecord = new Models.AuctionRecord;
			scope.myauctionrecord.Mid = mid;
			scope.myauctionrecord.Gid = id;
			scope.myauctionrecord.Name = '我的出价';
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

		postAuctionRecords(model: Models.AuctionRecord, successCallback: Function): void {
			this.httpService.post('/api/AuctionRecords', model).success(function (data, status) {
				successCallback(data);
			}).error(function (data) {
				if (data.Message)
					alert(data.Message);
				else
					alert(data);
			});
		}
	}
}

angular.module('lswapp', ['SignalR'])
	.controller("Auction.Controller", Auction.Controller);