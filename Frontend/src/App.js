//main react components for Todo List UI

import { useEffect, useState } from "react";

const API_URL = "http://localhost:5269/api/Todos";

//styles used for layout and UI appearance
const styles = {
  page: {
    minHeight: "100vh",
    background: "linear-gradient(135deg, #eef2ff, #e0e7ff)",
    padding: "40px",
    fontFamily: "system-ui, -apple-system, BlinkMacSystemFont",
  },
  card: {
    Width: "100%",
    maxWidth: "720px",
    margin: "0 auto",
    background: "#ffffff",
    borderRadius: "16px",
    padding: "32px",
    boxShadow: "0 20px 40px rgba(0,0,0,0.08)",
  },
  title: {
    textAlign: "center",
    marginBottom: "24px",
    fontSize: "32px",
    fontWeight: "700",
    color: "#1f2937",
  },
  input: {
    width: "100%",
    padding: "12px 14px",
    borderRadius: "8px",
    border: "1px solid #d1d5db",
    marginBottom: "12px",
    fontSize: "16px",
  },
  button: {
    padding: "12px 18px",
    borderRadius: "8px",
    border: "none",
    background: "#4f46e5",
    color: "white",
    fontWeight: "600",
    cursor: "pointer",
    marginBottom: "20px",
  },
  todo: {
    display: "flex",
    alignItems: "center",
    justifyContent: "space-between",
    padding: "12px 16px",
    border: "1px solid #e5e7eb",
    borderRadius: "10px",
    marginBottom: "10px",
  },
  actions: {
    display: "flex",
    gap: "10px",
  },
  actionBtn: {
    border: "none",
    background: "transparent",
    cursor: "pointer",
    fontSize: "14px",
  },
  editBtn: {
    marginLeft: "10px",
    background: "#facc15",
    border: "none",
    padding: "5px 10px",
    cursor: "pointer"
  },
  saveBtn: {
    background: "#22c55e",
    border: "none",
    padding: "5px 10px",
    marginLeft: "5px",
    cursor: "pointer"
  },
  cancelBtn: {
    background: "#9ca3af",
    border: "none",
    padding: "5px 10px",
    marginLeft: "5px",
    cursor: "pointer"
  }
};

function App() {
  const [todos, setTodos] = useState([]); //stores list of todos retrieved from backend
  const [newTodoTitle, setNewTodoTitle] = useState(""); //store value of new todo input field
  const [editingId, setEditingId] = useState(null); //stores the id fo the todo currently being edited
  const [editTitle,setEditTitle] = useState(""); //stores the temporary edited title

  useEffect(() => {
    fetchTodos();
  }, []);

  //fetch all todos from the backend API and update the UI
  const fetchTodos = async () => {
    const res = await fetch(API_URL);
    const data = await res.json();
    setTodos(data);
  };

  //create new todo by sending POST request to API
  const createTodo = async () => {
    if (!newTodoTitle.trim()) return; //prevent creating empty todo

    await fetch(API_URL, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ title: newTodoTitle }),
    });

    setNewTodoTitle("");
    fetchTodos();
  };

  //delete a todo by ID
  const deleteTodo = async (id) => {
    await fetch(`${API_URL}/${id}`, { method: "DELETE" });
    fetchTodos();
  };

  //edit title of a todo item
  const updateTodo = async (id) => {
    if (!editTitle.trim()) return;

    await fetch(`${API_URL}/${id}`, {
      method: "PUT",
      headers: {"Content-Type":"application/json"},
      body: JSON.stringify({title:editTitle})
    });
    setEditingId(null);
    setEditTitle("");
    fetchTodos();
  }

  return (
    <div style={styles.page}>
      <div style={styles.card}>
        <h1 style={styles.title}>My Todo List</h1>

        <div style = {{display: "flex", gap: "12px", marginBottom: "20px"}}>
        <input
          style={{...styles.input, marginBottom:0}}
          placeholder="What needs to be done?"
          value={newTodoTitle}
          onChange={(e) => setNewTodoTitle(e.target.value)}
        />
        <button 
        type="button" 
        style={{...styles.button, whiteSpace: "nowrap", marginBottom: 0,}} 
        onClick={createTodo}>
          Add Todo
        </button>
        </div>

        {todos.map((todo) => (
          <div key={todo.id} style={styles.todo}>
            {editingId === todo.id ? (
      
          // EDIT MODE
          <>
          <input
            type="text"
            value={editTitle}
            onChange={(e) => setEditTitle(e.target.value)}
            style={styles.input}
          />
          
          <div style={styles.actions}>
            <button
              style={{ ...styles.actionBtn, color: "#16a34a" }}
              onClick={() => updateTodo(todo.id)}
            >
            Save
            </button>

            <button
              style={{ ...styles.actionBtn, color: "#6b7280" }}
              onClick={() => setEditingId(null)}
            >
            Cancel
            </button>
          </div>
          </>
          ) : (
        
          // VIEW MODE
          <>
          <span>{todo.title}</span>
            <div style={styles.actions}>
              <button
                style={{ ...styles.actionBtn, color: "#2563eb" }}
                onClick={() => {
                  setEditingId(todo.id);
                  setEditTitle(todo.title);
                }}
              >
              Edit
              </button>

              <button
                style={{ ...styles.actionBtn, color: "#dc2626" }}
                onClick={() => deleteTodo(todo.id)}
              >
                Delete
              </button>
            </div>
          </>
        )}
      </div>
    ))}

      </div>
    </div>
  );
}

export default App;
