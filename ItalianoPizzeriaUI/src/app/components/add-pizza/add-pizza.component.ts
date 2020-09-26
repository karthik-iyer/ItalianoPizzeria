import { Component, OnInit } from '@angular/core';
import { PizzaService } from 'src/app/services/pizza.service';

import {DoughType} from '../../models/doughType';
import {DOUGHTYPEDATA} from '../../models/doughtType-data';

@Component({
  selector: 'app-add-pizza',
  templateUrl: './add-pizza.component.html',
  styleUrls: ['./add-pizza.component.css']
})

export class AddPizzaComponent implements OnInit {

  pizza ={
    pizzaName: "",
    doughType: "",
    isCalzone: false,
    pizzaIngredientsModel: []
  }

  pizzaIngredientList = [];
  doughtypeList: DoughType[] = DOUGHTYPEDATA;
  radioSel:any;
  radioSelected:string;
  submitted = false;
  isError = false;
  constructor(private pizzaService: PizzaService) {
      this.doughtypeList = DOUGHTYPEDATA;
      this.radioSelected = "New York Style"
      this.getSelectedItem();
      this.pizzaIngredientList = [];
   }

  ngOnInit(): void {
   this.retrieveIngredients();
  }

  getSelectedItem(){
    this.radioSel = DOUGHTYPEDATA.find(DoughType => DoughType.value === this.radioSelected);
  }

  onItemChange(){
    this.getSelectedItem();
  }

  savePizza(): void {
    const data = {
      pizzaName: this.pizza.pizzaName,
      doughType: this.pizza.doughType,
      isCalzone: this.pizza.isCalzone,
      pizzaIngredientsModel: this.pizza.pizzaIngredientsModel
    };

    this.pizzaService.create(data)
      .subscribe(
        response => {
          console.log(response);
          this.submitted = true;
          this.isError = false;
        },
        error => {
          this.isError = true;
          console.log(error + "Pizza cannot be created");
        });
  }

  newPizza(): void {
    this.isError = false;
    this.submitted = false;
    this.pizza = {
      pizzaName: "",
      doughType: "",
      isCalzone: false,
      pizzaIngredientsModel: []
    };
  }

  retrieveIngredients(): void {
    this.pizzaService.getAllIngredients()
      .subscribe(
        data => {
          this.pizzaIngredientList = data;
        },
        error => {
          console.log(error);
        });
  }
}
