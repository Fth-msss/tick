import React from "react";
import { useNavigate, useOutletContext } from "react-router-dom";
import { useEffect } from "react";

const SeatTable = () => {
    const {
        selectedEvent,
        seats,
        reserveSeat,
        backToEvents
    } = useOutletContext();

    const navigate = useNavigate();

    useEffect(() => {
        const token = localStorage.getItem("token");
        if (!token || !seats || !selectedEvent || !reserveSeat) {
            navigate("/events");
        }
    }, [seats, selectedEvent, reserveSeat, navigate]);

    // Guard clause to prevent rendering if data is missing
    if (!selectedEvent || !seats) {
        return null; // or <div>Loading...</div>
    }

    return (
        <div className="table-container">
            <h2>Seats for: {selectedEvent.name}</h2>
            <button onClick={backToEvents}>Back to Events</button>
            <table>
                <thead>
                    <tr>
                        <th>Seat Number</th>
                        <th>Row</th>
                        <th>Status</th>
                    </tr>
                </thead>
                <tbody>
                    {seats.map((seat) => (
                        <tr key={seat.seatId} onClick={() => reserveSeat(seat)}>
                            <td>{seat.seatNumber}</td>
                            <td>{seat.row}</td>
                            <td>{seat.seatStatus || "Unknown"}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default SeatTable;
