import {IProduct} from "./products.service";

export class Product implements IProduct {
    productId: string = '';
    category: string = '';
    cost: number = 0.00;
    imageUri: string = '';
    name: string = '';
    price: number = 0.00;
    quantity: number = 0;
}
