import { Route, Routes } from 'react-router-dom';
import { AutomicObjectDesigner } from '../pages/AutomicObjectDesigner';
import { FileTransferMany } from '../pages/FileTransferMany';
import { FileTransferOne } from '../pages/FileTransferOne';

import { SapJobBWStep1 } from '../pages/SapJobBWStep1';
import { SapJobBWStep2 } from '../pages/SapJobBWStep2';
import { SapJobBWStep3 } from '../pages/SapJobBWStep3';
import { SapJobBWStep4 } from '../pages/SapJobBWStep4';

import { SapJobMassen } from '../pages/SapJobMassen';

import { UnixGeneralStep1 } from '../pages/UnixGeneralStep1';
import { UnixGeneralStep2 } from '../pages/UnixGeneralStep2';
import { UnixGeneralStep3 } from '../pages/UnixGeneralStep3';
import { UnixGeneralStep4 } from '../pages/UnixGeneralStep4';
import { UnixGeneralStep5 } from '../pages/UnixGeneralStep5';
import { UnixGeneralStep6 } from '../pages/UnixGeneralStep6';

import { WindowsGeneralStep1 } from '../pages/WindowsGeneralStep1';
import { WindowsGeneralStep2 } from '../pages/WindowsGeneralStep2';
import { WindowsGeneralStep3 } from '../pages/WindowsGeneralStep3';
import { WindowsGeneralStep4 } from '../pages/WindowsGeneralStep4';
import { WindowsGeneralStep5 } from '../pages/WindowsGeneralStep5';
import { WindowsGeneralStep6 } from '../pages/WindowsGeneralStep6';

import { SapJobSimpleStep1 } from '../pages/SapJobSimpleStep1';
import { SapJobSimpleStep2 } from '../pages/SapJobSimpleStep2';
import { SapJobSimpleStep3 } from '../pages/SapJobSimpleStep3';
import { SapJobSimpleStep4 } from '../pages/SapJobSimpleStep4';

import { ImportFile } from '../pages/ImportFile';
import { ImportJson } from '../pages/ImportJson';


import { Register } from '../pages/Register';
import { Login } from '../pages/Login'

import { ExportSite } from '../pages/ExportSite';

import { BugReport } from '../pages/BugReport';

import { NotFound } from '../pages/NotFound';

import React from 'react';
import { Profile } from '../pages';
import { Files } from '../pages';

const Routing = () => {
  return (
    <div>
      <Routes>
        {/* Dashboard */}
        <Route path='/' element={<AutomicObjectDesigner />} />
        <Route path='/automicobjectdesigner' element={<AutomicObjectDesigner />} />
        <Route path = '/profile' element={<Profile/>}/>
        <Route path = '/files' element={<Files/>}/>
        {/* Pages */}
        <Route path='/Register' element={<Register />} />
        <Route path='/Login' element={<Login />} />

        <Route path='/sapjobsimple/1/' element={<SapJobSimpleStep1 />} />
        <Route path='/sapjobsimple/2/' element={<SapJobSimpleStep2 />} />
        <Route path='/sapjobsimple/3/' element={<SapJobSimpleStep3 />} />
        <Route path='/sapjobsimple/4/' element={<SapJobSimpleStep4 />} />

        <Route path='/SapJobBW/1' element={<SapJobBWStep1 />} />
        <Route path='/SapJobBW/2' element={<SapJobBWStep2 />} />
        <Route path='/SapJobBW/3' element={<SapJobBWStep3 />} />
        <Route path='/SapJobBW/4' element={<SapJobBWStep4 />} />

        <Route path='/ExportSite' element={<ExportSite />} />

        <Route path='/sapjobmassen' element={<SapJobMassen />} />

        <Route path='/windowsGeneral/1' element={<WindowsGeneralStep1 />} />
        <Route path='/windowsGeneral/2' element={<WindowsGeneralStep2 />} />
        <Route path='/windowsGeneral/3' element={<WindowsGeneralStep3 />} />
        <Route path='/windowsGeneral/4' element={<WindowsGeneralStep4 />} />
        <Route path='/windowsGeneral/5' element={<WindowsGeneralStep5 />} />
        <Route path='/windowsGeneral/6' element={<WindowsGeneralStep6 />} />

        <Route path='/unixGeneral/1' element={<UnixGeneralStep1 />} />
        <Route path='/unixGeneral/2' element={<UnixGeneralStep2 />} />
        <Route path='/unixGeneral/3' element={<UnixGeneralStep3 />} />
        <Route path='/unixGeneral/4' element={<UnixGeneralStep4 />} />
        <Route path='/unixGeneral/5' element={<UnixGeneralStep5 />} />
        <Route path='/unixGeneral/6' element={<UnixGeneralStep6 />} />

        <Route path='/filetransferone' element={<FileTransferOne />} />
        <Route path='/filetransfermany' element={<FileTransferMany />} />

        <Route path='/importfile' element={<ImportFile />} />
        <Route path='/importjson' element={<ImportJson />} />

        <Route path='/bugReport' element={<BugReport />} />

        {/* Apps */}
        <Route path='/worksimple' element="Workflow" />
        <Route path='/worksynch' element="Workflow" />
        {/* Function */}
        <Route path='*' element={<NotFound />} />
      </Routes>
    </div>
  )
}

export default Routing;