import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import { FormBuilder, Validators } from "@angular/forms";
import { ToastrService } from "ngx-toastr";
import { ProductService } from "src/app/services/web-api-clients";

@Component({
    selector: 'create-product',
    templateUrl: 'create-product.component.html'
})

export class CreateProductComponent implements OnInit{
@Output('callbackProduct') callback = new EventEmitter<object>();

    // for reactive form
    submitted : boolean = false;
    createProductForm : any;

    constructor(private _formBuilder : FormBuilder,
                private _productService: ProductService,
                private _toastrService: ToastrService){}

    ngOnInit(): void {
        this.createProductForm = this._formBuilder.group({
            name: ['',Validators.required],
            description: ['',Validators.required],
            unitPrice: ['',Validators.required],
            sellingUnitPrice: ['',Validators.required],
            quantity: ['',Validators.required],
            image: [''],
        });
    }

    get getFormControl(){
        return this.createProductForm.controls;
    }

    onSubmitCreateProduct(){
        this.submitted = true;
        if(this.createProductForm.invalid) return;

        this._productService.createProduct(this.createProductForm.value).subscribe(res =>{
            this._toastrService.success('Product created successfully.','Success!');
            this.callback.emit();
        },err =>{
            this._toastrService.error("Some thing went wrong.",'Error!');
        });
    }
}