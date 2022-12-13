import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import { FormBuilder, Validators } from "@angular/forms";
import { ToastrService } from "ngx-toastr";
import { ProductService } from "src/app/services/web-api-clients";

@Component({
    selector: 'update-product',
    templateUrl: './update-product.component.html'
})

export class UpdateProductComponent implements OnInit{
    @Input("update-product-details")  updateDetails: any;
    @Output("callbackProduct") callback = new EventEmitter<object>();
    // for reactive form
    submitted : boolean = false;
    updateProductForm : any;
    
    constructor(private _formBuilder : FormBuilder,
                private _productService: ProductService,
                private _toastrService: ToastrService) {}

    ngOnInit(): void {
        this.updateProductForm = this._formBuilder.group({
            id: [''],
            name: ['',Validators.required],
            description: ['',Validators.required],
            unitPrice: ['',Validators.required],
            sellingUnitPrice: ['',Validators.required],
            quantity: ['',Validators.required],
            image: [''],
        });

        this.updateProductForm.patchValue(this.updateDetails);
    }

    get getFormControl(){
        return this.updateProductForm.controls;
    }
    onSubmitUpdateProduct() {
        this.submitted = true;
        if(this.updateProductForm.invalid) return;

        this._productService.updateProduct(this.updateProductForm.value).subscribe(res => {
            this._toastrService.success("Product Updated Successfully.",'Success!');
            this.callback.emit();
        },err =>{
            this._toastrService.error("Something went wrong.",'Error!');
        });
    }

}