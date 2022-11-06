import React from 'react';
import { MdOutlineCancel } from 'react-icons/md';
import { Navigate, useNavigate } from 'react-router-dom';

import { Button } from '.';
import { userProfileData } from '../data/dummy';
import { useStateContext } from '../contexts/ContextProvider';
import avatar from '../data/avatar.jpg';

// const [formData] = React.useState(
//   {
//     userName: "",
//     userRole: "",
//     userEmail: "",
//   }
// )

async function getUserData(event) {
  event.preventDefault()
  try {
      await fetch('https://localhost:7017/api/Authorization/userInfo', {
      method: 'post',
      headers: {'Authorization': 'Bearer ' + localStorage.getItem("token"), 'Content-Type': 'application/json' },
      body: JSON.stringify(formData)
    })
  } catch (error) {
    console.log("ERROR" + error)
  }
}

function isLoggedIn() {
  let Navigate = useNavigate();
  if (localStorage.getItem("token") != null) {
    return <button type="button"
      onClick={(e) => {
        localStorage.removeItem("token")
        Navigate("/Register");
      }}
      style={{ backgroundColor: "White", color: "black", borderRadius: "10px", width: "100%", marginTop: "10px" }}
    >
      Logout
    </button>
  }
  else {
    return <button type="button"
      onClick={(e) => {
        Navigate("/Login");
      }}
      style={{ backgroundColor: "White", color: "black", borderRadius: "10px", width: "100%", marginTop: "10px"}}
    >
      Login
    </button>
  }
}
function register() {
  let Navigate = useNavigate();
  if (localStorage.getItem("token") == null) {
    return <button type="button"
      onClick={(e) => {
        Navigate("/Register");
      }}
      style={{ backgroundColor: "White", color: "black", borderRadius: "10px", width: "100%", marginTop: "10px" }}
    >
      Register
    </button>
  }
}

function goToProfile(){
  let Navigate = useNavigate();
  if (localStorage.getItem("token") != null) {
    return <button type="button"
      onClick={(e) => {
        Navigate("/Profile");
      }}
      style={{ backgroundColor: "White", color: "black", borderRadius: "10px", width: "100%", marginTop: "10px" }}
    >
      Profile
    </button>
  }
}

function userData(){
  if (localStorage.getItem("token") != null){
    return <div>
    <p className="font-semibold text-xl dark:text-gray-200"> Leon Stawecki </p>
    <p className="text-gray-500 text-sm dark:text-gray-400">  Admin   </p>
    <p className="text-gray-500 text-sm font-semibold dark:text-gray-400"> info@automicobjectdesigner.com </p>
    </div>
  }
}
function userPicture(){
  if (localStorage.getItem("token") != null){
    return <div>
              <img
          className="rounded-full h-24 w-24"
          src={avatar}
          alt="user-profile"
        />
    </div>
  }
}

const UserProfile = () => {
  const { currentColor } = useStateContext();


  return (
    <div className="nav-item absolute right-1 top-16 bg-white dark:bg-[#42464D] p-8 rounded-lg w-96">
      <div className="flex justify-between items-center">
        <p className="font-semibold text-lg dark:text-gray-200">User Profile</p>
        <Button
          icon={<MdOutlineCancel />}
          color="rgb(153, 171, 180)"
          bgHoverColor="light-gray"
          size="2xl"
          borderRadius="50%"
        />
      </div>
      <div className="flex gap-5 items-center mt-6 border-color border-b-1 pb-6">
        {userPicture()}
        <div>
          {userData()}
        </div>
      </div>
      <div className='my-2'>
        {goToProfile()}
        </div>
        <div className='my-2'>
        {isLoggedIn()}
        </div>
        <div className='my-2'>
        {register()}
      </div>
    </div>

  );
};

export default UserProfile;