import React from "react";
import "../styles/Payment.css";
import { useMemo } from "react";
import { useNavigate, useOutletContext } from "react-router-dom";
import { useEffect } from "react";

const Payment = () => {
    const {
        event,
        seatDetails,
        onPay,
        onBack
    } = useOutletContext();

   
    const navigate = useNavigate();
    if (!event || !seatDetails) {
       
        navigate("/events");
    }


    const price = useMemo(() => Math.floor(Math.random() * 41) + 10, []);

    return (
        <div className="event-container">
            <h2 className="event-title">Event: {event?.name}</h2>

            <div className="seat-section">
                <h3 className="seat-heading">Seat Details:</h3>
                <ul className="seat-list">
                    <li>
                        Row: {seatDetails?.row}, Seat: {seatDetails?.seatNumber}
                    </li>
                </ul>
            </div>

            <div className="price">Price: ${price}</div>

            <div className="button-group">
                <button className="pay-button" onClick={onPay}>Pay</button>
                <button className="back-button" onClick={onBack}>Back</button>
            </div>
        </div>
    );
};

export default Payment;
