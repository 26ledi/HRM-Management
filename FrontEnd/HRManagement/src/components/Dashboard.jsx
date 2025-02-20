import React from "react";
import './Dashboard.css';

const Dashboard = () => {
  const getStatusClass = (status) => `status status-${status.toLowerCase()}`;

  return (
    <div>
      <div className="box-container">
        <div className="box box1">
          <div className="text">
            <h2 className="topic-heading">60.5k</h2>
            <h2 className="topic">Task Completed On Time</h2>
          </div>
        </div>

        <div className="box box2">
          <div className="text">
            <h2 className="topic-heading">150</h2>
            <h2 className="topic">Average Task Delay</h2>
          </div>
        </div>

        <div className="box box3">
          <div className="text">
            <h2 className="topic-heading">320</h2>
            <h2 className="topic">Task Completion Over Time</h2>
          </div>
        </div>
      </div>

      <div className="report-container">
        <div className="report-header">
          <h1 className="recent-Articles">Recent Articles</h1>
          <button className="view">View All</button>
        </div>

        <div className="report-body">
          <div className="report-topic-heading">
            <h3 className="t-op">Assigned To</h3>
            <h3 className="t-op">Tasks</h3>
            <h3 className="t-op">Deadlines</h3>
            <h3 className="t-op">Priorities</h3>
            <h3 className="t-op">Evaluations</h3>
            <h3 className="t-op">Status</h3>
            <h3 className="t-op">CreatedBy</h3>
          </div>

          <div className="items">
            {[
              { AssignedTo: "joyce@gmail.com", task: "Fix bugs", deadline: "20.12.2024", priority: "Low", evaluation: "8/10", status: "Created", createdBy: "John"},
              { AssignedTo: "joyce@gmail.com", task: "Fix bugs", deadline: "20.12.2024", priority: "Low", evaluation: "8/10", status: "Pending", createdBy: "John"},
              { AssignedTo: "joyce@gmail.com", task: "Fix bugs", deadline: "20.12.2024", priority: "Low", evaluation: "8/10", status: "Completed", createdBy: "John"},
              { AssignedTo: "joyce@gmail.com", task: "Fix bugs", deadline: "20.12.2024", priority: "Low", evaluation: "8/10", status: "InProgress", createdBy: "John"},
              { AssignedTo: "joyce@gmail.com", task: "Fix bugs", deadline: "20.12.2024", priority: "Low", evaluation: "8/10", status: "Verified", createdBy: "John"},
            ].map((datas, index) => (
              <div className="item1" key={index}>
                <h3 className="t-op-nextlvl">{datas.AssignedTo}</h3>
                <h3 className="t-op-nextlvl">{datas.task}</h3>
                <h3 className="t-op-nextlvl">{datas.deadline}</h3>
                <h3 className="t-op-nextlvl">{datas.priority}</h3>
                <h3 className="t-op-nextlvl">{datas.evaluation}</h3>
                <h3 className={getStatusClass(datas.status)}>{datas.status}</h3>
                <h3 className="t-op-nextlvl">{datas.createdBy}</h3>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
};

export default Dashboard;
