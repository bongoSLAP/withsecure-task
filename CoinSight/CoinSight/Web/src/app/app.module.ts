import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { TrendsComponent } from './components/trends/trends.component';
import { PriceFluctuationsComponent } from './components/price-fluctuations/price-fluctuations.component';
import { MarketDominanceComponent } from './components/market-dominance/market-dominance.component';
import { HomeComponent } from './components/home/home.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    TrendsComponent,
    PriceFluctuationsComponent,
    MarketDominanceComponent,
    HomeComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent },
      { path: 'trends', component: TrendsComponent },
      { path: 'price-fluctuations', component: PriceFluctuationsComponent },
      { path: 'market-dominance', component: MarketDominanceComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
