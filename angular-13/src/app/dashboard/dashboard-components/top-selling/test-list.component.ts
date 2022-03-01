import { Component, OnInit } from '@angular/core';
import { DashboardService } from '../../dashboard.service';
import { Test } from './Test';

@Component({
  selector: 'app-test-list',
  templateUrl: './test-list.component.html'
})
export class TestsListComponent implements OnInit {

  tests: Test[] = [];

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
