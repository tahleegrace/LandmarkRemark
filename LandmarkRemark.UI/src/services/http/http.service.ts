import { injectable } from "inversify";
import config from "../../config";
import { HttpHeaders } from "../../interfaces/http/http-headers";

export interface IHttpService {
    post<BodyType, ReturnType>(url: string, body: BodyType): Promise<ReturnType>;
    get<ReturnType>(url: string): Promise<ReturnType>;
}

@injectable()
export class HttpService implements IHttpService {
    getHeaders(): HttpHeaders {
        const headers: HttpHeaders = {
            'Content-Type': 'application/json'
        };

        const authToken = sessionStorage.getItem("auth-token");

        if (authToken) {
            headers['Authorization'] = `Bearer ${JSON.parse(authToken).token}`;
        }

        return headers;
    }

    public async post<BodyType, ReturnType>(url: string, body: BodyType): Promise<ReturnType> {
        const response = await fetch(`${config.api.url}/${url}`, {
            method: 'POST',
            headers: this.getHeaders() as any,
            body: JSON.stringify(body)
        });

        return await response.json();
    }

    public async get<ReturnType>(url: string): Promise<ReturnType> {
        const response = await fetch(`${config.api.url}/${url}`, {
            method: 'GET',
            headers: this.getHeaders() as any,
        });

        return await response.json();
    }
}