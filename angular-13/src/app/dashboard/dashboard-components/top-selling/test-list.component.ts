import { Component, OnInit } from '@angular/core';
import { DashboardService } from '../../dashboard.service';
import { Test } from './Test';

@Component({
  selector: 'app-test-list',
  templateUrl: './test-list.component.html'
})
export class TestsListComponent implements OnInit {

  tests: Test[] = []; 
  selectedTests: any[] = [];

  constructor(private dashService: DashboardService) { 
  }

  ngOnInit(): void {
    this.load();
  }

  load(){
    this.getListOfTests();
  }

  getListOfTests(): void {
    this.dashService.getListOfTests().subscribe({
      next: (response) => this.tests = response,
    });
  }

  onRowSelected($event: any): void {
    if($event.target.checked){
      const selected = this.tests.find(test => test.Name === $event.target.value);
      this.selectedTests.push(selected)
      return;
    }

    if(!$event.target.checked){
      const toRemove = this.tests.findIndex(test => test.Name === $event.target.value);
      this.selectedTests.splice(toRemove, 1);
    }
  }

  onRun($event: any): void {
    //this method is to run the job
  }

  onStop($event: any): void {
    //this method if to stop the job
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
