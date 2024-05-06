import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment.prod';

@Injectable({
	providedIn: 'root',
})
export class ReportProblemService {
	constructor(private http: HttpClient) {}

	public reportProblem(kind: number, description: string) {
		return this.http.post(`${environment.apiUrl}/technical-problems`, {
			kind: Number(kind),
			description: description,
		});
	}
}
