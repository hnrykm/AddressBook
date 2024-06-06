import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { Person } from './models';

@Injectable({
  providedIn: 'root',
})
export class PersonService {

  constructor(private readonly http: HttpClient) { }

  getAll(): Observable<Person[]> {
    return this.http.get<Person[]>(`${environment.baseUrl}/people`);
  }

  create(person: Person): Observable<Person> {
    return this.http.post<Person>(`${environment.baseUrl}/people`, person);
  }

  update(person: Person): Observable<Person> {
    return this.http.put<Person>(`${environment.baseUrl}/people/${person.id}`, person);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${environment.baseUrl}/people/${id}`);
  }
}
