import React, { useState } from "react";
import './admin.css';


function Admin(){
    // Initial state for users
  const [users, setUsers] = useState([
    { id: 1, name: "John Doe", email: "john@example.com" },
    { id: 2, name: "Jane Doe", email: "jane@example.com" },
  ]);

  // States for form inputs
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [editing, setEditing] = useState(false);
  const [currentUser, setCurrentUser] = useState(null);

  // Create (Add) a user
  const addUser = (e) => {
    e.preventDefault();
    const newUser = { id: users.length + 1, name, email };
    setUsers([...users, newUser]);
    setName("");
    setEmail("");
  };

  // Delete a user
  const deleteUser = (id) => {
    const filteredUsers = users.filter((user) => user.id !== id);
    setUsers(filteredUsers);
  };

  // Edit (Update) a user
  const editUser = (user) => {
    setEditing(true);
    setCurrentUser(user);
    setName(user.name);
    setEmail(user.email);
  };

  // Update the user after editing
  const updateUser = (e) => {
    e.preventDefault();
    setUsers(
      users.map((user) =>
        user.id === currentUser.id ? { ...user, name, email } : user
      )
    );
    setEditing(false);
    setCurrentUser(null);
    setName("");
    setEmail("");
  };

  return (
    <div className="App">
      <h1>Mantenimiento</h1>
      
      {/* Form to Add / Edit users */}
      <form onSubmit={editing ? updateUser : addUser}>
        <h2>{editing ? "Edit User" : "Add User"}</h2>
        <input
          type="text"
          placeholder="Name"
          value={name}
          onChange={(e) => setName(e.target.value)}
          required
        />
        <input
          type="email"
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          required
        />
        <button type="submit">{editing ? "Update" : "Add"} User</button>
      </form>
      
      <h2>Luchadores</h2>
      <ul>
        {users.map((user) => (
          <li key={user.id}>
            {user.name} - {user.email}
            <button onClick={() => editUser(user)}>Edit</button>
            <button onClick={() => deleteUser(user.id)}>Delete</button>
          </li>
        ))}
      </ul>
    </div>
  );
};


export default Admin