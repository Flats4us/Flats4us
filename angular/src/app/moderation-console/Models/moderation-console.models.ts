export interface IProperty {
	propertyId: number;
	propertyType: number;
	ownerName: string;
	ownerEmail: string;
	address: string;
	imagesURLs: string[];
	documentURL: string;
	verificationStatus: number;
	creationDate: Date;
}

export interface IUser {
	userId: number;
	userType: number;
	name: string;
	surName: string;
	email: string;
	imagesPath: string;
	documentType: number;
	verificationStatus: number;
	documentExpireDate: Date;
	studentNumber: string;
	university: string;
	documentNumber: string;
}

export interface IDispute {
	disputeBetween: string;
	createdBy: string;
	creationDate: Date;
	moderatorAdditionDate: Date;
}
