import { Navigate, useLocation } from "react-router-dom"

// Adapted from https://stackoverflow.com/a/70357802/3713362
const AuthenticatedRoute = (props: { children: React.ReactNode }): JSX.Element => {
    const { children } = props;
    const location = useLocation();
    const authToken = sessionStorage.getItem("auth-token");

    return authToken ? (
        <>{children}</>
    ) : (
        <Navigate
            replace={true}
            to="/login"
            state={{ from: `${location.pathname}${location.search}` }}
        />
    )
}

export default AuthenticatedRoute;