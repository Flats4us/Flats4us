import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { IInterest, IOpinion, IUserProfile } from '../models/profile.models';
import { environment } from 'src/environments/environment.prod';
import { INumeric } from 'src/app/real-estate/models/real-estate.models';

@Injectable()
export class ProfileService {
	protected apiRoute = `${environment.apiUrl}`;

	public interests: IInterest[] = [];

	public documentTypes: INumeric[] = [
		{ value: 0, viewValue: 'Dowód osobisty' },
		{ value: 1, viewValue: 'Legitymacja studencka' },
		{ value: 2, viewValue: 'Paszport' },
	];

	constructor(private httpClient: HttpClient) {}

	public readAllInterests(): Observable<IInterest[]> {
		return this.getInterests('').pipe(
			map(interests => {
				interests.forEach(interest => this.interests.push(interest));
				return this.interests;
			})
		);
	}
	public addProfileFiles(formData: FormData): Observable<void> {
		return this.httpClient.post<void>(`${this.apiRoute}/users/files`, formData);
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
}
