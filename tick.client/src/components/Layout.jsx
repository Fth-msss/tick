// src/components/Layout.jsx
import React from "react";
import { Outlet } from "react-router-dom";

export default function Layout() {
    return (
        <div className="flex flex-col min-h-screen">
            <header
                className="bg-gray-100 p-4 border-b">Global Header
            </header>

            <main className="flex-1">
                <Outlet />
            </main>

            <footer className="bg-gray-200 p-4 border-t text-center">
                Global Footer
            </footer>
        </div>
    );
}
