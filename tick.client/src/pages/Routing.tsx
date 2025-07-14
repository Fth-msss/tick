import React from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';

import Home from './Home';
import About from './About';
import NotFound from './NotFound';
import Ticket from './Ticket';

import Layout from "../components/Layout";

export default function Routing() {
    return (
        <BrowserRouter>
            <Routes>
                <Route element={<Layout />}>
                    <Route path="/" element={<Home />} />
                    <Route path="/about" element={<About />} />
                    <Route path="*" element={<NotFound />} />
                    <Route path="/ticket" element={<Ticket />} />
                </Route>
            </Routes>
        </BrowserRouter>
    );
}