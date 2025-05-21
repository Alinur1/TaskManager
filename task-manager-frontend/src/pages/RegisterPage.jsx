import { useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import "../assets/style.css";

export default function RegisterPage() {
    const [username, setUsername] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");

    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError("");

        try {
            const response = await fetch("http://localhost:5107/api/Auth/register", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ username, email, password }),
            });

            if (!response.ok) {
                const err = await response.json();
                throw new Error(err.message || "Registration failed");
            }

            alert("Registration successful! Please log in.");
            navigate("/login");
        } catch (err) {
            setError(err.message);
        }
    };

    return (
        <div className="form-container">
            <h2>Register</h2>
            {error && <p style={{ color: "red" }}>{error}</p>}
            <form onSubmit={handleSubmit}>
                <input
                    type="text"
                    placeholder="Username"
                    value={username}
                    onChange={(e) => setUsername(e.target.value)}
                    className="form-input"
                    required
                />
                <input
                    type="email"
                    placeholder="Email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    className="form-input"
                    required
                />
                <input
                    type="password"
                    placeholder="Password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    className="form-input"
                    required
                />
                <button type="submit" className="form-button">Register</button>
            </form>

            <p style={{ marginTop: "1rem" }}>
                Already have an account?{" "}
                <Link to="/login" style={{ color: "#007bff", textDecoration: "none" }}>
                    Login Here
                </Link>
            </p>
        </div>
    );
}
