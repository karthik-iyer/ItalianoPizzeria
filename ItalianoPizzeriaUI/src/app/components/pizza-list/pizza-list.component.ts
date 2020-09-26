import { Component, OnInit } from '@angular/core';
import { PizzaModel } from 'src/app/api';
import {PizzaService } from '../../services/pizza.service';
import {Observable} from 'rxjs';

@Component({
  selector: 'app-pizza-list',
  templateUrl: './pizza-list.component.html',
  styleUrls: ['./pizza-list.component.css']
})
export class PizzaListComponent implements OnInit {
  pizzas: Observable<PizzaModel[]>;
  currentPizza: PizzaModel;
  currentIndex = -1;

  constructor(private pizzaService: PizzaService) { }

  ngOnInit(): void {
   this.getAllPizzas();
  }

  getAllPizzas(): void{
   this.pizzas = this.pizzaService.getAllPizza();
  }

  setActivePizza(pizza, index): void {
    this.currentPizza = pizza;
    this.currentIndex = index;
  }
}
