import "../styles/Home.css";
import React, { useEffect, useRef, useState } from "react";
import { Outlet, useNavigate } from "react-router-dom";
import useAuthGuard from "../hooks/authGuard";

function Home() {
    const navigate = useNavigate();

    const [events, setEvents] = useState([]);//holds event list
    const [seats, setSeats] = useState([]);//holds seat list
    const [loading, setLoading] = useState(true);//useless. played loading animation before
    const [loggedIn, setLoggedIn] = useState(false);
    const [username, setUsername] = useState("");//to use with loginform
    const [password, setPassword] = useState("");//to use with loginform
    const [loginError, setLoginError] = useState("");//for displaying msg

    const [authMode, setAuthMode] = useState("login"); // "login" or "register"
    const [selectedEvent, setSelectedEvent] = useState(null);
    const [selectedSeat, setSelectedSeat] = useState(null);
    
    const selectedEventRef = useRef(null);
    const selectedSeatRef = useRef(null);

    useEffect(() => {
        const token = localStorage.getItem("token");

        if (token) {
            setLoggedIn(true);
            getEvents(token);
        } else {
            setLoading(false);
        }
    }, []);

    async function onPay()
    {
        
        
        try {
            setLoading(true);
            const token = localStorage.getItem("token");
            const dto = {
                IdEvent: selectedEventRef?.current?.id,
                IdSeat: selectedSeatRef?.current?.seatId,
                OwnerName: "123",   // no need for this but the base object wants one
            };

            const res = await fetch("http://localhost:7026/Login/createTicket", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${token}`,
                },
                body: JSON.stringify(dto),
            });

            if (res.ok) {
                const data = await res.json();
                console.log("Ticket created successfully:", data);
                navigate("/seats");

            } else {
                const errorText = await res.text();
                console.error("Failed to create ticket:", res.status, errorText);
            }



        } catch (error) {
            console.error("Fetch error:", error);
        } finally {
            setLoading(false);
        }
    }

    async function onBack()
    {
        //setShowPayment(false);
        getSeats(selectedEventRef.current);
        navigate("/seats");
    }

    async function getEvents(token) {
        try {
            const res = await fetch("http://localhost:7026/Events/getEvents", {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });

            if (!res.ok) {
                throw new Error("Unauthorized or bad response");
            }

            const data = await res.json();
            setEvents(data);
        } catch (error) {
            console.error("Fetch error:", error);
        } finally {
            setLoading(false);
        }
    }

    async function logOut()
    {
        setLoading(true);
        localStorage.removeItem("token");
      
        navigate("/login");
        setLoading(false);
    }

    async function getSeats(event) {
        try {
            setLoading(true);
            const res = await fetch(`http://localhost:7026/Events/getEventSeats?eventId=${event.id}`);

            if (!res.ok) {
                throw new Error("Bad response from server");
            }

            const data = await res.json();
            console.log("event:" ,event);
            setSeats(data);
            setSelectedEvent(event);
            selectedEventRef.current = event;
            navigate("/seats");
        } catch (error) {
            console.error("Fetch error:", error);
        } finally {
            setLoading(false);
        }
    }

    async function reserveSeat(seat)
    {
        setSelectedSeat(seat);
        selectedSeatRef.current = seat;
        //if (seat.seatStatus != "available") { console.log("Seat unavaible:", seat.seatStatus); return; }
        try {
            setLoading(true);
            const token = localStorage.getItem("token");
            console.log("selected event:", selectedEventRef?.current + " and seat:", seat );
            const dto = {
                IdEvent: selectedEventRef?.current?.id,     
                IdSeat: seat.seatId,      
                OwnerName: "123",   // no need for this but the base object wants one
            };

            console.log("Token being sent:", token);
            const res = await fetch("http://localhost:7026/Login/reserveSeat", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${token}`,
                },
                body: JSON.stringify(dto),
            });

            if (res.ok) {
                navigate("/payment");
            } else {
                const errorText = await res.text();
                console.error("Failed to reserve seat:", res.status, errorText);
            }

            
        
        } catch (error) {
            console.error("Fetch error:", error);
        } finally {
            setLoading(false);
        }
    }

    function backToEvents() {
        setSelectedEvent(null);
        navigate("/events");
    }

    async function handleAuth(e) {
        e.preventDefault();
        setLoginError("");

        const endpoint = authMode === "login"
            ? "http://localhost:7026/Login/login"
            : "http://localhost:7026/Login/register";

        try {
            const res = await fetch(endpoint, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({ username, password }),
            });

            if (!res.ok) {
                throw new Error("Invalid credentials or registration failed");
            }

            const data = authMode === "login" ? await res.json() : null;

            if (authMode === "login") {
                localStorage.setItem("token", data.token);
                setLoggedIn(true);
                setLoading(true);
                getEvents(data.token);
                navigate("/events");    

            } else {
                setLoginError("Registration successful!");
                setUsername("");
                setPassword("");
            }

        } catch (error) {
            console.error(`${authMode} error:`, error);
            setLoginError(
                authMode === "login"
                    ? "Login failed. Please check your username and password."
                    : "Registration failed. Username might be taken."
            );
        }
    }
    useAuthGuard();
    return (
        <Outlet
            context={{
                loading,
                loggedIn,
                username,
                password,
                setUsername,
                setPassword,
                authMode,
                setAuthMode,
                loginError,
                handleAuth,
                events,
                getSeats,
                selectedEvent,
                seats,
                reserveSeat,
                selectedSeat,
                onPay,
                onBack,
                backToEvents,
                logOut,
            }}
        />
    );

};



export default Home;
