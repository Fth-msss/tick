import React, { useState } from "react";

export default function Ticket() {
    const [menuOpen, setMenuOpen] = useState(false);

    return (
        <div className="h-screen flex flex-col">
            {/* Header */}
            <header className="bg-gray-100 p-4 flex justify-between items-center border-b">
                <h1 className="text-xl font-bold">HEADER</h1>
                <button
                    onClick={() => setMenuOpen(!menuOpen)}
                    className="text-black md:hidden"
                >
                   {/* {menuOpen ? <X size={24} /> : <Menu size={24} />}*/}
                </button>
            </header>

            {/* Main Content */}
            <div className="flex flex-1">
                {/* Main Area */}
                <main className="flex-1 flex items-center justify-center text-xl">
                    website here
                </main>

                {/* Sidebar */}
                <aside
                    className={`bg-gray-900 text-white w-64 p-4 space-y-4 transition-transform duration-300 ${menuOpen ? "translate-x-0" : "translate-x-full"
                        } fixed top-0 right-0 h-full md:relative md:translate-x-0`}
                >
                    <div className="flex items-center justify-between mb-4">
                        <span>username</span>
                        <div className="w-6 h-6 bg-white rounded-full" />
                    </div>
                    <input
                        className="w-full p-1 text-black"
                        placeholder="email"
                        type="email"
                    />
                    <input
                        className="w-full p-1 text-black"
                        placeholder="password"
                        type="password"
                    />
                    <ul className="mt-4 space-y-2">
                        <li>list option 1</li>
                        <li>list option 2</li>
                        <li>etc etc...</li>
                    </ul>
                </aside>
            </div>
        </div>
    );
}
