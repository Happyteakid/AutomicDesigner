import React from 'react'
import { useNavigate } from 'react-router-dom';
import { Header } from '../components/Header';
import { useStateContext } from '../contexts/ContextProvider';

export const Profile = () => {
  const buttonStyle = "text-white static bg-blue-700 my-1 mx-5 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm sm:w-auto px-5 py-2.5 dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"

  let Navigate = useNavigate();
  function navToFiles() {
    Navigate('/files');
  }
  return (
    <div className="mt-24 flex flex-col w-52 text-center align-middle">
      <button type='button' className={buttonStyle}>Change email</button>
      <button type='button' className={buttonStyle}>Change password</button>
      <button type='button' onClick={navToFiles} className={buttonStyle}>View files</button>
      <button type='button' className="text-white bg-red-700 hover:bg-red-800 focus:ring-4 focus:outline-none focus:ring-red-300 
      font-medium rounded-lg text-sm w-full sm:w-auto px-5 my-1 mx-5 py-2.5 dark:bg-red-600 dark:hover:bg-red-700 dark:focus:ring-red-800">Delete account</button>
    </div>
  )
}