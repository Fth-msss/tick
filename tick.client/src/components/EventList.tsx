import { useOutletContext } from "react-router-dom";


const EventList = () => {
    const {

        events,
        getSeats,
        logOut
    } = useOutletContext();

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
