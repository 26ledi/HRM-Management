import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import "./Login.css";
import Cookies from "js-cookie";

const Login = () => {
    const [isExpanded, setIsExpanded] = useState(false);
    const navigate = useNavigate();
    const toggleLoginText = () => {
        setIsExpanded((prev) => !prev);
    };
    const handleSignUpClick = () => {
        navigate("/register");
    };

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const validateForm = () => {
        let isValid = true;

        if (!email) {
            alert("Email is required");
            isValid = false;
        } else if (!/\S+@\S+\.\S+/.test(email)) {
            alert("The email format is not valid");
            isValid = false;
        }

        if (!password) {
            alert("Password is required");
            isValid = false;
        } else if (password.length < 6) {
            alert("Your password has to contain at least 6 characters");
            isValid = false;
        }

        return isValid;
    };

    const handleLoginClick = async (e) => {
        e.preventDefault();

        if (!validateForm()) {
            return;
        }

        try {
            const response = await axios.post("https://localhost:7025/auth/login", {
                email: email,
                password: password,
            });

            console.log(response.data);
            const accessToken = response.data.accessToken;

            if (accessToken) {
                Cookies.set("accessToken", accessToken, { expires: 1, secure: true });
                alert("Login Successfully !");
                //navigate("/watch");
            } else {
                alert("Login Failed! Check your password");
            }
        } catch (error) {
            console.error("Login Failed :", error);
            const errorMessage =
                error.response?.data?.message || error.message || "An error occured.";
            alert("Login Failed : " + errorMessage);
        }
    };

    return (
        <div className="wrapper">
            <div className={`login-text ${isExpanded ? "expand" : ""}`}>
                <button className="cta" onClick={toggleLoginText}>
                    <i className={`fas ${isExpanded ? "fa-chevron-up" : "fa-chevron-down"} fa-1x`}></i> Click
                </button>
                <div className={`text ${isExpanded ? "show-hide" : ""}`}>
                    <a href="#">Login</a>
                    <hr />
                    <br />
                    <input
                        type="text"
                        placeholder="Email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                    />
                    <br />
                    <input
                        type="password"
                        placeholder="Password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                    />
                    <br />
                    <button className="login-btn" onClick={handleLoginClick}>
                        Log In
                    </button>
                    <button className="signup-btn" onClick={handleSignUpClick}>
                        Sign Up
                    </button>
                </div>
            </div>
            <div className="call-text">
                <h1>
                    Our System <span>management</span> :)
                </h1>
                <button>Welcome</button>
            </div>
        </div>
    );
};

export default Login;
