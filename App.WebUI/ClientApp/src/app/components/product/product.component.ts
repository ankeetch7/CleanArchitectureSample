
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ProductService } from 'src/app/services/web-api-clients';

     // imports (as you know goes to the beginning of the file).
     import {
      ConfirmBoxInitializer,
      DialogLayoutDisplay,
      DisappearanceAnimation,
      AppearanceAnimation
    } from '@costlydeveloper/ngx-awesome-popup';
@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  //ag grid 
  productColumnDefs : any;
  productData : any;
  domLayout: any;
  gridApi: any;
  gridColumnApi: any;
  quickSearchValue: any;

  //for popup modal
  addProductPopUpModal : boolean = false;
  updateProductPopUpModal : boolean = false;
  updateProductDetails : any;

  constructor(private _productService: ProductService,
              private _toastrService: ToastrService) {
                this.domLayout = "autoHeight";
               }

  ngOnInit(): void {
    this.loadProductDetails();
    this.productColumnDefs = [
      { headerName: "S.N", valueGetter: 'node.rowIndex+1', width: 40, resizable: true },
      { headerName: "Name", field: 'name', sortable: true, resizable: true, width: 100 },
      { headerName: "Description", field: 'description', sortable: true, resizable: true, width: 100 },
      { headerName: "Unit Price", field: 'unitPrice', sortable: true, resizable: true, width: 100 },
      { headerName: "Selling Unit Price", field: 'sellingUnitPrice', sortable: true, resizable: true, width: 100 },
      { headerName: "Quantity", field: 'quantity', sortable: true, resizable: true, width: 100},
      { headerName: "Product Status", field: 'productStatus', sortable: true, resizable: true, width: 100},
      { headerName: "Created By", field: 'createdBy', sortable: true, resizable: true, width: 100},
      { headerName: "Actions", field: 'action', cellRenderer: this.actions(),pinned: 'right', resizable: true, width: 100},
    ];

  }

  public actions() {
    return function(params :  any){
      return `<button type="button" data-action-type="Edit" class="btn ag-btn" 
              data-toggle="tooltip" data-placement="bottom" title = "Edit" >  Edit </button > &nbsp; &nbsp;
              <button type="button" data-action-type="Remove" class="btn ag-btn" 
              data-toggle="tooltip" data-placement="bottom" title = "Remove" >  Remove </button >`;
    }
       
  }

  loadProductDetails() : void {
    this._productService.getAllProducts().subscribe(productData =>{
      this.productData = productData;
    },err =>{
      this._toastrService.error('Something went wrong','Error');
    })
  }


  onGridReady(params : any){
    this.gridApi = params.api;
    this.gridColumnApi = params.columnApi;
  }

  onRowClicked(e : any){
    if (e.event.target) {
      let data = e.data;
      let actionType = e.event.target.getAttribute("data-action-type");

      switch (actionType) {
        case "Edit": {
          this.updateProductDetails =data;
          this.updateProductPopUpModal = true;
          break;
        }
        case "Remove":
          return this.onOpenDialog(data);
      }
    }
  }

  onFilterChanged(e : any){
    e.api.refreshCells();
  }

  onQuickFilterChanged() {
    this.gridApi.setQuickFilter(this.quickSearchValue);
    
  }

  onModelUpdated() {
    setTimeout(() => { this.gridColumnApi.autoSizeAllColumns() });
    
  }

  onAddNewProductPopUp(){
    this.addProductPopUpModal = true;
  }
  closePopUp(){
    this.addProductPopUpModal = false;
    this.updateProductPopUpModal = false;
  }
  callbackProduct(){
    this.closePopUp();
    this.loadProductDetails();
  }

  onOpenDialog(row: any){
    const newConfirmBox = new ConfirmBoxInitializer();

    newConfirmBox.setTitle('Warning!!!');
    newConfirmBox.setMessage('Are you sure you want to remove this product?');
    newConfirmBox.setButtonLabels('YES', 'NO');
    // Choose layout color type
    newConfirmBox.setConfig({
    layoutType: DialogLayoutDisplay.WARNING,// SUCCESS | INFO | NONE | DANGER | WARNING
    animationIn: AppearanceAnimation.BOUNCE_IN, // BOUNCE_IN | SWING | ZOOM_IN | ZOOM_IN_ROTATE | ELASTIC | JELLO | FADE_IN | SLIDE_IN_UP | SLIDE_IN_DOWN | SLIDE_IN_LEFT | SLIDE_IN_RIGHT | NONE
    animationOut: DisappearanceAnimation.BOUNCE_OUT, // BOUNCE_OUT | ZOOM_OUT | ZOOM_OUT_WIND | ZOOM_OUT_ROTATE | FLIP_OUT | SLIDE_OUT_UP | SLIDE_OUT_DOWN | SLIDE_OUT_LEFT | SLIDE_OUT_RIGHT | NONE
    });

    // Simply open the popup
    newConfirmBox.openConfirmBox$().subscribe(res =>{
      if(res.clickedButtonID ==='yes'){
        this._productService.deleteProduct(row.id).subscribe(res =>{
          this._toastrService.success('Product removed successfully.','Success!');
          this.loadProductDetails();
        },err=>{
          this._toastrService.error('Something went wrong! Please try again...','Error!');
        });
      }
    });
  }
}
