var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var Models;
(function (Models) {
    var EntityBase = (function () {
        function EntityBase() {
        }
        return EntityBase;
    }());
    Models.EntityBase = EntityBase;
    var AuctionRecord = (function (_super) {
        __extends(AuctionRecord, _super);
        function AuctionRecord() {
            _super.apply(this, arguments);
        }
        return AuctionRecord;
    }(EntityBase));
    Models.AuctionRecord = AuctionRecord;
    var Auction = (function (_super) {
        __extends(Auction, _super);
        function Auction() {
            _super.apply(this, arguments);
        }
        return Auction;
    }(EntityBase));
    Models.Auction = Auction;
    (function (EndStatus) {
        EndStatus[EndStatus["未开始"] = 0] = "未开始";
        EndStatus[EndStatus["进行中"] = 1] = "进行中";
        EndStatus[EndStatus["成交"] = 2] = "成交";
        EndStatus[EndStatus["流拍"] = 3] = "流拍";
    })(Models.EndStatus || (Models.EndStatus = {}));
    var EndStatus = Models.EndStatus;
})(Models || (Models = {}));
