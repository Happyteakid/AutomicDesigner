import React from 'react'
import { useEffect } from 'react'
import { useNavigate } from 'react-router-dom'

export function NotFound (){
  const navigate = useNavigate()

  useEffect(() => {
    setTimeout(() => {
      navigate("/")
    }, 500)
  },[])
  return (
    <div> 
    <h1 className='dark:text-white font-bold'>NotFound</h1>
    </div>
  )
}