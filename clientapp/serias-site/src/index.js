import React, {Suspense} from 'react';
import ReactDOM from 'react-dom/client';
import './index.css'
import {BrowserRouter} from 'react-router-dom';
import App from "./app";


const root = ReactDOM.createRoot(document.getElementById('root'));

root.render(
    <BrowserRouter>
        <App/>
    </BrowserRouter>
);
