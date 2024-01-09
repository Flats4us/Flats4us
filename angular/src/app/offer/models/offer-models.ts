export interface IWatchedOffer {
	totalCount: number;
	result: IOffer[];
}

export interface IOffer {
	offerId: number;
	date: Date;
	offerStatus: number;
	price: number;
	deposit: number;
	description: string;
	startDate: Date;
	endDate: Date;
	numberOfInterested: number;
	regulations: string;
	property: IProperty;
	equipment: IEquipment[];
	owner: IOwner;
	surveyOwnerOffer: ISurveyOwnerOffer;
}

export interface IProperty {
	propertyId: number;
	propertyType: number;
	province: string;
	district: string;
	street: string;
	number: number;
	flat: number;
	city: string;
	postalCode: string;
	geoLat: number;
	geoLon: number;
	area: number;
	maxNumberOfInhabitants: number;
	constructionYear: number;
	elevator: boolean;
	images: IImages[];
	verificationStatus: number;
	numberOfRooms: number;
	numberOfFloors: number;
	plotArea: number;
	floor: number;
}

export interface IImages {
	name: string;
	path: string;
}

export interface IOwner {
	userId: number;
	name: string;
	surname: string;
	email: string;
	phoneNumber: string;
	imagesPath: string;
	activityStatus: boolean;
}

export interface ISurveyOwnerOffer {
	smokingAllowed: boolean;
	partiesAllowed: boolean;
	animalsAllowed: boolean;
	gender: number;
}

export interface IEquipment {
	equipmentId: number;
	name: string;
}
