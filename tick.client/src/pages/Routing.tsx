
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';

import Home from './Home';
import NotFound from './NotFound';

import EventList from "../components/EventList";
import SeatTable from "../components/SeatTable";
import Payment from "../components/Payment";
import LoginForm from "../components/LoginForm";

export default function Routing() {
    return (
        <BrowserRouter>
            <Routes>
                
                    <Route path="/" element={<Home />}>
                        <Route path="events" element={<EventList />} />
                        <Route path="payment" element={<Payment />} />
                        <Route path="seats" element={<SeatTable />} />
                        <Route path="login" element={<LoginForm />} />
                        <Route index element={<Navigate to="login" />} />
                    </Route>
                    <Route path="*" element={<NotFound />} />

            </Routes>
        </BrowserRouter>
    );
}