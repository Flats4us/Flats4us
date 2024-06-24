import { Injectable } from '@angular/core';
import { IDecision, IStudent } from '../models/roommate-candidate.models';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable()
export class FindRoommateService {
	protected apiRoute = `${environment.apiUrl}/matcher`;

	constructor(private http: HttpClient) {}

	public getStudents() {
		return this.http.get<IStudent[]>(`${this.apiRoute}/potential-by-id`);
	}

	public getMatches() {
		return this.http.get<IStudent[]>(`${this.apiRoute}/existing-by-id`);
	}

	public accept(id: number, { decision: decision }: IDecision) {
		return this.http.post(`${this.apiRoute}/accept/students/${id}`, { decision });
	}
}
