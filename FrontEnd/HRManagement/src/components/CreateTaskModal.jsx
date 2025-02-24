import React, { useState, useEffect } from "react";
import axios from "axios";
import "./CreateTaskModal.css";

const CreateTaskModal = ({ isOpen, onClose, onCreate }) => {
  if (!isOpen) return null;

  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [deadline, setDeadline] = useState("");
  const [priority, setPriority] = useState("");
  const [createdBy, setCreatedBy] = useState("");
  const [attachmentUrl, setAttachmentUrl] = useState("");
  const [users, setUsers] = useState([]);
  const [assignedUser, setAssignedUser] = useState("");

  useEffect(() => {
    const fetchUsers = async () => {
      try {
        const response = await axios.get("https://localhost:7051/tasks/users");
        console.log("Fetched Users:", response.data);

        if (Array.isArray(response.data)) {
          setUsers(response.data);
        } else {
          console.error("Unexpected API response format:", response.data);
          setUsers([]);
        }
      } catch (error) {
        alert("Failed to load users");
        console.error("Failed to fetch users:", error);
      }
    };

    fetchUsers();
  }, []);

  const handleSubmit = async (e) => {
    e.preventDefault();
    const taskData = {
        title,
        description,
        deadline,
        priority,
        userEmail: assignedUser,
        attachmentUrl,
        createdBy,
        createdAt: new Date().toISOString(), 
    }
    
    onCreate(taskData);
    onClose();
  };

  return (
    <div className="modal-overlay">
      <div className="modal-content">
        <h2>Create New Task</h2>
        <form onSubmit={handleSubmit}>
          <div className="modal-input">
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
              required
            />
            <input
              type="text"
              placeholder="Attachment URL (http://...)"
              value={attachmentUrl}
              onChange={(e) => setAttachmentUrl(e.target.value)}
              required
            />
            <input
              type="date"
              value={deadline}
              onChange={(e) => setDeadline(e.target.value)}
              required
            />
          </div>

          {/* Creator Select */}
          <div className="dropdown">
            <select className="select-dropdown" value={createdBy} onChange={(e) => setCreatedBy(e.target.value)}>
                <option value="" disabled>Creator</option>
                <option value="HeadOfDepartment">Head Of Department</option>
                <option value="Assistant">Assistant</option>
            </select>

            {/* Assign To Select */}
            <select className="select-dropdown" value={assignedUser} onChange={(e) => setAssignedUser(e.target.value)}>
                <option value="" disabled>Assign To</option>
                {Array.isArray(users) &&
                users.map((user) => (
                    <option key={user.email} value={user.email}>
                    {user.email}
                    </option>
                ))}
            </select>

            {/* Priority Select */}
            <select className="select-dropdown" value={priority} onChange={(e) => setPriority(e.target.value)}>
                <option value="" disabled>Priority</option>
                <option value="Low">Low</option>
                <option value="Medium">Medium</option>
                <option value="High">High</option>
            </select>
          </div>
         
          <div className="button-container">
            <button className="create-task-btn" type="submit">Create Task</button>
            <button type="button" className="close-modal-btn" onClick={onClose}>Cancel</button>
          </div>
        </form>
      </div>
    </div>
  );
};
export default CreateTaskModal;
