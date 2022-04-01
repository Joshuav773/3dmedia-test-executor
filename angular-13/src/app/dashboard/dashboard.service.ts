import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { BaseService } from "../shared/base.service";

@Injectable({
    providedIn: 'root'
})
export class DashboardService extends BaseService {

    requestUrl: string;

    constructor(httpClient: HttpClient){
        super(httpClient);
        this.requestUrl = 'https://localhost:7289/api/Jenkins'
    }

    getListOfTests(): Observable<any> {
        return super.get(`${this.requestUrl}/GetListOfTests`);
    }

    ExecuteTests(req: any): Observable<any> {
        return this.post(`${this.requestUrl}/ExecuteTests`, req);
    }
}