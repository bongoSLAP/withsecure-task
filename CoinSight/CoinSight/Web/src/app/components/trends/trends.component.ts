import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Trend } from 'src/app/enums/trend';
import { ReccomendationsAnalysis } from 'src/app/models/reccomendations-analysis.model';
import { ReccomendationsRequest } from 'src/app/models/reccomendations-request.model';
import { TrendsRequest } from 'src/app/models/trends-request.model';
import { TrendsService } from 'src/app/services/trends.service';

@Component({
  selector: 'app-trends',
  templateUrl: './trends.component.html',
  styleUrls: ['./trends.component.css']
})
export class TrendsComponent implements OnInit {
  trendsSub: Subscription = new Subscription();
  trendResult: Trend | string = '';
  trendCoinId: string = '';
  trendDays: number = 1;
  isTrendVisible: boolean = false;
  isTrendError: boolean = false;

  recSub: Subscription = new Subscription();
  recResult: any;
  recCoinId: string = '';
  recDays: number = 1;
  recVisible: boolean = false;
  recError: boolean = false;
  recTopN: number = 2;
  isRecVisible: boolean = false;
  isRecError: boolean = false;

  constructor(private trendsService: TrendsService) { }

  ngOnInit(): void {

  }

  getTrend(): void {
    if (this.trendCoinId == '') {
      this.trendResult = "Enter a valid coin ID."
      this.isTrendError = true;
      this.isTrendVisible = true;
      return;
    }

    let request = new TrendsRequest();
    request.coinId = this.trendCoinId.toLowerCase();
    request.days = this.trendDays;

    this.trendsSub = this.trendsService.postTrends(request).subscribe(
      (result: Trend) => {
        this.isTrendError = false;
        this.trendResult = result;
        this.isTrendVisible = true;
      },
      (error: any) => {
        this.trendResult = error.error;
        this.isTrendError = true;
        this.isTrendVisible = true;
      }
    );
  }
  
  getRecommendations(): void {
    if (this.recCoinId == '') {
      this.recResult = "Enter a valid coin ID."
      this.isRecError = true;
      this.isRecVisible = true;
      return;
    }

    let request = new ReccomendationsRequest();
    request.coinId = this.recCoinId;
    request.days = this.recDays;
    request.topN = this.recTopN;

    this.recSub = this.trendsService.postReccomendations(request).subscribe(
      (result: ReccomendationsAnalysis) => {
        this.isRecError = false;
        this.recResult = result.reccomendations;
        this.isRecVisible = true;
      },
      (error: any) => {
        this.recResult = error.error;
        this.isRecError = true;
        this.isRecVisible = true;
      }
    );
  }

  trendModelChange(): void {
    this.isTrendVisible = false;
  }

  recModelChange(): void {
    this.isRecVisible = false;
  }
  
  ngOnDestroy() {
    this.trendsSub.unsubscribe();
    this.recSub.unsubscribe();
  }
}
