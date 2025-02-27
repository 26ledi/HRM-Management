import React from "react";
import { useNavigate, useLocation } from "react-router-dom";
import "./SideBar.css";

const SideBar = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const handleNavigation = (path) => {
    navigate(path);
  };

  return (
    <div className="sidebar">
      <a
        className={location.pathname === "/dashboard" ? "active" : ""}
        onClick={() => handleNavigation("/dashboard")}
      >
        <svg className="sidebar-icon" viewBox="0 0 24 24" fill="none">
          <path d="M3 3v18h18"></path>
          <path d="M9 17V9"></path>
          <path d="M14 17V5"></path>
          <path d="M19 17V12"></path>
        </svg>
        Dashboard
      </a>

      <a
        className={location.pathname === "/tasks" ? "active" : ""}
        onClick={() => handleNavigation("/tasks")}
      >
        <svg className="sidebar-icon" viewBox="0 0 24 24" fill="none">
          <path d="M5 3h14a2 2 0 0 1 2 2v14a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2z"></path>
          <path d="M9 9h6M9 13h6M9 17h6"></path>
          <path d="M7 7l2 2l4-4"></path>
        </svg>
        Tasks
      </a>

      <a
        className={location.pathname === "/employees" ? "active" : ""}
        onClick={() => handleNavigation("/employees")}
      >
        <svg className="sidebar-icon" viewBox="0 0 24 24" fill="none">
          <path d="M17 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2"></path>
          <circle cx="9" cy="7" r="4"></circle>
          <path d="M23 21v-2a4 4 0 0 0-3-3.87"></path>
          <path d="M16 3.13a4 4 0 0 1 0 7.75"></path>
        </svg>
        Employees
      </a>

      <a
        className={location.pathname === "/attendances" ? "active" : ""}
        onClick={() => handleNavigation("/attendances")}
      >
        <svg className="sidebar-icon" viewBox="0 0 24 24" fill="none">
          <path d="M5 12l5 5L20 7"></path>
          <circle cx="12" cy="12" r="10"></circle>
        </svg>
        Attendances
      </a>

      <a
        className={location.pathname === "/calendar" ? "active" : ""}
        onClick={() => handleNavigation("/calendar")}
      >
        <svg className="sidebar-icon" viewBox="0 0 24 24" fill="none">
          <rect x="3" y="4" width="18" height="18" rx="2" ry="2"></rect>
          <line x1="16" y1="2" x2="16" y2="6"></line>
          <line x1="8" y1="2" x2="8" y2="6"></line>
          <line x1="3" y1="10" x2="21" y2="10"></line>
        </svg>
        Calendar
      </a>

      <a
        className={location.pathname === "/leave" ? "active" : ""}
        onClick={() => handleNavigation("/leave")}
      >
        <svg className="sidebar-icon" viewBox="0 0 24 24" fill="none">
          <path d="M15 3h4a2 2 0 0 1 2 2v14a2 2 0 0 1-2 2h-4"></path>
          <path d="M10 17l-5-5l5-5"></path>
          <path d="M15 12H5"></path>
        </svg>
        Leave
      </a>
    </div>
  );
};

export default SideBar;
