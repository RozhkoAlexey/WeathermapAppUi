import { IWeatherMainModel } from "./weather-main.model";
import { IWeatherModel } from "./weather.model";

export interface IWeatherResponseModel {
    id: number;
    name: string;
    main: IWeatherMainModel;
    weather: IWeatherModel[];
}
