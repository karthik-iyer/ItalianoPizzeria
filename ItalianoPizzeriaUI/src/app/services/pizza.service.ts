import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IngredientModel, PizzaModel } from '../api';

const basePizzaUrl = "https://localhost:5001/api/Pizza";
const baseIngredientUrl =  "https://localhost:5001/api/Ingredients";

@Injectable({
  providedIn: 'root'
})
export class PizzaService {

  constructor(private http: HttpClient) { }

  getAllPizza(): Observable<PizzaModel[]> {
    return this.http.get<Array<PizzaModel>>(basePizzaUrl);
  }

  getPizzaById(id:number): Observable<PizzaModel>{
    return this.http.get<PizzaModel>(`${basePizzaUrl}/${id}`);
  }

  getAllIngredients(): Observable<IngredientModel[]> {
    return this.http.get<Array<IngredientModel>>(baseIngredientUrl);
  }

  create(data): Observable<PizzaModel> {
    return this.http.post<PizzaModel>(basePizzaUrl, data);
  }

  update(id, data): Observable<PizzaModel> {
    return this.http.put<PizzaModel>(`${basePizzaUrl}/${id}`, data);
  }

  delete(id): Observable<Blob> {
    return this.http.delete<Blob>(`${basePizzaUrl}/${id}`);
  }
}
