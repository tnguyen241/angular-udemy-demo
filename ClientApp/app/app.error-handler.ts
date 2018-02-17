import { ErrorHandler, Inject, NgZone } from '@angular/core';
import { ToastyService, ToastyConfig, ToastOptions, ToastData } from "ng2-toasty";
export class AppErrorHandler implements ErrorHandler {

    constructor(@Inject(NgZone) private ngZone: NgZone,
        @Inject(ToastyService) private toastyService: ToastyService, @Inject(ToastyConfig) private toastyConfig: ToastyConfig) {
        this.toastyConfig.theme = 'bootstrap';
    }
    handleError(error: any): void {
        this.ngZone.run(() => {
            console.log(error);
            // this.toastyService.info(toastOptions);
            //this.toastyService.success(toastOptions);
            //this.toastyService.wait(toastOptions);           
            //this.toastyService.warning(toastOptions);
            // this.toastyService.error({
            //    title: "Error",
            //    msg: "Unexpected error!!!",
            //    showClose: true,
            //    timeout: 5000,
            //    theme: 'bootstrap',
            //    onAdd: (toast: ToastData) => {
            //        console.log('Toast ' + toast.id + ' has been added!');
            //    },
            //    onRemove: function (toast: ToastData) {
            //        console.log('Toast ' + toast.id + ' has been removed!');
            //    }
            // });
        });        
    } 
}