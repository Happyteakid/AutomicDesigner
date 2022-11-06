import { data } from 'autoprefixer';
import React from 'react'
import { Form, NavLink } from 'react-bootstrap';
import { Navigate, useNavigate } from 'react-router-dom';

export const Register = () => {
    const cssStyle = `bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg
   focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700
   dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500
   dark:focus:border-blue-500`;

     const [formData, setFormData] = React.useState(
        {
          FirstName: "",
          LastName: "",
          UserName: "",
          Email: "",
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
            const RegisterResponse = await fetch('https://localhost:7017/api/Authorization/register', {
            method: 'post',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(formData)
          })
            data = await RegisterResponse.json();
            setPost(data);
            Navigate("/Login");
        } catch (error) {
            console.log("ERROR" + error)
        }
    }

    return (
        <div className='md:px-4 py-2.5 container w-800'>
          <form onSubmit={handleSubmit}>
          <label htmlFor="FirstName" className="block mb-2 text-sm font-medium text-gray-900 dark:text-gray-300">First name</label>
          <input type="text" onChange={handleChange} value={formData.FirstName} name='FirstName' minLength="1" maxLength="20" id="FirstName" className={cssStyle} required />
          <label htmlFor="LastName" className="block mb-2 text-sm font-medium text-gray-900 dark:text-gray-300">Last name</label>
          <input type="text" onChange={handleChange} value={formData.LastName} name='LastName' minLength="1" maxLength="20" id="LastName" className={cssStyle} required />
          <label htmlFor="UserName" className="block mb-2 text-sm font-medium text-gray-900 dark:text-gray-300">Username</label>
          <input type="text" onChange={handleChange} value={formData.UserName} name='UserName' minLength="1" maxLength="20" id="UserName" className={cssStyle} required />
          <label htmlFor="Email" className="block mb-2 text-sm font-medium text-gray-900 dark:text-gray-300">Email</label>
          <input type="email" onChange={handleChange} value={formData.Email} name='Email' minLength="1" maxLength="20" id="Email" className={cssStyle} required />
          <label htmlFor="Password" className="block mb-2 text-sm font-medium text-gray-900 dark:text-gray-300">Password</label>
          <input type="password" onChange={handleChange} value={formData.Password} name='Password' minLength="1" maxLength="20" id="Password" className={cssStyle} required />
            <p></p>
            <button type="submit" className="text-white my-2 bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-right dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800">Submit</button>
          </form>
        </div>
      )

}