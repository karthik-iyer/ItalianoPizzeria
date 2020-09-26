import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AddPizzaComponent } from './components/add-pizza/add-pizza.component';
import { PizzaDetailsComponent } from './components/pizza-details/pizza-details.component';
import { PizzaListComponent } from './components/pizza-list/pizza-list.component';

import {FormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {NgSelectModule} from '@ng-select/ng-select';
@NgModule({
  declarations: [
    AppComponent,
    AddPizzaComponent,
    PizzaDetailsComponent,
    PizzaListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    NgSelectModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
