import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PriceFluctuationRequest } from '../models/price-fluctuation-request.model';
import { PriceFluctuationAnalysis } from '../models/price-fluctuation-analysis.model';

@Injectable({
  providedIn: 'root'
})
export class PriceFluctuationService {

  constructor(private http: HttpClient) { }

  post(request: PriceFluctuationRequest): any {
    return this.http.post<PriceFluctuationAnalysis>('https://localhost:7224/PriceFluctuations', request);
  }
}
