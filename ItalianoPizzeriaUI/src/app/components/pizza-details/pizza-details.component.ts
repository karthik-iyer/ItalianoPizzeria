import { Component, OnInit } from '@angular/core';
import { PizzaService } from '../../services/pizza.service';
import { ActivatedRoute, Router } from '@angular/router';
import {DoughType} from '../../models/doughType';
import {DOUGHTYPEDATA} from '../../models/doughtType-data';
import { IngredientModel, PizzaModel } from 'src/app/api/model/models';
import { from, Observable } from 'rxjs';
import {tap} from 'rxjs/operators';
@Component({
  selector: 'app-pizza-details',
  templateUrl: './pizza-details.component.html',
  styleUrls: ['./pizza-details.component.css']
})
export class PizzaDetailsComponent implements OnInit {
  doughtypeList: DoughType[] = DOUGHTYPEDATA;
  currentPizza = null;
  message = '';
  radioSel:any;
  radioSelected:string;
  pizzaIngredientList = [];

  constructor(private pizzaService: PizzaService,
    private route: ActivatedRoute,
    private router: Router) {
      this.pizzaIngredientList = [];
    }

  ngOnInit(): void {
    this.message = '';
    this.retrieveIngredients();
    this.getPizza(this.route.snapshot.paramMap.get('id'));
  }

  getPizza(id): void {
    this.pizzaService.getPizzaById(id)
      .subscribe(
        data => {
          this.currentPizza = data;
          console.log(data);
        },
        error => {
          console.log(error);
        });
  }

  updatePizza(): void {
    this.pizzaService.update(this.currentPizza.pizzaId, this.currentPizza)
      .subscribe(
        response => {
          console.log(response);
          this.message = 'The pizza was updated successfully!';
        },
        error => {
          console.log(error);
        });
  }


  deletePizza(): void {
    this.pizzaService.delete(this.currentPizza.pizzaId)
      .subscribe(
        response => {
          console.log("response",response);
          this.router.navigate(['pizzas']);
        },
        error => {
          console.log("error",error);
        });
  }

  retrieveIngredients(): void {
    this.pizzaService.getAllIngredients()
      .subscribe(
        data => {
          let tmp = [];
          console.log("Ingredients data" + JSON.stringify(data));
          for(let dataItem of data)
          {
            tmp.push({
              "ingredientId" : dataItem["ingredientId"],
              "ingredientName": dataItem["name"]
            });
          }
          this.pizzaIngredientList = tmp;
        },
        error => {
          console.log(error);
        });
  }

  getSelectedItem(){
    this.radioSel = DOUGHTYPEDATA.find(DoughType => DoughType.value === this.radioSelected);
  }

  onItemChange(){
    this.getSelectedItem();
  }
}
