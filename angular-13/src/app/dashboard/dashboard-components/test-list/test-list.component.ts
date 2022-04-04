import { Component, OnInit } from '@angular/core';
import { ApiResponse } from '../../../shared/api-response';
import { ToastService } from '../../../shared/toasts/toasts.service';
import { DashboardService } from '../../dashboard.service';
import { Test } from './Test';

@Component({
  selector: 'app-test-list',
  templateUrl: './test-list.component.html'
})
export class TestsListComponent implements OnInit {

  tests: Test[] = []; 
  selectedTests: any[] = [];
  isAllSelected: boolean;
  isSendingRequest: boolean;

  constructor(private dashService: DashboardService, private toast: ToastService) { 
  }

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.getListOfTests();
  }

  getListOfTests(): void {
    this.dashService.getListOfTests().subscribe({
      next: (res: ApiResponse<Test[]>) => {
        if(res.error) {
          this.toast.showErrorMessage(res.errorMessage)
        }

        this.tests = res.data
      },
    });
  }

  onSelectAll($event: any): void {
    if($event.target.checked){
      this.tests.forEach(test => test.selected = true)
      this.isAllSelected = true;
      return;
    }

    this.tests.forEach(test => test.selected = false)
    this.isAllSelected = false;
  }

  onRowSelected($event: any): void {
    if($event.target.checked){
      let selected = this.tests.find(test => test.name === $event.target.value);
      selected!.selected = true;
      return;
    }

    if(!$event.target.checked){
      let toRemove = this.tests.find(test => test.name === $event.target.value);
      toRemove!.selected = false;
      return;
    }
  }

  onRun($event: any): void {
    const selectedTests = this.tests.filter(test => test.selected).map(test => test.name);
    console.log(selectedTests);

    this.dashService.ExecuteTests(selectedTests).subscribe({
      next: (res: ApiResponse<string>) => {
        if(res.error){
          this.toast.showErrorMessage(res.errorMessage)
        }

        this.toast.showSuccessMsg(`Success! \nthe build has been queued, go to this URL to see the build running: \n${res.data}`);
      }
    })
  }

  onStop($event: any): void {
    this.toast.showErrorMessage("this is an error alert!")
  }

  disableRun(): boolean {
    if (this.tests.filter(test => test.selected).length > 0){
      return false;
    }
    
    return true;
  }

  getStatusClass(testResult: number): string {
    switch(testResult){
      case 1:
        return "success";
      case 2:
        return "danger";
      case 3:
        return "warning";

      default: return ""
    }
  }
}
