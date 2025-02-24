import React, {useEffect, useState} from "react";
import axios from "axios";
import './Dashboard.css';
import SearchBar from "./SearchBar";
import CreateTaskModal from "./CreateTaskModal";
import binImg from "/Joyce Ledi/HRM Management/HRM-Management/FrontEnd/HRManagement/src/assets/bin.png";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

const Dashboard = () => {
    const [taskCompletedOnTime, setTaskCompletedOnTime] = useState("");
    const [averageTaskDelay, setAverageTaskDelay] = useState("");
    const [totalTasksAssigned, setTotalTaskAssigned] = useState("");
    const [taskData, setTaskData] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [users, setUsers] = useState([]);

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

        const fetchTasks = async () => {
                try {
                    const response = await axios.get("https://localhost:7051/tasks")
                    console.log(response.data);
                    setTaskData(response.data);
                }
                catch(error)
                {
                    toast.error('Failed to fetch tasks');
                    console.error('Failed to fetch tasks:', error);
                }
            };
        const fetchUsers = async () => {
            try {
                const response = await axios.get("https://localhost:7051/tasks/users");
                console.log("Fetched Users:", response.data);
                // Ensure response.data is an array
                setUsers(Array.isArray(response.data) ? response.data : []);
            } catch (error) {
                alert("Failed to load users");
                console.error("Failed to fetch users:", error);
            }
            };
        
        fetchReports();
        fetchTasks();
        fetchUsers();
    }, []); 

    const filteredTasks = taskData.filter((task) =>
        task.title.toLowerCase().includes(searchTerm.toLowerCase())
    );

    const handleCreateTask = async (taskData) => {
        try {
            const response = await axios.post("https://localhost:7051/tasks/task", taskData);
            setTaskData((prevTasks) => {
                const updatedTasks = [...prevTasks, response.data];
                updateReports(updatedTasks);

                return updatedTasks;
            })
            console.log("Task created successfully:", response.data);
            toast.success("Task created successfully")
            
        } catch (error) {
            console.error("Failed to create task:", error);
            toast.error("Failed to create task. Please try again.");
        }
    }
    
    const handleUpdateTaskField = async (taskId, field, value) => {
        try {
          let response;
      
          if (field === "status") {

            response = await axios.put(
              `https://localhost:7051/tasks/status/${taskId}?status=${encodeURIComponent(value)}`
            );
          } else if (field === "priority") {
      
            response = await axios.put(
              `https://localhost:7051/tasks/priority/${taskId}?priority=${encodeURIComponent(value)}`
            );
          } else if (field === "userEmail") {

            const selectedUser = users.find((user) => user.email === value);
            if (!selectedUser) {
              toast.error("User not found");
              return;
            }
            response = await axios.put(
              `https://localhost:7051/tasks/assignment/${taskId}?userEmail=${selectedUser.email}`
            );
          }
      
          // Update local state: if the API returns the updated task, you might replace the task.
          // Here, we update the specific field.
          setTaskData((prevTasks) =>
            prevTasks.map((task) =>
              task.id === taskId ? { ...task, [field]: value } : task
            )
          );
          toast.success(`The ${field.charAt(0).toUpperCase() + field.slice(1)} has been updated`);
        } catch (error) {
          console.error(`Failed to update ${field}:`, error);
          toast.error(`Failed to update ${field}`);
        }
      };
      
    const handleDeleteTask = async (taskId) => {
        try {
            if (!window.confirm("Are you sure that you want to delete it?")) {
                return; 
            }
            const response = await axios.delete(`https://localhost:7051/tasks/task/${taskId}`);
            console.log("Task deleted successfully:", response);
            setTaskData((prevTasks) => {
            const updatedTasks = prevTasks.filter((task) => task.id !== taskId);
            updateReports(updatedTasks);

            return updatedTasks;
        });

        } catch (error) {
        console.error("Failed to delete task:", error);
        }
    };

    const updateReports = (updatedTasks) => {
    const tasksCompletedOnTime = updatedTasks.filter(task => task.status === 'Completed' && new Date(task.deadline) <= new Date()).length;
    const totalTasksAssigned = updatedTasks.length;
  
    setTaskCompletedOnTime(tasksCompletedOnTime);
    setAverageTaskDelay(averageTaskDelay);
    setTotalTaskAssigned(totalTasksAssigned);
    };

    const truncateText = (text, maxLength = 20) => {
    if (!text) return "";
    return text.length > maxLength ? text.substring(0, maxLength) + "..." : text;
   };
  

  return (
    <div>
        <ToastContainer />
        <CreateTaskModal 
            isOpen={isModalOpen} 
            onClose={() => setIsModalOpen(false)}
            onCreate={handleCreateTask}  
        />
      <div className={`box-container ${isModalOpen ? "blur-background" : ""}`}>
        <div className="box box1">
          <div className="text">
            <h2 className="topic-heading">{taskCompletedOnTime ?? "0"}</h2>
            <h2 className="topic">Task Completed On Time</h2>
          </div>
        </div>

        <div className="box box2">
          <div className="text">
            <h2 className="topic-heading">{averageTaskDelay ?? "0"}</h2>
            <h2 className="topic">Average Task Delay</h2>
          </div>
        </div>

        <div className="box box3">
          <div className="text">
            <h2 className="topic-heading">{totalTasksAssigned ?? "0"}</h2>
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
            <h3 className="t-op">Actions</h3>
          </div>

          <div className="items">
          {filteredTasks.map(task => (
            <div className="item1" key={task.id}>
                {/* Dropdown for "Assign To" */}
                <select
                    className="table-dropdown"
                    value={task.userEmail}
                    onChange={(e) => handleUpdateTaskField(task.id, "userEmail", e.target.value)}
                    >
                    <option value="" disabled>Assign To</option>
                    {users.map((user) => (
                        <option key={user.email} value={user.email}>
                        {truncateText(user.email, 20)}
                        </option>
                    ))}
                </select>
                <h3 className="t-op-nextlvl">{truncateText(task.title, 20)}</h3>
                <h3 className="t-op-nextlvl">{truncateText(task.description, 30)}</h3>
                <h3 className="t-op-nextlvl">{new Date(task.deadline).toDateString()}</h3>
                <h3 className="t-op-nextlvl">{truncateText(task.attachmentUrl, 20)}</h3>
                 {/* Dropdown for "Priority" */}
                <select
                    className="table-dropdown"
                    value={task.priority}
                    onChange={(e) => handleUpdateTaskField(task.id, "priority", e.target.value)}
                    >
                    <option value="" disabled>Priority</option>
                    <option value="Low">Low</option>
                    <option value="Medium">Medium</option>
                    <option value="High">High</option>
                </select>
                <h3 className="t-op-nextlvl">{task.taskEvaluation}</h3>
                {/* Dropdown for "Status" */}
                <select
                    className="table-dropdown"
                    value={task.status}
                    onChange={(e) => handleUpdateTaskField(task.id, "status", e.target.value)}
                    >
                    <option value="" disabled>Status</option>
                    <option value="Created">Created</option>
                    <option value="Pending">Pending</option>
                    <option value="InProgress">InProgress</option>
                    <option value="Completed">Completed</option>
                    <option value="Verified">Verified</option>
                </select>
                <h3 className="t-op-nextlvl">{new Date(task.createdAt).toDateString()}</h3>
                <h3 className="t-op-nextlvl">{task.createdBy}</h3>
                <img 
                    src={binImg}
                    onClick={() => handleDeleteTask(task.id)} 
                    className="delete-icon" 
                />

            </div>
            ))}
           
          </div>
        </div>
      </div>
    </div>
  );
};
export default Dashboard;
