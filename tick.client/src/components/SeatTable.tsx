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
    if (!selectedEvent || !seats) {navigate("/events");}
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
