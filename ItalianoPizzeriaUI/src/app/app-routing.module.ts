import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddPizzaComponent } from './components/add-pizza/add-pizza.component';
import { PizzaDetailsComponent } from './components/pizza-details/pizza-details.component';
import { PizzaListComponent } from './components/pizza-list/pizza-list.component';

const routes: Routes = [
{ path: '', redirectTo: 'pizzas', pathMatch: 'full' },
{ path: 'pizzas' , component: PizzaListComponent},
{ path: 'pizzas/:id', component: PizzaDetailsComponent},
{path: 'add', component: AddPizzaComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
