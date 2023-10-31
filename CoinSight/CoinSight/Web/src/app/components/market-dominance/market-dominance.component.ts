import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { MarketDominanceRequest } from 'src/app/models/market-dominance-request.model';
import { MarketDominanceService } from 'src/app/services/market-dominance.service';

@Component({
  selector: 'app-market-dominance',
  templateUrl: './market-dominance.component.html',
  styleUrls: ['./market-dominance.component.css']
})
export class MarketDominanceComponent implements OnInit {
  domSub: Subscription = new Subscription();
  result: any;
  coinId: string = '';
  topN: number = 2;
  isResultVisible: boolean = false;
  isResultError: boolean = false;

  constructor(private dominanceService: MarketDominanceService) { }

  ngOnInit(): void {
  }

  getDominance(): void {
    if (this.coinId == '') {
      this.result = "Enter a valid coin ID."
      this.isResultError = true;
      this.isResultVisible = true;
      return;
    }

    let request = new MarketDominanceRequest();
    request.coinId = this.coinId;
    request.topN = this.topN;

    this.domSub = this.dominanceService.post(request).subscribe(
      (result: number) => {
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
    this.domSub.unsubscribe();
  }
}
