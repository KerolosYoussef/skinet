import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';
import {BreadcrumbService} from 'xng-breadcrumb'
import { BasketService } from 'src/app/basket/basket.service';
import { IBasketItem } from 'src/app/shared/models/basket';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  quantity: number;
  product : IProduct;


  constructor(
    private shopService : ShopService,
    private activatedRoute : ActivatedRoute,
    private bcService : BreadcrumbService,
    private basketService : BasketService
    ) { 
    this.bcService.set("@productDetails"," ");
    this.quantity = 1;
  }

  ngOnInit(): void {
    this.loadProduct();
  }

  
  loadProduct(){
    let productId = +this.activatedRoute.snapshot.paramMap.get('id');
    
    this.shopService.getProduct(productId).subscribe(product => {
      this.product = product;
      this.bcService.set("@productDetails",product.name);
    }, error => {
      console.log(error);
    })
  }

  addItemtoBasket(){
    this.basketService.addItemToBasket(this.product,this.quantity);
  }

  incrementItemQuantity(){
    this.quantity++;
  }

  decrementItemQuantity(){
    if(this.quantity > 1)
      this.quantity--;
  }

  private mapProductItemToBasketItem(item: IProduct, quantity: number): IBasketItem {
    return {
      id: item.id,
      productName: item.name,
      price: item.price,
      quantity,
      pictureUrl: item.pictureUrl,
      brand: item.productBrand,
      type: item.productType      
    };
  }

}
