export interface CreateLandmarkRequest {
    longitude: number;
    latitude: number;
    notes: string;
    userId: number; // TODO: This parameter should be removed once authentication is implemented.
}