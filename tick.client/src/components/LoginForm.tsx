import { useOutletContext } from "react-router-dom";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";

const LoginForm = () => {
    const {
        username,
        password,
        setUsername,
        setPassword,
        handleAuth,
        setAuthMode,
        loginError,
    } = useOutletContext();

    const navigate = useNavigate();
    useEffect(() => {
     
        const token = localStorage.getItem("token");

        if (token) { navigate("/events"); }
    })

    return (
        <div className="login-form">
            <form onSubmit={handleAuth}>
                <div>
                    <label>Username:</label>
                    <input
                        type="text"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label>Password:</label>
                    <input
                        type="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />
                </div>
                <button type="submit" onClick={() => setAuthMode("login")}>Login</button>
                <button type="submit" onClick={() => setAuthMode("register")}>Register</button>
                {loginError && <p style={{ color: "red" }}>{loginError}</p>}
            </form>
        </div>
    );
};

export default LoginForm;
