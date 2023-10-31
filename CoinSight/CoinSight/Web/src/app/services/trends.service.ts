import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TrendsRequest } from '../models/trends-request.model';
import { Trend } from '../enums/trend';
import { ReccomendationsAnalysis } from '../models/reccomendations-analysis.model';
import { ReccomendationsRequest } from '../models/reccomendations-request.model';

@Injectable({
  providedIn: 'root'
})
export class TrendsService {

  constructor(private http: HttpClient) { }

  postTrends(request: TrendsRequest): any {
    return this.http.post<Trend>('https://localhost:7224/Trends', request);
  }

  postReccomendations(request: ReccomendationsRequest): any {
    return this.http.post<ReccomendationsAnalysis>('https://localhost:7224/Trends/Reccomendations', request);
  }
}
