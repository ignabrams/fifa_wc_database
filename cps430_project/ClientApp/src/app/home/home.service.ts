import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class HomeService {
  constructor(@Inject('BASE_URL') private baseUrl: string, private http: HttpClient) {

  }

  getAll(query?: null) {
    return this.http.get<any[]>(`${this.baseUrl}team?query=${query||''}`);
  }

  getTopPerformers() {
    return this.http.get<any[]>(this.baseUrl + 'team/top-performers');
  }

  getExclusiveLosers() {
    return this.http.get<any[]>(this.baseUrl + 'team/exclusive-losers');
  }

  getWinners() {
    return this.http.get<any[]>(this.baseUrl + 'team/winners');
  }
}
