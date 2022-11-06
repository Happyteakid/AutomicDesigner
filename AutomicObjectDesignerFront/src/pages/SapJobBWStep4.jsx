import React from 'react';
import { data } from 'autoprefixer';
import { useLocation, useNavigate } from 'react-router-dom';



export const SapJobBWStep4 = () => {
  const cssStyle = `bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg
   focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700
   dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500
   dark:focus:border-blue-500`;

  const { state } = useLocation();
  console.log("Id: " + state);
  const jobType = "SapJobBwChain/DownloadSapJobBwChain";
  const [formData, setFormData] = React.useState(
    {
      Id: state,
      Title: "456",
      Archive1: "",
      Archive2: "",
      Folder: "/IMPORT/E1E/user ID",
      InternalAccount: "",
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
      const SapJobResponse = await fetch('https://localhost:7017/api/SapJobBwChain/step4', {
        method: 'post',
        headers: {'Authorization': 'Bearer ' + localStorage.getItem("token"), 'Content-Type': 'application/json' },
        body: JSON.stringify(formData)
      })
      data = await SapJobResponse.json();
      setPost(data);
      if (data != null) {
        const id = data.id;
        Navigate("/ExportSite", { state: { num: id, type: jobType } });
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
        <label htmlFor="Title" className="block mb-2 text-sm font-medium text-gray-900 dark:text-gray-300">Title</label>
        <input type="text" onChange={handleChange} value={formData.Title} name='Title' maxLength="200" id="Title" placeholder='Max 200 characters.' className={cssStyle} required />

        <label htmlFor="Archive1" className="block mb-2 text-sm font-medium text-gray-900 dark:text-gray-300">Archive Key 1</label>
        <input type="text" onChange={handleChange} value={formData.Archive1} name='Archive1' maxLength="60" id="Archive1" placeholder='Max 60 characters.' className={cssStyle} required />

        <label htmlFor="Archive2" className="block mb-2 text-sm font-medium text-gray-900 dark:text-gray-300">Archive Key 2</label>
        <input type="text" onChange={handleChange} value={formData.Archive2} name='Archive2' maxLength="20" id="Archive2" placeholder='Max 20 characters.' className={cssStyle} required />

        <label htmlFor="InternalAccount" className="block mb-2 text-sm font-medium text-gray-900 dark:text-gray-300">Internal account</label>
        <input type="text" onChange={handleChange} value={formData.InternalAccount} name='InternalAccount' maxLength="16" id="InternalAccount" placeholder='Max 16 characters.' className={cssStyle} required />

        <label htmlFor="Folder" className="block mb-2 text-sm font-medium text-gray-900 dark:text-gray-300">Folder</label>
        <input type="text" onChange={handleChange} value={formData.Folder} name='Folder' maxLength="160" id="Folder" placeholder='/IMPORT/E1E/user ID' className={cssStyle} required />

        <button type="submit" className="my-4 text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none
         focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-right dark:bg-blue-600 dark:hover:bg-blue-700
          dark:focus:ring-blue-800">Submit</button>
      </form>
    </div>
  )
}