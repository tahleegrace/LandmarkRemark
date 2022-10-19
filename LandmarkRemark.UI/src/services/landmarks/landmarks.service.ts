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
}