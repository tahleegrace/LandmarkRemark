import { injectable } from "inversify";
import { CreateLandmarkRequest } from "../../interfaces/landmarks/create-landmark-request";
import { LandmarkDTO } from "../../interfaces/landmarks/landmark-dto";
import { container } from "../../ioc";
import { IHttpService } from "../http/http.service";

export interface ILandmarksService {
    create(request: CreateLandmarkRequest): Promise<LandmarkDTO>;
    findByUserId(userId: number): Promise<LandmarkDTO[]>;
    findAll(): Promise<LandmarkDTO[]>;
    search(query: string): Promise<LandmarkDTO[]>;
}

@injectable()
export class LandmarksService {
    private readonly httpService: IHttpService = container.get<IHttpService>('http-service');

    public async create(request: CreateLandmarkRequest): Promise<LandmarkDTO> {
        const url = 'landmarks';

        return this.httpService.post<CreateLandmarkRequest, LandmarkDTO>(url, request);
    }

    public async findByUserId(userId: number): Promise<LandmarkDTO[]> {
        const url = `landmarks/my-landmarks?userId=${userId}`;

        return this.httpService.get<LandmarkDTO[]>(url);
    }

    public async findAll(): Promise<LandmarkDTO[]> {
        const url = 'landmarks';

        return this.httpService.get<LandmarkDTO[]>(url);
    }

    public async search(query: string): Promise<LandmarkDTO[]> {
        const url = `landmarks/search?query=${query}`;

        return this.httpService.get<LandmarkDTO[]>(url);
    }
}