import config from "../../config";
import { HttpHeaders } from "../../interfaces/http/http-headers";

// TODO: This should be injected into React components using a DI framework.
export class HttpService {
    getHeaders(): HttpHeaders {
        return {
            'Content-Type': 'application/json'
        }
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