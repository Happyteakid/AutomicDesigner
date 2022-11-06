import React, { useState } from 'react';
import { data } from 'autoprefixer';
import { useNavigate, useLocation } from 'react-router-dom';

export const WindowsGeneralStep6 = () => {
  const cssStyleInput = `bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg
   focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700
   dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500
   dark:focus:border-blue-500`;

  const cssStyleButton = `className="my-4 text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none
   focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-right dark:bg-blue-600 dark:hover:bg-blue-700
    dark:focus:ring-blue-800 my-4 mr-2`;
    const { state } = useLocation();

  const jobType = "WindowsGeneral/DownloadWindowsGeneral";
  const [inputFields, setInputFields] = useState(
    [
      {Id: state,
      VariableKey: '',
      VariableValue: ''}
    ])

  const addFields = () => {
    let newField = { VariableKey: '', VariableValue: '' }
    setInputFields([...inputFields, newField])
  }


  const handleFormChange = (index, event) => {
    console.log("Step 6 input fields: ");
    console.log(inputFields);
    console.log("Json stringify below: ")
    console.log(JSON.stringify(inputFields))
    let data = [...inputFields];
    data[index][event.target.name] = event.target.value;
    setInputFields(data);
  }

  const removeFields = (index) => {
    let data = [...inputFields];
    data.splice(index, 1)
    setInputFields(data)
  }

  let Navigate = useNavigate();

  async function handleSubmit(event) {
    event.preventDefault()
    console.log("Json stringify that is being send below: ")
    console.log(JSON.stringify(inputFields))
    try {
      const WindowsGeneralResponse = await fetch('https://localhost:7017/api/WindowsGeneral/step6', {
        method: 'post',
        headers: {'Authorization': 'Bearer ' + localStorage.getItem("token"), 'Content-Type': 'application/json' },
        body: JSON.stringify(inputFields)
      })
      data.prop = await WindowsGeneralResponse.json();
      if (data.prop != null) {
        Navigate("/ExportSite", { state: { num: state, type: jobType } });
      } else {
        console.log("Id = null");
      }
    } catch (error) {
      console.log("ERROR: " + error);
    }
  }

  return (
    <div className='md:px-4 py-2.5 container w-800 text-gray-900 dark:text-gray-300 my-3"'>
      <p className="block mb-2 text-sm font-medium text-gray-900 dark:text-gray-300 my-3">
        Add form for Variable key and Variable Value(optional)
      </p>
      <div id="container" />
      <form>
        {inputFields.map((input, index) => {
          return (
            <div key={index}>
              <input
                name='VariableKey'
                placeholder='Variable Key'
                value={input.VariableKey}
                onChange={event => handleFormChange(index, event)}
                className={cssStyleInput}
                required
              /><br></br>
              <input
                name='VariableValue'
                placeholder='Variable Value'
                value={input.VariableValue}
                onChange={event => handleFormChange(index, event)}
                className={cssStyleInput}
                required
              />
              <button className={cssStyleButton} onClick={() => removeFields(index)}>Remove fields</button>
            </div>
          )
        })}
      </form>
      <button className={cssStyleButton} type='button' onClick={addFields}>
        Add fields
      </button><br></br>
      <button className={cssStyleButton} onClick={handleSubmit}>Submit</button>
    </div>
  )
}