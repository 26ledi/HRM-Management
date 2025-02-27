import React from "react";
import { createRoot } from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import Dashboard from "./components/Dashboard";
import SideBar from "./components/SideBar";
import Tasks from "./components/Tasks.jsx";
import Employees from "./components/Employees";
import Attendances from "./components/Attendances";
import Calendar from "./components/Calendar";
import "./main.css";

const router = createBrowserRouter([
  {
    path: "/dashboard",
    element: (
      <div className="app-container">
        <SideBar />
        <div className="content">
          <Dashboard />
        </div>
      </div>
    ),
  },
  {
    path: "/tasks",
    element: (
      <div className="app-container">
        <SideBar />
        <div className="content">
          <Tasks />
        </div>
      </div>
    ),
  },
  {
    path: "/employees",
    element: (
      <div className="app-container">
        <SideBar />
        <div className="content">
          <Employees />
        </div>
      </div>
    ),
  },
  {
    path: "/attendances",
    element: (
      <div className="app-container">
        <SideBar />
        <div className="content">
          <Attendances />
        </div>
      </div>
    ),
  },
  {
    path: "/calendar",
    element: (
      <div className="app-container">
        <SideBar />
        <div className="content">
          <Calendar />
        </div>
      </div>
    ),
  },
]);


createRoot(document.getElementById("root")).render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);
