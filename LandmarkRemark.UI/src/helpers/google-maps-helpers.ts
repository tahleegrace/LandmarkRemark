// Functions for setting up Google Maps.
// SOURCE: https://stackblitz.com/github/googlemaps/js-samples/tree/sample-react-map?file=index.tsx
import { createCustomEqual } from 'fast-equals';
import { useEffect, useRef } from "react";

declare const isLatLngLiteral: (obj: any) => obj is google.maps.LatLngLiteral;

const createCustomEqualParam = (deepEqual: any) => (a: any, b: any) => {
    if (
        isLatLngLiteral(a) ||
        a instanceof google.maps.LatLng ||
        isLatLngLiteral(b) ||
        b instanceof google.maps.LatLng
    ) {
        return new google.maps.LatLng(a).equals(new google.maps.LatLng(b));
    }

    // TODO extend to other types

    // use fast-equals for other objects
    return deepEqual(a, b);
}

const deepCompareEqualsForMaps = createCustomEqual(
    createCustomEqualParam as any
);

function useDeepCompareMemoize(value: any) {
    const ref = useRef();

    if (!deepCompareEqualsForMaps(value, ref.current)) {
        ref.current = value;
    }

    return ref.current;
}

function useDeepCompareEffectForMaps(
    callback: React.EffectCallback,
    dependencies: any[]
) {
    useEffect(callback, dependencies.map(useDeepCompareMemoize));
}

export default useDeepCompareEffectForMaps;