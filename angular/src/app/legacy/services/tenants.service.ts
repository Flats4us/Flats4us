import {
	HttpClient,
	HttpErrorResponse,
	HttpHeaders,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
	providedIn: 'root',
})
export class TenantsService {
	apiUrl = 'https://localhost:44376/api/tenant';
	headers = new HttpHeaders().set('Content-Type', 'application/json');

	constructor(private httpClient: HttpClient) {}

	// GET
	list(): Observable<any> {
		return this.httpClient.get(this.apiUrl).pipe(catchError(this.handleError));
	}

	// GET/id
	getItem(id: any): Observable<any> {
		return this.httpClient
			.get(`${this.apiUrl}/${id}`)
			.pipe(catchError(this.handleError));
	}

	// CREATE
	create(data: any): Observable<any> {
		return this.httpClient
			.post(this.apiUrl, data)
			.pipe(catchError(this.handleError));
	}

	// UPDATE
	update(id: any, data: any): Observable<any> {
		return this.httpClient
			.put(`${this.apiUrl}/${id}`, data)
			.pipe(catchError(this.handleError));
	}

	// DELETE
	delete(id: any): Observable<any> {
		return this.httpClient
			.delete(`${this.apiUrl}/${id}`)
			.pipe(catchError(this.handleError));
	}

	handleError(error: HttpErrorResponse) {
		if (error.error instanceof ErrorEvent) {
			console.error('An error occurred: ', error.error.message);
		} else {
			console.error(
				`Server returned code ${error.status},` + `body was: ${error.error}`
			);
		}
		return throwError(() => new Error('Error Error'));
	}
}
