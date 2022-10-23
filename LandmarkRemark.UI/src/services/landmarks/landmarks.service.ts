import { CreateLandmarkRequest } from "../../interfaces/landmarks/create-landmark-request";
import { LandmarkDTO } from "../../interfaces/landmarks/landmark-dto";
import { HttpService } from "../http/http.service";

// TODO: This should be injected into React components using a DI framework.
export class LandmarksService {
    private httpService = new HttpService(); // TODO: Ideally this should be injected using a DI framework.

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