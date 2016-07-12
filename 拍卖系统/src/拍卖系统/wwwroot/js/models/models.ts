namespace Models {
	export class EntityBase {
		Id: number;
		Name: string;
	}
	export class AuctionRecord extends EntityBase {
		Gid: number;
		Mid: number;
		Money: number;
	}
	export class Auction extends EntityBase {
		Gid: number;
		Bidnb: string;
		Onset: number;
		NowPrice: number;
		StartTime: Date;
		EndTime: Date;
		StepSize: number;
		Mid: number;
		BidCount: number;
		EndStatus: EndStatus;
		AuctionRecords: Array<AuctionRecord>;
	}
	export enum EndStatus {
		未开始,
		进行中,
		成交,
		流拍
	}
}