import { useState, useEffect } from "react";
import { createTask, updateTask } from "../api/taskApi";

export default function TaskForm({ editingTask, onSuccess }) {
    const [title, setTitle] = useState("");
    const [description, setDescription] = useState("");

    useEffect(() => {
        if (editingTask) {
            setTitle(editingTask.title);
            setDescription(editingTask.description);
        }
    }, [editingTask]);

    const handleSubmit = async (e) => {
        e.preventDefault();
        const taskData = { title, description };

        try {
            if (editingTask) {
                await updateTask(editingTask.id, taskData);
            } else {
                await createTask(taskData);
            }
            setTitle("");
            setDescription("");
            onSuccess();
        } catch (err) {
            console.error(err.message);
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <input
                type="text"
                placeholder="Title"
                value={title}
                onChange={(e) => setTitle(e.target.value)}
                required
            />
            <textarea
                placeholder="Description"
                value={description}
                onChange={(e) => setDescription(e.target.value)}
            />
            <button type="submit">{editingTask ? "Update" : "Add"} Task</button>
        </form>
    );
}
