import { Status, Wrapper } from "@googlemaps/react-wrapper";
import { useEffect, useRef, useState } from "react";
import config from "../../config";
import useDeepCompareEffectForMaps from "../../helpers/google-maps-helpers";

const render = (status: Status) => {
    return <h1>{status}</h1>;
};

function LandmarkMap() {
    // Create the initial zoom level and center state.
    const [zoom, setZoom] = useState(10);

    const [center, setCenter] = useState<google.maps.LatLngLiteral>({
        lat: 0,
        lng: 0,
    });

    const [currentLocationIsSet, setCurrentLocationIsSet] = useState(false);

    // Get the current location.
    if (!currentLocationIsSet) {
        navigator.geolocation.getCurrentPosition((position) => {
            setCurrentLocationIsSet(true);
            setCenter({ lat: position.coords.latitude, lng: position.coords.longitude });
        });
    };

    const Map: React.FC<MapProps> = ({
        style,
        ...options
    }) => {
        // Set up the map.
        const ref = useRef<HTMLDivElement>(null);
        const [map, setMap] = useState<google.maps.Map>();

        useEffect(() => {
            // Creates the map.
            if (ref.current && !map) {
                setMap(new window.google.maps.Map(ref.current, {}));
            }
        }, [ref, map]);

        useDeepCompareEffectForMaps(() => {
            // Set the zoom level and center.
            if (map) {
                map.setOptions(options);
            }
        }, [map, options]);

        return <div ref={ref} style={style} />
    };

    return (
        <div>
            <Wrapper apiKey={config.googleMaps.apiKey} render={render}>
                <Map style={{ width: "1000px", height: "1000px" }} center={center} zoom={zoom}></Map>
            </Wrapper>
        </div>
    );
}

interface MapProps extends google.maps.MapOptions {
    style: { [key: string]: string };
}

export default LandmarkMap;