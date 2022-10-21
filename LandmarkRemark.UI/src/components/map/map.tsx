import React from "react";
import { useEffect, useRef, useState } from "react";
import useDeepCompareEffectForMaps from "../../helpers/google-maps-helpers";

interface MapProps extends google.maps.MapOptions {
    onClick?: (e: google.maps.MapMouseEvent) => void;
    children?: React.ReactNode;
    style: { [key: string]: string };
}

const Map: React.FC<MapProps> = ({
    onClick,
    children,
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

    return (
        <>
            <div ref={ref} style={style} />
            {React.Children.map(children, (child) => {
                if (React.isValidElement(child)) {
                    // set the map prop on the child component
                    // @ts-ignore
                    return React.cloneElement(child, { map });
                }
            })}
        </>
    );
};

export default Map;