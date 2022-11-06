import React from 'react'
import { data } from 'autoprefixer';
import { useNavigate } from 'react-router-dom';
//TODO : need to finish
export const BugReport = () => {
  const cssStyle = `bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg
   focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700
   dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500
   dark:focus:border-blue-500`;

   async function handleSubmit(event) {
    event.preventDefault()
    // try {
    //   const SapJobResponse = await fetch('https://localhost:7017/api/SapJobBwChain/step4', {
    //     method: 'post',
    //     headers: {'Authorization': 'Bearer ' + localStorage.getItem("token"), 'Content-Type': 'application/json' },
    //     body: JSON.stringify(formData)
    //   })
    //   data = await SapJobResponse.json();
    //   setPost(data);
    //   if (data != null) {
    //     const id = data.id;
    //     Navigate("/ExportSite", { state: { num: id, type: jobType } });
    //   } else {
    //     console.log("Id = null");
    //   }
    // } catch (error) {
    //   console.log("ERROR" + error)
    // }
  }

  return (
    
    <div className='md:px-4 py-2.5 container w-800'>
                <label htmlFor="BugReport" className="block mb-2 text-sm font-medium text-gray-900 dark:text-gray-300 my-3">Report bug here:</label>
              <textarea rows="4" type="text" name='BugReport' minLength="18" maxLength="255" placeholder='Max 254 charachters' id="BugReport" className={cssStyle} required />
        <p></p>
        <button onClick={handleSubmit} type="button" className="my-4 text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none
      focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-right dark:bg-blue-600 dark:hover:bg-blue-700
      dark:focus:ring-blue-800">Submit</button>
    </div>
  )
}