const API_BASE = "http://localhost:5107/api/Task";

const getAuthHeaders = () => ({
    "Content-Type": "application/json",
    Authorization: `Bearer ${localStorage.getItem("token")}`,
});

export const fetchTasks = async () => {
    const response = await fetch(API_BASE, {
        headers: getAuthHeaders(),
    });
    if (!response.ok) throw new Error("Failed to fetch tasks");
    return await response.json();
};

export const createTask = async (task) => {
    const response = await fetch(API_BASE, {
        method: "POST",
        headers: getAuthHeaders(),
        body: JSON.stringify(task),
    });
    return await response.json();
};

export const updateTask = async (id, task) => {
    const response = await fetch(`${API_BASE}/${id}`, {
        method: "PUT",
        headers: getAuthHeaders(),
        body: JSON.stringify(task),
    });
    return await response.json();
};

export const deleteTask = async (id) => {
    const response = await fetch(`${API_BASE}/${id}`, {
        method: "DELETE",
        headers: getAuthHeaders(),
    });
    return await response.json();
};
