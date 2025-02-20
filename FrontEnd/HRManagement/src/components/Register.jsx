import React, { useState} from "react";
import { useNavigate  } from "react-router-dom";
import './Register.css';
import axios from "axios";

const Register = () => {
    const navigate = useNavigate();
    const handleBackToMainPageClick = () => {
        navigate("/");
    };

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [username, setUsername] = useState("");
    const [name, setName] = useState("");

    const validateForm = () => {
        let isValid = true;
        let errors = [];
    
        if (!email) {
            errors.push("Email is required.");
            isValid = false;
        } else if (!/\S+@\S+\.\S+/.test(email)) {
            errors.push("The email format is not valid.");
            isValid = false;
        }
    
        if (!username) {
            errors.push("Username is required.");
            isValid = false;
        } else if (username.length < 3) {
            errors.push("Username must be at least 3 characters long.");
            isValid = false;
        }
    
        if (!name) {
            errors.push("Name is required.");
            isValid = false;
        } else if (name.length < 2) {
            errors.push("Name must be at least 2 characters long.");
            isValid = false;
        }
    
        if (!password) {
            errors.push("Password is required.");
            isValid = false;
        } else if (password.length < 6) {
            errors.push("Your password must contain at least 6 characters.");
            isValid = false;
        } else if (!/\d/.test(password)) {
            errors.push("Your password must contain at least one number.");
            isValid = false;
        } else if (!/[!@#$%^&*(),.?":{}|<>]/.test(password)) {
            errors.push("Your password must contain at least one special character.");
            isValid = false;
        }
    
        if (!confirmPassword) {
            errors.push("Please confirm your password.");
            isValid = false;
        } else if (confirmPassword !== password) {
            errors.push("Passwords do not match.");
            isValid = false;
        }
    
        if (errors.length > 0) {
            alert(errors.join("\n"));
        }
    
        return isValid;
    };
    
    const handleRegisterClick = async(e) => {
        e.preventDefault();

        if (!validateForm()) {
            return;
        }
        try
        {

            const response = await axios.post("https://localhost:7025/auth/register", {
                email: email,
                password: password,
                confirmPassword: confirmPassword,
                username: username,
                name: name
            });

            console.log(response.data);
            alert("User has been successfully registered!");
            navigate("/");
        }
        catch (error) {
            console.error("Registration failed:", error);
            const errorMessage = error.response?.data?.message || error.message || "An error occurred during Registration.";

            alert("Registration failed: " + errorMessage);
        }

    }

    return (
      
        <div className="register-container">
            <h1>Register</h1>
            <hr />
            <br />
            <input 
                type="text" 
                placeholder="Email" 
                value={email}
                onChange={e => setEmail(e.target.value)}
            />
            <br />
            <input 
                type="text" 
                placeholder="Username" 
                value={username}
                onChange={e => setUsername(e.target.value)}
            />
            <br />
            <input 
                type="text" 
                placeholder="Name"
                value={name}
                onChange={e => setName(e.target.value)}
             />
            <br />
            <input 
                type="password" 
                placeholder="Password"
                value={password}
                onChange={e => setPassword(e.target.value)} 
            />
            <br />
            <input 
                type="password" 
                placeholder="Confirm Password"
                value={confirmPassword}
                onChange={e => setConfirmPassword(e.target.value)} 
            />
            <br />
            <button className="login-btn"onClick={handleRegisterClick}>Sign Up</button>
            <button className="signup-btn" onClick={handleBackToMainPageClick}>Back</button>
        </div> 
    );
};

export default Register;
