import React, { useEffect, useState } from "react";
import axios from "axios";
import SearchBar from "./SearchBar";
import "./Employee.css";
import updateImg from "/Joyce Ledi/HRM Management/HRM-Management/FrontEnd/HRManagement/src/assets/update.png";
import TaskDetailModal from "./TaskDetailModal";

const Tasks = () => {
  const [tasks, setTasks] = useState([]);
  const [searchTerm, setSearchTerm] = useState("");
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selectedTaskId, setSelectedTaskId] = useState(null);

  useEffect(() => {
    const fetchTasks = async () => {
      try {
        const response = await axios.get("https://localhost:7051/tasks");
        console.log("Fetched Tasks:", response.data);
        setTasks(Array.isArray(response.data) ? response.data : []);
      } catch (error) {
        alert("Failed to load Tasks");
        console.error("Failed to fetch Tasks:", error);
      }
    };
    fetchTasks();
  }, []);

  const openModal = (taskId) => {
    setSelectedTaskId(taskId);
    setIsModalOpen(true);
  };

  const closeModal = () => {
    setIsModalOpen(false);
    setSelectedTaskId(null);
  };

  const filteredTasks = tasks.filter((task) =>
    task.userEmail.toLowerCase().includes(searchTerm.toLowerCase())
  );

  const truncateText = (text, maxLength = 20) => {
    if (!text) return "";
    return text.length > maxLength ? text.substring(0, maxLength) + "..." : text;
  };

  return (
    <div>
      <h2 className="page-title">Task List</h2>

      <ul className="responsive-table">
        <SearchBar
          onSearch={setSearchTerm}
          placeholder="Search task related to an email..."
        />
        <li className="table-header">
          <div>Assign To</div>
          <div>Title</div>
          <div>Description</div>
          <div>Deadline</div>
          <div>Priority</div>
          <div>Status</div>
          <div>Actions</div>
        </li>
        {filteredTasks.map((task) => (
          <li className="table-row" key={task.id}>
            <div>{truncateText(task.userEmail, 15)}</div>
            <div>{truncateText(task.title, 15)}</div>
            <div>{truncateText(task.description, 15)}</div>
            <div>{truncateText(task.deadline, 10)}</div>
            <div>{task.priority}</div>
            <div>{task.status}</div>
            <div className="action-icon">
              <img
                src={updateImg}
                alt="Update"
                onClick={() => openModal(task.id)}
              />
            </div>
          </li>
        ))}
      </ul>

      {/* Task Detail Modal */}
      <TaskDetailModal
        isOpen={isModalOpen}
        onClosed={closeModal}
        taskId={selectedTaskId}
      />
    </div>
  );
};

export default Tasks;
