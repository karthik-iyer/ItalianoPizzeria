export * from './ingredients.service';
import { IngredientsService } from './ingredients.service';
export * from './pizza.service';
import { PizzaService } from './pizza.service';
export const APIS = [IngredientsService, PizzaService];
