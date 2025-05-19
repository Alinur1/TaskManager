import { useNavigate } from "react-router-dom";

export default function DashboardPage() {
    const user = JSON.parse(localStorage.getItem("user"));
    const navigate = useNavigate();

    const handleLogout = () => {
        localStorage.removeItem("token");
        localStorage.removeItem("user");
        navigate("/login");
    };

    return (
        <div style={{ padding: "2rem" }}>
            <h1>Welcome, {user?.username} 👋</h1>
            <p>You’re successfully logged in!</p>
            <button
                onClick={handleLogout}
                style={{
                    marginTop: "1rem",
                    padding: "10px 20px",
                    backgroundColor: "#cc0000",
                    color: "#fff",
                    border: "none",
                    borderRadius: "5px",
                    cursor: "pointer",
                }}
            >
                Logout
            </button>
        </div>
    );
}
