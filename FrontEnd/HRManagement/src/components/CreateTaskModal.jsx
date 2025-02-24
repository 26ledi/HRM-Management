import React, { useState } from "react";
import "./CreateTaskModal.css";

const CreateTaskModal = ({ isOpen, onClose, onCreate }) => {
  if (!isOpen) return null;

  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [deadline, setDeadline] = useState("");
  const [priority, setPriority] = useState("");
  const [createdBy, setCreatedBy] = useState("");
  const [attachmentUrl, setAttachmentUrl] = useState("");
  const [userEmail, setUserEmail] = useState([]);


  const handleSubmit = (e) => {
    e.preventDefault();
    onCreate({ title, description, deadline, priority, userEmail, attachmentUrl, createdBy});
    onClose();
  };

  return (
    <div className="modal-overlay">
      <div className="modal-content">
        <h2>Create New Task</h2>
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
            required
          />
          <input
            type="text"
            placeholder="Attachment url...'http//:'"
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
          <select className = "select-dropdown" value={createdBy} onChange={(e) => setCreatedBy(e.target.value)}>
          <option value="" disabled>Creator</option>
            <option value="HeadOfDepartment">HeadOfDepartment</option>
            <option value="Assistant">Assistant</option>
          </select>
          <select className = "select-dropdown" value={userEmail} onChange={(e) => setUserEmail(e.target.value)}>
          <option value="" disabled>Assign To</option>
            <option value="joyce">joyce</option>
            <option value="king">king</option>
          </select>
          <select className = "select-dropdown" value={priority} onChange={(e) => setPriority(e.target.value)}>
          <option value="" disabled>Priority</option>
            <option value="Low">Low</option>
            <option value="Medium">Medium</option>
            <option value="High">High</option>
          </select>
          <div className="button-container">
            <button className= "create-task-btn" type="submit">Create Task</button>
            <button type="button" className="close-modal-btn" onClick={onClose}>Cancel</button>
          </div>   
        </form>
      </div>
    </div>
  );
};

export default CreateTaskModal;
