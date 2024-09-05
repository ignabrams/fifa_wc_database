import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class TeamService {
  constructor(@Inject('BASE_URL') private baseUrl: string, private http: HttpClient) {

  }

  get(id: number) {
    return this.http.get<any>(`${this.baseUrl}team/${id}`);
  }

  getAllPlayers(id: number) {
    return this.http.get<any[]>(this.baseUrl + 'team/' + id + '/players');
  }
}
