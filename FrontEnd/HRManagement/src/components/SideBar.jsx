import React from "react";
import "./SideBar.css";

const SideBar = () => {

    
  return (
    <div className="sidebar">
      <a className="active" href="#home">
      <svg class="sidebar-icon" viewBox="0 0 24 24" fill="none">
        <path d="M3 3v18h18"></path>
        <path d="M9 17V9"></path>
        <path d="M14 17V5"></path>
        <path d="M19 17V12"></path>
      </svg>
        Dashboard
      </a>
      <a href="#task">
        <svg className="sidebar-icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
            <path d="M5 3h14a2 2 0 0 1 2 2v14a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2z" fill="none" />
            <path d="M9 9h6M9 13h6M9 17h6" stroke="currentColor" />
            <path d="M7 7l2 2l4-4" stroke="currentColor" />
        </svg>
        Tasks
      </a>

      <a href="#employees">
      <svg class="sidebar-icon" viewBox="0 0 24 24" fill="none">
          <path d="M17 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2"></path>
          <circle cx="9" cy="7" r="4"></circle>
          <path d="M23 21v-2a4 4 0 0 0-3-3.87"></path>
          <path d="M16 3.13a4 4 0 0 1 0 7.75"></path>
        </svg>
        Employees
      </a>
      <a href="#attendance">
      <svg class="sidebar-icon" viewBox="0 0 24 24" fill="none">
        <path d="M5 12l5 5L20 7"></path>
        <circle cx="12" cy="12" r="10"></circle>
      </svg>
        Attendances
      </a>
      <a href="#calendar">
      <svg class="sidebar-icon" viewBox="0 0 24 24" fill="none">
          <rect x="3" y="4" width="18" height="18" rx="2" ry="2"></rect>
          <line x1="16" y1="2" x2="16" y2="6"></line>
          <line x1="8" y1="2" x2="8" y2="6"></line>
          <line x1="3" y1="10" x2="21" y2="10"></line>
        </svg>
        Calendar
      </a>
      <a href="#leave">
      <svg class="sidebar-icon" viewBox="0 0 24 24" fill="none">
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
