// src/components/Layout.jsx
import React from "react";
import { Outlet } from "react-router-dom";

export default function Layout() {
    return (
        <div className="flex flex-col min-h-screen">

            <main className="flex-1">
                <Outlet />
            </main>

        </div>
    );
}
