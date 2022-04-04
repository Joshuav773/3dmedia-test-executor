import { Injectable, TemplateRef } from '@angular/core';

@Injectable({ 
  providedIn: 'root' 
})
export class ToastService {
    /*
    for toaster go here https://ng-bootstrap.github.io/#/components/toast/examples
    */
  toasts: any[] = [];

  show(textOrTpl: string | TemplateRef<any>, options: any = {}) {
    this.toasts.push({ textOrTpl, ...options });
  }

  remove(toast: any) {
    this.toasts = this.toasts.filter(t => t !== toast);
  }

  clear() {
    this.toasts.splice(0, this.toasts.length);
  }

  showSuccessMsg(msg: string){
    this.show(msg, { classname: 'bg-success text-light'});
  }

  showErrorMessage(err: string){
    this.show(err, { classname: 'bg-danger text-light'});
  }

  showInfoMsg(msg: string){
    this.show(msg, { classname: 'bg-info text-light' });
  }
}