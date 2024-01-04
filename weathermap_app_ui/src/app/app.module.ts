import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import {MaterialExampleModule} from '../material.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {HttpClientModule} from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

import { WeatherComponent } from './components/weather/weather.component';
import { HistoryComponent } from './components/history/history.component';
import { FormatIconPipe } from './pipes/format-icon.pipe';
import { WeatherDescriptionPipe } from './pipes/weather-description.pipe';
import { PreloaderComponent } from './components/preloader/preloader.component';
import { ErrorComponent } from './components/error/error.component';

@NgModule({
  declarations: [
    AppComponent,
    WeatherComponent,
    HistoryComponent,
    FormatIconPipe,
    WeatherDescriptionPipe,
    PreloaderComponent,
    ErrorComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MaterialExampleModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
