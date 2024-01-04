import { IWeatherResponseModel } from "./weather-response.model";

export interface IHistoryQueryModel {
    id: string;
    createDt: string;
    weather: IWeatherResponseModel;
}
