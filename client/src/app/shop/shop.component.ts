import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IProduct } from '../shared/models/product';
import { IType } from '../shared/models/productType';
import { ShopParams } from '../shared/models/shopParams';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  @ViewChild('search',{static: true}) searchTerm : ElementRef;
  shopParams = new ShopParams();
  products : IProduct[];
  brands : IBrand[];
  productTypes : IType[];
  totalCount? = 0;
  totalPageSize : number;
  sortOptions = [
    {name: 'Alphabetical',value: 'name'},
    {name: 'Price: Low to High', value: 'priceAsc'},
    {name: 'Price: High to Low', value: 'priceDsc'},
  ];

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getProductTypes();
  }

  getProducts(){
    this.shopService.getProducts(this.shopParams).subscribe(response => {
      this.products = response?.data;
      this.shopParams.pageSize = response?.pageSize;
      this.shopParams.pageNumber = response?.pageIndex;
      this.totalCount = response?.count;
      this.totalPageSize = Math.min((this.shopParams.pageNumber * this.shopParams.pageSize),this.totalCount);

    }, error => {
      console.log(error);
    });
  }

  getBrands(){
    this.shopService.getBrands().subscribe(response => {
      this.brands = [{id:0, name:"All"},...response];
    }, error => {
      console.log(error);
    });
  }

  getProductTypes(){
    this.shopService.getProductTypes().subscribe(response => {
      this.productTypes = [{id:0, name:"All"},...response];
    }, error => {
      console.log(error);
    });
  }

  onBrandSelected(brandId: number){
    this.shopParams.brandIdSelected = brandId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onTypeSelected(typeId: number){
    this.shopParams.typeIdSelected = typeId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onSortSelected(sort: string){
    this.shopParams.sortSelected = sort;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onPageChanged(event: any){
    if(this.shopParams.pageNumber !== event){
      this.shopParams.pageNumber = event;
      this.getProducts();
    }
  }

  onSearch(){
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onReset(){
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProducts();
  }

}
