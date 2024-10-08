import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map } from 'rxjs';
import {
	IEditProfile,
	IEditProfileOwner,
	IEditProfileOwnerConfidential,
	IEditProfileStudent,
	IEditProfileStudentConfiential,
	IInterest,
	IOpinion,
	IUserProfile,
} from '../models/profile.models';
import { environment } from 'src/environments/environment';
import { INumeric } from 'src/app/real-estate/models/real-estate.models';
import { IResult } from '@shared/models/shared.models';

@Injectable({
	providedIn: 'root',
})
export class ProfileService {
	protected apiRoute = `${environment.apiUrl}`;

	public interests: IInterest[] = [];

	public documentTypes: INumeric[] = [
		{ value: 0, viewValue: 'Profile.document-type0' },
		{ value: 1, viewValue: 'Profile.document-type1' },
		{ value: 2, viewValue: 'Profile.document-type2' },
	];

	constructor(private httpClient: HttpClient) {}

	private static headerPhotoURL: BehaviorSubject<string> =
		new BehaviorSubject<string>('');

	private toEdit: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

	public readAllInterests(): Observable<IInterest[]> {
		return this.getInterests('').pipe(
			map(interests => {
				interests.forEach(interest => this.interests.push(interest));
				return this.interests;
			})
		);
	}

	public addProfileFiles(formData: FormData): Observable<IResult> {
		return this.httpClient.post<IResult>(
			`${this.apiRoute}/users/files`,
			formData
		);
	}

	public deleteProfileFile(fileId: string): Observable<IResult> {
		return this.httpClient.delete<IResult>(
			`${this.apiRoute}/users/files/${fileId}`
		);
	}

	public editProfile(newData: IEditProfile) {
		return this.httpClient.put(`${this.apiRoute}/users/current`, newData);
	}

	public editProfileOwner(
		newData: IEditProfileOwner | IEditProfileOwnerConfidential
	) {
		return this.httpClient.put(`${this.apiRoute}/users/current`, newData);
	}

	public editProfileStudent(
		newData: IEditProfileStudent | IEditProfileStudentConfiential
	) {
		return this.httpClient.put(`${this.apiRoute}/users/current`, newData);
	}

	public editProfileModerator(newData: { phoneNumber: string }) {
		return this.httpClient.put(`${this.apiRoute}/users/current`, newData);
	}

	public getActualProfile(): Observable<IUserProfile> {
		return this.httpClient.get<IUserProfile>(`${this.apiRoute}/users/my-profile`);
	}
	public getInterests(name = ''): Observable<IInterest[]> {
		let params = new HttpParams();
		params = params.append('name', name);
		return this.httpClient.get<IInterest[]>(`${this.apiRoute}/interests`, {
			params: params,
		});
	}
	public addOpinion(id: number, opinion: IOpinion) {
		return this.httpClient.post(`${this.apiRoute}/users/${id}/opinion`, opinion);
	}

	public setHeaderPhotoURL(url?: string) {
		ProfileService.headerPhotoURL.next(url ?? '');
		this.toEdit.next(false);
	}
	public getHeaderPhotoURL(): Observable<string> {
		return ProfileService.headerPhotoURL.asObservable();
	}

	public isHeaderPhotoURL(): boolean {
		return !!ProfileService.headerPhotoURL.getValue();
	}

	public startEdit() {
		this.toEdit.next(true);
	}

	public isToEdit(): boolean {
		return this.toEdit.getValue();
	}
}
