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
  isAllSelected: boolean;

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

  onSelectAll($event: any){
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
    //send the selectecTest Back to Api for build processing
  }

  onStop($event: any): void {
    //this method if to stop the job
  }

  anySelected = (): boolean => {
    return this.tests.filter(test => test.selected).length > 0
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
