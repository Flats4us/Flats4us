import { Injectable } from '@angular/core';
import { IQuestionsData } from '@shared/models/survey.models';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment.prod';

@Injectable()
export class SurveyService {
	protected apiRoute = `${environment.apiUrl}/surveys/template`;

	constructor(private http: HttpClient) {}

	public getStudentQuestions() {
		const headers = {
			'accept-language': 'PL',
		};

		return this.http.get<IQuestionsData[]>(`${this.apiRoute}/student`, {
			headers,
		});
	}

	public getOwnerQuestions() {
		const headers = {
			'accept-language': 'PL',
		};

		return this.http.get<IQuestionsData[]>(`${this.apiRoute}/owner`, {
			headers,
		});
	}
}
