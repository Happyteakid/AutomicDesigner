import React, {useState} from 'react';
import { useNavigate } from 'react-router-dom';



export const ImportFile = () => {
  const cssStyle = `bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg
   focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700
   dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500
   dark:focus:border-blue-500`;


   const [formData, setFormData] = React.useState(
    {
      Job: "SapSimple",
      FileFormat: "JSON"
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

  let Navigate = useNavigate();
  async function handleSubmit(event) {
    window.open("/ImportJson", '_blank', 'noopener,noreferrer');
    // console.log(localStorage.getItem("token"))
    // event.preventDefault()
    // try {
    //   const SapJobResponse = await fetch('https://localhost:7017/api/SapJobBwChain/step1', {
    //     method: 'post',
    //     headers: {'Authorization': 'Bearer ' + localStorage.getItem("token"), 'Content-Type': 'application/json' },
    //     body: JSON.stringify(formData)
    //   })
    //   data.prop = await SapJobResponse.json();
    //   // setPost(data);
    //   if (data.prop != null) {
    //     const id = data.prop.id;
    //     Navigate("/ImportJson/2", { state: id });
    //   } else {
    //     console.log("Id = null");
    //   }
    // } catch (error) {
    //   console.log("ERROR" + error)
    // }
  }

  return (
    <div className='md:px-4 py-2.5 container w-800'>
      <form onSubmit={handleSubmit}>

        <label htmlFor="Job" className="block mb-2 text-sm font-medium text-gray-900 dark:text-gray-400">Select Job for import</label>
        <select onChange={handleChange} value={formData.Job} name='Job' className={cssStyle}>
          <option value={"Option1"}>SapSimple</option>
          <option value={"Option2"}>SapJobBw</option>
          <option value={"Option3"}>UnixGeneral</option>
          <option value={"Option4"}>WindowsGeneral</option>
        </select>
        <label htmlFor="FileFormat" className="block mb-2 text-sm font-medium text-gray-900 dark:text-gray-400 my-3">Select File format</label>
          <select onChange={handleChange} value={formData.FileFormat} name='FileFormat' className={cssStyle}>
            <option value={"Option1"}>JSON</option>
            <option value={"Option2"}>Csv</option>
          </select>
        <button type="submit" className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-right dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800">Submit</button>
      </form>
    </div>


    
  )
}
