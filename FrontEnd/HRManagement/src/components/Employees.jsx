import React, { useEffect, useState } from "react";
import axios from "axios";
import SearchBar from "./SearchBar";
import "./Employee.css";

const Employees = () => {
  const [employees, setEmployees] = useState([]);
  const [searchTerm, setSearchTerm] = useState("");

  useEffect (() => {
    const fetchEmployees = async () => {
        try {

            const response = await axios.get("https://localhost:7051/tasks/users");
            console.log("Fetched Employees:", response.data);
            setEmployees(Array.isArray(response.data) ? response.data : []);
        
        } catch (error) {
            alert("Failed to load Employees");
            console.error("Failed to fetch Employees:", error);
        }
    }

    fetchEmployees()
  }, []);

  const filteredEmployees = employees.filter((employee) =>
    employee.username.toLowerCase().includes(searchTerm.toLowerCase())
  );

  const truncateText = (text, maxLength = 20) => {
    if (!text) return "";
    return text.length > maxLength ? text.substring(0, maxLength) + "..." : text;
   };

return (
    <div>

      <h2 className="page-title">Employees List</h2>

      <ul className="responsive-table">
        <SearchBar
            onSearch={setSearchTerm}
            placeholder="Search user..."
        />
        <li className="table-header">
          <div>Email</div>
          <div>Username</div>
          <div>Status</div>
          <div>Assigned Task</div>
          <div>Completed Task</div>
        </li>
          {filteredEmployees.map((employee) => (
            <li className="table-row" key={employee.id}>
                <div>{truncateText(employee.email, 20)}</div>
                <div>{employee.username}</div>
                <div>Working</div>
                <div>{employee.assignedTaskNumber}</div>
                <div>{employee.completedMonthlyTaskNumber}</div>
           </li>

        ))}
      </ul>
    </div>
  );
};

export default Employees;
