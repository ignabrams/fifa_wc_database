import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class PlayerService {
  constructor(@Inject('BASE_URL') private baseUrl: string, private http: HttpClient) { }

  get = (id: number) => this.http.get<any>(`${this.baseUrl}team/player/${id}`);
}
