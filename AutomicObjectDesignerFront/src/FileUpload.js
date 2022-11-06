// import React, { useState } from "react";
// import axios from "axios";


// export const FileUpload = () => {
//     const [file, setFile] = useState();
//     const [fileName, setFileName] = useState();

//     const saveFile = (e) => {
//         console.log(e.target.files[0]);
//         setFile(e.target.files[0])
//         setFileName(e.target.files[0]);
//     };

//     const uploadFile = async (e) => {
//         console.log(file);
//         const fromData = new FormData();
//         formData.append("formFile", file);
//         formData.append("fileName", fileName);
//         try{
//             const res = await axios.post("https://localhost:7017/api/file", formData);
//             console.log(res);
//         } catch (ex){
//             console.log(ex);
//         }
//     };

//     return (
//         <>
//         <input type="file" onChange={saveFile} />
//         <input type="button" value="upload" onClick={uploadFile} />
        
//         </>
//     );
// };