import React, { useEffect } from "react";
import { useNavigate, useOutlet, useOutletContext } from "react-router-dom";
import useAuthGuard from "../hooks/authGuard";



const EventList = () => {
    const {

        events,
        getSeats,
        logOut
    } = useOutletContext();

    const navigate = useNavigate();

    
    

    return (
        <div className="table-container">
            <button onClick={logOut}>Logout</button>
            <h2>Events</h2>
            <table>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Event Start</th>
                        <th>State</th>
                    </tr>
                </thead>
                <tbody>
                    {events.map((event) => (
                        <tr key={event.id} onClick={() => getSeats(event)}>
                            <td>{event.name}</td>
                            <td>{event.eventStart}</td>
                            <td>{event.state}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default EventList;
