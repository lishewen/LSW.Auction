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
            return _super.apply(this, arguments) || this;
        }
        return AuctionRecord;
    }(EntityBase));
    Models.AuctionRecord = AuctionRecord;
    var Auction = (function (_super) {
        __extends(Auction, _super);
        function Auction() {
            return _super.apply(this, arguments) || this;
        }
        return Auction;
    }(EntityBase));
    Models.Auction = Auction;
    var Member = (function (_super) {
        __extends(Member, _super);
        function Member() {
            return _super.apply(this, arguments) || this;
        }
        return Member;
    }(EntityBase));
    Models.Member = Member;
    var EndStatus;
    (function (EndStatus) {
        EndStatus[EndStatus["\u672A\u5F00\u59CB"] = 0] = "\u672A\u5F00\u59CB";
        EndStatus[EndStatus["\u8FDB\u884C\u4E2D"] = 1] = "\u8FDB\u884C\u4E2D";
        EndStatus[EndStatus["\u6210\u4EA4"] = 2] = "\u6210\u4EA4";
        EndStatus[EndStatus["\u6D41\u62CD"] = 3] = "\u6D41\u62CD";
    })(EndStatus = Models.EndStatus || (Models.EndStatus = {}));
})(Models || (Models = {}));
