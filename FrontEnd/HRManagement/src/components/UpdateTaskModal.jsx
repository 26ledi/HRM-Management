import React , { useState, useEffect }  from "react";
import "./CreateTaskModal.css";
import { ToastContainer, toast } from "react-toastify";
import axios from "axios";
import "react-toastify/dist/ReactToastify.css";

const UpdateTaskModal = ({isOpen, onClosed, onUpdated, taskId}) => {
  if (!isOpen) return null;

  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [deadline, setDeadline] = useState("");
  const [evaluation, setEvaluation] = useState("");
  const [createdBy, setCreatedBy] = useState("");
  const [attachmentUrl, setAttachmentUrl] = useState("");
  
  useEffect(() => {
    const fetchTask = async (taskId) => {
      try {
        const response = await axios.get(`https://localhost:7051/tasks/task/${taskId}`);
        console.log("Fetched task:", response.data);
        setTitle(response.data.title || "");
        setDescription(response.data.description || "");
        setEvaluation(response.data.taskEvaluation || "");
        setDeadline(response.data.deadline || "");
        setCreatedBy(response.data.createdBy || "");
        setAttachmentUrl(response.data.attachmentUrl || "");

    } catch (error) {
        toast.error("Failed to load the task");
        console.error("Failed to fetch the task:", error);
      }
    };

    if (taskId) {
      fetchTask(taskId);
    }
  }, [taskId]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    const updatedTask = {
      title,
      description,
      deadline,
      attachmentUrl,
      createdBy,
    };

    onUpdated(updatedTask);
    onClosed();
  };

return (
    <div className="modal-overlay">
      <ToastContainer/>
      <div className="modal-content">
        <h2>Update Task</h2>
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
            <select className="select-dropdown" 
                value={createdBy}
                onChange={(e) => setCreatedBy(e.target.value)}
                >
                <option value="" disabled>CreatedBy</option>
                <option value="HeadOfDepartment">HeadOfDepartment</option>
                <option value="Assistant">Assistant</option>
            </select>
          </div>
          <div className="button-container">
            <button className="create-task-btn" type="submit">Update</button>
            <button type="button" className="close-modal-btn" onClick={onClosed}>Cancel</button>
          </div>
        </form>
      </div>
    </div>
  );
};
export default UpdateTaskModal;