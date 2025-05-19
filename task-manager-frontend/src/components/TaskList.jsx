import { useEffect, useState } from "react";
import { fetchTasks, deleteTask } from "../api/taskApi";

export default function TaskList({ onEdit }) {
    const [tasks, setTasks] = useState([]);

    const loadTasks = async () => {
        try {
            const data = await fetchTasks();
            setTasks(data);
        } catch (err) {
            console.error(err.message);
        }
    };

    const handleDelete = async (id) => {
        await deleteTask(id);
        loadTasks();
    };

    useEffect(() => {
        loadTasks();
    }, []);

    return (
        <div>
            <h3>Your Tasks</h3>
            {tasks.length === 0 && <p>No tasks yet.</p>}
            <ul>
                {tasks.map((task) => (
                    <li key={task.id}>
                        <strong>{task.title}</strong> - {task.description}
                        <button onClick={() => onEdit(task)}>✏️</button>
                        <button onClick={() => handleDelete(task.id)}>🗑️</button>
                    </li>
                ))}
            </ul>
        </div>
    );
}
