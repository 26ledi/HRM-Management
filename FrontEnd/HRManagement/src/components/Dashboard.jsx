import React, {useEffect, useState} from "react";
import axios from "axios";
import './Dashboard.css';
import SearchBar from "./SearchBar";
import CreateTaskModal from "./CreateTaskModal";

const Dashboard = () => {
  const [taskCompletedOnTime, setTaskCompletedOnTime] = useState("");
  const [averageTaskDelay, setAverageTaskDelay] = useState("");
  const [totalTasksAssigned, setTotalTaskAssigned] = useState("");
  const [taskData, setTaskData] = useState([]);
  const [searchTerm, setSearchTerm] = useState("");
  const [isModalOpen, setIsModalOpen] = useState(false);


  useEffect(() => {
    const fetchReports = async () => {
      try {
        const response = await axios.get("https://localhost:7051/tasks/task-reports");
        setTaskCompletedOnTime(response.data.tasksCompletedOnTime);
        setAverageTaskDelay(response.data.averageTaskDelay);
        setTotalTaskAssigned(response.data.totalTasksAssigned);
      }
       catch (error) {
        alert('Failed to fetch reports');
        console.error('Failed to fetch reports:', error);
      }
    };

    const fetchTasks = async () => 
        {
            try
            {
                const response = await axios.get("https://localhost:7051/tasks")
                console.log(response.data);
                setTaskData(response.data);
            }
            catch(error)
            {
                alert('Failed to fetch tasks');
                console.error('Failed to fetch tasks:', error);
            }
        };
        
    fetchReports();
    fetchTasks();
  }, []); 

  const filteredTasks = taskData.filter((task) =>
    task.title.toLowerCase().includes(searchTerm.toLowerCase())
  );

  return (
    <div>
        <CreateTaskModal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)} />
      <div className={`box-container ${isModalOpen ? "blur-background" : ""}`}>
        <div className="box box1">
          <div className="text">
            <h2 className="topic-heading">{taskCompletedOnTime || "Loading..."}</h2>
            <h2 className="topic">Task Completed On Time</h2>
          </div>
        </div>

        <div className="box box2">
          <div className="text">
            <h2 className="topic-heading">{averageTaskDelay || "Loading..."}</h2>
            <h2 className="topic">Average Task Delay</h2>
          </div>
        </div>

        <div className="box box3">
          <div className="text">
            <h2 className="topic-heading">{totalTasksAssigned || "Loading..."}</h2>
            <h2 className="topic">Total Tasks Assigned</h2>
          </div>
        </div>
      </div>
      <div className={`report-container ${isModalOpen ? "blur-background" : ""}`}>
        <div className="report-header">
          <h1 className="recent-Tasks">Recent Tasks</h1>
          <button className="view" onClick={() => setIsModalOpen(true)}>Create Task</button>
        </div>
        <SearchBar
            onSearch={setSearchTerm}
            placeholder="Search task..."
        />
        <div className="report-body">
          <div className="report-topic-heading">
            <h3 className="t-op">Assigned To</h3>
            <h3 className="t-op">Tasks</h3>
            <h3 className="t-op">Description</h3>
            <h3 className="t-op">Deadlines</h3>
            <h3 className="t-op">AttachmentUrl</h3>
            <h3 className="t-op">Priorities</h3>
            <h3 className="t-op">Evaluations</h3>
            <h3 className="t-op">Status</h3>
            <h3 className="t-op">CreatedAt</h3>
            <h3 className="t-op">CreatedBy</h3>
          </div>

          <div className="items">
          {filteredTasks.map(task => (
            <div className="item1" key={task.id}>
                <h3 className="t-op-nextlvl">{task.userEmail}</h3>
                <h3 className="t-op-nextlvl">{task.title}</h3>
                <h3 className="t-op-nextlvl">{task.description}</h3>
                <h3 className="t-op-nextlvl">{new Date(task.deadline).toDateString()}</h3>
                <h3 className="t-op-nextlvl">{task.attachmentUrl}</h3>
                <h3 className="t-op-nextlvl">{task.priority}</h3>
                <h3 className="t-op-nextlvl">{task.taskEvaluation}</h3>
                <h3 className={`status status-${task.status.toLowerCase()}`}>{task.status}</h3>
                <h3 className="t-op-nextlvl">{new Date(task.createdAt).toDateString()}</h3>
                <h3 className="t-op-nextlvl">{task.createdBy}</h3>
            </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
};

export default Dashboard;
