import { injectable } from "inversify";
import { AuthTokenDTO } from "../../interfaces/authentication/auth-token-dto";
import { LoginDetailsDTO } from "../../interfaces/authentication/login-details-dto";
import { container } from "../../ioc";
import { IHttpService } from "../http/http.service";

export interface IAuthenticationService {
    login(emailAddress: string, password: string): Promise<AuthTokenDTO>;
}

@injectable()
export class AuthenticationService {
    private readonly httpService: IHttpService = container.get<IHttpService>('http-service');

    public async login(emailAddress: string, password: string): Promise<AuthTokenDTO> {
        const request: LoginDetailsDTO = {
            emailAddress: emailAddress,
            password: password
        };

        const result = await this.httpService.post<LoginDetailsDTO, AuthTokenDTO>("auth/token", request);
        sessionStorage.setItem("auth-token", JSON.stringify(result));

        return result;
    }
}