import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Pessoa } from '../Models/pessoas';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  // Base URL for the API, taken from the environment configuration
  private baseUrl = environment.api;

  constructor(private http: HttpClient) {}

  // Returns an Observable that fetches the list of people from the API
  getPeople(): Observable<Pessoa[]> {
    return this.http.get<Pessoa[]>(`${this.baseUrl}/pessoas`);
  }

  // Returns an Observable that fetches a specific person based on the provided identifier
  getUpSpecificPerson(person: string): Observable<Pessoa[]> {
    console.log('Search for person:', person);
    return this.http.get<Pessoa[]>(`${this.baseUrl}/pessoas/${person}`);
  }

  // Sends a POST request to create a new person and returns an Observable of the created person
  postPerson(person: any): Observable<Pessoa> {
    return this.http.post<Pessoa>(`${this.baseUrl}/pessoas/create/`, person);
  }

  // Sends a PUT request to update an existing person by ID and returns an Observable of the updated person
  updatePerson(id: any, person: any): Observable<Pessoa> {
    return this.http.put<Pessoa>(
      `${this.baseUrl}/pessoas/update/${id}/`,
      person
    );
  }

  // Sends a DELETE request to remove a person by ID and returns an Observable of void
  deletePerson(id: any): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/pessoas/delete/${id}`);
  }
}
