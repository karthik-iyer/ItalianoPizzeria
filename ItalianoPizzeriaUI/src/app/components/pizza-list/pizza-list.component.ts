import { Component, OnInit } from '@angular/core';
import {PizzaService } from '../../services/pizza.service';

@Component({
  selector: 'app-pizza-list',
  templateUrl: './pizza-list.component.html',
  styleUrls: ['./pizza-list.component.css']
})
export class PizzaListComponent implements OnInit {
  pizzas: any;
  currentPizza = null;
  currentIndex = -1;

  constructor(private pizzaService: PizzaService) { }

  ngOnInit(): void {
   this.getAllPizzas();
  }

  getAllPizzas(): void{
    this.pizzaService.getAllPizza()
      .subscribe(
          data => {
            this.pizzas = data;
            console.log(data);
          },
          error =>{
            console.log(error);
          });
  }

  setActivePizza(pizza, index): void {
    this.currentPizza = pizza;
    this.currentIndex = index;
  }
}
