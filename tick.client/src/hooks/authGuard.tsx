import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { jwtDecode } from "jwt-decode";

//checks if user's token is still valid.
export default function useAuthGuard(redirectTo = "/login") {
    const navigate = useNavigate();

    useEffect(() => {
        const token = localStorage.getItem("token");

        if (!token) {
            navigate(redirectTo);
            return;
        }

        try {
            const decoded = jwtDecode(token);

            if (decoded.exp * 1000 < Date.now()) {
                console.log("token expired");
                localStorage.removeItem("token");
                navigate(redirectTo);
            }

        } catch (error) {
            console.error("Invalid token:", error);
            localStorage.removeItem("token");
            navigate(redirectTo);
        }
    }, [navigate, redirectTo]);
}
