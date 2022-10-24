import { Container } from "inversify";
import { HttpService, IHttpService } from "./services/http/http.service";
import { ILandmarksService, LandmarksService } from "./services/landmarks/landmarks.service";

export const container = new Container();
container.bind<IHttpService>("http-service").to(HttpService);
container.bind<ILandmarksService>("landmarks-service").to(LandmarksService);