import { Container } from "inversify";
import { AuthenticationService, IAuthenticationService } from "./services/authentication/authentication.service";
import { HttpService, IHttpService } from "./services/http/http.service";
import { ILandmarksService, LandmarksService } from "./services/landmarks/landmarks.service";

export const container = new Container();
container.bind<IHttpService>("http-service").to(HttpService);
container.bind<IAuthenticationService>("authentication-service").to(AuthenticationService);
container.bind<ILandmarksService>("landmarks-service").to(LandmarksService);