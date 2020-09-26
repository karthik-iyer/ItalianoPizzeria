import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

const basePizzaUrl = "https://localhost:5001/api/Pizza";
const baseIngredientUrl =  "https://localhost:5001/api/Ingredients";

@Injectable({
  providedIn: 'root'
})
export class PizzaService {

  constructor(private http: HttpClient) { }

  getAllPizza(): Observable<any> {
    return this.http.get(basePizzaUrl);
  }

  getPizzaById(id:number): Observable<any>{
    return this.http.get(`${basePizzaUrl}/${id}`);
  }

  getAllIngredients(): Observable<any> {
    return this.http.get(baseIngredientUrl);
  }

  create(data): Observable<any> {
    return this.http.post(basePizzaUrl, data);
  }

  update(id, data): Observable<any> {
    return this.http.put(`${basePizzaUrl}/${id}`, data);
  }

  delete(id): Observable<any> {
    return this.http.delete(`${basePizzaUrl}/${id}`);
  }
}
