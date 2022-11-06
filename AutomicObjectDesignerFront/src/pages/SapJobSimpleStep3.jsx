import React from 'react';
import { useLocation, useParams, useNavigate } from 'react-router-dom';
import { useEffect } from 'react';
import { data } from 'autoprefixer';
import { Form } from 'react-bootstrap';
import InputGroup from 'react-bootstrap/InputGroup';
import FloatingLabel from 'react-bootstrap/FloatingLabel';


export const SapJobSimpleStep3 = () => {
  const cssStyle = `bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg
   focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700
   dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500
   dark:focus:border-blue-500`;

   const { state } = useLocation();
   console.log("Id: " + state);
   

   const [formData, setFormData] = React.useState(
    {
        Id: state,
        Documentation: ""
    }
    )

    function handleChange(event) {
      console.log(event)
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
        const SapJobRespone = await fetch('https://localhost:7017/api/SapSimple/step3', {
          method: 'post',
          headers: {'Authorization': 'Bearer ' + localStorage.getItem("token"), 'Content-Type': 'application/json' },
          body: JSON.stringify(formData)
        })
        data = await SapJobRespone.json();
        setPost(data);
        if (data != null) {
          const id = data.id;
          Navigate("/SAPJobSimple/4", { state : id });
        } else {
          console.log("Id = null");
        }
      } catch (error) {
        console.log("ERROR" + error)
      }
      
    }


  return (
    <div className='md:px-4 py-2.5 container w-800'>
      <p className="block mb-2 text-sm font-medium text-gray-900 dark:text-gray-300 my-3">
      </p>
      <form onSubmit={handleSubmit}>
        <label htmlFor="Documentation" className="block mb-2 text-sm font-medium text-gray-900 dark:text-gray-400 my-3">Documentation</label>
        <textarea id="Documentation" onChange={handleChange} value={formData.Documentation} maxLength="255" name='Documentation' rows="4" className={cssStyle} placeholder="Optional documentation."></textarea>
        <button type="submit" className="my-4 text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none
         focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-right dark:bg-blue-600 dark:hover:bg-blue-700
          dark:focus:ring-blue-800">Submit</button>
      </form>
    </div>
  )
}