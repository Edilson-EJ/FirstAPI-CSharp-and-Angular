import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Pessoa } from '../Models/pessoas';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private baseUrl = environment.api;

  constructor(private http: HttpClient) {}

  //Returns an Observable to be able to use subscribe
  getPeople(): Observable<Pessoa[]> {
    return this.http.get<Pessoa[]>(`${this.baseUrl}/pessoas`);
  }

  getUpSpecificPerson(person: any): Observable<Pessoa> {
    return this.http.get<Pessoa>(`${this.baseUrl}/pessoas/${person}`);
  }

  postPerson(person: any): Observable<Pessoa> {
    return this.http.post<Pessoa>(`${this.baseUrl}/pessoas/create/`, person);
  }

  updatePerson(id: any, person: any): Observable<Pessoa> {
    return this.http.put<Pessoa>(
      `${this.baseUrl}/pessoas/update/${id}/`,
      person
    );
  }

  deletePerson(id: any): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/pessoas/delete/${id}`);
  }
}
