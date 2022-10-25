import "reflect-metadata";
import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { BrowserRouter, Route, Routes } from "react-router-dom";
import LandmarkMap from "./components/landmark-map/landmark-map";
import AuthenticatedRoute from "./route-guards/authenticated-route";
import UnauthenticatedOnlyRoute from "./route-guards/unauthenticated-only-route";
import Login from "./components/login/login";

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
  <React.StrictMode>
    <BrowserRouter>
        <Routes>
            <Route path="/" element={<App />}>
                <Route path="" element={<AuthenticatedRoute><LandmarkMap /></AuthenticatedRoute>} />
                <Route path="login" element={<UnauthenticatedOnlyRoute><Login /></UnauthenticatedOnlyRoute>} />
            </Route>
        </Routes>
    </BrowserRouter>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
