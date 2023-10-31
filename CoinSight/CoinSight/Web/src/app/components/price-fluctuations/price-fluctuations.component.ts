import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { PriceFluctuationAnalysis } from 'src/app/models/price-fluctuation-analysis.model';
import { PriceFluctuationRequest } from 'src/app/models/price-fluctuation-request.model';
import { PriceFluctuationService } from 'src/app/services/price-fluctuation.service';

@Component({
  selector: 'app-price-fluctuations',
  templateUrl: './price-fluctuations.component.html',
  styleUrls: ['./price-fluctuations.component.css']
})
export class PriceFluctuationsComponent implements OnInit {
  fluctSub: Subscription = new Subscription();
  result: any;
  coinId: string = '';
  days: number = 2;
  isResultVisible: boolean = false;
  isResultError: boolean = false;

  constructor(private fluctuationService: PriceFluctuationService) { }

  ngOnInit(): void {

  }

  getFluctuations(): void {
    if (this.coinId == '') {
      this.result = "Enter a valid coin ID."
      this.isResultError = true;
      this.isResultVisible = true;
      return;
    }

    let request = new PriceFluctuationRequest();
    request.coinId = this.coinId.toLowerCase();
    request.days = this.days;

    this.fluctSub = this.fluctuationService.post(request).subscribe(
      (result: PriceFluctuationAnalysis) => {
        this.isResultError = false;
        this.result = result;
        this.isResultVisible = true;
      },
      (error: any) => {
        this.result = error.error;
        this.isResultError = true;
        this.isResultVisible = true;
      }
    );
  }

  modelChange(): void {
    this.isResultVisible = false;
  }

  ngOnDestroy(): void {
    this.fluctSub.unsubscribe();
  }
}
