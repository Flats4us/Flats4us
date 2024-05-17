import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment.prod';
import { IProblem } from '../models/report-problem.models';

@Injectable({
	providedIn: 'root',
})
export class ReportProblemService {
	constructor(private http: HttpClient) {}

	public reportProblem(problem: IProblem) {
		return this.http.post(`${environment.apiUrl}/technical-problems`, {
			kind: Number(problem.kind),
			description: problem.description,
		});
	}
}
