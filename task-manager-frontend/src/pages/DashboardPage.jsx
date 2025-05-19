import { useState } from "react";
import { useNavigate } from "react-router-dom";
import TaskList from "../components/TaskList";
import TaskForm from "../components/TaskForm";
import "../assets/style.css";

export default function DashboardPage() {
    const [editingTask, setEditingTask] = useState(null);
    const [refresh, setRefresh] = useState(false);
    const navigate = useNavigate();

    const user = JSON.parse(localStorage.getItem("user"));

    const handleEdit = (task) => {
        setEditingTask(task);
    };

    const handleSuccess = () => {
        setEditingTask(null);
        setRefresh(!refresh);
    };

    const handleLogout = () => {
        localStorage.removeItem("token");
        localStorage.removeItem("user");
        navigate("/login");
    };

    return (
        <div className="dashboard">
            <div className="dashboard-header">
                <h2>Welcome, {user?.username} 👋</h2>
                <button className="logout-button" onClick={handleLogout}>
                    Logout
                </button>
            </div>

            <TaskForm editingTask={editingTask} onSuccess={handleSuccess} />
            <TaskList key={refresh} onEdit={handleEdit} />
        </div>
    );
}
