import { data } from 'autoprefixer';
import React from 'react'
import { Form, NavLink } from 'react-bootstrap';
import { Navigate, useNavigate } from 'react-router-dom';
import useLocalStorage from "../hooks/useLocalStorage";

export const Login = () => {
    const cssStyle = `bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg
   focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700
   dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500
   dark:focus:border-blue-500`;

     const [formData, setFormData] = React.useState(
        {
          UserName: "",
          Password: "",
        }
      )
      
      function handleChange(event) {
        console.log(event)
        const { name, value, type, checked } = event.target
        setFormData(prevFormData => {
          return {
            ...prevFormData,
            [name]: type === "checkbox" ? checked : value
          }
        })
      }

      const [post, setPost] = React.useState(null);

      let Navigate = useNavigate();
      async function handleSubmit(event) {
        event.preventDefault()
        try {
            const LoginResponse = await fetch('https://localhost:7017/api/Authorization/login', {
            method: 'post',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(formData)
          })
          data = LoginResponse.json();
          setPost(data);
          fetch('https://localhost:7017/api/Authorization/login', {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json'
            },
            body: JSON.stringify(formData)
          }).then(res => {
            return res.json()
          })
          .then(data => localStorage.setItem("token", data.token))
          Navigate("/");
        } catch (error) {
          console.log("ERROR" + error)
        }
    }

    return (
        <div className='md:px-4 py-2.5 container w-800'>
          <form onSubmit={handleSubmit}>
          <label htmlFor="UserName" className="block mb-2 text-sm font-medium text-gray-900 dark:text-gray-300">Username</label>
          <input type="text" onChange={handleChange} value={formData.UserName} name='UserName' minLength="1" maxLength="20" id="UserName" className={cssStyle} required />
          <label htmlFor="Password" className="block mb-2 text-sm font-medium text-gray-900 dark:text-gray-300">Password</label>
          <input type="password" onChange={handleChange} value={formData.Password} name='Password' minLength="1" maxLength="20" id="Password" className={cssStyle} required />
            <p></p>
            <button type="submit" className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-right dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800">Submit</button>
          </form>
        </div>
      )

}