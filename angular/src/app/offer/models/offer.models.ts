export interface IProperty {
	propertyId: number;
	propertyType: number;
	province: string;
	district: string;
	street: string;
	number: string;
	flat: number;
	city: string;
	postalCode: string;
	geoLat: number;
	geoLon: number;
	area: number;
	maxNumberOfInhabitants: number;
	constructionYear: number;
	elevator: boolean;
	imagesURLs: string[];
	verificationStatus: number;
	numberOfRooms: number;
	numberOfFloors: number;
	plotArea: number;
	floor: number;
	equipment: IPropertyEquipment[];
}

export interface IPropertyEquipment {
	equipmentId: number;
	name: string;
}
