import React, { useState, useEffect } from "react";
import "./CreateTaskModal.css";
import { ToastContainer, toast } from "react-toastify";
import axios from "axios";
import "react-toastify/dist/ReactToastify.css";

const TaskDetailModal = ({ isOpen, onClosed, taskId}) => {
  if (!isOpen) return null;

  const [taskLooked, setTaskLooked] = useState({});
  const [status, setStatus] = useState("");
  const[selectedStatus, setSelectedStatus] = useState("");

  useEffect(() => {
    const fetchTask = async () => {
      try {
        const response = await axios.get(`https://localhost:7051/tasks/task/${taskId}`);
        console.log("Fetched task:", response.data);

        setTaskLooked({
          id: response.data.id,
          title: response.data.title || "",
          description: response.data.description || "",
          deadline: response.data.deadline || "",
          createdBy: response.data.createdBy || "",
          priority: response.data.priority || "",
          attachmentUrl: response.data.attachmentUrl || "",
          userEmail: response.data.userEmail || "",
          taskEvaluation: response.data.taskEvaluation || "",
          status: response.data.status || "",
        });

        setStatus(response.data.status || "");
        setSelectedStatus(response.data.status || "");
      } catch (error) {
        toast.error("Failed to load the task");
        console.error("Failed to fetch the task:", error);
      }
    };

    if (taskId) {
      fetchTask();
    }
  }, [taskId]);

  const truncateText = (text, maxLength = 20) => {
    if (!text) return "";
    return text.length > maxLength ? text.substring(0, maxLength) + "..." : text;
  };

  const handleSubmit = async () => {
    try {
        await axios.put(`https://localhost:7051/tasks/status/${taskId}?status=${selectedStatus}`);
        toast.success("The status has been changed successfully");
        setTimeout(() => {
            window.location.reload();
          }, 1000);
    } catch (error) {
        toast.error("Failed to change status");
        console.error("Failed to update status:", error);
    }
};


  return (
    <div className="modal-overlay">
      <ToastContainer />
      <div className="modal-content">
        <h2>Details</h2>
        <form onSubmit={(e) => e.preventDefault()}>
          <div className="modal-input">
            <label className="label-text">Title</label>
            <input type="text" placeholder="Title" value={taskLooked.title} readOnly/>
            <label className="label-text">Description</label>
            <textarea placeholder="Description" value={taskLooked.description} readOnly />
            <label className="label-text">Evaluation:{taskLooked.taskEvaluation}</label>
            <label className="label-text">Created By:{taskLooked.createdBy}</label>
            <label className="label-text">Attachment URL: 
            {taskLooked.attachmentUrl ? (
            <a href={taskLooked.attachmentUrl} target="_blank" rel="noopener noreferrer" style={{ color: "blue", textDecoration: "underline" }}>
            {truncateText(taskLooked.attachmentUrl, 10)}
            </a>
            ) : (
            <span>No attachment</span>
            )}
            </label>
            <label className="label-text">Status</label>
            <select
              className="select-dropdown"
              value={selectedStatus}
              onChange={(e) => setSelectedStatus(e.target.value)}
            >
              <option value="" disabled>Status</option>
              <option value="Created">Created</option>
              <option value="InProgress">In Progress</option>
              <option value="Completed">Completed</option>
            </select>
          </div>

          <div className="button-container">
            <button type="button" className="create-task-btn" onClick={handleSubmit}>
              Submit
            </button>
            <button type="button" className="close-modal-btn" onClick={onClosed}>
              Cancel
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default TaskDetailModal;
