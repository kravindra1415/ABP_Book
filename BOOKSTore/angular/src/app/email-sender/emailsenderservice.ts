import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { iemail } from "./iemail";

@Injectable({
    providedIn: 'root'
  })
  export class EmailSenderService
  
   {
    private baseUrl="https://localhost:44352/Email"
    constructor(private http:HttpClient) { }

    
 SendEmail(data: iemail): Observable<any> 
 {
  return this.http.post(`${this.baseUrl}`, data)
}
  }