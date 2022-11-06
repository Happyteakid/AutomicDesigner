import React from 'react';
import { data } from 'autoprefixer';
import { Form, Nav } from 'react-bootstrap';
import InputGroup from 'react-bootstrap/InputGroup';
import FloatingLabel from 'react-bootstrap/FloatingLabel';
import { useLocation, useNavigate } from 'react-router-dom';
import { useEffect } from 'react';


export const SapJobSimpleStep2 = () => {
  const cssStyle = `bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg
   focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700
   dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500
   dark:focus:border-blue-500`;

   const { state } = useLocation();
   const{ id } = state;

   const [formData, setFormData] = React.useState(
    {
        Id: state,
        ObjectName: "Loading data...",
        SapReport: "Loading data...",
        SapVariant: "Loading data..."
    }
    )

    useEffect( () =>{
      const fetchData = async () =>{
        try {
          const result = await fetch(
            'https://localhost:7017/api/SapSimple/GetSapSimpleStep2/' + state, {
              headers: {'Authorization': 'Bearer ' + localStorage.getItem("token")}
            });
            const dat = await result.json()
            if (dat != null) {
              setFormData(prev => ({
                ...prev,
              ObjectName: dat.objectName,
              SapReport: dat.sapReport,
              SapVariant: dat.sapVariant,
              }))
            } else {
              console.warn("Data could not be fetched");
            }
          } catch (error) {
            console.error(error);
          };}
          fetchData();
        }, []);

    

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
        const SapJobRespone = await fetch('https://localhost:7017/api/SapSimple/step2', {
          method: 'post',
          headers: {'Authorization': 'Bearer ' + localStorage.getItem("token"), 'Content-Type': 'application/json' },
          body: JSON.stringify(formData)
        })
        data = await SapJobRespone.json();
        setPost(data);
        if (data != null) {
          const id = data.id;
          Navigate("/SAPJobSimple/3", { state : id });
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
        Confirm data:
      </p>
      <form onSubmit={handleSubmit}>
        <label htmlFor="ObjectName" className="block mb-2 text-sm font-medium text-gray-900 dark:text-gray-300 my-3">Object Name</label>
        <input type="text" onChange={handleChange} value={formData.ObjectName} name='ObjectName' id="ObjectName" className={cssStyle} required />
        <label htmlFor="SapReport" className="block mb-2 text-sm font-medium text-gray-900 dark:text-gray-400 my-3">SAP Report</label>
        <textarea id="SapReport" onChange={handleChange} value={formData.SapReport} name='SapReport' rows="4" className={cssStyle} placeholder="SAP Report..."></textarea>
        <label htmlFor="SapVariant" className="block mb-2 text-sm font-medium text-gray-900 dark:text-gray-400 my-3">SAP Variant</label>
        <textarea id="SapVariant" onChange={handleChange} value={formData.SapVariant} name='SapVariant' rows="4" className={cssStyle} placeholder="SAP Variant..."></textarea>
        <button type="submit" className="my-4 text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-right dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800">Confirm</button>
      </form>
    </div>
  )
}