namespace Models {
	export class EntityBase {
		id: number;
		name: string;
		createTime: Date;
	}
	export class AuctionRecord extends EntityBase {
		gid: number;
		mid: number;
		money: number;
		member: Member;
	}
	export class Auction extends EntityBase {
		gid: number;
		bidnb: string;
		onset: number;
		nowPrice: number;
		startTime: Date;
		endTime: Date;
		stepSize: number;
		mid: number;
		bidCount: number;
		endStatus: EndStatus;
		auctionRecords: Array<AuctionRecord>;
	}
	export class Member extends EntityBase {
		nickName: string;
		avatar: string;
	}
	export enum EndStatus {
		未开始,
		进行中,
		成交,
		流拍
	}
}