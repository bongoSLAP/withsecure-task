import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MarketDominanceRequest } from '../models/market-dominance-request.model';

@Injectable({
  providedIn: 'root'
})
export class MarketDominanceService {

  constructor(private http: HttpClient) { }

  post(request: MarketDominanceRequest): any {
    return this.http.post<number>('https://localhost:7224/MarketDominance', request);
  }
}
