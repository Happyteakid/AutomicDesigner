import React from 'react';
import { data } from 'autoprefixer';
import { useLocation, useNavigate } from 'react-router-dom';

export const WindowsGeneralStep4 = () => {
  const cssStyle = `bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg
   focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700
   dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500
   dark:focus:border-blue-500`;
   
   const { state } = useLocation();

   console.log("Id: "+  state);

   const [formData, setFormData] = React.useState(
    {
        Id: state,
        Documentation: ""
    }
    )

    function handleChange(event) {
      const {name, value, type, checked} = event.target
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
        const WindowsGeneralResponse = await fetch('https://localhost:7017/api/WindowsGeneral/step4', {
          method: 'post',
          headers: {'Authorization': 'Bearer ' + localStorage.getItem("token"), 'Content-Type': 'application/json' },
          body: JSON.stringify(formData)
        })
        data = await WindowsGeneralResponse.json();
        setPost(data);
        if (data != null) {
          const id = data.id;
          Navigate("/WindowsGeneral/5", { state: id });
        } else {
          console.log("Id = null");
        }
      } catch (error) {
        console.log("ERROR" + error)
      }
    }

  return (
    <div className='md:px-4 py-2.5 container w-800'>
      <form onSubmit={handleSubmit}>
        <label htmlFor="Documentation" className="block mb-2 text-sm font-medium text-gray-900 dark:text-gray-300">Documentation</label>
        <input type="text" onChange={handleChange} value={formData.Documentation}  name='Documentation' id="Documentation" className={cssStyle} required />
        <p></p>
        <button type="submit" className="text-white my-2 bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-right dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800">Submit</button>
      </form>
    </div>
  )
}