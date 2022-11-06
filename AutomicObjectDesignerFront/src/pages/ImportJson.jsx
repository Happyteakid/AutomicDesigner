import React, {useState} from 'react';


export const ImportJson = () => {



    const [selectedFile, setSelectedFile] = useState();
    const [isFilePicked, setIsFilePicked] = useState(false);

    const changeHandler = (event) => {
      setSelectedFile(event.target.files[0]);
      setIsSelected(true);
    };
    const handleSubmission = () => {

    };

    return(
      <div>
        <input type="file" name="file" onChange={changeHandler} />
        <div>
          <button onClick={handleSubmission}type="submit" className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-right dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800">Submit</button>
          {/* <button onClick={handleSubmission}>Submit</button> */}
        </div>
      </div>
    )

}
