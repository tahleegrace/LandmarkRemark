import { Status, Wrapper } from "@googlemaps/react-wrapper";
import { isNil } from "lodash";
import { useEffect, useState } from "react";
import config from "../../config";
import Map from "../map/map";
import { CreateLandmarkRequest } from "../../interfaces/landmarks/create-landmark-request";
import { LandmarksService } from "../../services/landmarks/landmarks.service";
import { LandmarkDTO } from "../../interfaces/landmarks/landmark-dto";
import Marker from "../marker/marker";

const render = (status: Status) => {
    return <h1>{status}</h1>;
};

function LandmarkMap() {
    const landmarksService = new LandmarksService(); // TODO: Ideally this should be injected using a DI framework.

    // Create the initial zoom level and center state.
    const [zoom] = useState(10);

    const [center, setCenter] = useState<google.maps.LatLngLiteral>({
        lat: 0,
        lng: 0,
    });

    const [currentLocationIsSet, setCurrentLocationIsSet] = useState(false);

    // Set the initial landmarks.
    const [landmarks, setLandmarks] = useState<LandmarkDTO[]>([]);

    // Get the current location and show that on the map.
    if (!currentLocationIsSet) {
        navigator.geolocation.getCurrentPosition((position) => {
            setCurrentLocationIsSet(true);
            setCenter({ lat: position.coords.latitude, lng: position.coords.longitude });
        });
    };

    const mapClicked = async (e: google.maps.MapMouseEvent) => {
        if (!isNil(e.latLng?.lat()) && !isNil(e.latLng?.lng())) {
            var notes = prompt('Enter notes about this location');

            if (notes && notes.length > 0) {
                const request: CreateLandmarkRequest = {
                    notes: notes,
                    longitude: e.latLng?.lng() as number,
                    latitude: e.latLng?.lat() as number,
                    userId: 1 // TODO: Remove this once authentication is implemented.
                };

                await landmarksService.create(request);

                alert('Marker created');
            }
        }
    };

    const showMyLandmarks = () => {
        // TODO: Remove the user ID once authentication is implemented.
        landmarksService.findByUserId(1).then((myLandmarks) => {
            setLandmarks(myLandmarks);
        });
    };

    return (
        <div>
            <div>Click on the map to create a marker</div>
            <div>
                <a href="#" onClick={showMyLandmarks}>Show My Landmarks</a>
            </div>
            <Wrapper apiKey={config.googleMaps.apiKey} render={render}>
                <Map style={{ width: "1000px", height: "1000px" }} center={center} zoom={zoom} onClick={mapClicked}>
                    {landmarks.map((landmark, i) => (
                        <Marker key={i} position={{ lat: landmark.latitude, lng: landmark.longitude }} title={landmark.notes}></Marker>
                    ))}
                </Map>
            </Wrapper>
        </div>
    );
}

export default LandmarkMap;