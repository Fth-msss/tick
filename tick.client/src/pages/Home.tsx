import "../styles/Home.css";
import React, { useEffect, useState } from "react";

function Home() {
    const [events, setEvents] = useState([]);
    const [loading, setLoading] = useState(true);
  

    useEffect(() =>
    {
        getEvents();
    },[]
    )

    async function getEvents()
    {
        fetch("https://localhost:7026/Events/getEvents")
            .then((res) => {
                if (!res.ok) {
                    throw new Error("Network response was not ok");
                }
                return res.json();
            })
            .then((data) => {
                setEvents(data);
                setLoading(false);
            })
            .catch((error) => {
                console.error("Fetch error:", error);
                setLoading(false);
            });
    }

    return (
        <div>
            <h1>Event Table</h1>
            <div class="table-container">
               
                
            <table>
                <thead>
                    <tr>
                        <th>Event ID</th>
                        <th>Name</th>
                        <th>Event Start</th>
                        <th>State</th>
                        <th>Seat Layout ID</th>
                    </tr>
                </thead>
                <tbody>
                        {events.map((event) => (
                            <tr onClick={() => { console.log(event); }} key={event.id}>
                            <td>{event.id}</td>
                            <td>{event.name}</td>
                            <td>{event.eventStart}</td>
                            <td>{event.state}</td>
                            <td>{event.idPhysicalSeatLayout ?? "N/A"}</td>
                        </tr>
                    ))}
                </tbody>
                    </table>
            </div>
        </div>
    );
}

export default Home;
