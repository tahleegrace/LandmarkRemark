import { Status, Wrapper } from "@googlemaps/react-wrapper";
import { isNil } from "lodash";
import { useEffect, useRef, useState } from "react";
import config from "../../config";
import useDeepCompareEffectForMaps from "../../helpers/google-maps-helpers";
import { CreateLandmarkRequest } from "../../interfaces/landmarks/create-landmark-request";
import { LandmarksService } from "../../services/landmarks/landmarks.service";

const render = (status: Status) => {
    return <h1>{status}</h1>;
};

function LandmarkMap() {
    const landmarksService = new LandmarksService(); // TODO: Ideally this should be injected using a DI framework.

    // Create the initial zoom level and center state.
    const [zoom, setZoom] = useState(10);

    const [center, setCenter] = useState<google.maps.LatLngLiteral>({
        lat: 0,
        lng: 0,
    });

    const [currentLocationIsSet, setCurrentLocationIsSet] = useState(false);

    // Get the current location and show that on the map.
    if (!currentLocationIsSet) {
        navigator.geolocation.getCurrentPosition((position) => {
            setCurrentLocationIsSet(true);
            setCenter({ lat: position.coords.latitude, lng: position.coords.longitude });
        });
    };

    const Map: React.FC<MapProps> = ({
        onClick,
        style,
        ...options
    }) => {
        // Set up the map.
        const ref = useRef<HTMLDivElement>(null);
        const [map, setMap] = useState<google.maps.Map>();

        // Creates the map.
        useEffect(() => {
            if (ref.current && !map) {
                setMap(new window.google.maps.Map(ref.current, {}));
            }
        }, [ref, map]);

        // Set the map options.
        useDeepCompareEffectForMaps(() => {
            if (map) {
                map.setOptions(options);
            }
        }, [map, options]);

        // Add event handlers.
        useEffect(() => {
            if (map) {
                ["click", "idle"].forEach((eventName) =>
                    google.maps.event.clearListeners(map, eventName)
                );

                if (onClick) {
                    map.addListener("click", onClick);
                }
            }
        }, [map, onClick]);

        return <div ref={ref} style={style} />
    };

    const mapClicked = async (e: google.maps.MapMouseEvent) => {
        if (!isNil(e.latLng?.lat()) && !isNil(e.latLng?.lng())) {
            const request: CreateLandmarkRequest = {
                notes: 'This is a test', // TODO: Allow this to be input by the end user.
                longitude: e.latLng?.lng() as number,
                latitude: e.latLng?.lat() as number,
                userId: 1 // TODO: Remvoe this once authentication is implemented.
            };

            await landmarksService.create(request);
        }
    };

    return (
        <div>
            <Wrapper apiKey={config.googleMaps.apiKey} render={render}>
                <Map style={{ width: "1000px", height: "1000px" }} center={center} zoom={zoom} onClick={mapClicked}></Map>
            </Wrapper>
        </div>
    );
}

interface MapProps extends google.maps.MapOptions {
    onClick?: (e: google.maps.MapMouseEvent) => void;
    style: { [key: string]: string };
}

export default LandmarkMap;