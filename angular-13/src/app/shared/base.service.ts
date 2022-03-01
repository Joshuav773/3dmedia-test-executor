import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";


export class BaseService {

    constructor(private httpClient: HttpClient){

    }

    protected get(reqUrl: string) {
        return this.httpClient.get(reqUrl);
    }

    protected post(reqUrl: string, body: any) {
        return this.httpClient.post(reqUrl, body);
    }
}